using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Qiwi.BillPayments.Json.Newtonsoft;
using Qiwi.BillPayments.Model;
using RoboKassaExample.Models;
using RoboKassaExample.Services;

namespace RoboKassaExample.Controllers
{
	[Route("payment")]
	public class PaymentController : Controller
	{
		private readonly IQiwiService _qiwiService;

		public PaymentController(IQiwiService qiwiService)
		{
			_qiwiService = qiwiService;
		}

		[HttpPost("create-bill")]
		public async Task<IActionResult> CreateBill([FromBody]CreateBillModel model)
		{
			var result = await _qiwiService.CreateBill(model);
			return Ok(result);
		}
		
		[HttpGet("callback")]
		public async Task<IActionResult> Callback()
		{
			var request = HttpContext.Request;

			string bodyStr;
			using (var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
			{
				bodyStr = await reader.ReadToEndAsync();
			}

			var data = new NewtonsoftMapper().ReadValue<Notification>(bodyStr);
			var signature = Request.Headers["X-Api-Signature-SHA256"].ToString();

			var result = _qiwiService.HandleCallback(data, signature);

			if (result)
			{
				//todo: добавить начисление баланса
				return Ok();
			}
			
			return BadRequest();
		}
	}
}