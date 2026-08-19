window.BENCHMARK_DATA = {
  "lastUpdate": 1787143876805,
  "repoUrl": "https://github.com/Chris-Wolfgang/ETL-Json",
  "entries": {
    "BenchmarkDotNet": [
      {
        "commit": {
          "author": {
            "email": "210299580+Chris-Wolfgang@users.noreply.github.com",
            "name": "Chris Wolfgang",
            "username": "Chris-Wolfgang"
          },
          "committer": {
            "email": "210299580+Chris-Wolfgang@users.noreply.github.com",
            "name": "Chris Wolfgang",
            "username": "Chris-Wolfgang"
          },
          "distinct": true,
          "id": "3dd4c0f0c7925a8f7f11d002c41e2c491433fec8",
          "message": "Canonical config + workflows: protected-only PR ahead of vNext #112\n\nExtracts the 8 protected configuration files from the v0.2.1 vNext branch\nso they can be admin-bypass-merged in isolation, instead of bypassing the\nentire vNext -> main release diff (which would waive review-thread\nenforcement on all of it).\n\nProtected files:\n- .editorconfig\n- Directory.Build.props\n- BannedSymbols.txt\n- .github/workflows/{benchmarks,codeql,pr,release,stryker}.yaml\n\nDetect .NET Projects will fail on this PR by design (that is the guard's\npurpose). A maintainer reviews these 8 files and admin-bypass-merges.\nAfter this lands on main, PR #112's protected-file delta vanishes and it\nmerges through the normal ruleset with no bypass.\n\nCo-Authored-By: Claude Opus 4.8 <noreply@anthropic.com>",
          "timestamp": "2026-06-22T13:45:38-04:00",
          "tree_id": "c5c7ac71f1af7fd20d078cb5fa13633f349ce8d7",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/3dd4c0f0c7925a8f7f11d002c41e2c491433fec8"
        },
        "date": 1782150537743,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6441.985092163086,
            "unit": "ns",
            "range": "± 7.819333970181716"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 61101.316080729164,
            "unit": "ns",
            "range": "± 518.1905970802236"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 559575.7692057291,
            "unit": "ns",
            "range": "± 4325.181877979418"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 4952.859959920247,
            "unit": "ns",
            "range": "± 22.27042561198563"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 47098.25478108724,
            "unit": "ns",
            "range": "± 668.179870136898"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 499452.7854817708,
            "unit": "ns",
            "range": "± 2453.2650460016016"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 7296.2109375,
            "unit": "ns",
            "range": "± 23.357951680672222"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 70139.5801595052,
            "unit": "ns",
            "range": "± 348.82370246493696"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 710255.8208007812,
            "unit": "ns",
            "range": "± 1802.6249564792265"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 5357.564712524414,
            "unit": "ns",
            "range": "± 80.30567731162176"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 53092.66328938802,
            "unit": "ns",
            "range": "± 142.3260372824425"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 525704.9381510416,
            "unit": "ns",
            "range": "± 1697.074940046008"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6298.945915222168,
            "unit": "ns",
            "range": "± 12.320046296496898"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 54608.12194824219,
            "unit": "ns",
            "range": "± 67.6180261565714"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 510361.8385416667,
            "unit": "ns",
            "range": "± 1608.4732115575837"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3603.8893953959146,
            "unit": "ns",
            "range": "± 21.02188417087059"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 4160.4232228597,
            "unit": "ns",
            "range": "± 26.34051873524321"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 33531.30975341797,
            "unit": "ns",
            "range": "± 356.95632814548975"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 34357.56632486979,
            "unit": "ns",
            "range": "± 112.03676241713511"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 390235.24397786456,
            "unit": "ns",
            "range": "± 1720.782063695575"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 387054.9928385417,
            "unit": "ns",
            "range": "± 1718.6152635887393"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "210299580+Chris-Wolfgang@users.noreply.github.com",
            "name": "Chris Wolfgang",
            "username": "Chris-Wolfgang"
          },
          "committer": {
            "email": "noreply@github.com",
            "name": "GitHub",
            "username": "web-flow"
          },
          "distinct": true,
          "id": "7f217584691abb9e9e424aeea8996997daf06b65",
          "message": "Merge pull request #112 from Chris-Wolfgang/vNext\n\nRelease v0.2.1: canonical maintenance round + AssemblyVersion fix",
          "timestamp": "2026-06-22T15:25:29-04:00",
          "tree_id": "f6c644392786141bcac9f22d05570503bf49e39e",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/7f217584691abb9e9e424aeea8996997daf06b65"
        },
        "date": 1782156522514,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6189.492917378743,
            "unit": "ns",
            "range": "± 240.37386963720215"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 58426.67222086588,
            "unit": "ns",
            "range": "± 147.32489901713106"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 588378.3138020834,
            "unit": "ns",
            "range": "± 1936.9269431713797"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 4850.587908426921,
            "unit": "ns",
            "range": "± 17.23274224583468"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 45265.27404785156,
            "unit": "ns",
            "range": "± 361.74787395413847"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 538241.572265625,
            "unit": "ns",
            "range": "± 12216.418166779948"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 7763.887034098308,
            "unit": "ns",
            "range": "± 11.40895713680289"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 72077.67720540364,
            "unit": "ns",
            "range": "± 168.44483249117567"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 699467.7496744791,
            "unit": "ns",
            "range": "± 1186.6398030746132"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 5427.584482828776,
            "unit": "ns",
            "range": "± 57.6836906383486"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 53584.69305419922,
            "unit": "ns",
            "range": "± 311.86820157379503"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 531467.7978515625,
            "unit": "ns",
            "range": "± 4073.0654909134373"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6431.211784362793,
            "unit": "ns",
            "range": "± 61.65997794091474"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 52314.91786702474,
            "unit": "ns",
            "range": "± 39.73418116589431"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 519715.9680989583,
            "unit": "ns",
            "range": "± 1367.0194011511187"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3575.8462092081704,
            "unit": "ns",
            "range": "± 10.013793532835775"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 4331.400390625,
            "unit": "ns",
            "range": "± 94.09493293680981"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 34365.92258707682,
            "unit": "ns",
            "range": "± 266.22057252664536"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 35368.29305013021,
            "unit": "ns",
            "range": "± 147.76655932865418"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 395917.5107421875,
            "unit": "ns",
            "range": "± 945.4867369553939"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 390845.69091796875,
            "unit": "ns",
            "range": "± 2420.46637173822"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "210299580+Chris-Wolfgang@users.noreply.github.com",
            "name": "Chris Wolfgang",
            "username": "Chris-Wolfgang"
          },
          "committer": {
            "email": "noreply@github.com",
            "name": "GitHub",
            "username": "web-flow"
          },
          "distinct": true,
          "id": "ebc51317fe1046b7f048722efaa63a90b8fb00da",
          "message": "Merge pull request #162 from Chris-Wolfgang/security/sha-pin-actions\n\nsecurity: SHA-pin GitHub-owned actions (#95)",
          "timestamp": "2026-06-22T17:29:46-04:00",
          "tree_id": "1bd464fb4c2a26256f016d8ad948dbaf3f247bf3",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/ebc51317fe1046b7f048722efaa63a90b8fb00da"
        },
        "date": 1782163980947,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6190.588005065918,
            "unit": "ns",
            "range": "± 250.14682808173248"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 58427.55806477865,
            "unit": "ns",
            "range": "± 527.4116856479468"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 585335.9443359375,
            "unit": "ns",
            "range": "± 8552.11238993418"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 4887.695627848308,
            "unit": "ns",
            "range": "± 51.697037957324035"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 43316.126708984375,
            "unit": "ns",
            "range": "± 177.26323000899555"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 511544.451171875,
            "unit": "ns",
            "range": "± 5190.938985653123"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 7180.1156056722,
            "unit": "ns",
            "range": "± 46.6503221306311"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 70752.22054036458,
            "unit": "ns",
            "range": "± 411.8777125219756"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 696164.419921875,
            "unit": "ns",
            "range": "± 761.8860146606445"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 5283.990244547526,
            "unit": "ns",
            "range": "± 12.365367550642686"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 53175.2461344401,
            "unit": "ns",
            "range": "± 199.37827985111693"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 521361.7272135417,
            "unit": "ns",
            "range": "± 5174.6443597536"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6231.586179097493,
            "unit": "ns",
            "range": "± 16.571681606203473"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 53545.013763427734,
            "unit": "ns",
            "range": "± 194.89006442698584"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 528556.6090494791,
            "unit": "ns",
            "range": "± 1282.42432438341"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3622.380657196045,
            "unit": "ns",
            "range": "± 34.171883643236434"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 4381.375277201335,
            "unit": "ns",
            "range": "± 25.670287541166086"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 35436.24631754557,
            "unit": "ns",
            "range": "± 129.83710644627376"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 36794.23262532552,
            "unit": "ns",
            "range": "± 11.115808228860788"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 392840.82340494794,
            "unit": "ns",
            "range": "± 1343.0398453226614"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 400551.54313151044,
            "unit": "ns",
            "range": "± 1214.9335594905497"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "210299580+Chris-Wolfgang@users.noreply.github.com",
            "name": "Chris Wolfgang",
            "username": "Chris-Wolfgang"
          },
          "committer": {
            "email": "noreply@github.com",
            "name": "GitHub",
            "username": "web-flow"
          },
          "distinct": true,
          "id": "4d31d22fbcbf36b7ea62a08c12f3727dba1535b5",
          "message": "Merge pull request #166 from Chris-Wolfgang/perf/benchmarks-twae\n\nperf: hold benchmarks to TreatWarningsAsErrors (#103)",
          "timestamp": "2026-06-22T22:18:23-04:00",
          "tree_id": "81b2eeb12ee0404621e03c7ce4ffe225d389951b",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/4d31d22fbcbf36b7ea62a08c12f3727dba1535b5"
        },
        "date": 1782181303706,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 5691.41539255778,
            "unit": "ns",
            "range": "± 76.58536609704"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 52145.4924621582,
            "unit": "ns",
            "range": "± 309.0959221019765"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 514698.8053385417,
            "unit": "ns",
            "range": "± 1578.5642402025023"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3735.9860496520996,
            "unit": "ns",
            "range": "± 25.473879108695602"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 33923.96708170573,
            "unit": "ns",
            "range": "± 146.16102617074003"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 417957.94091796875,
            "unit": "ns",
            "range": "± 5815.0365061133925"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6394.992090861003,
            "unit": "ns",
            "range": "± 32.64443761407519"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 63959.2246500651,
            "unit": "ns",
            "range": "± 268.5494886724445"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 632952.1516927084,
            "unit": "ns",
            "range": "± 4131.272213387186"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 4131.454653422038,
            "unit": "ns",
            "range": "± 20.000966093109664"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 40586.90797932943,
            "unit": "ns",
            "range": "± 13.375595737153242"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 411872.828125,
            "unit": "ns",
            "range": "± 6161.089996979725"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 5686.91007232666,
            "unit": "ns",
            "range": "± 11.60898851338992"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 49606.00221761068,
            "unit": "ns",
            "range": "± 106.75885171355378"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 482663.2932128906,
            "unit": "ns",
            "range": "± 1186.8697169869806"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 2671.9062321980796,
            "unit": "ns",
            "range": "± 4.928846497313077"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 3347.2633056640625,
            "unit": "ns",
            "range": "± 7.093277804461928"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 25425.026830037434,
            "unit": "ns",
            "range": "± 85.44715107427751"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 26994.9775390625,
            "unit": "ns",
            "range": "± 275.6766642152483"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 322574.4973958333,
            "unit": "ns",
            "range": "± 1047.0233753809764"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 320286.4345703125,
            "unit": "ns",
            "range": "± 384.52158208549105"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "210299580+Chris-Wolfgang@users.noreply.github.com",
            "name": "Chris Wolfgang",
            "username": "Chris-Wolfgang"
          },
          "committer": {
            "email": "noreply@github.com",
            "name": "GitHub",
            "username": "web-flow"
          },
          "distinct": true,
          "id": "130aa4b3dceff62b2dc50945b9e3a1f50544d592",
          "message": "Merge pull request #167 from Chris-Wolfgang/fix/single-stream-configureawait\n\nfix: ConfigureAwait(false) on JsonSingleStreamExtractor (#79 review finding)",
          "timestamp": "2026-06-23T17:42:44-04:00",
          "tree_id": "d2ab4a27bb6fed82160268a1882244bb9f7a17c4",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/130aa4b3dceff62b2dc50945b9e3a1f50544d592"
        },
        "date": 1782251168970,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 5677.731174468994,
            "unit": "ns",
            "range": "± 81.70864843375638"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 51036.57654825846,
            "unit": "ns",
            "range": "± 183.15293725435131"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 522006.1194661458,
            "unit": "ns",
            "range": "± 3530.0107537943745"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3690.14603805542,
            "unit": "ns",
            "range": "± 23.924222407706054"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 33866.88409423828,
            "unit": "ns",
            "range": "± 51.922924629337516"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 427844.42887369794,
            "unit": "ns",
            "range": "± 2352.121447456"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6551.632113138835,
            "unit": "ns",
            "range": "± 8.763399576834315"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 62743.11564127604,
            "unit": "ns",
            "range": "± 198.5860471394751"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 632200.953125,
            "unit": "ns",
            "range": "± 1684.1524255867093"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 4295.06510925293,
            "unit": "ns",
            "range": "± 31.280949222734122"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 41080.88157145182,
            "unit": "ns",
            "range": "± 197.46366152700594"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 425056.08805338544,
            "unit": "ns",
            "range": "± 5173.0043793507175"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 5777.618614196777,
            "unit": "ns",
            "range": "± 20.193324425249568"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 48890.11233520508,
            "unit": "ns",
            "range": "± 10.519486209597314"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 487977.22216796875,
            "unit": "ns",
            "range": "± 4485.909558571588"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 2734.834902445475,
            "unit": "ns",
            "range": "± 18.92050284413297"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 3549.091547648112,
            "unit": "ns",
            "range": "± 64.7335581094205"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 25668.01914469401,
            "unit": "ns",
            "range": "± 151.34183725307292"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 26869.66943359375,
            "unit": "ns",
            "range": "± 201.60756665612286"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 313745.3899739583,
            "unit": "ns",
            "range": "± 1410.3071063827422"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 317507.61572265625,
            "unit": "ns",
            "range": "± 354.89548913479746"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "210299580+Chris-Wolfgang@users.noreply.github.com",
            "name": "Chris Wolfgang",
            "username": "Chris-Wolfgang"
          },
          "committer": {
            "email": "noreply@github.com",
            "name": "GitHub",
            "username": "web-flow"
          },
          "distinct": true,
          "id": "24ada0f9670799aae0616718d413a7c541d003aa",
          "message": "Merge pull request #171 from Chris-Wolfgang/dependabot/nuget/dotnet-dependencies-ff2bc74646\n\nBump the dotnet-dependencies group with 6 updates",
          "timestamp": "2026-06-23T21:57:16-04:00",
          "tree_id": "741ed395cf4ddc6701882df2230f7e5a9a537469",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/24ada0f9670799aae0616718d413a7c541d003aa"
        },
        "date": 1782266443369,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 5872.8916664123535,
            "unit": "ns",
            "range": "± 61.46101497067767"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 52699.718882242836,
            "unit": "ns",
            "range": "± 499.029915749741"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 521742.4736328125,
            "unit": "ns",
            "range": "± 1646.3032766171918"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3951.782127380371,
            "unit": "ns",
            "range": "± 75.69632452589389"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 33974.39133707682,
            "unit": "ns",
            "range": "± 161.11947594272027"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 437562.18766276044,
            "unit": "ns",
            "range": "± 2783.3153637198234"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6441.790318806966,
            "unit": "ns",
            "range": "± 8.524941012872333"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 64345.880126953125,
            "unit": "ns",
            "range": "± 138.42485041396475"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 637572.6472981771,
            "unit": "ns",
            "range": "± 1479.125948623748"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 4332.24419148763,
            "unit": "ns",
            "range": "± 11.547621075400501"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 43388.992655436195,
            "unit": "ns",
            "range": "± 235.93231624970477"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 425334.92887369794,
            "unit": "ns",
            "range": "± 5337.628174640852"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 5805.734804789226,
            "unit": "ns",
            "range": "± 40.205816106148895"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 49739.6516011556,
            "unit": "ns",
            "range": "± 42.74003698933915"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 488131.6110026042,
            "unit": "ns",
            "range": "± 1388.9547258467073"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 2712.2511812845864,
            "unit": "ns",
            "range": "± 7.7946183025902345"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 3465.7344284057617,
            "unit": "ns",
            "range": "± 55.13121545934016"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 25777.651794433594,
            "unit": "ns",
            "range": "± 141.9697802384616"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 27060.496185302734,
            "unit": "ns",
            "range": "± 203.47450863823363"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 318662.02034505206,
            "unit": "ns",
            "range": "± 352.31511867177767"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 319427.23876953125,
            "unit": "ns",
            "range": "± 645.0538814574339"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "210299580+Chris-Wolfgang@users.noreply.github.com",
            "name": "Chris Wolfgang",
            "username": "Chris-Wolfgang"
          },
          "committer": {
            "email": "noreply@github.com",
            "name": "GitHub",
            "username": "web-flow"
          },
          "distinct": true,
          "id": "0867273c3627cd551fc3d63a5059e6e18a053f7e",
          "message": "Merge pull request #170 from Chris-Wolfgang/dependabot/github_actions/github-actions-2217aebe03\n\nbuild(deps): bump actions/checkout from 6.0.3 to 7.0.0 in the github-actions group",
          "timestamp": "2026-06-23T22:14:36-04:00",
          "tree_id": "b9357827e8d0330df731b21b6a2666f3a6876c36",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/0867273c3627cd551fc3d63a5059e6e18a053f7e"
        },
        "date": 1782267479817,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6386.832572937012,
            "unit": "ns",
            "range": "± 87.21131758365932"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 60927.129150390625,
            "unit": "ns",
            "range": "± 1695.4975454953633"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 584337.34765625,
            "unit": "ns",
            "range": "± 2776.8452173619667"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 4330.132840474446,
            "unit": "ns",
            "range": "± 12.345708448912724"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 40503.07767740885,
            "unit": "ns",
            "range": "± 288.89690703541953"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 458635.57014973956,
            "unit": "ns",
            "range": "± 5712.14764171744"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6804.533309936523,
            "unit": "ns",
            "range": "± 9.804436270078302"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 68068.67020670573,
            "unit": "ns",
            "range": "± 142.8063815678287"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 677956.4235026041,
            "unit": "ns",
            "range": "± 776.9647127987706"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 4317.455561319987,
            "unit": "ns",
            "range": "± 18.903670103262105"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 44316.971211751305,
            "unit": "ns",
            "range": "± 265.4949790790282"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 435632.24951171875,
            "unit": "ns",
            "range": "± 1226.3531639309956"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 5987.396179199219,
            "unit": "ns",
            "range": "± 12.215643733161691"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 49796.224365234375,
            "unit": "ns",
            "range": "± 45.28213133782708"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 489867.7805989583,
            "unit": "ns",
            "range": "± 1029.8054997195675"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 2989.5550168355308,
            "unit": "ns",
            "range": "± 4.988010795064654"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 3580.332352956136,
            "unit": "ns",
            "range": "± 12.63575781981861"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 26726.091110229492,
            "unit": "ns",
            "range": "± 51.84644596154554"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 29090.293935139973,
            "unit": "ns",
            "range": "± 240.41803958950672"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 292399.13720703125,
            "unit": "ns",
            "range": "± 796.6614575538125"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 300588.7099609375,
            "unit": "ns",
            "range": "± 3303.5213081672377"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "210299580+Chris-Wolfgang@users.noreply.github.com",
            "name": "Chris Wolfgang",
            "username": "Chris-Wolfgang"
          },
          "committer": {
            "email": "noreply@github.com",
            "name": "GitHub",
            "username": "web-flow"
          },
          "distinct": true,
          "id": "c98dc2e69d85b9845fc5eb6dbefa432e4f77c9c6",
          "message": "Merge pull request #173 from Chris-Wolfgang/chore/bump-testkit-0.9.0\n\nchore: bump Abstractions to 0.14.1 and TestKit/Xunit to 0.9.0",
          "timestamp": "2026-06-25T20:59:02-04:00",
          "tree_id": "5c6bb887379ff07b3c55f64bcdfbece112216da2",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/c98dc2e69d85b9845fc5eb6dbefa432e4f77c9c6"
        },
        "date": 1782435756796,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6415.965282440186,
            "unit": "ns",
            "range": "± 10.428959552804923"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 54703.95323689779,
            "unit": "ns",
            "range": "± 268.9626330983672"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 555924.3994140625,
            "unit": "ns",
            "range": "± 1762.380212910169"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3845.62979888916,
            "unit": "ns",
            "range": "± 41.343054877241975"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 34813.211181640625,
            "unit": "ns",
            "range": "± 155.20443465937686"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 434011.84977213544,
            "unit": "ns",
            "range": "± 7095.354088747045"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 7326.7447992960615,
            "unit": "ns",
            "range": "± 33.95905779182113"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 65727.49340820312,
            "unit": "ns",
            "range": "± 168.73633846009034"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 668420.5657552084,
            "unit": "ns",
            "range": "± 1201.6882765061416"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 4366.419342041016,
            "unit": "ns",
            "range": "± 116.73516723390149"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 41855.87664794922,
            "unit": "ns",
            "range": "± 52.53969251761922"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 403942.74609375,
            "unit": "ns",
            "range": "± 3019.095456626631"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6203.200350443522,
            "unit": "ns",
            "range": "± 15.232189608102324"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 52383.32200113932,
            "unit": "ns",
            "range": "± 96.42224804228037"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 505869.2389322917,
            "unit": "ns",
            "range": "± 623.1405736313048"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 2804.2427825927734,
            "unit": "ns",
            "range": "± 5.315809669605617"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 3568.0224634806314,
            "unit": "ns",
            "range": "± 18.91264024576447"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 26156.821685791016,
            "unit": "ns",
            "range": "± 28.160791472991413"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 27336.67837524414,
            "unit": "ns",
            "range": "± 411.7215028090543"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 320360.66617838544,
            "unit": "ns",
            "range": "± 1928.5972182033267"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 329211.57731119794,
            "unit": "ns",
            "range": "± 796.8200039269503"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "210299580+Chris-Wolfgang@users.noreply.github.com",
            "name": "Chris Wolfgang",
            "username": "Chris-Wolfgang"
          },
          "committer": {
            "email": "noreply@github.com",
            "name": "GitHub",
            "username": "web-flow"
          },
          "distinct": true,
          "id": "2a3ae6c986d9774c80637894a276ff78ad7e0513",
          "message": "Merge pull request #205 from Chris-Wolfgang/vNext\n\nrelease: Wolfgang.Etl.Json v0.3.0",
          "timestamp": "2026-07-13T22:10:03-04:00",
          "tree_id": "50580642a998e8efb8cc1d8d8b62dd946de26682",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/2a3ae6c986d9774c80637894a276ff78ad7e0513"
        },
        "date": 1783995205015,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6754.14000193278,
            "unit": "ns",
            "range": "± 34.97791750181697"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 64261.08955891927,
            "unit": "ns",
            "range": "± 194.96193854527766"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 635743.9889322916,
            "unit": "ns",
            "range": "± 2842.3091224001487"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 4882.501953125,
            "unit": "ns",
            "range": "± 26.347966090882714"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 45633.634033203125,
            "unit": "ns",
            "range": "± 944.1976991284131"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 514332.6022135417,
            "unit": "ns",
            "range": "± 3188.255041398628"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 8134.510594685872,
            "unit": "ns",
            "range": "± 6.703919647113287"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 76977.64383951823,
            "unit": "ns",
            "range": "± 113.85504434855284"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 763170.0859375,
            "unit": "ns",
            "range": "± 168.8774604420472"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 5965.459884643555,
            "unit": "ns",
            "range": "± 14.265214618906846"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 56096.45332845052,
            "unit": "ns",
            "range": "± 92.3166819338667"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 560456.6396484375,
            "unit": "ns",
            "range": "± 6006.206069502289"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6823.630839029948,
            "unit": "ns",
            "range": "± 5.557032011172616"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 54064.19665527344,
            "unit": "ns",
            "range": "± 201.25122284627844"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 559116.7194010416,
            "unit": "ns",
            "range": "± 703.6937480380002"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3692.887795766195,
            "unit": "ns",
            "range": "± 6.485602822164268"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 4458.1370366414385,
            "unit": "ns",
            "range": "± 42.09088792184197"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 34216.44553629557,
            "unit": "ns",
            "range": "± 190.65440012519952"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 35941.897399902344,
            "unit": "ns",
            "range": "± 113.78099706491227"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 391269.1940104167,
            "unit": "ns",
            "range": "± 581.6747977945669"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 409522.4765625,
            "unit": "ns",
            "range": "± 7367.680285992546"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "210299580+Chris-Wolfgang@users.noreply.github.com",
            "name": "Chris Wolfgang",
            "username": "Chris-Wolfgang"
          },
          "committer": {
            "email": "noreply@github.com",
            "name": "GitHub",
            "username": "web-flow"
          },
          "distinct": true,
          "id": "fa758242f98043df6f5405e5708e3cff0bce5ad8",
          "message": "Merge pull request #209 from Chris-Wolfgang/dependabot/github_actions/github-actions-cc5b2f080a\n\nchore(deps): bump the github-actions group with 5 updates",
          "timestamp": "2026-07-14T09:46:34-04:00",
          "tree_id": "c48bd8af1c883e470a8a42e144d7dcb41fcfd8b0",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/fa758242f98043df6f5405e5708e3cff0bce5ad8"
        },
        "date": 1784036987130,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 4935.493798573812,
            "unit": "ns",
            "range": "± 24.256047785676255"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 44057.357889811195,
            "unit": "ns",
            "range": "± 797.2511292205837"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 434528.9671223958,
            "unit": "ns",
            "range": "± 2070.3352303426645"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3303.4160397847495,
            "unit": "ns",
            "range": "± 33.25141737029057"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 28938.13853963216,
            "unit": "ns",
            "range": "± 113.00617304078321"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 375053.625,
            "unit": "ns",
            "range": "± 11359.67341752496"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 5953.583521525065,
            "unit": "ns",
            "range": "± 7.852745524124107"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 57127.55836995443,
            "unit": "ns",
            "range": "± 91.93605636176508"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 545593.814453125,
            "unit": "ns",
            "range": "± 830.8441037608336"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3633.0839347839355,
            "unit": "ns",
            "range": "± 4.834812875713706"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 35264.16872151693,
            "unit": "ns",
            "range": "± 55.91844954569875"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 350984.38785807294,
            "unit": "ns",
            "range": "± 2437.6490769083775"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 4681.30099995931,
            "unit": "ns",
            "range": "± 7.749838578757652"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 39135.64031982422,
            "unit": "ns",
            "range": "± 65.34815168232836"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 374989.9820963542,
            "unit": "ns",
            "range": "± 993.4694022200637"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 2275.5032272338867,
            "unit": "ns",
            "range": "± 13.153968951173729"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 2841.018486022949,
            "unit": "ns",
            "range": "± 23.171814856569235"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 21278.190887451172,
            "unit": "ns",
            "range": "± 44.36474172977583"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 22317.638061523438,
            "unit": "ns",
            "range": "± 177.21193771152718"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 258885.04931640625,
            "unit": "ns",
            "range": "± 1500.6646389317957"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 259523.0810546875,
            "unit": "ns",
            "range": "± 945.2725899582873"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "210299580+Chris-Wolfgang@users.noreply.github.com",
            "name": "Chris Wolfgang",
            "username": "Chris-Wolfgang"
          },
          "committer": {
            "email": "noreply@github.com",
            "name": "GitHub",
            "username": "web-flow"
          },
          "distinct": true,
          "id": "f61d84778708dd96440e09a44e2cd82628b2f47f",
          "message": "Merge pull request #214 from Chris-Wolfgang/maint/doc-example-rot\n\nmaint: add Tests.Docs project for XML-doc example rot detection",
          "timestamp": "2026-07-15T16:08:29-04:00",
          "tree_id": "57f99031aca666473b1088a416cfb67a641038e3",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/f61d84778708dd96440e09a44e2cd82628b2f47f"
        },
        "date": 1784146320937,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6012.4544105529785,
            "unit": "ns",
            "range": "± 28.744125936260385"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 53794.310038248695,
            "unit": "ns",
            "range": "± 464.45966266150884"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 549313.8037109375,
            "unit": "ns",
            "range": "± 2569.6268901260373"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3978.556053161621,
            "unit": "ns",
            "range": "± 89.49505310493603"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 34861.622395833336,
            "unit": "ns",
            "range": "± 42.541467723347594"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 420010.37109375,
            "unit": "ns",
            "range": "± 2656.3808461143294"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 7428.097119649251,
            "unit": "ns",
            "range": "± 37.84731320683444"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 72970.06064860027,
            "unit": "ns",
            "range": "± 1338.9340278036675"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 712548.9446614584,
            "unit": "ns",
            "range": "± 1516.4690456059013"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 4497.202891031901,
            "unit": "ns",
            "range": "± 15.395670116143897"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 44641.992024739586,
            "unit": "ns",
            "range": "± 558.6389176813403"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 439906.23779296875,
            "unit": "ns",
            "range": "± 4422.427329505181"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6113.540918986003,
            "unit": "ns",
            "range": "± 27.043852718363205"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 52225.99271647135,
            "unit": "ns",
            "range": "± 142.74401403290022"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 500422.0315755208,
            "unit": "ns",
            "range": "± 1226.8760771042068"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 2763.195302327474,
            "unit": "ns",
            "range": "± 6.4807101959829145"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 3550.8773663838706,
            "unit": "ns",
            "range": "± 23.579806803163073"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 26100.341888427734,
            "unit": "ns",
            "range": "± 264.96684946974176"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 26907.187428792316,
            "unit": "ns",
            "range": "± 185.28706432452728"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 313672.4759114583,
            "unit": "ns",
            "range": "± 1085.5713027333134"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 319849.8349609375,
            "unit": "ns",
            "range": "± 736.7854174204736"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "210299580+Chris-Wolfgang@users.noreply.github.com",
            "name": "Chris Wolfgang",
            "username": "Chris-Wolfgang"
          },
          "committer": {
            "email": "noreply@github.com",
            "name": "GitHub",
            "username": "web-flow"
          },
          "distinct": true,
          "id": "7af37670414544961ccdcc9aa2c0610df082b73e",
          "message": "Merge pull request #231 from Chris-Wolfgang/vNext\n\nRelease v0.4.0",
          "timestamp": "2026-07-17T11:59:41-04:00",
          "tree_id": "35ec1ff6b7c1d8d344ba699dc9d248e824bb8b62",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/7af37670414544961ccdcc9aa2c0610df082b73e"
        },
        "date": 1784304181877,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6336.470919291179,
            "unit": "ns",
            "range": "± 8.596734745732462"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 62104.07377115885,
            "unit": "ns",
            "range": "± 366.79793548557296"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 617477.1266276041,
            "unit": "ns",
            "range": "± 5191.049511248645"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 4340.465443929036,
            "unit": "ns",
            "range": "± 5.9841405868389606"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 40266.071716308594,
            "unit": "ns",
            "range": "± 86.60611019178295"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 461222.71110026044,
            "unit": "ns",
            "range": "± 4367.227420935342"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 8460.159907023111,
            "unit": "ns",
            "range": "± 20.489107306343247"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 87068.79899088542,
            "unit": "ns",
            "range": "± 161.79525318283058"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 769373.9173177084,
            "unit": "ns",
            "range": "± 993.284826214621"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 5921.794860839844,
            "unit": "ns",
            "range": "± 11.952457989556596"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 56447.375996907555,
            "unit": "ns",
            "range": "± 148.21754755429683"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 574860.5791015625,
            "unit": "ns",
            "range": "± 1820.898146225782"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6704.00468190511,
            "unit": "ns",
            "range": "± 47.01290224759747"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 54908.781717936195,
            "unit": "ns",
            "range": "± 113.70028681817007"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 531058.7965494791,
            "unit": "ns",
            "range": "± 90.26401137501686"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3595.311080932617,
            "unit": "ns",
            "range": "± 5.504834324363925"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 4332.143109639485,
            "unit": "ns",
            "range": "± 13.802012442008305"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 33878.15401204427,
            "unit": "ns",
            "range": "± 416.55351472928555"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 34822.26430257162,
            "unit": "ns",
            "range": "± 317.1212048139574"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 395850.2685546875,
            "unit": "ns",
            "range": "± 1977.6341621826127"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 388781.814453125,
            "unit": "ns",
            "range": "± 1254.1924945386631"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "210299580+Chris-Wolfgang@users.noreply.github.com",
            "name": "Chris Wolfgang",
            "username": "Chris-Wolfgang"
          },
          "committer": {
            "email": "noreply@github.com",
            "name": "GitHub",
            "username": "web-flow"
          },
          "distinct": true,
          "id": "e9eb80f08b75379374ba473cedad81b0ba6259cb",
          "message": "Merge pull request #246 from Chris-Wolfgang/dependabot/github_actions/github-actions-a17fceba2e\n\nbuild(deps): bump the github-actions group with 7 updates",
          "timestamp": "2026-07-21T15:28:55-04:00",
          "tree_id": "79eae4240faca37a2ef88ba9aa5fac94f109dc50",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/e9eb80f08b75379374ba473cedad81b0ba6259cb"
        },
        "date": 1784662338999,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6559.038756052653,
            "unit": "ns",
            "range": "± 8.776432224798302"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 63716.65580240885,
            "unit": "ns",
            "range": "± 1759.966270937326"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 606773.4459635416,
            "unit": "ns",
            "range": "± 782.5183909539148"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 4561.21203104655,
            "unit": "ns",
            "range": "± 2.435503741579841"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 46603.52561442057,
            "unit": "ns",
            "range": "± 599.6711475728767"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 498680.6702473958,
            "unit": "ns",
            "range": "± 1356.7159783643406"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 8076.825576782227,
            "unit": "ns",
            "range": "± 58.56053062946883"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 77707.13730875652,
            "unit": "ns",
            "range": "± 242.5577019632864"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 808860.8077799479,
            "unit": "ns",
            "range": "± 1462.768511564016"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 5868.009831746419,
            "unit": "ns",
            "range": "± 29.956540997024803"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 57281.787109375,
            "unit": "ns",
            "range": "± 510.7249627580749"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 552577.3284505209,
            "unit": "ns",
            "range": "± 1910.9043955935504"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 7189.328066507976,
            "unit": "ns",
            "range": "± 10.370002291000079"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 53843.81403605143,
            "unit": "ns",
            "range": "± 100.24371946783052"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 528999.3955078125,
            "unit": "ns",
            "range": "± 1382.5131725587396"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3571.833023071289,
            "unit": "ns",
            "range": "± 5.238319574864387"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 4428.484776814778,
            "unit": "ns",
            "range": "± 19.10321032160441"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 34576.216796875,
            "unit": "ns",
            "range": "± 103.47556711103718"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 36153.9765218099,
            "unit": "ns",
            "range": "± 96.96933156756218"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 383895.2109375,
            "unit": "ns",
            "range": "± 1185.6512768682524"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 395195.162109375,
            "unit": "ns",
            "range": "± 715.0728650290032"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "210299580+Chris-Wolfgang@users.noreply.github.com",
            "name": "Chris Wolfgang",
            "username": "Chris-Wolfgang"
          },
          "committer": {
            "email": "noreply@github.com",
            "name": "GitHub",
            "username": "web-flow"
          },
          "distinct": true,
          "id": "ca00ddb9a022661acf8e87b576b91c440cf4587e",
          "message": "Merge pull request #241 from Chris-Wolfgang/vNext\n\nrelease: v0.5.0",
          "timestamp": "2026-07-21T22:02:21-04:00",
          "tree_id": "c164e621495d234fc479b12e77c81e968d801282",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/ca00ddb9a022661acf8e87b576b91c440cf4587e"
        },
        "date": 1784685934560,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 5116.353004455566,
            "unit": "ns",
            "range": "± 42.84013623717502"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 47812.53554280599,
            "unit": "ns",
            "range": "± 169.13795920161346"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 457825.31070963544,
            "unit": "ns",
            "range": "± 1347.5515490000068"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 2723.6711616516113,
            "unit": "ns",
            "range": "± 64.96881915655479"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 24077.72265625,
            "unit": "ns",
            "range": "± 550.4280429247899"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 281697.9781901042,
            "unit": "ns",
            "range": "± 1485.7912581362627"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6025.049057006836,
            "unit": "ns",
            "range": "± 114.3102630615606"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 55940.5469156901,
            "unit": "ns",
            "range": "± 145.83916362944277"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 545947.0504557291,
            "unit": "ns",
            "range": "± 2179.0409349052156"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3450.4125226338706,
            "unit": "ns",
            "range": "± 36.866589745548715"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 33838.072428385414,
            "unit": "ns",
            "range": "± 165.47237951424725"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 330082.39501953125,
            "unit": "ns",
            "range": "± 1812.9559085506557"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 4809.62033589681,
            "unit": "ns",
            "range": "± 17.2852756384831"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 38043.43048095703,
            "unit": "ns",
            "range": "± 377.978251002985"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 368403.30387369794,
            "unit": "ns",
            "range": "± 752.9947689174116"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 2267.417325337728,
            "unit": "ns",
            "range": "± 2.5164075099703918"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 2716.0391743977866,
            "unit": "ns",
            "range": "± 18.501878863780146"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 18708.818135579426,
            "unit": "ns",
            "range": "± 68.00603731930008"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 19825.83447265625,
            "unit": "ns",
            "range": "± 327.34685141148026"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 211131.10441080728,
            "unit": "ns",
            "range": "± 3434.1691133560817"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 213960.15633138022,
            "unit": "ns",
            "range": "± 4339.190067182707"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "210299580+Chris-Wolfgang@users.noreply.github.com",
            "name": "Chris Wolfgang",
            "username": "Chris-Wolfgang"
          },
          "committer": {
            "email": "noreply@github.com",
            "name": "GitHub",
            "username": "web-flow"
          },
          "distinct": true,
          "id": "b59dfbdd76fca71cd586e4401e9973e57e0f9790",
          "message": "Merge pull request #259 from Chris-Wolfgang/vNext\n\nRelease 0.6.0",
          "timestamp": "2026-08-09T21:59:15-04:00",
          "tree_id": "8adfe3078a65f17f182030de779e916d34371810",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/b59dfbdd76fca71cd586e4401e9973e57e0f9790"
        },
        "date": 1786327362594,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6309.195486704509,
            "unit": "ns",
            "range": "± 5.9725539218716825"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 55681.80537923177,
            "unit": "ns",
            "range": "± 288.81207279294625"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 556705.6155598959,
            "unit": "ns",
            "range": "± 707.5320068212002"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3341.422908782959,
            "unit": "ns",
            "range": "± 10.382442819057264"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 31967.975036621094,
            "unit": "ns",
            "range": "± 282.8963282941188"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 393178.0559895833,
            "unit": "ns",
            "range": "± 1031.690277725182"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 7819.052627563477,
            "unit": "ns",
            "range": "± 45.36563969590843"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 74395.17297363281,
            "unit": "ns",
            "range": "± 212.3357214775167"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 714276.1248372396,
            "unit": "ns",
            "range": "± 916.7710139102163"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 4594.359504699707,
            "unit": "ns",
            "range": "± 31.299036881365115"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 43963.53535970052,
            "unit": "ns",
            "range": "± 77.48561629791135"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 430454.6432291667,
            "unit": "ns",
            "range": "± 1565.391890545293"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6253.8933664957685,
            "unit": "ns",
            "range": "± 22.149779362417235"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 51353.627909342445,
            "unit": "ns",
            "range": "± 34.0396701253292"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 505426.5732421875,
            "unit": "ns",
            "range": "± 653.3981166623299"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3015.1382586161294,
            "unit": "ns",
            "range": "± 2.513223896856437"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 3598.6839853922525,
            "unit": "ns",
            "range": "± 27.37160967961516"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 26134.519795735676,
            "unit": "ns",
            "range": "± 73.16990640539399"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 27142.430165608723,
            "unit": "ns",
            "range": "± 29.66782597862076"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 319915.92236328125,
            "unit": "ns",
            "range": "± 1313.86384170916"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 327145.24658203125,
            "unit": "ns",
            "range": "± 2090.473254999846"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "210299580+Chris-Wolfgang@users.noreply.github.com",
            "name": "Chris Wolfgang",
            "username": "Chris-Wolfgang"
          },
          "committer": {
            "email": "noreply@github.com",
            "name": "GitHub",
            "username": "web-flow"
          },
          "distinct": true,
          "id": "0a830023714b7b9f9c6b09cc8b3e5669a4321e19",
          "message": "Merge pull request #262 from Chris-Wolfgang/chore/post-0.6.0-baseline\n\nAdvance PackageValidation baseline to 0.6.0 (post-release)",
          "timestamp": "2026-08-10T21:11:26-04:00",
          "tree_id": "92b848e5be6a0fa21f44b6e58469121d49292b98",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/0a830023714b7b9f9c6b09cc8b3e5669a4321e19"
        },
        "date": 1786410892102,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 7020.060106913249,
            "unit": "ns",
            "range": "± 14.964971058323275"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 62212.42850748698,
            "unit": "ns",
            "range": "± 670.8426813755837"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 653254.3863932291,
            "unit": "ns",
            "range": "± 3512.44052581045"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 4664.323565165202,
            "unit": "ns",
            "range": "± 10.578050731387094"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 42165.65114339193,
            "unit": "ns",
            "range": "± 165.86327799846865"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 477544.64860026044,
            "unit": "ns",
            "range": "± 1448.2940256470654"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 8696.526972452799,
            "unit": "ns",
            "range": "± 22.285583311935817"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 80439.26546223958,
            "unit": "ns",
            "range": "± 188.0803836182496"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 807770.6328125,
            "unit": "ns",
            "range": "± 6613.69859689691"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 6022.850738525391,
            "unit": "ns",
            "range": "± 20.518765562774508"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 56369.211446126305,
            "unit": "ns",
            "range": "± 582.7591611396989"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 609191.3792317709,
            "unit": "ns",
            "range": "± 2434.8712725951978"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 7104.125086466472,
            "unit": "ns",
            "range": "± 13.275911595682864"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 54949.43438720703,
            "unit": "ns",
            "range": "± 201.17499103420758"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 558883.9576822916,
            "unit": "ns",
            "range": "± 885.1963409908501"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3789.9650815327964,
            "unit": "ns",
            "range": "± 14.011705531723548"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 4538.089988708496,
            "unit": "ns",
            "range": "± 54.473171451094174"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 36867.60575358073,
            "unit": "ns",
            "range": "± 226.62568326718537"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 37315.23384602865,
            "unit": "ns",
            "range": "± 268.32666603196327"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 402194.751953125,
            "unit": "ns",
            "range": "± 952.2256765630403"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 392630.0830078125,
            "unit": "ns",
            "range": "± 552.4175604546969"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "210299580+Chris-Wolfgang@users.noreply.github.com",
            "name": "Chris Wolfgang",
            "username": "Chris-Wolfgang"
          },
          "committer": {
            "email": "noreply@github.com",
            "name": "GitHub",
            "username": "web-flow"
          },
          "distinct": true,
          "id": "a3f4e4440415432ee187fb772778abc633abb227",
          "message": "Merge pull request #270 from Chris-Wolfgang/vNext\n\nRelease 0.7.0",
          "timestamp": "2026-08-13T13:18:29-04:00",
          "tree_id": "9cd980b4cd4b4dbc4abbcaf3d08b26e8a040dc78",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/a3f4e4440415432ee187fb772778abc633abb227"
        },
        "date": 1786641712103,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6514.415390014648,
            "unit": "ns",
            "range": "± 24.86204978271456"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 56795.96059163412,
            "unit": "ns",
            "range": "± 108.37712284463083"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 569865.6165364584,
            "unit": "ns",
            "range": "± 4427.040097002088"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3383.3108406066895,
            "unit": "ns",
            "range": "± 15.141735331801762"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 32908.19392903646,
            "unit": "ns",
            "range": "± 247.35272990568367"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 399667.36116536456,
            "unit": "ns",
            "range": "± 2112.5593640224884"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 7651.740381876628,
            "unit": "ns",
            "range": "± 19.334250973215283"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 75680.29833984375,
            "unit": "ns",
            "range": "± 1107.6051778368394"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 749634.3776041666,
            "unit": "ns",
            "range": "± 584.2672077042206"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 4728.332557678223,
            "unit": "ns",
            "range": "± 36.02658785783552"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 44694.964680989586,
            "unit": "ns",
            "range": "± 782.6534922910914"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 438682.5348307292,
            "unit": "ns",
            "range": "± 3184.653276139034"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6361.425687154134,
            "unit": "ns",
            "range": "± 39.188076109647156"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 51783.14768473307,
            "unit": "ns",
            "range": "± 68.6849661586305"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 509914.0833333333,
            "unit": "ns",
            "range": "± 860.8604966283236"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 2989.4543266296387,
            "unit": "ns",
            "range": "± 5.4783861201920185"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 3746.9749743143716,
            "unit": "ns",
            "range": "± 33.71764573059521"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 27844.709503173828,
            "unit": "ns",
            "range": "± 299.1680973354577"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 27606.783528645832,
            "unit": "ns",
            "range": "± 281.2882058973089"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 330219.4641927083,
            "unit": "ns",
            "range": "± 3324.6424096201767"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 325542.07210286456,
            "unit": "ns",
            "range": "± 656.9151348221326"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "210299580+Chris-Wolfgang@users.noreply.github.com",
            "name": "Chris Wolfgang",
            "username": "Chris-Wolfgang"
          },
          "committer": {
            "email": "noreply@github.com",
            "name": "GitHub",
            "username": "web-flow"
          },
          "distinct": true,
          "id": "96bc9d168a2f18c1d68cdccf5763e7f4889bd328",
          "message": "Merge pull request #271 from Chris-Wolfgang/maint/security-267-analyzer-gate\n\nGate PublicApiAnalyzers on Exists('PublicAPI.Shipped.txt'); top up 0.7.0 tracking (refs #267, #254)",
          "timestamp": "2026-08-13T21:19:19-04:00",
          "tree_id": "5e5c131be16878292f2ab580651de401d26fb43c",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/96bc9d168a2f18c1d68cdccf5763e7f4889bd328"
        },
        "date": 1786670562001,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 7180.287874857585,
            "unit": "ns",
            "range": "± 183.89911755043536"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 65005.116536458336,
            "unit": "ns",
            "range": "± 2424.7497084583147"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 620420.2939453125,
            "unit": "ns",
            "range": "± 1068.0637886079323"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 4442.289288838704,
            "unit": "ns",
            "range": "± 7.754695996102622"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 42738.841288248695,
            "unit": "ns",
            "range": "± 329.3831033132327"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 472705.8492838542,
            "unit": "ns",
            "range": "± 1154.0528585949348"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 8397.630030314127,
            "unit": "ns",
            "range": "± 112.09054206335115"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 77372.72005208333,
            "unit": "ns",
            "range": "± 120.17977858701153"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 794595.3209635416,
            "unit": "ns",
            "range": "± 721.5752177207396"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 6081.242469787598,
            "unit": "ns",
            "range": "± 19.633292994089654"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 56426.45644124349,
            "unit": "ns",
            "range": "± 296.65412755667074"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 558722.5989583334,
            "unit": "ns",
            "range": "± 4427.185032642386"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6867.8746999104815,
            "unit": "ns",
            "range": "± 14.76335633208483"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 56070.744049072266,
            "unit": "ns",
            "range": "± 123.38683315365331"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 548529.8395182291,
            "unit": "ns",
            "range": "± 1437.3885636105892"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3830.1901257832847,
            "unit": "ns",
            "range": "± 52.567889819360495"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 4595.350949605306,
            "unit": "ns",
            "range": "± 7.2515272280176015"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 35619.709533691406,
            "unit": "ns",
            "range": "± 406.27839729520423"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 36123.33559163412,
            "unit": "ns",
            "range": "± 464.38223774107183"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 390984.70328776044,
            "unit": "ns",
            "range": "± 1194.7970926072232"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 400831.2431640625,
            "unit": "ns",
            "range": "± 640.4368178577203"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "210299580+Chris-Wolfgang@users.noreply.github.com",
            "name": "Chris Wolfgang",
            "username": "Chris-Wolfgang"
          },
          "committer": {
            "email": "noreply@github.com",
            "name": "GitHub",
            "username": "web-flow"
          },
          "distinct": true,
          "id": "dee8e61deb5375997623f7bf3653eec47a7aec15",
          "message": "Merge pull request #275 from Chris-Wolfgang/chore/baseline-0.7.0\n\nchore(release): advance PackageValidation baseline to 0.7.0",
          "timestamp": "2026-08-14T14:36:40-04:00",
          "tree_id": "9abd15b4249d763ab15dce4dca2355c735f00109",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/dee8e61deb5375997623f7bf3653eec47a7aec15"
        },
        "date": 1786732797740,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 7087.355211893718,
            "unit": "ns",
            "range": "± 30.34922337151968"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 63861.9364827474,
            "unit": "ns",
            "range": "± 1459.5987842384377"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 642600.5188802084,
            "unit": "ns",
            "range": "± 3432.624623761495"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 4512.091639200847,
            "unit": "ns",
            "range": "± 11.116204896775578"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 42396.892252604164,
            "unit": "ns",
            "range": "± 331.93018040420975"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 492675.0244140625,
            "unit": "ns",
            "range": "± 2847.273848999054"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 9107.289210001627,
            "unit": "ns",
            "range": "± 11.347366285808482"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 83380.82718912761,
            "unit": "ns",
            "range": "± 2001.3016533012453"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 770616.1637369791,
            "unit": "ns",
            "range": "± 1016.4818982119832"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 6077.804247538249,
            "unit": "ns",
            "range": "± 6.966873800987943"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 57730.42100016276,
            "unit": "ns",
            "range": "± 115.9472453164074"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 560022.3551432291,
            "unit": "ns",
            "range": "± 968.4796081696124"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 7085.095520019531,
            "unit": "ns",
            "range": "± 19.419018982521695"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 57078.6894124349,
            "unit": "ns",
            "range": "± 158.99848944699457"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 539452.8662109375,
            "unit": "ns",
            "range": "± 4207.788700357098"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3812.1705271402993,
            "unit": "ns",
            "range": "± 7.613973476363983"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 4566.431063334147,
            "unit": "ns",
            "range": "± 20.70764388189614"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 35135.27872721354,
            "unit": "ns",
            "range": "± 156.18543834431986"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 36479.422912597656,
            "unit": "ns",
            "range": "± 222.211424903094"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 392777.0319010417,
            "unit": "ns",
            "range": "± 3204.7561143110456"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 403160.29378255206,
            "unit": "ns",
            "range": "± 1778.15486853115"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "210299580+Chris-Wolfgang@users.noreply.github.com",
            "name": "Chris Wolfgang",
            "username": "Chris-Wolfgang"
          },
          "committer": {
            "email": "noreply@github.com",
            "name": "GitHub",
            "username": "web-flow"
          },
          "distinct": true,
          "id": "a48d89cf2140a1184ace0f63a8cad09ea259f774",
          "message": "Merge pull request #278 from Chris-Wolfgang/chore/bump-etl-abstractions-0.23.1\n\nchore(deps): bump Wolfgang.Etl.* NuGet packages to 0.23.1",
          "timestamp": "2026-08-17T21:27:29-04:00",
          "tree_id": "25e355e6f5eca8cc70707081dcb71e3a31dc325f",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/a48d89cf2140a1184ace0f63a8cad09ea259f774"
        },
        "date": 1787016643866,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 3649.8409729003906,
            "unit": "ns",
            "range": "± 65.8796719756763"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 33852.525553385414,
            "unit": "ns",
            "range": "± 752.1790622216485"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 336944.31298828125,
            "unit": "ns",
            "range": "± 2217.0684648597066"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 1943.7272466023762,
            "unit": "ns",
            "range": "± 21.961393468589872"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 18308.777577718098,
            "unit": "ns",
            "range": "± 360.01069437937724"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 211212.94262695312,
            "unit": "ns",
            "range": "± 541.5966348624614"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 4270.29495493571,
            "unit": "ns",
            "range": "± 180.96761944037658"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 41036.29052734375,
            "unit": "ns",
            "range": "± 2130.952550830032"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 377796.41520182294,
            "unit": "ns",
            "range": "± 1497.0627400577453"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 2474.194704691569,
            "unit": "ns",
            "range": "± 31.83693991369236"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 23037.420552571613,
            "unit": "ns",
            "range": "± 176.9269404090733"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 239664.09509277344,
            "unit": "ns",
            "range": "± 12159.329057731044"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 3928.486567179362,
            "unit": "ns",
            "range": "± 69.79764629699224"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 27535.008331298828,
            "unit": "ns",
            "range": "± 406.99140874194876"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 263740.9202473958,
            "unit": "ns",
            "range": "± 9057.858139003078"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 1651.1934630076091,
            "unit": "ns",
            "range": "± 74.87578469622659"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 2006.2678680419922,
            "unit": "ns",
            "range": "± 41.1527493182882"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 13970.45192972819,
            "unit": "ns",
            "range": "± 202.8689304852258"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 14528.207499186197,
            "unit": "ns",
            "range": "± 155.625686962006"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 178479.06770833334,
            "unit": "ns",
            "range": "± 2846.60955217241"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 182624.73347981772,
            "unit": "ns",
            "range": "± 1523.632727835143"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "210299580+Chris-Wolfgang@users.noreply.github.com",
            "name": "Chris Wolfgang",
            "username": "Chris-Wolfgang"
          },
          "committer": {
            "email": "noreply@github.com",
            "name": "GitHub",
            "username": "web-flow"
          },
          "distinct": true,
          "id": "d6d0242dca08c0cab45da8928ec4dffb3bfc71ab",
          "message": "Merge pull request #283 from Chris-Wolfgang/vNext\n\nMerge vNext → main for v0.8.0 release",
          "timestamp": "2026-08-18T21:56:31-04:00",
          "tree_id": "57cb656afac3e39bbae31b91fc7da269117e36a3",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/d6d0242dca08c0cab45da8928ec4dffb3bfc71ab"
        },
        "date": 1787104794953,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6632.589612325032,
            "unit": "ns",
            "range": "± 86.19631562822407"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 64024.01395670573,
            "unit": "ns",
            "range": "± 1983.0728086270967"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 619424.3258463541,
            "unit": "ns",
            "range": "± 12836.651672944488"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 4279.0950113932295,
            "unit": "ns",
            "range": "± 6.032513963872625"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 41461.367207845055,
            "unit": "ns",
            "range": "± 94.01177211993144"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 473179.71370442706,
            "unit": "ns",
            "range": "± 6528.659645191693"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 8348.828297932943,
            "unit": "ns",
            "range": "± 45.10658053093411"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 79237.61568196614,
            "unit": "ns",
            "range": "± 1287.861832386442"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 771602.6988932291,
            "unit": "ns",
            "range": "± 2790.707265118887"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 5886.943133036296,
            "unit": "ns",
            "range": "± 8.345534710140923"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 55172.3896484375,
            "unit": "ns",
            "range": "± 205.1848880824967"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 552859.3391927084,
            "unit": "ns",
            "range": "± 313.8827415255002"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6942.252978006999,
            "unit": "ns",
            "range": "± 18.419667052973914"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 59197.42049153646,
            "unit": "ns",
            "range": "± 50.988410564556624"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 542669.3108723959,
            "unit": "ns",
            "range": "± 1939.9635460713212"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3810.6233838399253,
            "unit": "ns",
            "range": "± 5.923893571403537"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 4300.904416402181,
            "unit": "ns",
            "range": "± 5.1220278540282775"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 33814.67130533854,
            "unit": "ns",
            "range": "± 158.5200571053519"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 35147.24595133463,
            "unit": "ns",
            "range": "± 290.2004357920914"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 384647.05517578125,
            "unit": "ns",
            "range": "± 655.260451409226"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 395455.10367838544,
            "unit": "ns",
            "range": "± 5928.934350650386"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "210299580+Chris-Wolfgang@users.noreply.github.com",
            "name": "Chris Wolfgang",
            "username": "Chris-Wolfgang"
          },
          "committer": {
            "email": "noreply@github.com",
            "name": "GitHub",
            "username": "web-flow"
          },
          "distinct": true,
          "id": "f3593488fb56daad1ae6b519a9b90b06745f8535",
          "message": "Merge pull request #285 from Chris-Wolfgang/chore/baseline-0.8.0\n\nchore(release): advance PackageValidation baseline to 0.8.0",
          "timestamp": "2026-08-19T07:57:23-04:00",
          "tree_id": "37bf0e3cedb75acbabd54029cf9ad2da57a9aeff",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/f3593488fb56daad1ae6b519a9b90b06745f8535"
        },
        "date": 1787140843447,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 7097.227663675944,
            "unit": "ns",
            "range": "± 62.31018520033135"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 64866.59395345052,
            "unit": "ns",
            "range": "± 2265.9405496633367"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 677620.6129557291,
            "unit": "ns",
            "range": "± 3950.4519754515213"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 4587.526725769043,
            "unit": "ns",
            "range": "± 23.358646746734323"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 42386.995178222656,
            "unit": "ns",
            "range": "± 209.1523928881155"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 504241.5686848958,
            "unit": "ns",
            "range": "± 9438.17298935042"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 8629.149007161459,
            "unit": "ns",
            "range": "± 34.410335013718246"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 78473.44315592448,
            "unit": "ns",
            "range": "± 253.90025379866003"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 897951.7542317709,
            "unit": "ns",
            "range": "± 1808.4672108772236"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 6166.475217183431,
            "unit": "ns",
            "range": "± 15.735576912561708"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 57384.840311686195,
            "unit": "ns",
            "range": "± 434.27657262384247"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 585133.0022786459,
            "unit": "ns",
            "range": "± 3336.9219728335456"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 7350.680955251058,
            "unit": "ns",
            "range": "± 17.985113575907004"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 58049.28126017252,
            "unit": "ns",
            "range": "± 240.4997666156567"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 548523.7737630209,
            "unit": "ns",
            "range": "± 2050.495170887638"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3886.038833618164,
            "unit": "ns",
            "range": "± 6.2837405487651194"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 4563.303438822429,
            "unit": "ns",
            "range": "± 26.67021332354023"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 35913.83119710287,
            "unit": "ns",
            "range": "± 100.77283366428395"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 36264.473693847656,
            "unit": "ns",
            "range": "± 126.58157705060124"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 398056.4850260417,
            "unit": "ns",
            "range": "± 2501.420014395917"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 399593.0188802083,
            "unit": "ns",
            "range": "± 1484.5753498196862"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "210299580+Chris-Wolfgang@users.noreply.github.com",
            "name": "Chris Wolfgang",
            "username": "Chris-Wolfgang"
          },
          "committer": {
            "email": "noreply@github.com",
            "name": "GitHub",
            "username": "web-flow"
          },
          "distinct": true,
          "id": "42e13239d48f2dbfdf17f10c4565d78a7e00d990",
          "message": "Merge pull request #286 from Chris-Wolfgang/maint/code-scanning-followups\n\nFix actionable code-scanning findings",
          "timestamp": "2026-08-19T08:16:22-04:00",
          "tree_id": "ed9995684c7747b4c09a0d5f123474e7ac68d1a2",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/42e13239d48f2dbfdf17f10c4565d78a7e00d990"
        },
        "date": 1787141964613,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 4460.167165120442,
            "unit": "ns",
            "range": "± 243.71539303581733"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 37816.01434326172,
            "unit": "ns",
            "range": "± 1559.3676023977039"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 368909.01123046875,
            "unit": "ns",
            "range": "± 2444.4780669701613"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 2134.1642557779946,
            "unit": "ns",
            "range": "± 11.720951830887074"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 19353.66958618164,
            "unit": "ns",
            "range": "± 118.41928981481476"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 231104.5431315104,
            "unit": "ns",
            "range": "± 1275.8688405719324"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 4590.8437093098955,
            "unit": "ns",
            "range": "± 12.317052046820432"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 45539.111979166664,
            "unit": "ns",
            "range": "± 2568.82918202856"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 448721.873046875,
            "unit": "ns",
            "range": "± 9454.084974859024"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 2764.116579691569,
            "unit": "ns",
            "range": "± 3.110986015019748"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 26960.96055094401,
            "unit": "ns",
            "range": "± 656.5645480038628"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 262244.64762369794,
            "unit": "ns",
            "range": "± 522.7147985768062"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 3850.989885965983,
            "unit": "ns",
            "range": "± 5.7669747302431515"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 30646.713236490887,
            "unit": "ns",
            "range": "± 41.57465199403351"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 338857.21077473956,
            "unit": "ns",
            "range": "± 10733.002904817396"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 1769.1234652201335,
            "unit": "ns",
            "range": "± 6.661203144540806"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 2230.3306477864585,
            "unit": "ns",
            "range": "± 23.168281129379256"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 15404.071940104166,
            "unit": "ns",
            "range": "± 46.70721750243732"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 16039.427530924479,
            "unit": "ns",
            "range": "± 97.41421891427106"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 211892.85701497397,
            "unit": "ns",
            "range": "± 1042.6062729891921"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 194197.01139322916,
            "unit": "ns",
            "range": "± 2603.194603580936"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "210299580+Chris-Wolfgang@users.noreply.github.com",
            "name": "Chris Wolfgang",
            "username": "Chris-Wolfgang"
          },
          "committer": {
            "email": "noreply@github.com",
            "name": "GitHub",
            "username": "web-flow"
          },
          "distinct": true,
          "id": "ac636bc540bfe3c90901d8edd6f5e5c62f8efcd8",
          "message": "Merge pull request #287 from Chris-Wolfgang/maint/inspectcode-followup\n\nCorrect the InspectCode follow-ups from #286",
          "timestamp": "2026-08-19T08:44:26-04:00",
          "tree_id": "d678508902581703a6c9d76b6e8dad95cad20425",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/ac636bc540bfe3c90901d8edd6f5e5c62f8efcd8"
        },
        "date": 1787143668285,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 7011.719596862793,
            "unit": "ns",
            "range": "± 80.31737349411316"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 61468.53181966146,
            "unit": "ns",
            "range": "± 489.0734543847484"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 639171.8502604166,
            "unit": "ns",
            "range": "± 5408.00621791526"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 4353.91402943929,
            "unit": "ns",
            "range": "± 12.421566020716574"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 40851.23181152344,
            "unit": "ns",
            "range": "± 174.2301133205993"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 492817.2122395833,
            "unit": "ns",
            "range": "± 4730.440312444998"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 8444.669235229492,
            "unit": "ns",
            "range": "± 84.16568460766864"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 79146.42073567708,
            "unit": "ns",
            "range": "± 184.52127100485083"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 860716.3180338541,
            "unit": "ns",
            "range": "± 3100.1515736954807"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 6039.183970133464,
            "unit": "ns",
            "range": "± 34.01019597393399"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 55730.70845540365,
            "unit": "ns",
            "range": "± 799.8145342933668"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 570687.2906901041,
            "unit": "ns",
            "range": "± 7170.128791209838"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6947.423080444336,
            "unit": "ns",
            "range": "± 30.706442391139763"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 55721.36971028646,
            "unit": "ns",
            "range": "± 245.8238392662931"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 545664.6904296875,
            "unit": "ns",
            "range": "± 2950.7279736397213"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3834.7090810139975,
            "unit": "ns",
            "range": "± 28.262141178044676"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 4708.0948486328125,
            "unit": "ns",
            "range": "± 127.60179570187391"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 35384.18526204427,
            "unit": "ns",
            "range": "± 284.0573449707847"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 35472.966959635414,
            "unit": "ns",
            "range": "± 63.819640587007584"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 392038.74462890625,
            "unit": "ns",
            "range": "± 2522.100934041897"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 398755.40283203125,
            "unit": "ns",
            "range": "± 665.122444856414"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "210299580+Chris-Wolfgang@users.noreply.github.com",
            "name": "Chris Wolfgang",
            "username": "Chris-Wolfgang"
          },
          "committer": {
            "email": "noreply@github.com",
            "name": "GitHub",
            "username": "web-flow"
          },
          "distinct": true,
          "id": "ac636bc540bfe3c90901d8edd6f5e5c62f8efcd8",
          "message": "Merge pull request #287 from Chris-Wolfgang/maint/inspectcode-followup\n\nCorrect the InspectCode follow-ups from #286",
          "timestamp": "2026-08-19T08:44:26-04:00",
          "tree_id": "d678508902581703a6c9d76b6e8dad95cad20425",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/ac636bc540bfe3c90901d8edd6f5e5c62f8efcd8"
        },
        "date": 1787143874189,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 7370.385513305664,
            "unit": "ns",
            "range": "± 44.76688330409532"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 67543.77689615886,
            "unit": "ns",
            "range": "± 1316.97509514524"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 641367.6380208334,
            "unit": "ns",
            "range": "± 1387.395719046232"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 4525.823654174805,
            "unit": "ns",
            "range": "± 35.998227151531005"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 43453.87731933594,
            "unit": "ns",
            "range": "± 127.92198745123682"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 494559.91796875,
            "unit": "ns",
            "range": "± 3746.2104464997788"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 8483.04946899414,
            "unit": "ns",
            "range": "± 43.81580258546508"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 79417.42403157552,
            "unit": "ns",
            "range": "± 229.927422186401"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 817627.9580078125,
            "unit": "ns",
            "range": "± 960.4463838389943"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 6267.130821228027,
            "unit": "ns",
            "range": "± 13.1311814491256"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 61061.51981608073,
            "unit": "ns",
            "range": "± 78.19567633036765"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 587291.3678385416,
            "unit": "ns",
            "range": "± 340.51909664610116"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 7052.052169799805,
            "unit": "ns",
            "range": "± 10.727669225546821"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 62348.09232584635,
            "unit": "ns",
            "range": "± 204.16179406085038"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 557029.8177083334,
            "unit": "ns",
            "range": "± 6600.979178301638"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3878.837432861328,
            "unit": "ns",
            "range": "± 2.054795508468879"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 4773.961115519206,
            "unit": "ns",
            "range": "± 22.967305465992247"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 36207.82826741537,
            "unit": "ns",
            "range": "± 31.558751622209748"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 36644.39367675781,
            "unit": "ns",
            "range": "± 671.5297703770394"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 415812.74853515625,
            "unit": "ns",
            "range": "± 3569.929707212696"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 413810.83219401044,
            "unit": "ns",
            "range": "± 4244.873682070821"
          }
        ]
      }
    ]
  }
}