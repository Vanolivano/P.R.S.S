using System;
using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
		private readonly IBearerAuthorizer _bearerAuthorizer;
		private readonly IValidator<PersonDto> _validator;

		public RmqPublisherController(
			IValidator<PersonDto> validator,
			IBearerAuthorizer bearerAuthorizer,
			IRmqPublisherService publisherService,
			ILogger<RmqPublisherController> logger)
		{
			_logger = logger;
			_publisherService = publisherService;
			_validator = validator;
			_bearerAuthorizer = bearerAuthorizer;
		}

		[HttpPost]
		[Route("send-data")]
		public ActionResult SendData([FromBody] PersonDto dto)
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
					return StatusCode((int) HttpStatusCode.BadRequest, validationResult.ToString());
				}

				_logger.LogInformation($"Validation has been successful: {dto}");

				var result = _publisherService.SendData(dto.FromDto());

				if (result.ErrorData != null)
				{
					return StatusCode(result.ErrorData.ErrorCode, result.ErrorData.ErrorMessage);
				}

				_logger.LogInformation($"{nameof(RmqPublisherController)} has successfully sent the data.");
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