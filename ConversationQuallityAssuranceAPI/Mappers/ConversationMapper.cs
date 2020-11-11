namespace ConversationQuallityAssuranceAPI.Mappers
{
    using ConversationQuallityAssuranceAPI.ViewModels;
    using Domain.Models;
    using System.Collections.Generic;

    public static class ConversationMapper
    {
        public static List<ConversationViewModel> MapDataModelToViewModel(List<ConversationDataModel> dataModels)
        {
            List<ConversationViewModel> viewModels = new List<ConversationViewModel>();
            foreach (var dataModel in dataModels)
            {
                viewModels.Add(new ConversationViewModel() {Name = dataModel.Name, TotalScore = dataModel.TotalScore });
            }
            return viewModels;
        }
    }
}
