namespace Domain.Services.Impl
{
    using Domain.Exceptions;
    using Domain.Models;
    using System;
    using System.Collections.Generic;

    public class ScoreCalculatorService : ScoreCalculator, IScoreCalculatorService
    {
        public List<ConversationDataModel> RateConversations(List<ConversationDataModel> unscoredConversations, string urgentMatchWord, List<string> goodSeriveceMatches, string excellentMatch)
        {
            List<ConversationDataModel> models = new List<ConversationDataModel>() ;
            var functionToExecute = new Func<bool>(() =>
            {
                models = CalculateScores(unscoredConversations, urgentMatchWord, goodSeriveceMatches, excellentMatch);
                return true;
            });
            var functionToExecuteInCaseOfError = new Func<Exception, bool>((ex) =>
            {
                throw new ScoreCalculatorServiceException($"RateConversations => {ex.Message}", ex.InnerException);
            }
            );
            TryToPerformAction(functionToExecute, functionToExecuteInCaseOfError);
            return models;
        }
    }
}
