﻿using MachineHealthCheck.Domain.Entities;
using MachineHealthCheck.Domain.Interfaces;
using MachineHealthCheck.Domain.Models;
using MachineHealthCheck.Service;
using Microsoft.AspNetCore.Mvc;

namespace MachineHealthCheck.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthCheckController : Controller
    {
        private readonly ILogger<HealthCheckController> _logger;
        private readonly IHealthCheckService _healthCheckService;
        public HealthCheckController(ILogger<HealthCheckController> logger, IHealthCheckService healthCheckService)
        {
            _logger = logger;
            _healthCheckService = healthCheckService;
        }
        [HttpGet("List")]
        public async Task<ActionResult<IList<HealthCheckDTO>>> List(string key)
        {
            IList<HealthCheckDTO> returns = new List<HealthCheckDTO>();
            IList<HealthCheck> entites = new List<HealthCheck>();
            entites = await _healthCheckService.GetAll(key);

            if (entites == null)
            {
                return (ActionResult<IList<HealthCheckDTO>>)NotFound();
            }
            foreach (var e in entites)
            {
                returns.Add(HealthCheckDTO.FromEntity(e));
            }

            return (ActionResult<IList<HealthCheckDTO>>)Ok(returns);
        }
        [HttpGet("MostRecent")]
        public async Task<ActionResult<IList<HealthCheckDTO>>> MostRecent(string key)
        {
            var one = await _healthCheckService.GetMostRecent(key);

            if (one == null)
            {
                return (ActionResult<IList<HealthCheckDTO>>)NotFound();
            }

            return (ActionResult<IList<HealthCheckDTO>>)Ok(HealthCheckDTO.FromEntity(one));
        }
    }
}
