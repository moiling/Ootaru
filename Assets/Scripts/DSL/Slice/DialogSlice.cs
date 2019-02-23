using System.Collections.Generic;

namespace Scripts.DSL.Slice {
    public enum DialogSliceType {
        Sentence,
        Page
    }

    public class DialogSlice : BaseSlice {
        public DialogSliceType DialogType { get; set; }
        public string Content { get; set; }
        public Operation EndOperation { get; set; }

        public DialogSlice() : base(DslSliceType.Dialog, null, null) { }

        public DialogSlice(List<Operation> atOperations, List<Operation> sharpOperations) : base(DslSliceType.Dialog,
            atOperations, sharpOperations) { }

        public DialogSlice(List<Operation> atOperations, List<Operation> sharpOperations, DialogSliceType dialogType, string content,
            Operation endOperation)
            : base(DslSliceType.Dialog, atOperations, sharpOperations) {

            DialogType = dialogType;
            Content = content;
            EndOperation = endOperation;
        }

        public override string ToString() {
            var result = "DialogSlice:\n" + DialogType + "\n";

            foreach (var operation in SharpOperations) {
                result += operation + "\n";
            }
            
            foreach (var operation in AtOperations) {
                result += operation + "\n";
            }

            result += Content + "\n" + EndOperation;

            return result;
        }
    }
}