using BenchmarkDotNet.Running;
using Wolfgang.Etl.Json.Benchmarks;

BenchmarkSwitcher.FromAssembly(typeof(JsonSingleStreamExtractorBenchmarks).Assembly).Run(args);
