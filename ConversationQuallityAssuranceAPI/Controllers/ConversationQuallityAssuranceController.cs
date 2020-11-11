using System;
using System.Collections.Generic;
using Application.ConversationProcess.viewModels;
using ConversationQuallityAssuranceAPI.Exception;
using ConversationQuallityAssuranceAPI.Mappers;
using ConversationQuallityAssuranceAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ConversationQuallityAssuranceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConversationQuallityAssuranceController : ControllerBase
    {
        private readonly IConversationProcess _conversationProcess;

        private static string routeToFile = "C:\\\\\\historial_de_conversaciones.txt";

        private readonly ILogger<ConversationQuallityAssuranceController> _logger;

        public ConversationQuallityAssuranceController(ILogger<ConversationQuallityAssuranceController> logger, IConversationProcess conversationProcess)
        {
            _conversationProcess = conversationProcess ?? throw new ArgumentNullException(nameof(conversationProcess));
            _logger = logger;
        }

        [HttpGet]
        public List<ConversationViewModel> Get()
        {
            try
            {
                return ConversationMapper.MapDataModelToViewModel(_conversationProcess.ProcessConversations(routeToFile));
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Internal Server Error, reason: {ex.Message}");
                if(ex.Message.Contains("format")) return new List<ConversationViewModel>() { new ConversationViewModel() { Name = "FORMATERROR"} };
                else return new List<ConversationViewModel>() { new ConversationViewModel() { Name = "GENERICERROR" } };
            }            
        }
    }
}
