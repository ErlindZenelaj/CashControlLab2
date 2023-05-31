using System;
using Domain.Common;

namespace API.Domain.Entities
{
	public class Company : BaseEntity
	{
        public string CompanyName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }


    }
}

