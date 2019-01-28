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
                    Face(operation.Parameter.Split(','), operation.Extra.Split(','));
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
                case Constants.OPERATION_DO:
                    Do(operation.Parameter);
                    break;
                case Constants.OPERATION_FX:
                    Fx(operation.Parameter);
                    break;     
            }
        }
        
        // ----- 添加@标签内容 -----
        
        protected abstract void Fx(string fxId);

        protected abstract void Do(string things);

        protected abstract void Bgm(string bgmId);

        protected abstract void Voice(string voiceId);

        protected abstract void Name(string name);

        protected abstract void Face(string[] emotionIds, string[] positions);

        protected abstract void Character(string[] characterIds);

        protected abstract void Set(string parameter, string value);
    }

}