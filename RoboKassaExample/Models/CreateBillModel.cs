using Qiwi.BillPayments.Model.In;

namespace RoboKassaExample.Models
{
	public class CreateBillModel
	{
		public decimal Amount { get; set; }
		public string Comment { get; set; }
		public Customer Customer { get; set; }
	}
}