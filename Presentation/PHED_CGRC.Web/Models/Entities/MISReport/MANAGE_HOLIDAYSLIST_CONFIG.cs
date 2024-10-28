using System;
namespace PHED_CGRC.MANAGE_HOLIDAYSLIST_CONFIG
{
	public class MANAGE_HOLIDAYSLIST_CONFIG_Model
	{
		public int? HolidayId { get; set; }
		public string? FromDate { get; set; }
		public string? ToDate { get; set; }
		public int? TotalDays { get; set; }
		public string? HolidayNameE { get; set; }
		public string? HolidayNameH { get; set; }
		public string? Description { get; set; }
		public int? CreatedBy { get; set; }
		public int? UpdatedBy { get; set; }
		public int? UserId { get; set; }
		public int? DeletedFlag { get; set; }
	}
}

