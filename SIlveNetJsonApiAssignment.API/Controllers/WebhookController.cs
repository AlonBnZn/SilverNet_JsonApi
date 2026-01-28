using Microsoft.AspNetCore.Mvc;
using NserviceBus.Messages.Subscriptions.Commands;

namespace SilveNetJsonApiAssignment.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebhookController : ControllerBase
    {
        private readonly IMessageSession _messageSession;

        public WebhookController(IMessageSession messageSession)
        {
            _messageSession = messageSession;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            await _messageSession.SendLocal<StartCalculateSubscriptionsCommand>(m =>
            {
                m.JobGuid = Guid.NewGuid();
            }).ConfigureAwait(false);

            return Ok();
        }
    }
}
