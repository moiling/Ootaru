using UnityEngine.Analytics;

namespace Scripts.DSL {
    /// <summary>
    /// 指定了所需@命令标签
    /// </summary>
    public abstract class OperationDslRunner : BaseDslRunner {
        
        protected OperationDslRunner(string text) : base(text) {
            
        }

        protected override void OtherOperations(Operation operation) {
            switch (operation.Method) {
                case Constants.OPERATION_SET:
                    Set(operation.Parameter, operation.Extra);
                    break;
                case Constants.OPERATION_CHARACTER:
                    Character(operation.Parameter.Split(','));
                    break;
                case Constants.OPERATION_FACE:
                    string[] positions;
                    // 缺省为L
                    if (operation.Extra == null || operation.Extra.Equals("")) {
                        positions = new string[operation.Parameter.Length];

                        for (var i = 0; i < operation.Parameter.Length; i++) {
                            positions[i] = "L";
                        }
                    } else {
                        positions = operation.Extra.Split(',');
                    }
                    Face(operation.Parameter.Split(','), positions);
                    break;
                case Constants.OPERATION_NAME:
                    Name(operation.Parameter);
                    break;
                case Constants.OPERATION_VOICE:
                    Voice(operation.Parameter);
                    break;
                case Constants.OPERATION_BGM:
                    Bgm(operation.Parameter);
                    break;
                case Constants.OPERATION_SOUND:
                    Sound(operation.Parameter);
                    break;
                case Constants.OPERATION_DO:
                    Do(operation.Parameter);
                    break;
                case Constants.OPERATION_FX:
                    Fx(operation.Parameter);
                    break;     
                case Constants.OPERATION_CG:
                    Cg(operation.Parameter);
                    break;
                case Constants.OPERATION_ITEM:
                    Item(operation.Parameter);
                    break;
                
            }
        }

        // ----- 添加@标签内容 -----
        
        protected abstract void Fx(string fxId);

        protected abstract void Bgm(string bgmId);

        protected abstract void Voice(string voiceId);
        
        protected abstract void Sound(string soundId);

        protected abstract void Name(string name);

        protected abstract void Face(string[] emotionIds, string[] positions);

        protected abstract void Character(string[] characterIds);
        
        protected abstract void Cg(string cg);
        
        // ----- 添加#标签内容 -----

        protected abstract void Set(string parameter, string value);
        
        protected abstract void Do(string things);
        
        protected abstract void Item(string item);
    }

}