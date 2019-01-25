using System.Collections.Generic;
using System.Xml;

namespace Scripts.DSL.Slice {
    public class OptionSlice : BaseSlice {

        public List<Option> Options { set; get; }

        public OptionSlice() : base(DslSliceType.Option, null, null) {
            
        }

        public OptionSlice(List<Operation> operations, string tag) : base(DslSliceType.Option, operations, tag) {
            
        }

        public OptionSlice(List<Operation> operations, string tag, List<Option> options) 
            : base(DslSliceType.Option, operations, tag) {
            
            Options = options;
        }

        public override string ToString() {
            var result = "OptionSlice:\n" + Tag + "\n";

            foreach (var operation in Operations) {
                result += operation + "\n";
            }

            foreach (var option in Options) {
                result += option + "\n";
            }
            return result;
        }
    }
}