using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealTimeChat.Application.Conversations;
using RealTimeChat.Application.Conversations.CreateGroupConversation;
using RealTimeChat.Application.Conversations.CreatePrivateConversation;
using RealTimeChat.Application.Conversations.GetConversationById;
using RealTimeChat.Application.Conversations.GetUserConversations;

namespace RealTimeChat.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ConversationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConversationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ConversationResponse>> GetById(Guid id)
        {
            var response = await _mediator.Send(new GetConversationByIdQuery() { ConversationId = id });

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<List<ConversationResponse>>> GetMyConversations()
        {
            var response = await _mediator.Send(new GetUserConversationsQuery());

            return Ok(response);
        }

        [HttpPost("private")]
        public async Task<ActionResult<ConversationResponse>> CreatePrivateChat(CreatePrivateConversationCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("group")]
        public async Task<ActionResult<ConversationResponse>> CreateGroupChat(CreateGroupConversationCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
