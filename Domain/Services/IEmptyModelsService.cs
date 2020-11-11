using Domain.Models;
using System.Collections.Generic;

namespace Domain.Services
{
    public interface IUnscoredEmptyModelsService
    {
        /// <summary>
        /// Gets all the conversation modeles unscored
        /// </summary>
        /// <param name="conversations">Array of lines from the source file</param>
        /// <param name="coversationStartIdentifier">"Keyword wich determines when a conversation has started"</param>
        /// <returns>LIST of unscored conversation Model</returns>
        List<ConversationDataModel> GetNotScoredConversationModels(string[] conversations, string coversationStartIdentifier);
    }
}
