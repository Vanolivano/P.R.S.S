using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Dto;

namespace Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Daemon.Controllers
{
	[ApiController]
	[Route("/api/v1/rmq-publisher/")]
	public class RmqPublisherController : ControllerBase
	{
		private readonly ILogger<RmqPublisherController> _logger;

		public RmqPublisherController(ILogger<RmqPublisherController> logger)
		{
			_logger = logger;
		}

		[HttpPost]
		[Route("send-data")]
		public string SendData([FromBody] PersonDto dto)
		{
			_logger.LogInformation($"{ dto.ToString() ?? "Empty"}");
			return dto.ToString() ?? "Empty";
		}
	}
}