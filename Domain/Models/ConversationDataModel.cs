using System.Collections.Generic;

namespace Domain.Models
{
    public class ConversationDataModel
    {
        public string[] Content { get; set; }
        public int LinesCount { get; set; }

        public Dictionary<string, int> Scores = new Dictionary<string, int>();
        public int Duration { get; set; }
        public int TotalScore { get; set; }
        public string Name { get; set; }
    }
}
