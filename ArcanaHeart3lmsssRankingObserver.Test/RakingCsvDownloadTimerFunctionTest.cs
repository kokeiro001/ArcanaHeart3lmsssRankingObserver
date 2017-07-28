using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using ArcanaHeart3lmsssRankingObserver.Functions;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArcanaHeart3lmsssRankingObserver.Test
{
    [TestClass]
    public class RakingCsvDownloadTimerFunctionTest
    {
        [TestMethod]
        public async Task Test()
        {
            var traceWriter = new TestTraceWriter(TraceLevel.Verbose);
            await RakingCsvDownloadTimerFunction.Run(null, traceWriter);
        }
    }

    public class TestTraceWriter : TraceWriter
    {
        private Collection<TraceEvent> _traces = new Collection<TraceEvent>();

        public TestTraceWriter(TraceLevel level) : base(level)
        {
        }

        public Collection<TraceEvent> Traces
        {
            get
            {
                return _traces;
            }
        }

        public bool Flushed { get; private set; }

        public override void Trace(TraceEvent traceEvent)
        {
            Traces.Add(traceEvent);
        }

        public override void Flush()
        {
            Flushed = true;

            base.Flush();
        }
    }
}
