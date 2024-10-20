using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected IUnitOfWork _uow;

        protected readonly ITracer _tracer;

        private long _sessionId = 0;

        private Stopwatch _sw = null;

        public BaseApiController(IUnitOfWork uow, ITracer tracer)
        {
            _uow = uow;
            _tracer = tracer;
        }

        protected async Task BeginPerformanceTraceAsync(string msg, string user, bool addContext = false)
        {
            string remoteIp = null;
            string pageSize = null;
            string workingSet = null;

            if (addContext)
            {
                remoteIp = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                pageSize = Environment.SystemPageSize.ToString();
                workingSet = Environment.WorkingSet.ToString();
            }

            _sessionId = Stopwatch.GetTimestamp();
            _sw = Stopwatch.StartNew();

            await _tracer.TraceAsync(msg + " - begin", user, 0, _sessionId, remoteIp, pageSize, workingSet);
        }

        protected async Task EndPerformanceTraceAsync(string msg, string user)
        {
            await _tracer.TraceAsync(msg + " - end", user, _sw.ElapsedTicks, _sessionId);
        }

        protected async Task TraceAsync(string msg, string user, bool addContext = false)
        {
            string remoteIp = null;
            string pageSize = null;
            string workingSet = null;

            if (addContext)
            {
                remoteIp = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                pageSize = Environment.SystemPageSize.ToString();
                workingSet = Environment.WorkingSet.ToString();
            }

            await _tracer.TraceAsync(msg, user, null, null, remoteIp, pageSize, workingSet);
        }
    }
}