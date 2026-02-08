// ==========================================
// BookingServices.API/Program.cs
// ==========================================

using BookingService.Api.Helpers;
using BookingService.Api.Seeds;
using BookingService.Application.Common;
using BookingService.Application.Interfaces;
using BookingService.Application.Mapping;
using BookingService.Application.Services;
using BookingService.Application.Validators.User;
using BookingService.Domain.Models;
using BookingService.Infrastructure.Data;
using BookingService.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ========== Database Configuration ==========
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(
		builder.Configuration.GetConnectionString("DefaultConnection")
	)
);

// ========== Identity Configuration ==========
builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
{
	// Password settings
	options.Password.RequireDigit = true;
	options.Password.RequireLowercase = true;
	options.Password.RequireUppercase = true;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequiredLength = 6;

	// User settings
	options.User.RequireUniqueEmail = true;

	// Lockout settings (optional)
	options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
	options.Lockout.MaxFailedAccessAttempts = 5;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();




// ========== JWT Configuration + Identity ==========
var jwtSettings = builder.Configuration
	.GetSection("JwtSettings")
	.Get<JwtSettings>();  
builder.Services.Configure<JwtSettings>(
	builder.Configuration.GetSection("JwtSettings"));

// مفتاح التوقيع
var key = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
	options.RequireHttpsMetadata = false;
	options.SaveToken = true;

	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(key),

		ValidateIssuer = true,
		ValidIssuer = jwtSettings.Issuer,  // نفس القيمة المستخدمة في توليد التوكن

		ValidateAudience = true,
		ValidAudience = jwtSettings.Audience,

		ValidateLifetime = true,
		ClockSkew = TimeSpan.Zero
	};
});

// ========== Mappster ==========
var mappingConfig =TypeAdapterConfig.GlobalSettings;
mappingConfig.Scan(typeof(UserMappingConfigration).Assembly);
builder.Services.AddSingleton(mappingConfig);
var mapper = new Mapper(mappingConfig);
builder.Services.AddSingleton<IMapper>(mapper);

// ========== FluentValidation ==========
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterDtoValidator>();

// ========== Dependency Injection ==========
builder.Services.AddScoped<JwtHelper>();
builder.Services.AddScoped<StripeService>();
// User
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Category
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// Service
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IServiceService, ServiceService>();

// Booking
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IBookingService, BookingServices>();

// Payment
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

// Notification
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();

// ========== CORS ==========
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll", policy =>
	{
		policy.AllowAnyOrigin()
			  .AllowAnyMethod()
			  .AllowAnyHeader();
	});
});

// ========== Controllers ==========
builder.Services.AddControllers();

// ========== Swagger Configuration ==========
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "Booking Services API",
		Version = "v1",
		Description = "API لنظام حجز الخدمات"
	});

	// Add JWT Authentication to Swagger
	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "أدخل JWT Token هنا"
	});

	options.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			Array.Empty<string>()
		}
	});
});

var app = builder.Build();

// ========== Middleware Pipeline ==========
//if (app.Environment.IsDevelopment())
//{
//	app.UseSwagger();
//	app.UseSwaggerUI();
//}
//using (var scope = app.Services.CreateScope())
//{
//	var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//	db.Database.Migrate();
//}


app.UseSwagger();
app.UseSwaggerUI(c =>
{
	c.SwaggerEndpoint("/swagger/v1/swagger.json", "Booking Services API V1");
	c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
await IdentitySeed.SeedAsync(app.Services);
app.Run();

