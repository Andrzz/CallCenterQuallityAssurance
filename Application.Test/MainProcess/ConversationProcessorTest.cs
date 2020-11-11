namespace Application.Test.MainProcess
{
    using Application.ConversationProcess.viewModels.Impl;
    using Domain.Models;
    using Domain.Services;
    using Infrastructure.Managers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System.Collections.Generic;

    [TestClass]
    public class ConversationProcessorTest
    {
        [TestMethod]
        public void ProcessConversations_PathFileOk_DoesNotThrowAndMatchesAllAssets()
        {
            #region Mocks
            MockRepository mockRepository = new MockRepository(MockBehavior.Default) { DefaultValue = DefaultValue.Empty };
            Mock<IFileManager> fileManagerMock = mockRepository.Create<IFileManager>();
            Mock<IUnscoredEmptyModelsService> unscoredEmptyModelsServiceMock = mockRepository.Create<IUnscoredEmptyModelsService>();
            Mock<IScoreCalculatorService> scoreCalculatorServiceMock = mockRepository.Create<IScoreCalculatorService>();
            var linesMock = GetLinesMock();
            var conversationDataModelsMock = GetConversationDataModelsMock();
            #endregion
            #region setup
            fileManagerMock.Setup(fm => fm.ReadLines(It.IsAny<string>())).Returns(linesMock);
            unscoredEmptyModelsServiceMock.Setup(ue => ue.GetNotScoredConversationModels(It.IsAny<string[]>(), It.IsAny<string>())).Returns(conversationDataModelsMock);
            scoreCalculatorServiceMock.Setup(sc => sc.RateConversations(It.IsAny<List<ConversationDataModel>>(), It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<string>())).Returns(conversationDataModelsMock);
            var sut = new ConversationProcessor(fileManagerMock.Object, unscoredEmptyModelsServiceMock.Object, scoreCalculatorServiceMock.Object);
            #endregion
            #region Asserts
            NUnit.Framework.Assert.DoesNotThrow(() => sut.ProcessConversations());
            fileManagerMock.Verify(fm => fm.ReadLines(It.IsAny<string>()), Times.Once);
            unscoredEmptyModelsServiceMock.Verify(ue => ue.GetNotScoredConversationModels(It.IsAny<string[]>(), It.IsAny<string>()), Times.Once);
            scoreCalculatorServiceMock.Verify(sc => sc.RateConversations(It.IsAny<List<ConversationDataModel>>(), It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<string>()), Times.Once);
            #endregion

        }

        #region private mocks
        private string[] GetLinesMock()
        {
            return new string[4] { "ExampleLine1", "ExampleLine2", "ExampleLine3","ExampleLine4"};
        }

        private List<ConversationDataModel> GetConversationDataModelsMock()
        {
            List<ConversationDataModel> conversationDataModels = new List<ConversationDataModel>();
            conversationDataModels.Add(new ConversationDataModel() { });
            return conversationDataModels;
        }
         #endregion
    }
}
