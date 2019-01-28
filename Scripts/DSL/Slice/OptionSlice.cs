using System.Collections.Generic;

namespace Scripts.DSL.Slice {
    public class OptionSlice : BaseSlice {
        public List<Option> Options { set; get; }

        public OptionSlice() : base(DslSliceType.Option, null, null) { }

        public OptionSlice(List<Operation> atOperations, List<Operation> sharpOperations) : base(DslSliceType.Option,
            atOperations, sharpOperations) { }

        public OptionSlice(List<Operation> atOperations, List<Operation> sharpOperations, List<Option> options)
            : base(DslSliceType.Option, atOperations, sharpOperations) {

            Options = options;
        }

        public override string ToString() {
            var result = "OptionSlice:\n";

            foreach (var operation in SharpOperations) {
                result += operation + "\n";
            }

            foreach (var operation in AtOperations) {
                result += operation + "\n";
            }

            foreach (var option in Options) {
                result += option + "\n";
            }

            return result;
        }
    }
}