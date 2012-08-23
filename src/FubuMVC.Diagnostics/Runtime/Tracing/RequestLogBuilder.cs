using FubuCore.Dates;
using FubuMVC.Core.Http;
using FubuMVC.Core.Urls;

namespace FubuMVC.Diagnostics.Runtime.Tracing
{
    public class RequestLogBuilder : IRequestLogBuilder
    {
        private readonly IUrlRegistry _urls;
        private readonly ISystemTime _systemTime;
        private readonly ICurrentHttpRequest _request;
        private readonly ICurrentChain _currentChain;

        public RequestLogBuilder(IUrlRegistry urls, ISystemTime systemTime, ICurrentHttpRequest request, ICurrentChain currentChain)
        {
            _urls = urls;
            _systemTime = systemTime;
            _request = request;
            _currentChain = currentChain;
        }

        public RequestLog BuildForCurrentRequest()
        {
            var log = new RequestLog{
                ChainId    = _currentChain.OriginatingChain.UniqueId,
                HttpMethod = _request.HttpMethod(),
                Url = _request.RelativeUrl(),
                Time = _systemTime.UtcNow()
            };

            log.ReportUrl = _urls.UrlFor(log);

            return log;
        }
    }
}