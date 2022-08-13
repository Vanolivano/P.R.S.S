using System;
using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Domain.Services;
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
		private readonly IValidator<PersonDto> _validator;

		public RmqPublisherController(
			ILogger<RmqPublisherController> logger,
			IRmqPublisherService publisherService,
			IValidator<PersonDto> validator)
		{
			_logger = logger;
			_publisherService = publisherService;
			_validator = validator;
		}

		[HttpPost]
		[Route("send-data")]
		public ActionResult SendData([FromBody] PersonDto dto)
		{
			try
			{
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