using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ESourcing.Application.PipelineBehaviors
{
    public class PerformanceBehavior<TRequest,TResponse> : IPipelineBehavior<TRequest,TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;

        public PerformanceBehavior( ILogger<TRequest> logger)
        {
            _timer = new Stopwatch();
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();
            var response = await next();
            _timer.Stop();

            var elapsedMilliSeconds = _timer.ElapsedMilliseconds;

            if (elapsedMilliSeconds>500)
            {
                var requestName = typeof(TRequest).Name;
                _logger.LogWarning("Long Runnig Request : {Name} ({ElapsedMilliSeconds} milliseconds) {@Request}",requestName,elapsedMilliSeconds,request);
            }
            return response;
        }
    }
}
