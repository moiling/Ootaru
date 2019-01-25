namespace Scripts.DSL {
    public class Option {
        public string Name { get; set; }
        public Operation Operation { get; set; }

        public Option(string name, Operation operation) {
            Name = name;
            Operation = operation;
        }

        public Option(string optionStr) {
            var list = optionStr.Split(':');

            if (list.Length < 2) {
                return;
            }
            
            Name = list[0];
            Operation = new Operation(list[1].Substring(1, list[1].Length - 1).Split(' '));
        }

        public override string ToString() {
            var result = "Option:" + Name + ":" + Operation;
            return result;
        }
    }
}