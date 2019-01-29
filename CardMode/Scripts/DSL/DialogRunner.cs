using System.Linq;
using Scripts.DSL;
using UnityEngine;

namespace CardMode.Scripts {

    public enum SpeakerPosition {
        Left,
        Right,
        Both,
        None
    }

    public class DialogRunner : OperationDslRunner {
        
        // TODO 要有一个开关判断是否可以进行下一段话，选项时不能按回车下一段话，只能选择
        // TODO 显示文字又显示对话框的时候需要再点击一次才能显示对话框
        // TODO 所有标志都有一个特殊的none标志，表示清空值

        private DialogController _controller;
        private string[] _currentSpeakers;
        private SpeakerPosition _lastSpeakerPosition = SpeakerPosition.None;
        private string[] _currentFaces;
        private string[] _currentFacePosition;
        private string _currentName;
        private string _currentVoice;
        private string _currentFx;
        private string _currentBgm;
        private string _currentSound;

        private bool _lastHideDialog;
        private string[] _lastFaces;
        private string[] _lastFacePosition;
        private string _lastName;
        private string _lastBgm;
        
        public DialogRunner(string text, DialogController controller) : base(text) {
            _controller = controller;
        }
        
        protected override void Fx(string fxId) {
            Debug.Log("执行特效：" + fxId);
            _currentFx = fxId;
        }

        protected override void Bgm(string bgmId) {
            Debug.Log("设置BGM：" + bgmId);
            _currentBgm = bgmId;
        }

        protected override void Voice(string voiceId) {
            Debug.Log("设置Voice：" + voiceId);
            _currentVoice = voiceId;
        }

        protected override void Sound(string soundId) {
            Debug.Log("设置Sound：" + soundId);
            _currentSound = soundId;
        }

        protected override void Name(string name) {
            Debug.Log("设置Name：" + name);
            _currentName = name;
        }

        protected override void Face(string[] emotionIds, string[] positions) {
            var show = "设置表情：";

            for (var i = 0; i < emotionIds.Length; i++) {
                show += emotionIds[i] + "[" + positions[i] + "]";
            }

            Debug.Log(show);
            _currentFaces = emotionIds;
            _currentFacePosition = positions;
        }

        protected override void Character(string[] characterIds) {
            var show = characterIds.Aggregate("设置说话角色：", (current, c) => current + c);

            Debug.Log("设置Character：" + show);
            _currentSpeakers = characterIds;
            // TODO 名字现在是测试用的
            _currentName = _currentSpeakers[0].Equals("Chu") ? "小宙" : "阿让";
        }

        // TODO
        protected override void AfterAllOperation() {
            Debug.Log("设置完所有属性，可以判断显示什么内容了");

            var speakerPosition = SpeakerPosition.Both;

            foreach (var speaker in _currentSpeakers) {
                for (var i = 0; i < _currentFaces.Length; i++) {
                    if (!_currentFaces[i].StartsWith(speaker)) continue;

                    if (_currentFacePosition[i].Equals("R")) {
                        speakerPosition = speakerPosition == SpeakerPosition.Left ? SpeakerPosition.Both : SpeakerPosition.Right;
                    } else {
                        speakerPosition = speakerPosition == SpeakerPosition.Right ? SpeakerPosition.Both : SpeakerPosition.Left;
                    }
                }
            }

            if (!(_currentFaces == _lastFaces && _currentFacePosition == _lastFacePosition)) {
                _controller.ClearFace();
                for (var i = 0; i < _currentFaces.Length; i++) {
                    var isRight = _currentFacePosition[i].Equals("R");
                    _controller.SetFace(_currentFaces[i], isRight);
                }

                _lastFaces = _currentFaces;
                _lastFacePosition = _currentFacePosition;
            }
            
            var showName = true;
            if (_currentFaces.Length > 0 && _currentFaces[0].Equals("none")) {
                speakerPosition = SpeakerPosition.None;
            }

            if (_currentSpeakers.Length > 0 && _currentSpeakers[0].Equals("none")) {
                speakerPosition = SpeakerPosition.None;
                showName = false;
            }

            if (speakerPosition != _lastSpeakerPosition || _lastHideDialog) {
                _controller.SwitchDialogModel(speakerPosition, showName);
                _lastSpeakerPosition = speakerPosition;
                _lastHideDialog = false;
            }

            if (_currentName != _lastName) {
                _controller.SetName(_currentName);
                _lastName = _currentName;
            }
            // TODO 设置BGM VOICE SOUND FX
            
        }

        protected override void End() {
            _controller.End();
            Debug.Log("结束");
        }

        protected override void ShowDialogOnly() {
            Debug.Log("显示对话框");
            _controller.ShowDialog();
        }

        protected override void AfterSentenceSlice() {
            Debug.Log("断句，不操作");
        }

        protected override void AfterPageSlice() {
            Debug.Log("换页");
            _controller.ClearContent();
        }

        // 这个在AfterAllOperation之后执行
        protected override void HideDialogOnly() {
            Debug.Log("隐藏对话框");
            _lastHideDialog = true;
            _controller.HideDialog();
        }

        protected override void ShowDialogPanel() {
            Debug.Log("显示对话框面板");
            _controller.ShowDialogPanel();
        }

        protected override void AppendContent(string content) {
            Debug.Log("添加文字：" + content);
            _controller.AddContent(content);
        }

        protected override void ShowOptions(string[] options) {
            var show = "显示选择支：\n";

            foreach (var option in options) {
                show += option + "\n";
            }

            show += ", 关闭点击换页功能";
            
            Debug.Log(show);
            
            _controller.ShowOption(options);
        }
        
        
        protected override void Do(string things) {
            Debug.Log("执行事件：" + things);
            _controller.Do(things);
        }
        
        // TODO 
        protected override void Set(string parameter, string value) {
            Debug.Log("设置：" + parameter + ":" + value);
        }
    }
}