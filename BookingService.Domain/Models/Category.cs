using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Domain.Models;
public class Category
{
	#region Fields
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public DateTime CreatedAt { get; set; }= DateTime.UtcNow;
	public bool IsActive { get; set; } = true;
	public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
	#endregion


	#region Navigation Properties
	public ICollection<Service> Services { get; set; } = new List<Service>();
	#endregion


}
