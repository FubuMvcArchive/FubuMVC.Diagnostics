using System;
using FubuCore.Binding;
using FubuCore.Binding.Values;
using FubuCore.Dates;
using FubuMVC.Core.Http;
using FubuMVC.Core.Urls;
using FubuMVC.Diagnostics.Chains;

namespace FubuMVC.Diagnostics.Runtime.Tracing
{
    public class RequestLogBuilder : IRequestLogBuilder
    {
        private readonly IUrlRegistry _urls;
        private readonly ISystemTime _systemTime;
        private readonly ICurrentHttpRequest _request;
        private readonly ICurrentChain _currentChain;
        private readonly IRequestData _requestData;

        public RequestLogBuilder(IUrlRegistry urls, ISystemTime systemTime, ICurrentHttpRequest request, ICurrentChain currentChain, IRequestData requestData)
        {
            _urls = urls;
            _systemTime = systemTime;
            _request = request;
            _currentChain = currentChain;
            _requestData = requestData;
        }

        public RequestLog BuildForCurrentRequest()
        {
            var report = new ValueReport();
            _requestData.WriteReport(report);

            var chainId = _currentChain.OriginatingChain == null ? Guid.Empty :
                _currentChain.OriginatingChain.UniqueId;
            var log = new RequestLog{
                ChainId    = chainId,
                HttpMethod = _request.HttpMethod(),
                Url = _request.RelativeUrl(),
                Time = _systemTime.UtcNow(),
                RequestData = report,
                ChainUrl = _urls.UrlFor(new ChainRequest{Id = chainId}),
                DetailsUrl = _urls.UrlFor(new ChainDetailsRequest{Id = chainId})
            };

            log.ReportUrl = _urls.UrlFor(log);

            return log;
        }
    }
}