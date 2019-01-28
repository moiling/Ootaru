using System.Linq;
using Scripts.DSL;
using UnityEngine;

namespace CardMode.Scripts {
    public class DslRunner : OperationDslRunner {
        
        // TODO 要有一个开关判断是否可以进行下一段话，选项时不能按回车下一段话，只能选择
        // TODO 显示文字又显示对话框的时候需要再点击一次才能显示对话框
        
        public DslRunner(string text) : base(text) {
            
        }
        
        protected override void Fx(string fxId) {
            Debug.Log("执行特效：" + fxId);
        }

        protected override void Do(string things) {
            Debug.Log("执行事件：" + things);
        }

        protected override void Bgm(string bgmId) {
            Debug.Log("设置BGM：" + bgmId);
        }

        protected override void Voice(string voiceId) {
            Debug.Log("设置Voice：" + voiceId);
        }

        protected override void Name(string name) {
            Debug.Log("设置Name：" + name);
        }

        protected override void Face(string[] emotionIds, string[] positions) {
            var show = "设置表情：";

            for (var i = 0; i < emotionIds.Length; i++) {
                show += emotionIds[i] + "[" + positions[i] + "]";
            }

            Debug.Log(show);
        }

        protected override void Character(string[] characterIds) {
            var show = characterIds.Aggregate("设置说话角色：", (current, c) => current + c);

            Debug.Log("设置Character：" + show);
        }

        protected override void Set(string parameter, string value) {
            Debug.Log("设置：" + parameter + ":" + value);
        }

        protected override void AfterSentenceSlice() {
            Debug.Log("断句，不操作");
        }

        protected override void AfterPageSlice() {
            Debug.Log("换页");
        }

        protected override void HideDialog() {
            Debug.Log("隐藏对话框");
        }

        protected override void ShowDialog() {
            Debug.Log("显示对话框");
        }

        protected override void AppendContent(string content) {
            Debug.Log("添加文字：" + content);
        }

        protected override void ShowOptions(string[] options) {
            Debug.Log("显示选择支：" + options + "，关闭点击换页功能");
        }
    }
}