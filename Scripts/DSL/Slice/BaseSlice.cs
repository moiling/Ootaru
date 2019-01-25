using System.Collections.Generic;

namespace Scripts.DSL.Slice {
    public enum DslSliceType {
        Dialog,
        Option
    }

    public class BaseSlice {
        public DslSliceType Type { set; get; }
        public List<Operation> Operations { set; get; }
        public string Tag { set; get; }

        protected BaseSlice(DslSliceType type, List<Operation> operations, string tag) {
            Type = type;
            Operations = operations;
            Tag = tag;
        }
    }
}