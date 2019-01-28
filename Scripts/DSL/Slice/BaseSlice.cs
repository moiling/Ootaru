using System.Collections.Generic;

namespace Scripts.DSL.Slice {
    public enum DslSliceType {
        Dialog,
        Option
    }

    public class BaseSlice {
        public DslSliceType Type { set; get; }
        public List<Operation> AtOperations { set; get; }
        public List<Operation> SharpOperations { set; get; }

        protected BaseSlice(DslSliceType type, List<Operation> atOperations, List<Operation> sharpOperations) {
            Type = type;
            AtOperations = atOperations;
            SharpOperations = sharpOperations;
        }
    }
}