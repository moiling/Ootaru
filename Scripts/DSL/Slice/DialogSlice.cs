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

        public DialogSlice() : base(DslSliceType.Dialog, null, null) {
            
        }

        public DialogSlice(List<Operation> operations, string tag) : base(DslSliceType.Dialog, operations, tag) {
            
        }

        public DialogSlice(List<Operation> operations, string tag, DialogSliceType dialogType, string content, Operation endOperation) 
            : base(DslSliceType.Dialog, operations, tag) {
            
            DialogType = dialogType;
            Content = content;
            EndOperation = endOperation;
        }

        public override string ToString() {
            var result = "DialogSlice:\n" + Tag + "\n" + DialogType + "\n";

            foreach (var operation in Operations) {
                result += operation + "\n";
            }
            
            result += Content + "\n" + EndOperation;
            return result;
        }
    }
}