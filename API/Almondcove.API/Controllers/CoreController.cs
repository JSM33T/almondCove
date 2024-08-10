﻿using Almondcove.Entities.Shared;
using Almondcove.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Almondcove.API.Controllers
{
    [Route("api/core")]

    [ApiController]
    public class CoreController : ControllerBase
    {

        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly INotificationService _notificationService;
        private readonly IMailService _mailService;
        private readonly ITelegramService _telegramService;

        public CoreController(IWebHostEnvironment hostingEnvironment, INotificationService notificationService, IMailService mailService, ITelegramService telegramService)
        {
            _hostingEnvironment = hostingEnvironment;
            _notificationService = notificationService;
            _mailService = mailService;
            _telegramService = telegramService;
        }

        [Authorize(Roles = "admin")]
        [HttpPost("restart")]
        public IActionResult Restart()
        {
            string appPath = _hostingEnvironment.ContentRootPath;

            ProcessStartInfo info = new()
            {
                FileName = System.Reflection.Assembly.GetEntryAssembly().Location,
                Arguments = string.Join(" ", Environment.GetCommandLineArgs().Skip(1))
            };

            Process.Start(info);

            Environment.Exit(0);

            return Ok("Restarting the application");
        }

        [HttpGet]
        public async Task<IActionResult> Hello()
        {
            _ = Task.Run(async () =>
            {
                await _notificationService.SendNotificationAsync("hey there did the noti came?");
            });
            return Ok("hello form the server");
        }

        [HttpPost("sendEmail")]
        public async Task<IActionResult> SendEmail([FromBody] EmailMessage emailMessage)
        {
            await _mailService.SendEmailAsync(emailMessage);

            return Ok("Email has been sent");
        }

        [HttpGet("tele")]
        public async Task<IActionResult> testtele()
        {
            await _telegramService.SendMessageAsync("Hello from the server");

            return Ok("Hello from the server");
        }

    }
}