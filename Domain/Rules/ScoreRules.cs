namespace Domain.Rules
{
    using Domain.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ScoreRules
    {
        public static int LinesScoreRule(this ConversationDataModel conversation) => conversation.LinesCount <= 5 && conversation.LinesCount > 1 ? 20 : conversation.LinesCount == 1 ? -100 : conversation.LinesCount > 5 ? 10 : 0;

        public static int DurationScoreRule(this ConversationDataModel conversation) => conversation.Duration < 1 ? 50 : 25;


        public static int UrgentWordScoreRule(this ConversationDataModel conversation, string urgentWordMatch)
        {
            int urgentCount = 0;
            foreach (string line in conversation.Content)
            {
                urgentCount += line.ToLowerInvariant().Split(' ').Where(l => l.StartsWith(urgentWordMatch.ToLowerInvariant())).Count();
            }
            return urgentCount <= 2 && urgentCount >= 1 ? -5 : urgentCount > 2 ? -10 : 0;
        }

        public static int GoodServiceScoreRule(this ConversationDataModel conversation, List<string> goodServiceList, string excellentMatch)
        {
            int goodWordsMatches = 0;
            foreach (var item in conversation.Content)
            {
                foreach (var service in goodServiceList)
                {
                    var serviceCout = item.Split(new string[] { service }, StringSplitOptions.None).Count();
                    goodWordsMatches = goodWordsMatches == 0 && serviceCout > 1 ? 10 : goodWordsMatches;
                }
                if (item.Contains(excellentMatch))
                {
                    goodWordsMatches = goodWordsMatches + 100;
                    break;
                }
            }
            return goodWordsMatches;
        }
    }
}
