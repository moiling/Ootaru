namespace Nonograms.Scripts {
    public class NonogramsNumber {
        public int Count { get; set; }
        public NonogramsType Type { get; set; }

        public NonogramsNumber(int count, NonogramsType type) {
            this.Count = count;
            this.Type = type;
        }

        // [count,type]
        public NonogramsNumber(string str) {
            str = str.Substring(1, str.Length - 2);

            var split = str.Split('|');
            this.Count = int.Parse(split[0]);
            this.Type = (NonogramsType) int.Parse(split[1]);
        }
    }
}