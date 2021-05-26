using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Qiwi.BillPayments.Client;
using Qiwi.BillPayments.Model;
using Qiwi.BillPayments.Model.In;
using Qiwi.BillPayments.Model.Out;
using Qiwi.BillPayments.Utils;
using RoboKassaExample.Models;

namespace RoboKassaExample.Services
{
	public class QiwiService : IQiwiService
	{
		private readonly BillPaymentsClient _client;
		private readonly IConfiguration _configuration;
		
		public QiwiService(IConfiguration configuration)
		{
			_configuration = configuration;
			_client = BillPaymentsClientFactory.Create(_configuration["QiwiApi:PrivateKey"]);
		}

		public async Task<BillResponse> CreateBill(CreateBillModel createBillModel)
		{
			var result = await _client.CreateBillAsync(
				new CreateBillInfo
				{
					BillId = Guid.NewGuid().ToString(),
					Amount = new MoneyAmount
					{
						ValueDecimal = createBillModel.Amount,
						CurrencyEnum = CurrencyEnum.Rub
					},
					Comment = createBillModel.Comment,
					ExpirationDateTime = DateTime.Now.AddDays(45),
					Customer = createBillModel.Customer,
					SuccessUrl = new Uri(_configuration["QiwiApi:SuccessUrl"])
				}
			);

			return result;
		}

		public bool HandleCallback(Notification data, string signature)
		{
			var result =
				BillPaymentsUtils.CheckNotificationSignature(signature, data, _configuration["QiwiApi:PrivateKey"]);

			//todo: добавить логирование
			
			return result;
		}
	}
}