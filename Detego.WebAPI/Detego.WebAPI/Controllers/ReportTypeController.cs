using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Detego.WebAPI.Data.Repositories.Interfaces;
using Detego.WebAPI.Models.LookUpModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Detego.WebAPI.Controllers
{

    [Produces("application/json")]
    [Route("api/ReportType")]
    [ApiController]
    public class ReportTypeController : ControllerBase
    {
        IReportTypeRepository _reportTypeRepository;

        public ReportTypeController(IReportTypeRepository reportTypeRepository)
        {
            _reportTypeRepository = reportTypeRepository;
        }

        [HttpGet("getReportTypes")]
        public async Task<List<ReportType>> GetReportTypes()
        {

            var reportTypes = await _reportTypeRepository.GetReportTypes();

            return reportTypes;
        }

    }
}