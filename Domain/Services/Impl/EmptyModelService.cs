namespace Domain.Services.Impl
{
    using Domain.Exceptions;
    using Domain.Models;
    using System;
    using System.Collections.Generic;
    public class UnscoredEmptyModelService : ConversationsExtractor, IUnscoredEmptyModelsService
    {
        public List<ConversationDataModel> GetNotScoredConversationModels(string[] conversations, string coversationStartIdentifier)
        {
            List<ConversationDataModel> models = new List<ConversationDataModel>();
            var functionToExecute = new Func<bool>(() =>
            {
                models = ExtractConversationFromArray(conversations, coversationStartIdentifier);
                return true;
            });
            var functionToExecuteInCaseOfError = new Func<Exception, bool>((ex) =>
            {
                throw new EmptyModelServiceException($"GetNotScoredConversationModels => {ex.Message}", ex.InnerException);
            }
            );
            TryToPerformAction(functionToExecute, functionToExecuteInCaseOfError);
            return models;
        }
    }
}
