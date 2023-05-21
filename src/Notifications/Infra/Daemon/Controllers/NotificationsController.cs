using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Publication.Rabbit.Subscription.Storage.Notifications.Domain;

namespace Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Daemon.Controllers
{
    [ApiController]
    [Route("/api/v1/notifications/")]
    public class NotificationsController : ControllerBase
    {
        private readonly ILogger<NotificationsController> _logger;
        private readonly INotificationService _notificationService;
        // private readonly IBearerAuthorizer _bearerAuthorizer;
        // private readonly IValidator<PersonDto> _validator;

        public NotificationsController(
            // IValidator<PersonDto> validator,
            // IBearerAuthorizer bearerAuthorizer,
            INotificationService notificationService,
            ILogger<NotificationsController> logger)
        {
            _logger = logger;
            _notificationService = notificationService;
            // _validator = validator;
            // _bearerAuthorizer = bearerAuthorizer;
        }

        [HttpPost]
        [Route("push-message")]
        public async Task<ActionResult> SendData([FromBody] string message)
        {
            try
            {
                if (message is null)
                {
                    _logger.LogWarning("message is null");
                }
                _logger.LogInformation(message);

                var result = await _notificationService.HandleAsync(message);

                if (result.Succeeded == false)
                {
                   return StatusCode(result.ErrorData.ErrorCode, result.ErrorData.ErrorMessage);
                }
                // var authResult = _bearerAuthorizer.Authorize(Request);

                // if (authResult.Succeeded == false)
                // {
                // 	return StatusCode(authResult.ErrorData.ErrorCode, authResult.ErrorData.ErrorMessage);
                // }

                // var validationResult = _validator.Validate(dto);

                // if (validationResult.IsValid == false)
                // {
                // 	return StatusCode((int) HttpStatusCode.BadRequest, validationResult.ToString());
                // }

                // _logger.LogInformation($"Validation has been successful: {dto}");

                // var result = _publisherService.SendData(dto.FromDto());

                // if (result.ErrorData != null)
                // {
                // 	return StatusCode(result.ErrorData.ErrorCode, result.ErrorData.ErrorMessage);
                // }

                _logger.LogInformation($"{nameof(NotificationsController)} has successfully handled the data.");
               return new OkObjectResult(result.Succeeded);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(400, ex.Message);
            }
        }
    }
}