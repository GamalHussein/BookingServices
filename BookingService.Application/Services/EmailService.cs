using BookingService.Application.Common;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Services;
public class EmailService
{
	private readonly EmailSettings _emailSettings;

	public EmailService(IOptions<EmailSettings> emailSettings)
	{
		_emailSettings = emailSettings.Value;
	}

	/// <summary>
	/// إرسال إيميل بسيط
	/// </summary>
	public async Task SendEmailAsync(string toEmail, string subject, string body)
	{
		try
		{
			using var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort)
			{
				Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password),
				EnableSsl = true
			};

			var mailMessage = new MailMessage
			{
				From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
				Subject = subject,
				Body = body,
				IsBodyHtml = true
			};

			mailMessage.To.Add(toEmail);

			await client.SendMailAsync(mailMessage);
		}
		catch (Exception ex)
		{
			// Log error but don't throw - email failure shouldn't break the app
			Console.WriteLine($"فشل إرسال الإيميل: {ex.Message}");
		}
	}

	// ==========================================
	// Email Templates - قوالب جاهزة
	// ==========================================

	/// <summary>
	/// إيميل تأكيد التسجيل
	/// </summary>
	public async Task SendWelcomeEmailAsync(string toEmail, string userName)
	{
		var subject = "مرحباً بك في نظام حجز الخدمات";
		var body = $@"
                <html>
                <body style='font-family: Arial; direction: rtl;'>
                    <h2>مرحباً {userName}!</h2>
                    <p>شكراً لتسجيلك في نظام حجز الخدمات.</p>
                    <p>يمكنك الآن تصفح الخدمات المتاحة وحجز ما يناسبك.</p>
                    <br>
                    <p>مع تحياتنا،<br>فريق العمل</p>
                </body>
                </html>
            ";

		await SendEmailAsync(toEmail, subject, body);
	}

	/// <summary>
	/// إيميل حجز جديد للعميل
	/// </summary>
	public async Task SendBookingConfirmationAsync(
		string toEmail,
		string customerName,
		string serviceName,
		DateTime bookingDate,
		decimal price)
	{
		var subject = "تأكيد الحجز - نظام حجز الخدمات";
		var body = $@"
                <html>
                <body style='font-family: Arial; direction: rtl;'>
                    <h2>عزيزي {customerName}</h2>
                    <p>تم إنشاء حجزك بنجاح!</p>
                    
                    <div style='background: #f5f5f5; padding: 15px; border-radius: 5px;'>
                        <h3>تفاصيل الحجز:</h3>
                        <p><strong>الخدمة:</strong> {serviceName}</p>
                        <p><strong>التاريخ:</strong> {bookingDate:dd/MM/yyyy HH:mm}</p>
                        <p><strong>السعر:</strong> {price} جنيه</p>
                        <p><strong>الحالة:</strong> في انتظار التأكيد</p>
                    </div>
                    
                    <p>سيتم إرسال إيميل آخر عند تأكيد الحجز من مقدم الخدمة.</p>
                    
                    <br>
                    <p>مع تحياتنا،<br>فريق العمل</p>
                </body>
                </html>
            ";

		await SendEmailAsync(toEmail, subject, body);
	}

	/// <summary>
	/// إيميل تأكيد الحجز من مقدم الخدمة
	/// </summary>
	public async Task SendBookingApprovedAsync(
		string toEmail,
		string customerName,
		string serviceName,
		DateTime bookingDate)
	{
		var subject = "تم تأكيد حجزك ✅";
		var body = $@"
                <html>
                <body style='font-family: Arial; direction: rtl;'>
                    <h2>عزيزي {customerName}</h2>
                    <p style='color: green; font-size: 18px;'>✅ تم تأكيد حجزك!</p>
                    
                    <div style='background: #e8f5e9; padding: 15px; border-radius: 5px;'>
                        <p><strong>الخدمة:</strong> {serviceName}</p>
                        <p><strong>التاريخ:</strong> {bookingDate:dd/MM/yyyy HH:mm}</p>
                    </div>
                    
                    <p>نتطلع لخدمتك!</p>
                    
                    <br>
                    <p>مع تحياتنا،<br>فريق العمل</p>
                </body>
                </html>
            ";

		await SendEmailAsync(toEmail, subject, body);
	}

	/// <summary>
	/// إيميل إلغاء الحجز
	/// </summary>
	public async Task SendBookingCancelledAsync(
		string toEmail,
		string customerName,
		string serviceName,
		string reason)
	{
		var subject = "تم إلغاء الحجز";
		var body = $@"
                <html>
                <body style='font-family: Arial; direction: rtl;'>
                    <h2>عزيزي {customerName}</h2>
                    <p>تم إلغاء حجزك لخدمة: <strong>{serviceName}</strong></p>
                    
                    <p><strong>السبب:</strong> {reason}</p>
                    
                    <p>يمكنك حجز خدمة أخرى في أي وقت.</p>
                    
                    <br>
                    <p>مع تحياتنا،<br>فريق العمل</p>
                </body>
                </html>
            ";

		await SendEmailAsync(toEmail, subject, body);
	}

	/// <summary>
	/// إيميل نجاح الدفع
	/// </summary>
	public async Task SendPaymentSuccessAsync(
		string toEmail,
		string customerName,
		decimal amount,
		string serviceName,
		string transactionId)
	{
		var subject = "تم الدفع بنجاح ✅";
		var body = $@"
                <html>
                <body style='font-family: Arial; direction: rtl;'>
                    <h2>عزيزي {customerName}</h2>
                    <p style='color: green; font-size: 18px;'>✅ تم الدفع بنجاح!</p>
                    
                    <div style='background: #e8f5e9; padding: 15px; border-radius: 5px;'>
                        <h3>تفاصيل الدفع:</h3>
                        <p><strong>الخدمة:</strong> {serviceName}</p>
                        <p><strong>المبلغ:</strong> {amount} جنيه</p>
                        <p><strong>رقم العملية:</strong> {transactionId}</p>
                        <p><strong>التاريخ:</strong> {DateTime.Now:dd/MM/yyyy HH:mm}</p>
                    </div>
                    
                    <p>شكراً لاستخدامك خدماتنا!</p>
                    
                    <br>
                    <p>مع تحياتنا،<br>فريق العمل</p>
                </body>
                </html>
            ";

		await SendEmailAsync(toEmail, subject, body);
	}

	/// <summary>
	/// إيميل فشل الدفع
	/// </summary>
	public async Task SendPaymentFailedAsync(
		string toEmail,
		string customerName,
		string serviceName)
	{
		var subject = "فشلت عملية الدفع";
		var body = $@"
                <html>
                <body style='font-family: Arial; direction: rtl;'>
                    <h2>عزيزي {customerName}</h2>
                    <p style='color: red;'>⚠️ فشلت عملية الدفع لخدمة: <strong>{serviceName}</strong></p>
                    
                    <p>يرجى التحقق من بيانات البطاقة والمحاولة مرة أخرى.</p>
                    
                    <p>إذا استمرت المشكلة، يرجى التواصل معنا.</p>
                    
                    <br>
                    <p>مع تحياتنا،<br>فريق العمل</p>
                </body>
                </html>
            ";

		await SendEmailAsync(toEmail, subject, body);
	}

	/// <summary>
	/// إيميل حجز جديد لمقدم الخدمة
	/// </summary>
	public async Task SendNewBookingToProviderAsync(
		string toEmail,
		string providerName,
		string customerName,
		string serviceName,
		DateTime bookingDate)
	{
		var subject = "🔔 حجز جديد!";
		var body = $@"
                <html>
                <body style='font-family: Arial; direction: rtl;'>
                    <h2>عزيزي {providerName}</h2>
                    <p style='font-size: 18px;'>🔔 لديك حجز جديد!</p>
                    
                    <div style='background: #fff3e0; padding: 15px; border-radius: 5px;'>
                        <h3>تفاصيل الحجز:</h3>
                        <p><strong>العميل:</strong> {customerName}</p>
                        <p><strong>الخدمة:</strong> {serviceName}</p>
                        <p><strong>التاريخ:</strong> {bookingDate:dd/MM/yyyy HH:mm}</p>
                    </div>
                    
                    <p>يرجى الدخول للنظام لتأكيد أو رفض الحجز.</p>
                    
                    <br>
                    <p>مع تحياتنا،<br>فريق العمل</p>
                </body>
                </html>
            ";

		await SendEmailAsync(toEmail, subject, body);
	}
}
