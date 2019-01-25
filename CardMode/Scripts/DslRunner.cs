using Scripts.DSL;
using UnityEngine;

namespace CardMode.Scripts {
    public class DslRunner : BaseDslRunner {
        
        // TODO 要有一个开关判断是否可以进行下一段话，选项时不能按回车下一段话，只能选择
        // TODO 显示文字又显示对话框的时候需要再点击一次才能显示对话框
        
        public DslRunner(string text) : base(text) {
            
        }
        
        protected override void Fx(string fxId) {
            Debug.Log("执行特效" + fxId);
        }

        protected override void Do(string things) {
            Debug.Log("执行事件" + things);
        }

        protected override void Bgm(string bgmId) {
            Debug.Log("设置BGM" + bgmId);
        }

        protected override void Voice(string voiceId) {
            Debug.Log("设置Voice" + voiceId);
        }

        protected override void Name(string name) {
            Debug.Log("设置Name" + name);
        }

        protected override void TextSize(string size) {
            Debug.Log("设置TextSize" + size);
        }

        protected override void TextColor(string color) {
            Debug.Log("设置TextColor" + color);
        }

        protected override void TextSpeed(string speed) {
            Debug.Log("设置TextSpeed" + speed);
        }

        protected override void Emotion(string emotionId) {
            Debug.Log("设置Emotion" + emotionId);
        }

        protected override void Character(string characterId) {
            Debug.Log("设置Character" + characterId);
        }

        protected override void Set(string parameter, string value) {
            Debug.Log("设置" + parameter + ":" + value);
        }

        protected override void AfterSentenceSlice() {
            Debug.Log("断句");
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
            Debug.Log("添加文字" + content);
        }

        protected override void ShowOptions(string[] options) {
            Debug.Log("显示选择支" + options + "关闭点击换页功能");
        }
    }
}