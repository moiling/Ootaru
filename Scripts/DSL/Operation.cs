namespace Scripts.DSL {
    public class Operation {
        public string Method { get; set; }
        public string Parameter { get; set; }
        public string Extra { get; set; }

        /*
         *  单命令: none、end
         *  双命令: emotion SAD、bgm BGM_001
         *  三命令: source STAGE_0 TAG_1、set BOOK 1
         */

        public Operation(string method, string parameter, string extra) {
            Method = method;
            Parameter = parameter;
            Extra = extra;
        }

        public Operation(string[] strList) {
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
            var result = "Operation:@" + Method + " " + Parameter + " " + Extra;
            return result;
        }
    }
}