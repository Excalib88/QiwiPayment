using System.Threading.Tasks;
using Qiwi.BillPayments.Model;
using Qiwi.BillPayments.Model.Out;
using RoboKassaExample.Models;

namespace RoboKassaExample.Services
{
	public interface IQiwiService
	{
		Task<BillResponse> CreateBill(CreateBillModel model);
		bool HandleCallback(Notification data, string signature);
	}
}