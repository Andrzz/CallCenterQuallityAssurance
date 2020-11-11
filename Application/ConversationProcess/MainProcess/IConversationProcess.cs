namespace Application.ConversationProcess.viewModels
{
    using Domain.Models;
    using System.Collections.Generic;
    public interface IConversationProcess
    {
        /// <summary>
        /// procces the file of conversation in the given path
        /// </summary>
        /// <param name="pathToFile"></param>
        /// <returns></returns>
        List<ConversationDataModel> ProcessConversations(string pathToFile);
    }
}
