using System;
using Domain.Common;

namespace API.Domain.Entities
{
	public class User : BaseEntity
	{
		public string Name { get; set; }

		public string LastName { get; set; }

		public string UserName { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }
		
	}
}

