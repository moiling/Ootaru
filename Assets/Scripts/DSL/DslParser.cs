using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Scripts.DSL.Slice;
using UnityEngine;

namespace Scripts.DSL {
    public class DslParser {
        public List<BaseSlice> Slices = new List<BaseSlice>();
        public Hashtable TagSliceIndex = new Hashtable();

        private List<Operation> _currentAtOperations = new List<Operation>();
        private List<Operation> _currentSharpOperations = new List<Operation>();
        private DslSliceType _currentSliceType = DslSliceType.Dialog; // 默认是对话
        private StringBuilder _currentContent = new StringBuilder();
        private Operation _currentEndOperation;
        private DialogSliceType _currentyDialogSliceType = DialogSliceType.Sentence; // 默认断句
        private List<Option> _currentOptions = new List<Option>();

        public void Parse(string dsl) {
            Slices.Clear();
            TagSliceIndex.Clear();
            Clear();

            foreach (var s in dsl.Split('\n')) {
                ParseLine(s);
            }

            foreach (var slice in Slices) {
                Debug.Log(slice.Type == DslSliceType.Dialog
                    ? ((DialogSlice) slice).ToString()
                    : ((OptionSlice) slice).ToString());
            }

            foreach (DictionaryEntry entry in TagSliceIndex) {
                Debug.Log(entry.Key + ":" + entry.Value);
            }
        }

        private void ParseLine(string s) {
            
            // 去掉注释
            var annotationIndex = s.IndexOf("//", StringComparison.Ordinal);
            if (annotationIndex >= 0) {
                s = s.Substring(0, annotationIndex);
            }
            
            s = s.Trim(); // 去掉前后空格
            
            if (s.Equals("")) { // 过滤空行
                return;
            }

            if (s.StartsWith("@")) { // 操作
                _currentAtOperations.Add(new Operation(OperationType.At, s.Substring(1, s.Length - 1).Split(' ')));

                return;
            }

            // TODO 不止有TAG了，#变为一次性操作符
            if (s.StartsWith("#")) { // TAG
                // _currentTag = s.Substring(1, s.Length - 1);
                _currentSharpOperations.Add(new Operation(OperationType.Sharp, s.Substring(1, s.Length - 1).Split(' ')));
                return;
            }

            //if (s.StartsWith("{")) {              // 选择支
            if (s.StartsWith("{") && (s.Length == 1 || (s[1] != 's' && s[1] != 'p'))) {
                if (_currentContent.Length > 0) { // 上一个对话分片没有以{p}结束，继续显示，手动切片
                    Cut();
                }

                _currentSliceType = DslSliceType.Option;

                return;
            }

            if (_currentSliceType == DslSliceType.Option) { // 选择支
                if (s.StartsWith("}")) {                    // 选择支结束
                    Cut();
                } else { // 解析选项
                    foreach (var optionStr in s.Split(',')) {
                        if (!optionStr.Equals("")) {
                            _currentOptions.Add(new Option(optionStr));
                        }
                    }
                }

                return;
            }

            var index = s.IndexOf('{');

            // 文本段操作
            if (index >= 0) {                // 该行包含{，说明该行会被分片
                if (index + 2 >= s.Length) { // 结构错误，}在末尾时应该是index+2 = s.Length-1
                    return;
                }

                if (s[index + 1].Equals('s')) { // 断句
                    _currentyDialogSliceType = DialogSliceType.Sentence;
                    _currentContent.Append(s.Substring(0, index));
                    Cut();

                    // {s}后可能还有剩余的不换行的字符串，需要保存起来，下次继续解析，一次递归就好
                    if (s.Length > index + 3) {
                        ParseLine(s.Substring(index + 3, s.Length - (index + 3)));
                    } else {
                        _currentContent.Append("\n");
                    }
                } else if (s[index + 1].Equals('p')) { // 换页
                    _currentyDialogSliceType = DialogSliceType.Page;
                    // {p}后一定没有字符串了，所以只需要把前面的字符串添加进去即可
                    _currentContent.Append(s.Substring(0, index));

                    // 但{p}还有特殊跳转操作需要解析，如{p@TAG}和{p@TAG STAGE}
                    if (s[index + 2].Equals('@')) {
                        var list = s.Substring(index + 3, s.Length - (index + 4)).Split(' '); // 把TAG剪出来

                        if (list.Length == 1) {
                            if (list[0].Equals(Constants.OPERATION_END)) {
                                _currentEndOperation = new Operation(OperationType.At, Constants.OPERATION_END, null, null);
                            } else {
                                _currentEndOperation = new Operation(OperationType.At, Constants.OPERATION_JUMP, list[0], null);
                            }
                        } else if (list.Length == 2) {
                            _currentEndOperation = new Operation(OperationType.At, Constants.OPERATION_SOURCE, list[0], list[1]);
                        }
                    }

                    Cut();
                } else {
                    // TODO 文本格式错误，加个报错提示
                }
            } else {                                    // 当前行为纯文字
                _currentContent.Append(s).Append('\n'); // 添加换行
            }
        }

        private void Cut() {
            var tag = "";

            foreach (var operation in _currentSharpOperations) {
                if (!operation.Method.Equals("tag")) continue;

                tag = operation.Parameter;
                break;
            }
            
            if (!tag.Equals("")) {                    // 当前切片有Tag，保存在HashMap中
                TagSliceIndex.Add(tag, Slices.Count); // 这个Count正好是将要添加的切片的下标
            }        

            switch (_currentSliceType) {
                case DslSliceType.Dialog:

                    Slices.Add(new DialogSlice(_currentAtOperations, _currentSharpOperations, _currentyDialogSliceType,
                        _currentContent.ToString(), _currentEndOperation));

                    break;
                case DslSliceType.Option:
                    Slices.Add(new OptionSlice(_currentAtOperations, _currentSharpOperations, _currentOptions));

                    break;
                default:

                    throw new ArgumentOutOfRangeException();
            }

            Clear();
        }

        private void Clear() {
            // 清场
            _currentAtOperations = new List<Operation>();
            _currentSharpOperations = new List<Operation>();
            _currentSliceType = DslSliceType.Dialog;
            _currentContent.Remove(0, _currentContent.Length);
            _currentEndOperation = null;
            _currentyDialogSliceType = DialogSliceType.Sentence;
            _currentOptions = new List<Option>();
        }
    }
}