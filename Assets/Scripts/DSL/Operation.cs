namespace Scripts.DSL {

    public enum OperationType {
        At,
        Sharp
    }

    public class Operation {
        public OperationType Type { get; set; }
        public string Method { get; set; }
        public string Parameter { get; set; }
        public string Extra { get; set; }

        /*
         *  单命令: none、end
         *  双命令: emotion SAD、bgm BGM_001
         *  三命令: source STAGE_0 TAG_1、set BOOK 1
         */

        public Operation(OperationType type, string method, string parameter, string extra) {
            Type = type;
            Method = method;
            Parameter = parameter;
            Extra = extra;
        }

        public Operation(OperationType type, string[] strList) {
            Type = type;
            
            if (strList.Length < 1) return;

            Method = strList[0];

            if (strList.Length > 1) {
                Parameter = strList[1];
            }   

            if (strList.Length == 3) {
                Extra = strList[2];
            }
        }

        public override string ToString() {
            var result = "Operation:";

            if (Type == OperationType.At) {
                result += "@";
            } else {
                result += "#";
            }
            
            result += Method + " " + Parameter + " " + Extra;
            return result;
        }
    }
}