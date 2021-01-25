using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Order;
using System;
using System.Collections.Generic;

namespace Benchmarks
{
    [SimpleJob(RunStrategy.Throughput)]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [MinColumn, MaxColumn, Q3Column]
    public class DictionaryBenchmarks
    {
        private readonly Dictionary<string, string> invariantComparerDict;
        private readonly Dictionary<string, string> defaultComparerDict;

        public DictionaryBenchmarks()
        {
            string[] keys = new[] { "key1", "key2", "key" };
            invariantComparerDict = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            defaultComparerDict = new Dictionary<string, string>();
            foreach(var key in keys)
            {
                invariantComparerDict.Add(key, "TRUE");
                defaultComparerDict.Add(key, "TRUE");
            }
        }

        [Benchmark]
        public bool InvariantDictTryGetValue()
        {
            if (invariantComparerDict.TryGetValue("KEY", out string value) && "true".Equals(value, StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }

            return false;
        }

        [Benchmark]
        public bool DefaultDictContainsGetValue()
        {
            if (defaultComparerDict.ContainsKey("KEY".ToLowerInvariant()) && defaultComparerDict["KEY".ToLowerInvariant()].ToLowerInvariant() == "true")
            {
                return true;
            }

            return false;
        }

        [Benchmark]
        public bool DefaultDictTryGetValueCompareWithEqualsInvariantCulture()
        {
            string key = "KEY".ToLowerInvariant();
            if (defaultComparerDict.TryGetValue(key, out string value) && "true".Equals(value, StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }

            return false;
        }

        [Benchmark]
        public bool DefaultDictTryGetValueCompareWithEqualityOperator()
        {
            string key = "KEY".ToLowerInvariant();
            if (defaultComparerDict.TryGetValue(key, out string value) && value.ToLowerInvariant() == "true")
            {
                return true;
            }

            return false;
        }
    }
}
