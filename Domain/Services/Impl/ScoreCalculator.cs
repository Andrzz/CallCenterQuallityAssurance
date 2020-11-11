namespace Domain.Services.Impl
{
    using Domain.Exceptions;
    using Domain.Handlers;
    using Domain.Models;
    using Domain.Rules;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ScoreCalculator : ErrorHandlerAbs
    {
        private const string LINESCORE = "LineScore";
        private const string URGENTWORDSCORE = "UrgentScore";
        private const string GOODSERVICESCORE = "GoodServiceScore";
        private const string DURATIONSCORE = "DurationScore";
        protected List<ConversationDataModel> CalculateScores(List<ConversationDataModel> conversations, string urgentMatchWord, List<string> goodServiceSentences, string excellentMatch)
        {            
            var functionToExecute = new Func<bool>(() =>
            {
                foreach (var unscoredConversation in conversations)
                {
                    CalculateLineScore(unscoredConversation);
                    int linesScore;
                    unscoredConversation.Scores.TryGetValue(LINESCORE, out linesScore);
                    if (linesScore != -100)
                    {
                        CalculateUrgentWordScore(unscoredConversation, urgentMatchWord);
                        CalculateGoodServiceScore(unscoredConversation, goodServiceSentences, excellentMatch);
                        CalculateDurationScore(unscoredConversation);
                    }
                    CalculateTotalScore(unscoredConversation);                    
                }
                return true;
            }
             );
            var functionToExecuteInCaseOfError = new Func<Exception, bool>((ex) =>
            {
                throw new ScoreCalculatorServiceException($"ScoreCalculator => CalculateScores => {ex.Message}", ex.InnerException);
            }
            );
            TryToPerformAction(functionToExecute, functionToExecuteInCaseOfError);
            return conversations;
        }

        private void CalculateLineScore(ConversationDataModel conversation)
        {
            var functionToExecute = new Func<bool>(() =>
            {
                conversation.Scores.Add(LINESCORE, conversation.LinesScoreRule());
                return true;
            });
            var functionToExecuteInCaseOfError = new Func<Exception, bool>((ex) =>
            {
                throw new ScoreCalculatorServiceException($"CalculateLineScore => {ex.Message}", ex.InnerException);
            }
            );
            TryToPerformAction(functionToExecute, functionToExecuteInCaseOfError);
        }

        private void CalculateUrgentWordScore(ConversationDataModel conversation, string urgentMatchWord)
        {
            var functionToExecute = new Func<bool>(() =>
            {
                conversation.Scores.Add(URGENTWORDSCORE, conversation.UrgentWordScoreRule(urgentMatchWord));
                return true;
            });
            var functionToExecuteInCaseOfError = new Func<Exception, bool>((ex) =>
            {
                throw new ScoreCalculatorServiceException($"CalculateUrgentWordScore => {ex.Message}", ex.InnerException);
            }
            );
            TryToPerformAction(functionToExecute, functionToExecuteInCaseOfError);            
        }

        private void CalculateGoodServiceScore(ConversationDataModel conversation, List<string> goodServiceSentences, string excellentMatch)
        {
            var functionToExecute = new Func<bool>(() =>
            {
                conversation.Scores.Add(GOODSERVICESCORE, conversation.GoodServiceScoreRule(goodServiceSentences, excellentMatch));
                return true;
            });
            var functionToExecuteInCaseOfError = new Func<Exception, bool>((ex) =>
            {
                throw new ScoreCalculatorServiceException($"CalculateGoodServiceScore => {ex.Message}", ex.InnerException);
            }
            );
            TryToPerformAction(functionToExecute, functionToExecuteInCaseOfError);           
        }

        private void CalculateDurationScore(ConversationDataModel conversation)
        {
            var functionToExecute = new Func<bool>(() =>
            {
                conversation.Scores.Add(DURATIONSCORE, conversation.DurationScoreRule());
                return true;
            });
            var functionToExecuteInCaseOfError = new Func<Exception, bool>((ex) =>
            {
                throw new ScoreCalculatorServiceException($"CalculateDurationScore => {ex.Message}", ex.InnerException);
            }
            );
            TryToPerformAction(functionToExecute, functionToExecuteInCaseOfError);            
        }

        private void CalculateTotalScore(ConversationDataModel conversation)
        {            
            var functionToExecute = new Func<bool>(() =>
            {
                conversation.TotalScore = conversation.Scores.Sum(x => x.Value);
                return true;
            });
            var functionToExecuteInCaseOfError = new Func<Exception, bool>((ex) =>
            {
                throw new ScoreCalculatorServiceException($"CalculateTotalScore => {ex.Message}", ex.InnerException);
            }
            );
            TryToPerformAction(functionToExecute, functionToExecuteInCaseOfError);
        }
    }
}
