namespace Application.ConversationProcess.viewModels.Impl
{
    using Application.ConversationProcess.Configurations;
    using Application.ConversationProcess.Exceptions;
    using Domain.Handlers;
    using Domain.Models;
    using Domain.Services;
    using Infrastructure.Managers;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ConversationProcessor : ErrorHandlerAbs, IConversationProcess
    {
        private readonly IFileManager _fileManager;
        private readonly IUnscoredEmptyModelsService _unscoredEmptyModelsService;
        private readonly IScoreCalculatorService _scoreCalculatorService;

        public ConversationProcessor(IFileManager fileManager, IUnscoredEmptyModelsService unscoredEmptyModelsService, IScoreCalculatorService scoreCalculatorService)
        {
            _fileManager = fileManager ?? throw new ArgumentNullException(nameof(fileManager));
            _unscoredEmptyModelsService = unscoredEmptyModelsService ?? throw new ArgumentNullException(nameof(unscoredEmptyModelsService));
            _scoreCalculatorService = scoreCalculatorService ?? throw new ArgumentNullException(nameof(scoreCalculatorService));
        }

        public List<ConversationDataModel> ProcessConversations(string pathToFile)
        {
            List<ConversationDataModel> scoredConversations = new List<ConversationDataModel>();
            var functionToExecute = new Func<bool>(() =>
            {
                var fileLines = _fileManager.ReadLines(pathToFile);
                var matchingWordsList = ConfigurationKeys.GetListOfMatchingWords().Split(',').ToList();
                var unscoredConversationModelsFromFile = _unscoredEmptyModelsService.GetNotScoredConversationModels(fileLines, ConfigurationKeys.GetConversationStartIdentifier());
                scoredConversations = _scoreCalculatorService.RateConversations(unscoredConversationModelsFromFile, ConfigurationKeys.GetUrgenMatch(), matchingWordsList, ConfigurationKeys.GetExcelentMatch());
                return true;
            });
            var functionToExecuteInCaseOfError = new Func<Exception, bool>((ex) =>
            {
                throw new ConversationProcessorException($"ConversationProcessor => ProcessConversations => {ex.Message}", ex.InnerException);
            }
            );
            TryToPerformAction(functionToExecute, functionToExecuteInCaseOfError);
            return scoredConversations;
        }
    }
}
