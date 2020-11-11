namespace Domain.Services
{
    using Domain.Models;
    using System.Collections.Generic;
    public interface IScoreCalculatorService
    {
        /// <summary>
        /// Calculates all the scores of the given conversations
        /// </summary>
        /// <param name="unscoredConversations"></param>
        /// <param name="urgentMatchWord"></param>
        /// <param name="goodSeriveceMatches"></param>
        /// <param name="excellentMatch"></param>
        /// <returns>List with the conversations with all their scores calculated</returns>
        List<ConversationDataModel> RateConversations(List<ConversationDataModel> unscoredConversations, string urgentMatchWord, List<string> goodSeriveceMatches, string excellentMatch);
    }
}
