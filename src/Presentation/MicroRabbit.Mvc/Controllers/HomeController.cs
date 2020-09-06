using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MicroRabbit.Mvc.Models;
using MicroRabbit.Mvc.Models.Dto;
using MicroRabbit.Mvc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MicroRabbit.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public const string Name = "Home";
        private readonly ILogger<HomeController> _logger;
        private readonly ITransferService _transferService;

        public HomeController(ILogger<HomeController> logger, ITransferService transferService) {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _transferService = transferService ?? throw new ArgumentNullException(nameof(transferService));
        }

        [HttpGet]
        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult Privacy() => View();

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });

        [HttpPost]
        public async Task<IActionResult> Transfer([FromForm] TransferViewModel model) {
            var transferDto = new TransferDto {
                FromAccount = model.FromAccount,
                ToAccount = model.ToAccount,
                TransferAmount = model.TransferAmount
            };
            await _transferService.Transfer(transferDto);
            return View();
        }
    }
}
