using BookingService.Domain.Enums;
using BookingService.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace BookingService.Api.Seeds;

public static class IdentitySeed
{
	public static async Task SeedAsync(
		IServiceProvider serviceProvider)
	{
		using var scope = serviceProvider.CreateScope();

		var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
		var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

		await SeedRoles(roleManager);
		await SeedAdminUser(userManager);
	}

	private static async Task SeedRoles(
		RoleManager<IdentityRole<Guid>> roleManager)
	{
		string[] roles = { "Admin", "Customer", "ServiceProvider" };

		foreach (var role in roles)
		{
			if (!await roleManager.RoleExistsAsync(role))
			{
				await roleManager.CreateAsync(new IdentityRole<Guid>
				{
					Name = role,
					NormalizedName = role.ToUpper()
				});
			}
		}
	}

	private static async Task SeedAdminUser(
		UserManager<ApplicationUser> userManager)
	{
		var adminEmail = "gamilhhussein@gmail.com";

		var admin = await userManager.FindByEmailAsync(adminEmail);

		if (admin != null) return;

		admin = new ApplicationUser
		{
			UserName = adminEmail,
			Email = adminEmail,
			EmailConfirmed = true,
			FirstName = "System",
			LastName = "Admin",
			UserType = UserType.Admin
		};

		var result = await userManager.CreateAsync(admin, "Admin@123");

		if (result.Succeeded)
		{
			await userManager.AddToRoleAsync(admin, "Admin");
		}
	}
}
