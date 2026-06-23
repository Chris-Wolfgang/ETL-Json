window.BENCHMARK_DATA = {
  "lastUpdate": 1782181304775,
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
      }
    ]
  }
}