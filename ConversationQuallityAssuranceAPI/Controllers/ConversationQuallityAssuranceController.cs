using System;
using System.Collections.Generic;
using System.Linq;
using Application.ConversationProcess.viewModels;
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
                return ConversationMapper.MapDataModelToViewModel(_conversationProcess.ProcessConversations());
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Internal Server Error, reason: {ex.Message}");
                return new List<ConversationViewModel>() { new ConversationViewModel() { Name = ex.Message.Split('>').ToList().LastOrDefault() } };
            }            
        }
    }
}
