using System.Collections.Generic;

namespace digits {
    public class Record {
        public int Value { get; set; }
        public List<int> Image { get; set; }
    }

    public class Prediction {
        public Record Actual { get; set; }
        public Record Predicted { get; set; }
    }
}