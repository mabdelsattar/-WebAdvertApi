using AdvertAPI.Sevices;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdvertAPI.HealthChecks
{
    public class DBStorageService:IHealthCheck
    {

        private readonly IAdvertStorageService _advertStorageService;
        public DBStorageService(IAdvertStorageService advertStorageService)
        {
            _advertStorageService = advertStorageService;
        }
        public DBStorageService()
        {

        }


        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            //configuration information - startup class 
            CancellationToken cancellationToken = default)
        //allows to cancel any async operation
        {

          

            var res = await _advertStorageService.CheckHealthAsync();
            return res == true ? new HealthCheckResult(HealthStatus.Healthy) : new HealthCheckResult(HealthStatus.Unhealthy);
        }
    }
}
