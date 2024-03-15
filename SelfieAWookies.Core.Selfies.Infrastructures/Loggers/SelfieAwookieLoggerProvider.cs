using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfieAWookies.Core.Selfies.Infrastructures.Loggers
{
    public class SelfieAwookieLoggerProvider : ILoggerProvider
    {
        #region Fiels
        private ConcurrentDictionary<string, SelfieAwookieLogger> _loggers = new ConcurrentDictionary<string, SelfieAwookieLogger>();
        #endregion
        #region Public methods
        public ILogger CreateLogger(string categoryName)
        {
            _loggers.GetOrAdd(categoryName, key => new SelfieAwookieLogger());

            return _loggers[categoryName];
        }

        public void Dispose()
        {
            _loggers.Clear();
        }
        #endregion
    }
}
