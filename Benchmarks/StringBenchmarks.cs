using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Order;
using System;

namespace Benchmarks
{
    [SimpleJob(RunStrategy.Throughput)]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [MinColumn, MaxColumn, Q3Column]
    public class StringBenchmarks
    {
        [Benchmark]
        [Arguments("true")]
        [Arguments("truE")]
        [Arguments("TRUE")]
        [Arguments("false")]
        public bool InvariantCultureEquals(string value)
        {
            return "true".Equals(value, StringComparison.InvariantCultureIgnoreCase);
        }

        [Benchmark]
        [Arguments("true")]
        [Arguments("truE")]
        [Arguments("TRUE")]
        [Arguments("false")]
        public bool OneToLowerInvariantOperatorEquals(string value)
        {
            return "true" == value.ToLowerInvariant();
        }

        [Benchmark]
        [Arguments("true")]
        [Arguments("truE")]
        [Arguments("TRUE")]
        [Arguments("false")]
        public bool BothToLowerInvariantOperatorEquals(string value)
        {
            return "true".ToLowerInvariant() == value.ToLowerInvariant();
        }
    }
}
