using System;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Publication.Rabbit.Subscription.Storage.Notifications.Facade;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Domain.Services;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Daemon.Authorizers.Bearer;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Daemon.Mappers;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Dto;

namespace Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Daemon.Controllers
{
    [ApiController]
    [Route("/api/v1/rmq-publisher/")]
    public class RmqPublisherController : ControllerBase
    {
        private readonly ILogger<RmqPublisherController> _logger;
        private readonly IRmqPublisherService _publisherService;
        private readonly INotificationClient _notificationClient;
        private readonly IBearerAuthorizer _bearerAuthorizer;
        private readonly IValidator<PersonDto> _validator;

        public RmqPublisherController(
            IValidator<PersonDto> validator,
            IBearerAuthorizer bearerAuthorizer,
            IRmqPublisherService publisherService,
            ILogger<RmqPublisherController> logger,
            INotificationClient notificationClient)
        {
            _logger = logger;
            _publisherService = publisherService;
            _validator = validator;
            _bearerAuthorizer = bearerAuthorizer;
            _notificationClient = notificationClient;
        }

        [HttpPost]
        [Route("send-data")]
        public async Task<ActionResult> SendData([FromBody] PersonDto dto)
        {
            try
            {
                var authResult = _bearerAuthorizer.Authorize(Request);

                if (authResult.Succeeded == false)
                {
                    return StatusCode(authResult.ErrorData.ErrorCode, authResult.ErrorData.ErrorMessage);
                }

                var validationResult = _validator.Validate(dto);

                if (validationResult.IsValid == false)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, validationResult.ToString());
                }

                _logger.LogInformation($"Validation has been successful: {dto}");

                var result = _publisherService.SendData(dto.FromDto());

                if (result.ErrorData != null)
                {
                    return StatusCode(result.ErrorData.ErrorCode, result.ErrorData.ErrorMessage);
                }

                _logger.LogInformation($"{nameof(RmqPublisherController)} has successfully sent the data.");

                var notificationResult = await _notificationClient.PushMessageAsync("Data has been sended to RmqSubsriber from RmqPublisher.");
                if (notificationResult.ErrorData != null)
                {
                    _logger.LogError($"Error occured while sending notification. {notificationResult.ErrorData.ToString()}");
                }

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