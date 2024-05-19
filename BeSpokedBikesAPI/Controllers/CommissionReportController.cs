using BeSpokedBikesAPI.Interface;
using BeSpokedBikesAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeSpokedBikesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommissionReportController : ControllerBase
    {
        private readonly ICommissionReportRepository _commissionReportRepository;

        public CommissionReportController(ICommissionReportRepository commissionReportRepository)
        {
            _commissionReportRepository = commissionReportRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuarterlyCommissionReport>>>GetQuarterlyCommissionReport(int year)
        {
            var report = await _commissionReportRepository.GetQuarterlyCommissionReportAsync(year);
            return Ok(report);
        }
    }

}
