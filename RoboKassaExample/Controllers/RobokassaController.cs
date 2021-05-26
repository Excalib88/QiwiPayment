using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Qiwi.BillPayments.Client;
using Qiwi.BillPayments.Model;
using Qiwi.BillPayments.Model.In;

namespace RoboKassaExample.Controllers
{
	[Route("qiwi")]
	public class RobokassaController : Controller
	{
		[HttpGet]
		public async Task<IActionResult> Test()
		{
			var client = BillPaymentsClientFactory.Create("eyJ2ZXJzaW9uIjoiUDJQIiwiZGF0YSI6eyJwYXlpbl9tZXJjaGFudF9zaXRlX3VpZCI6InNzaWc2cS0wMCIsInVzZXJfaWQiOiI3OTUyMDM3MTI2MiIsInNlY3JldCI6IjRiMzkzNGNkNDM1NjhjZDA0ZGRjYjFlMTc4NDg0NjY1MGFjNjE4MWNjODE2MjU5ZjA1YTc1MWNmMDhhMGM5M2YifX0=");
			
			var result = client.CreatePaymentForm(
				new PaymentInfo
				{
					PublicKey = "48e7qUxn9T7RyYE1MVZswX1FRSbE6iyCj2gCRwwF3Dnh5XrasNTx3BGPiMsyXQFNKQhvukniQG8RTVhYm3iPwQ4btmN5hThm5Ed8jX1xdeNM1nUbqYTWRkUBHt9GsEPwCHduRBEVvEJDhcaECmkTChKwxRbXMBPm7rqukr8AT2gwnp3CBVN33LR1EqmWi",
					Amount = new MoneyAmount
					{
						ValueDecimal = 50.0m,
						CurrencyEnum = CurrencyEnum.Rub
					},
					BillId = Guid.NewGuid().ToString(),
					SuccessUrl = new Uri("https://vk.com/")
				}
			);
			
			return Ok(result);
		}

		[HttpGet("bill/info")]
		public async Task<IActionResult> GetBillInfo()
		{
			var client = BillPaymentsClientFactory.Create("eyJ2ZXJzaW9uIjoiUDJQIiwiZGF0YSI6eyJwYXlpbl9tZXJjaGFudF9zaXRlX3VpZCI6InNzaWc2cS0wMCIsInVzZXJfaWQiOiI3OTUyMDM3MTI2MiIsInNlY3JldCI6IjRiMzkzNGNkNDM1NjhjZDA0ZGRjYjFlMTc4NDg0NjY1MGFjNjE4MWNjODE2MjU5ZjA1YTc1MWNmMDhhMGM5M2YifX0=");
			
			var result = client.GetBillInfo(
				"bb348953-384c-4267-b12b-2ab87c8061dd"
			);
			
			return Ok(result);
		}
		
		[HttpGet("bill")]
		public async Task<IActionResult> CreateBill()
		{
			var client = BillPaymentsClientFactory.Create("eyJ2ZXJzaW9uIjoiUDJQIiwiZGF0YSI6eyJwYXlpbl9tZXJjaGFudF9zaXRlX3VpZCI6InNzaWc2cS0wMCIsInVzZXJfaWQiOiI3OTUyMDM3MTI2MiIsInNlY3JldCI6IjRiMzkzNGNkNDM1NjhjZDA0ZGRjYjFlMTc4NDg0NjY1MGFjNjE4MWNjODE2MjU5ZjA1YTc1MWNmMDhhMGM5M2YifX0=");

			var result = await client.CreateBillAsync(
				new CreateBillInfo
				{
					BillId = Guid.NewGuid().ToString(),
					Amount = new MoneyAmount
					{
						ValueDecimal = 199.9m,
						CurrencyEnum = CurrencyEnum.Rub
					},
					Comment = "comment",
					ExpirationDateTime = DateTime.Now.AddDays(45),
					Customer = new Customer
					{
						Email = "rave9373@gmail.com",
						Account = Guid.NewGuid().ToString(),
						Phone = "70520371262"
					},
					SuccessUrl = new Uri("")
				}
			);
			
			return Ok(result);
		}
	}
}