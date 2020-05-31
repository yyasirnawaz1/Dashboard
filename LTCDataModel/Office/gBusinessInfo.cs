using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTCDataModel.Office
{
	public class gBusinessInfo
	{
		public int Id { get; set; }
		public int? Office_Sequence { get; set; }
		public int? Office_Number { get; set; }
		public string ClinicName { get; set; }
		public bool Active { get; set; }
		public bool Newsletter { get; set; }
	}
}