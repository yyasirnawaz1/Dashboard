using LTCDataModel.PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTCDataModel.Office
{
	[TableName("authentication")]
	[PrimaryKey("UserID", AutoIncrement  = true)]
	public class gUserProfile
	{

		public int UserID { get; set; }
		public int? Office_Sequence { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Provider { get; set; }
		public string Salutation { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public string Initials { get; set; }
		public string Phone { get; set; }
		public string Fax { get; set; }
		public string AddressLine1 { get; set; }
		public string AddressLine2 { get; set; }
		public string AddressLine3 { get; set; }
		public string City { get; set; }
		public string Province { get; set; }
		public string Country { get; set; }
		public string PostalCode { get; set; }
		public DateTime? LastLogin { get; set; }
		public string PhotoImageURL { get; set; }
		public string WebsiteURL { get; set; }
		public string DateFormat { get; set; }
		public int LanguageSelected { get; set; }
		public short IsActive { get; set; }

		

	}
}