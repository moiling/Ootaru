
using System;
using System.Collections.Generic;
using Scripts.DSL.Slice;

namespace Scripts.DSL {
    /// <summary>
    ///  DSL执行过程的主逻辑
    /// </summary>
    public abstract class BaseDslRunner {
        private readonly DslParser _parser = new DslParser();
        private int _currentSliceIndex;
        private DslSliceType _lastSliceType;
        private DialogSliceType _lastDialogSliceType;
        private Operation _lastEndOperation; // 上一个切片留下的尾巴操作
        private List<Option> _lastOptions = new List<Option>();
        
        public BaseDslRunner(string text) {
            _parser.Parse(text);
        }

        public void Next() { // 不跳转，直接执行下一个切片
            Next(_currentSliceIndex);
        }

        public void Select(int optionIndex) {
            Next(_currentSliceIndex, optionIndex);
        }

        private void Next(int sliceIndex, int optionIndex = -1) {

            if (optionIndex >= 0) { // 一定是走Select进来的
                DoOperation(_lastOptions[optionIndex].Operation);

                var operationMethod = _lastOptions[optionIndex].Operation.Method;
                
                // 如果是jump、source和end的话就不继续执行当前的分片了
                if (operationMethod.Equals(Constants.OPERATION_JUMP) ||
                    operationMethod.Equals(Constants.OPERATION_SOURCE) ||
                    operationMethod.Equals(Constants.OPERATION_END)) {
                    return;
                }         
            }

            if (_lastEndOperation != null) { // 如果有尾巴操作的话
                DoOperation(_lastEndOperation);
                return;
            }
            
            if (sliceIndex == _parser.Slices.Count) { // 已经没有了，最后一片都执行完了
                HideDialog();
                return;
            }

            // 第一次要显示对话框
            if (sliceIndex == 0) {
                ShowDialog();
            }
            
            _currentSliceIndex = sliceIndex; // 跳转到指定的slice中
            var slice = _parser.Slices[sliceIndex];
            
            // 断句和换页操作(对话框显示操作)
            if (_lastSliceType == DslSliceType.Dialog) {
                switch (_lastDialogSliceType) {
                    case DialogSliceType.Sentence:
                        AfterSentenceSlice();
                        break;
                    case DialogSliceType.Page:
                        AfterPageSlice();
                        // 如果选择支显示之前换页，那么会先关闭对话框
                        if (slice.Type == DslSliceType.Option) {
                            HideDialog();
                        }
                        break;
                }
            } else if (_lastDialogSliceType == DialogSliceType.Page) { // 如果之前是选择支，则很可能对话框被关闭了           
                ShowDialog();
            }

            foreach (var operation in slice.Operations) {
                DoOperation(operation);          
            }
            
            switch (slice.Type) {

                case DslSliceType.Dialog:
                    _lastDialogSliceType = ((DialogSlice) slice).DialogType;
                    _lastEndOperation = ((DialogSlice) slice).EndOperation;
                    AppendContent(((DialogSlice) slice).Content);
                    break;
                case DslSliceType.Option:
                    _lastOptions.Clear();
                    _lastOptions.AddRange(((OptionSlice) slice).Options);

                    var optionNames = new List<string>();
                    foreach (var option in ((OptionSlice) slice).Options) {
                        optionNames.Add(option.Name);
                    }
                    ShowOptions(optionNames.ToArray());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _lastSliceType = slice.Type;
            _currentSliceIndex++; // 移到下一个切片
        }

        private void DoOperation(Operation operation) {
            switch (operation.Method) {
                case Constants.OPERATION_JUMP:
                    // 跳转必须要换页                    
                    _lastDialogSliceType = DialogSliceType.Page;
                    _lastEndOperation = null; // 否则会死循环
                    Next((int) _parser.TagSliceIndex[operation.Parameter]);
                    break;
                case Constants.OPERATION_SOURCE:
                    // 跳转必须要换页
                    _lastDialogSliceType = DialogSliceType.Page;
                    _lastEndOperation = null; // 否则会死循环
                    // TODO 换脚本文件再跳转
                    break;
                case Constants.OPERATION_END:
                    // 结束了就和执行到末尾的操作一样
                    HideDialog();
                    _lastEndOperation = null; // 否则会死循环
                    break;               
                case Constants.OPERATION_NONE:
                    // 没有操作就表示继续执行
                    Next();
                    break;
                default:
                    OtherOperations(operation);
                    break;
            }
        }

        protected abstract void OtherOperations(Operation operation);

        /**
         * 断句分片结束的操作
         */
        protected abstract void AfterSentenceSlice();
        /**
         * 分页分片结束的操作
         */
        protected abstract void AfterPageSlice();
        /**
         * 隐藏对话框
         */
        protected abstract void HideDialog();
        /**
         * 显示对话框
         */
        protected abstract void ShowDialog();
        /**
         * 添加文本
         */
        protected abstract void AppendContent(string content);
        /**
         * 显示选项
         */
        protected abstract void ShowOptions(string[] options);
    }
}