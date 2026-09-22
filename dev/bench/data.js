window.BENCHMARK_DATA = {
  "lastUpdate": 1790118279126,
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
          "id": "00c752d664c40b571bd400a3c3fecdfa6b13139a",
          "message": "Merge pull request #290 from Chris-Wolfgang/vNext\n\nRelease v0.8.1 — Scorecard SARIF filter + zizmor waiver (CI-only PATCH)",
          "timestamp": "2026-08-22T19:28:23-04:00",
          "tree_id": "826b4a0f329e524882c5163f9c2f30bf8618dfa7",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/00c752d664c40b571bd400a3c3fecdfa6b13139a"
        },
        "date": 1787441495391,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 7223.89993540446,
            "unit": "ns",
            "range": "± 35.2995459459204"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 65760.15270996094,
            "unit": "ns",
            "range": "± 477.26001651137733"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 626332.177734375,
            "unit": "ns",
            "range": "± 4772.756940392936"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 4528.038182576497,
            "unit": "ns",
            "range": "± 5.847656874720615"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 43057.62025960287,
            "unit": "ns",
            "range": "± 89.69902442828213"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 494873.8287760417,
            "unit": "ns",
            "range": "± 7122.611152756929"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 8391.857157389322,
            "unit": "ns",
            "range": "± 137.39348085189977"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 79126.54642740886,
            "unit": "ns",
            "range": "± 749.1466598197686"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 799117.0647786459,
            "unit": "ns",
            "range": "± 1684.2021612676176"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 6240.392524719238,
            "unit": "ns",
            "range": "± 17.283828375798493"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 58798.08345540365,
            "unit": "ns",
            "range": "± 142.02963736553323"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 597682.01953125,
            "unit": "ns",
            "range": "± 5512.162737393248"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 7263.08775583903,
            "unit": "ns",
            "range": "± 6.338158032127539"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 56199.732401529945,
            "unit": "ns",
            "range": "± 31.05004388749361"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 559596.8102213541,
            "unit": "ns",
            "range": "± 1845.9798304370167"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3843.5819956461587,
            "unit": "ns",
            "range": "± 4.094430564477354"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 4737.975456237793,
            "unit": "ns",
            "range": "± 13.783837744348153"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 35925.917948404945,
            "unit": "ns",
            "range": "± 119.00150706144211"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 36146.89717610677,
            "unit": "ns",
            "range": "± 223.53607443526636"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 394068.86995442706,
            "unit": "ns",
            "range": "± 1155.5538411887965"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 410800.85139973956,
            "unit": "ns",
            "range": "± 1961.3637455399423"
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
          "id": "753ab190e5ae81d6f121e69e7b3f00d883ca968c",
          "message": "Merge pull request #306 from Chris-Wolfgang/dependabot/nuget/benchmarks/Wolfgang.Etl.Json.Benchmarks/dotnet-dependencies-ea231d8f49\n\nBump Meziantou.Analyzer and 9 others",
          "timestamp": "2026-09-15T15:05:23-04:00",
          "tree_id": "a70725ac761fbdf723f6102c1a63fc9ece57bb40",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/753ab190e5ae81d6f121e69e7b3f00d883ca968c"
        },
        "date": 1789499329201,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 7330.247041066487,
            "unit": "ns",
            "range": "± 15.967980780205025"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 64270.6884358724,
            "unit": "ns",
            "range": "± 420.7683937218358"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 635218.4339192709,
            "unit": "ns",
            "range": "± 6211.5959521414925"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 4411.785446166992,
            "unit": "ns",
            "range": "± 7.7320647137965155"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 45966.006998697914,
            "unit": "ns",
            "range": "± 583.8154183568048"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 516638.6533203125,
            "unit": "ns",
            "range": "± 5842.372000566815"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 8727.599751790365,
            "unit": "ns",
            "range": "± 50.78062595570027"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 77882.31803385417,
            "unit": "ns",
            "range": "± 123.64085127419999"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 785877.0092773438,
            "unit": "ns",
            "range": "± 1040.5815109614155"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 6026.942665100098,
            "unit": "ns",
            "range": "± 18.513079993566688"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 57089.402323404945,
            "unit": "ns",
            "range": "± 73.92097085007275"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 573086.0914713541,
            "unit": "ns",
            "range": "± 295.369332529703"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6998.342226664226,
            "unit": "ns",
            "range": "± 11.885455414498516"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 59802.41562906901,
            "unit": "ns",
            "range": "± 24.613827268904597"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 558732.8203125,
            "unit": "ns",
            "range": "± 4456.150083364213"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3796.79917271932,
            "unit": "ns",
            "range": "± 3.873842328276553"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 4620.417317708333,
            "unit": "ns",
            "range": "± 56.48148163160702"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 38507.313903808594,
            "unit": "ns",
            "range": "± 169.77082131887414"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 36916.99578857422,
            "unit": "ns",
            "range": "± 279.9857370929263"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 418555.02620442706,
            "unit": "ns",
            "range": "± 501.4458609080517"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 409122.30061848956,
            "unit": "ns",
            "range": "± 1222.4954130637948"
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
          "id": "51ef1f3b1b2409a91aeb9359f87c59a68807c23d",
          "message": "Release v0.9.0 — options records with nested SerializerOptions inherit the Abstractions 0.24 base records; 7 setters deprecated; superseded constructors hidden (#318)\n\n* feat: make logger an optional trailing ctor parameter, defaulting to NullLogger\n\nAligns all six extractor/loader types with the fleet-wide constructor\nconvention (logger always last, always optional) already followed by\nEtl-DbClient.\n\nEight (source, ILogger<T> logger) constructors become\n(source, ILogger<T>? logger = null), across JsonLineExtractor,\nJsonLineLoader, JsonSingleStreamExtractor, JsonSingleStreamLoader,\nJsonMultiStreamExtractor (stream + named-source forms) and\nJsonMultiStreamLoader (stream-factory + named-destination forms).\n\nnull (or omitted) now resolves to NullLogger.Instance instead of throwing\nArgumentNullException.\n\nNot a breaking change: each parameter list is unchanged, so the emitted\nsignatures are identical. Release build with TreatWarningsAsErrors is clean and\nPackageValidation passes, so the 8 PublicAPI.Shipped.txt entries were corrected\nin place rather than recorded as an add/remove pair.\n\nNo overload became ambiguous: the one-argument (source) constructors still win\nresolution outright because all of their parameters have a corresponding\nargument, while the two-argument form now requires default substitution.\n\nThe JsonTypeInfo<TRecord> overloads are untouched - they are a separate\nAOT-safe family with a different trim posture, not redundant.\n\nTests: the eight tests asserting a null logger throws now assert the NullLogger\ncontract. 489 unit + 12 integration + 7 docs tests pass in Release with\nTreatWarningsAsErrors.\n\nRefs Chris-Wolfgang/ETL-Json#266\n\nCo-Authored-By: Claude Opus 4.8 <noreply@anthropic.com>\n\n* chore: put the logger last on the internal test-injection ctors\n\nApplies Rule 6 of the fleet constructor standard: the logger is the final\nparameter on EVERY constructor, internal ones included.\n\n  internal Json*(…, options, ILogger? logger, IProgressTimer timer)\n    -> internal Json*(…, options, IProgressTimer timer, ILogger? logger = null)\n\nCovers all six types - JsonLineExtractor, JsonLineLoader,\nJsonSingleStreamExtractor, JsonSingleStreamLoader, JsonMultiStreamExtractor and\nJsonMultiStreamLoader - across both their JsonSerializerOptions and\nJsonTypeInfo internal overloads.\n\nInternal-only: no public API change, no PublicAPI entry, no consumer impact and\nnothing to deprecate. Test call sites updated, including the\n\"when_timer_is_null_throws\" tests where the timer is passed as a bare null! and\ntherefore had to be identified by position rather than name.\n\n489 unit + 12 integration + 7 docs tests pass in Release with\nTreatWarningsAsErrors.\n\nCo-Authored-By: Claude Opus 4.8 <noreply@anthropic.com>\n\n* fix(review): #302 — honest names for the null-logger tests; hide the eight superseded single-argument ctors\n\nCopilot (x6): the nine Constructor_*_when_logger_is_null_uses_NullLogger\nfacts asserted only that construction succeeded — the NullLogger fallback\nis private state they cannot observe. Renamed to *_does_not_throw and the\ncomment says what the fact can assert.\n\nChris's threads on JsonLineExtractor: with logger optional, the (Stream) /\n(IEnumerable<Stream>) / (IEnumerable<JsonNamedStream>) / factory ctors are\nsuperseded by their optional-logger overloads. They cannot usefully be\n[Obsolete] (exact-match binding leaves callers only a redundant\n`logger: null`) and removing them is a binary break no marker can warn an\nun-rebuilt caller about, so they are hidden with [EditorBrowsable(Never)]\nand retained permanently — the rule set for ETL-Abstractions#461 and\napplied to ETL-Csv in #284. No public API text change; CHANGELOG Changed.\n\nThe record-shaped ctors (ADR-0009, #303) are a separate PR against the\nunreleased Abstractions 0.24 and are answered in the threads.\n\nCo-Authored-By: Claude Fable 5.1 <noreply@anthropic.com>\n\n* feat(options): options records and record constructors for all six stages; Abstractions 0.24 (#303, part 1)\n\nSix sealed records — JsonLineExtractorOptions, JsonSingleStreamExtractorOptions,\nJsonMultiStreamExtractorOptions (: ExtractorOptions) and JsonLineLoaderOptions,\nJsonSingleStreamLoaderOptions, JsonMultiStreamLoaderOptions (: LoaderOptions) —\ncarrying each stage's own settings (Encoding / EnableCheckpointing /\nStartByteOffset; Encoding / IsDryRun; IsDryRun) plus the four inherited ones.\nSerializer configuration stays a constructor parameter: a JsonTypeInfo<T>\nalready carries its options, so a record-borne copy would be inert on the\nsource-generated path.\n\nNew constructors, one per input shape and serializer family, chain base(options):\n  (source, TOptions options, JsonSerializerOptions? serializerOptions = null, ILogger? logger = null)\n  (source, JsonTypeInfo<T> typeInfo, TOptions options, ILogger? logger = null)\n  (string path, TOptions options, ...) on the two extractors that open files\nThe record is REQUIRED (not defaulted) so every existing call keeps binding\nwhere it binds today; 18 new public ctors.\n\nThe shipped constructors' JsonSerializerOptions parameter is renamed\noptions -> serializerOptions on all 18 of them: with the record parameter\nalso named options, a named argument options: null was ambiguous, and\nfleet-wide options means the record. Binary-compatible; a source break only\nfor callers passing that argument by name (7 in-repo sites updated).\n\nISupportDryRun dropped from the three loaders (Abstractions 0.24 removes\nit); the three dry-run contract tests use the now non-generic TestKit base\nand configure IsDryRun through the records. Abstractions / ErrorPolicies /\nTestKit / TestKit.Xunit 0.23.4 -> 0.24.0 — not published yet; built against\nthe local feed, PR stays draft.\n\nPublicAPI: 10 *REMOVED* (renamed parameter) + 96 added; derived-record\n<Clone>$ lines left out (unmatchable); 21 pre-existing unrecorded members\nre-surfaced and tracked in #314. ApiCompat: CP0008 x15 for ISupportDryRun.\nTests: JsonOptionsRecordTests (15 cases). CHANGELOG Added / Changed / Removed.\n\nCo-Authored-By: Claude Fable 5.1 <noreply@anthropic.com>\n\n* feat(options): deprecate the seven configuration setters; migrate every write site to the records (#303, part 2)\n\nSecond half of #303, stacked on part 1. Encoding, EnableCheckpointing and\nStartByteOffset on JsonLineExtractor<T>, Encoding and IsDryRun on\nJsonLineLoader<T>, and IsDryRun on JsonSingleStreamLoader<T> and\nJsonMultiStreamLoader<T> are [Obsolete] on the setter accessor only, so\nreads stay warning-free and the message names the record to use. Nothing\nis removed.\n\nRelease builds with TreatWarningsAsErrors, so every internal write became\na build error: the four ApplyOptions methods that write them by design sit\nunder a CS0618 pragma (same as Csv and FixedWidth), and the nine test\ninitializers that configured through the setters now pass the record\n(two of them with MaximumItemCount folded in as well). No writes existed\nin src outside ApplyOptions, nor in examples or benchmarks.\n\nAccessor-level [Obsolete] changes no PublicAPI text; CHANGELOG Deprecated.\n\nCo-Authored-By: Claude Fable 5.1 <noreply@anthropic.com>\n\n* docs(options): hide the ten superseded (source, JsonSerializerOptions?, ILogger?) ctors; README and migration guide for the records (#303, part 3)\n\nThe (source, JsonSerializerOptions? serializerOptions = null, ILogger?\nlogger = null) constructors — ten across the six stages, including the two\nfile-path forms — are superseded by the record constructors\n(source, options, serializerOptions, logger). They cannot usefully be\n[Obsolete] (a positional serializer-options argument binds here by exact\nmatch, so the warning could only be silenced by inserting a record the\ncaller may not want) and removal is a binary break no marker reaches, so\nthey are hidden with [EditorBrowsable(Never)] and retained permanently —\nthe same rule as the single-argument constructors in #313. No PublicAPI\ntext change.\n\nREADME gains a \"Configuring a stage\" section (the records, their members,\nthe inherited settings) and the serialization example now passes the\nrecord; docs/migrations/v0.8-to-v0.9.md covers the serializerOptions\nrename, ISupportDryRun, the deprecated setters, the hidden constructors and\nthe before/after of the record spelling. CHANGELOG Added / Changed.\n\nCo-Authored-By: Claude Fable 5.1 <noreply@anthropic.com>\n\n* test(options): compile-time overload-resolution guards; CHANGELOG wording (Copilot review on #315)\n\nCopilot claimed the record constructors make positional-null calls such as\nnew X(stream, null) and new X(stream, typeInfo, null) ambiguous. They do\nnot: the (source, ILogger?) and (source, JsonTypeInfo<T>, ILogger?)\noverloads have every parameter supplied by those calls and are therefore\npreferred over any overload needing default substitution.\nConstructorOverloadResolutionTests pins that for every stage and both\nfamilies, so a future overload that breaks it fails to compile here.\n(source, null, null) is not included: it has been ambiguous since 0.8.x\nbetween the serializer-options and type-info overloads, unrelated to this PR.\n\nCHANGELOG: the Added note no longer says the existing constructors are\nunchanged (their JsonSerializerOptions parameter is renamed; a Changed\nbullet now documents that named-argument source break), and the Removed\nnote says loaders, not readers.\n\nCo-Authored-By: Claude Fable 5.1 <noreply@anthropic.com>\n\n* docs(options): constructor order per family in the README; qualify the hidden-constructor compatibility note (Copilot review on #317)\n\nThe README sentence implied the record always follows the source; on the\nsource-generated constructors it follows the type info. Both orders are now\nspelled out. The migration guide said existing calls to the hidden\nconstructors keep compiling; that is true of positional calls only - a\nnamed serializer argument is subject to the rename documented above.\n\nCo-Authored-By: Claude Fable 5.1 <noreply@anthropic.com>\n\n* feat(options): the record carries SerializerOptions; one options parameter per constructor (review on #315)\n\nChris's review: the record constructors took two \"options\" - the stage\nrecord and a separate JsonSerializerOptions. The serializer options are\nconfiguration too, so they now live on the record (SerializerOptions,\nnested as-is, not flattened: the JSO surface is ~30 settings and growing,\nseveral are object graphs, and a consumer's shared instance keeps its\nmetadata cache). The reflection record constructors become\n(source, options, logger = null); the file-path forms likewise.\n\nThe source-generated constructors keep JsonTypeInfo<TRecord> as their own\nparameter (the type info carries its own serializer options) and reject a\nrecord that also sets SerializerOptions with ArgumentException, so the\ncombination is a visible error rather than an ignored setting.\n\nPublicAPI: 10 constructor entries shortened, SerializerOptions get/init on\nall six records. Tests: SerializerOptions applied through the record;\ntype-info + SerializerOptions rejected on the line extractor and loader;\noverload guards cover (source, record) and (source, record, null).\nCHANGELOG updated.\n\nCo-Authored-By: Claude Fable 5.1 <noreply@anthropic.com>\n\n* docs(options): SerializerOptions lives on the record; constructor shape (source, options, logger)\n\nREADME, migration guide and the hidden-constructor CHANGELOG bullet follow\nthe #315 change: the record carries SerializerOptions, the reflection\nconstructors take (source, options, logger), and the source-generated ones\nreject a record that also sets SerializerOptions.\n\nCo-Authored-By: Claude Fable 5.1 <noreply@anthropic.com>\n\n* test(options): the serializer options travel on the record in the folded JsonLineLoader test\n\nCo-Authored-By: Claude Fable 5.1 <noreply@anthropic.com>\n\n* refactor(options): the internal timer-injection constructors take the record too (review on #315)\n\nThe seven internal (source, JsonSerializerOptions, IProgressTimer, ILogger?)\nconstructors now take the stage record instead - (source, options, timer,\nlogger) - chain base(options) and apply it, so the serializer options travel\non the record everywhere and 'options' means one thing on every constructor,\npublic or internal. The seven type-info internal constructors are unchanged.\nThirty test call sites updated (internal API; no PublicAPI change).\n\nCo-Authored-By: Claude Fable 5.1 <noreply@anthropic.com>\n\n* revert(options): keep the shipped constructors' JsonSerializerOptions parameter named options (review on #315)\n\nRenaming it to serializerOptions traded a source break for every caller\nwho wrote 'options: myJso' against rescuing one call shape - a NAMED null,\n'options: null' - which is ambiguous between the serializer and record\nconstructors and which nobody should write. Wrong side of the bargain.\nOverloads may share a parameter name: 'options: jso' and 'options: record'\neach bind by type, positional calls are unaffected. The seven test sites\nthat passed a named null now pass a typed positional null. The ten\nPublicAPI entries return to their shipped text; the Changed bullet and the\nmigration-guide rows for the rename go away. No source break remains in\nthis stack.\n\nCo-Authored-By: Claude Fable 5.1 <noreply@anthropic.com>\n\n* docs: resolve the CHANGELOG merge properly and drop the rename from the migration guide\n\nThe merge commit before this one carried conflict markers in CHANGELOG.md;\nthis removes them, keeps the hidden-constructor bullet without its\nnamed-argument clause, and removes the migration-guide row and checklist\nitem for the rename that no longer exists.\n\nCo-Authored-By: Claude Fable 5.1 <noreply@anthropic.com>\n\n* release: v0.9.0\n\noptions records with nested SerializerOptions inherit the Abstractions 0.24 base records; 7 setters deprecated; superseded constructors hidden MINOR bump from v0.8.1: new public surface (options records inheriting the Abstractions 0.24.0 base records, new constructors) and new [Obsolete] markers; no removals.\n\nCo-Authored-By: Claude Opus 5 <noreply@anthropic.com>\n\n* test: cover the JsonTypeInfo<T> record constructors and their SerializerOptions guard (coverage gate)\n\nRelease PR #318's Stage 1 gate failed at 88.1% line coverage: the\ntype-info constructor overloads that #315 added on all four stream\nstages, their RejectSerializerOptions guards, and the path / named-\ndestination / named-sources record overloads had no direct tests.\nJsonTypeInfoConstructorTests adds 11 facts: each stage round-trips\nthrough the type info with the record's inherited settings applied,\nevery type-info overload rejects a record carrying SerializerOptions\n(ArgumentException, paramName \"options\"), and the path and named\noverloads are exercised. Wolfgang.Etl.Json unit line coverage on\nnet10.0: 88.2% -> 95.1%.\n\nCo-Authored-By: Claude Opus 5 <noreply@anthropic.com>\n\n* docs: record the named-null (`options: null`) CS0121 break; test proves the record's SerializerOptions are applied (review on #318)\n\nBoth overloads name their parameter `options` by design (#315), which\nmakes the one spelling `new X(source, options: null)` ambiguous. That\nwas decided but not written down; the migration guide's Breaking\nChanges table and the CHANGELOG now say so, with the spellings that\nstill bind.\n\nJsonLineExtractor_when_the_record_carries_SerializerOptions_uses_them\nnow feeds camelCase JSON and asserts the extracted record, so it fails\nif the constructor ignored the record's SerializerOptions.\n\nCo-Authored-By: Claude Opus 5 <noreply@anthropic.com>\n\n---------\n\nCo-authored-by: Claude Opus 4.8 <noreply@anthropic.com>",
          "timestamp": "2026-09-16T20:33:31-04:00",
          "tree_id": "429194305f279608fddba6dd918c745c297cd8ea",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/51ef1f3b1b2409a91aeb9359f87c59a68807c23d"
        },
        "date": 1789605417197,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6749.105692545573,
            "unit": "ns",
            "range": "± 18.08002340794076"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 63153.9276936849,
            "unit": "ns",
            "range": "± 1894.855134128296"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 606772.6848958334,
            "unit": "ns",
            "range": "± 4948.2238336969285"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 4532.804036458333,
            "unit": "ns",
            "range": "± 46.29786144041413"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 41859.85561116537,
            "unit": "ns",
            "range": "± 511.92477892386546"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 482546.00764973956,
            "unit": "ns",
            "range": "± 2893.459804073712"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 8363.470499674479,
            "unit": "ns",
            "range": "± 85.90040917935882"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 77839.13448079427,
            "unit": "ns",
            "range": "± 65.72584324962405"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 779758.6956380209,
            "unit": "ns",
            "range": "± 777.9309990209425"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 5984.693270365397,
            "unit": "ns",
            "range": "± 29.53705328021696"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 58671.509521484375,
            "unit": "ns",
            "range": "± 590.3677494497426"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 579318.6220703125,
            "unit": "ns",
            "range": "± 1091.720221664771"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6981.904502868652,
            "unit": "ns",
            "range": "± 13.81688890677115"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 58563.63104248047,
            "unit": "ns",
            "range": "± 139.31148171353544"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 529412.8238932291,
            "unit": "ns",
            "range": "± 2528.770659287622"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3833.3690223693848,
            "unit": "ns",
            "range": "± 100.46379341850934"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 4600.006640116374,
            "unit": "ns",
            "range": "± 16.16029624930466"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 35047.885192871094,
            "unit": "ns",
            "range": "± 318.6219058180672"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 36965.35791015625,
            "unit": "ns",
            "range": "± 69.46678039531173"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 395676.96077473956,
            "unit": "ns",
            "range": "± 652.3151108069782"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 397386.66813151044,
            "unit": "ns",
            "range": "± 1224.1242112029258"
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
          "id": "299f3358b2a6db734edb5bd7632ba9e5049c5fc2",
          "message": "chore(pack): advance PackageValidation baseline to 0.9.0 (#323)\n\nv0.9.0 is published and indexed on nuget.org; CompatibilitySuppressions.xml regenerated against it (the ADR-0009 entries were one-release-lived and are now pruned, as PackageValidation requires). compat-suppressions.txt keeps its ISupportDryRun line: the release-time script compares against the highest published version below <Version>, which stays 0.8.1 until the next bump.\n\nCo-authored-by: Claude Opus 5 <noreply@anthropic.com>",
          "timestamp": "2026-09-17T14:44:58-04:00",
          "tree_id": "ef0bde7df8d6dcbb445e036f0bdf29bf65beb8d3",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/299f3358b2a6db734edb5bd7632ba9e5049c5fc2"
        },
        "date": 1789670897057,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 4146.0967445373535,
            "unit": "ns",
            "range": "± 29.476863351902082"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 36580.00657145182,
            "unit": "ns",
            "range": "± 243.34040197343415"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 374056.8756510417,
            "unit": "ns",
            "range": "± 6208.8488618870015"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 2396.5133628845215,
            "unit": "ns",
            "range": "± 82.23593235500941"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 19789.157389322918,
            "unit": "ns",
            "range": "± 52.718367569650205"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 254434.10123697916,
            "unit": "ns",
            "range": "± 12786.927348385572"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 4861.0033620198565,
            "unit": "ns",
            "range": "± 44.25445480957261"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 45484.33166503906,
            "unit": "ns",
            "range": "± 64.13314753833615"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 450150.2213541667,
            "unit": "ns",
            "range": "± 1705.7702180452632"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 2800.105515797933,
            "unit": "ns",
            "range": "± 11.560586325903172"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 30334.572591145832,
            "unit": "ns",
            "range": "± 83.24199596229455"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 271895.828125,
            "unit": "ns",
            "range": "± 495.8413226726413"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 4017.2131220499673,
            "unit": "ns",
            "range": "± 170.9565666452811"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 31332.682067871094,
            "unit": "ns",
            "range": "± 27.417356083358108"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 308096.26839192706,
            "unit": "ns",
            "range": "± 1163.8379831828372"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 1790.82967821757,
            "unit": "ns",
            "range": "± 7.059233918083871"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 2247.0582071940103,
            "unit": "ns",
            "range": "± 4.784687271258203"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 15989.9377339681,
            "unit": "ns",
            "range": "± 125.60025946710849"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 16136.467213948568,
            "unit": "ns",
            "range": "± 170.88339197371263"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 182291.09497070312,
            "unit": "ns",
            "range": "± 485.20844972725644"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 188491.24422200522,
            "unit": "ns",
            "range": "± 8586.350417901685"
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
          "id": "f117f3b4fca4c0a0fad049447356f82db20c25fc",
          "message": "fix(tfm): ship net5.0/net6.0/net7.0 assemblies (inherited init-setter modreq hazard) (#327)\n\n* fix(test): restore test discovery on netcoreapp3.1 and net5.0\n\nSame defect as Chris-Wolfgang/Etl-Csv#254 and Chris-Wolfgang/ETL-FixedWidth#336: xunit.runner.visualstudio 2.8.2 ships build/lib assets for net462 and net6.0 only, so the netcoreapp3.1 and net5.0 slots loaded no test adapter and ran zero tests (\"No test is available\") while the suite still read as clean. Pin 2.4.5 on those two slots, 2.8.2 elsewhere, both capped below 3.0.0.\n\nCo-Authored-By: Claude Opus 5 <noreply@anthropic.com>\n\n* fix(tfm): ship net5.0/net6.0/net7.0 assemblies — inherited init setters would fail on .NET 5-7\n\nWolfgang.Etl.Abstractions ships per-runtime assemblies, and an init-only setter's IsExternalInit modreq has a different identity in its netstandard2.0 build (internal polyfill) and its net5.0+ builds (System.Runtime). This package's netstandard2.0 assembly is compiled against the former but, on .NET 5/6/7, runs beside the latter, so any write to an inherited options-record property from this assembly throws MissingMethodException — the defect Etl-Csv 0.9.0 hit in its release gate (Chris-Wolfgang/Etl-Csv#287). Same remedy as Abstractions, Etl-DbClient and Etl-Csv.\n\nCompiling for net5.0/net6.0 for the first time exposed two guards that assumed \"NET5_0_OR_GREATER\" meant \"net8.0+\": [RequiresDynamicCode] (42 sites) and StreamReader.ReadLineAsync(CancellationToken) both first shipped in .NET 7 and are now guarded with NET7_0_OR_GREATER; the net5/6 builds take the same path netstandard2.0 does.\n\nCo-Authored-By: Claude Opus 5 <noreply@anthropic.com>\n\n* docs: changelog fragment instead of a CHANGELOG.md edit; framework lists brought in line\n\nCo-Authored-By: Claude Opus 5 <noreply@anthropic.com>\n\n---------\n\nCo-authored-by: Claude Opus 5 <noreply@anthropic.com>",
          "timestamp": "2026-09-17T19:49:36-04:00",
          "tree_id": "2282a280cb64ca34bb8fd152003e38538844a01e",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/f117f3b4fca4c0a0fad049447356f82db20c25fc"
        },
        "date": 1789689175870,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 7007.3218510945635,
            "unit": "ns",
            "range": "± 95.6678931209035"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 64692.00107828776,
            "unit": "ns",
            "range": "± 411.65350315406647"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 648383.6027018229,
            "unit": "ns",
            "range": "± 1437.6323557076973"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 4528.237083435059,
            "unit": "ns",
            "range": "± 12.237686355348393"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 41448.78135172526,
            "unit": "ns",
            "range": "± 61.395451882472045"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 504260.3167317708,
            "unit": "ns",
            "range": "± 2790.206591111909"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 8569.486017862955,
            "unit": "ns",
            "range": "± 24.72414747784798"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 79177.4912109375,
            "unit": "ns",
            "range": "± 83.01887234247867"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 782435.8590494791,
            "unit": "ns",
            "range": "± 1100.2117053716977"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 6145.260856628418,
            "unit": "ns",
            "range": "± 15.724517550117492"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 56587.737630208336,
            "unit": "ns",
            "range": "± 258.92401549554734"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 604003.9270833334,
            "unit": "ns",
            "range": "± 1008.21017838827"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6998.008389790853,
            "unit": "ns",
            "range": "± 5.84043644318961"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 59660.87325032552,
            "unit": "ns",
            "range": "± 105.84232454831908"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 551699.8916015625,
            "unit": "ns",
            "range": "± 2576.0503340634527"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3919.694048563639,
            "unit": "ns",
            "range": "± 23.7581645697488"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 4454.991905212402,
            "unit": "ns",
            "range": "± 10.97959962563216"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 36437.535481770836,
            "unit": "ns",
            "range": "± 167.21294543856033"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 35449.07354736328,
            "unit": "ns",
            "range": "± 225.54005353590392"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 395365.37711588544,
            "unit": "ns",
            "range": "± 923.1099078067966"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 396067.32438151044,
            "unit": "ns",
            "range": "± 226.58947576597652"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "49699333+dependabot[bot]@users.noreply.github.com",
            "name": "dependabot[bot]",
            "username": "dependabot[bot]"
          },
          "committer": {
            "email": "noreply@github.com",
            "name": "GitHub",
            "username": "web-flow"
          },
          "distinct": true,
          "id": "e4caeed69805c89a4b1ff4b91362fcabf5037e9d",
          "message": "Bump the dotnet-dependencies group with 5 updates (#328)\n\nBumps Meziantou.Analyzer from 3.0.258 to 3.0.259\nBumps Microsoft.Bcl.AsyncInterfaces from 10.0.11 to 10.0.12\nBumps Microsoft.Extensions.Logging.Abstractions from 10.0.11 to 10.0.12\nBumps System.Diagnostics.DiagnosticSource from 10.0.11 to 10.0.12\nBumps System.Text.Json from 10.0.11 to 10.0.12\n\n---\nupdated-dependencies:\n- dependency-name: Meziantou.Analyzer\n  dependency-version: 3.0.259\n  dependency-type: direct:production\n  update-type: version-update:semver-patch\n  dependency-group: dotnet-dependencies\n- dependency-name: Microsoft.Bcl.AsyncInterfaces\n  dependency-version: 10.0.12\n  dependency-type: direct:production\n  update-type: version-update:semver-patch\n  dependency-group: dotnet-dependencies\n- dependency-name: Microsoft.Bcl.AsyncInterfaces\n  dependency-version: 10.0.12\n  dependency-type: direct:production\n  update-type: version-update:semver-patch\n  dependency-group: dotnet-dependencies\n- dependency-name: Microsoft.Bcl.AsyncInterfaces\n  dependency-version: 10.0.12\n  dependency-type: direct:production\n  update-type: version-update:semver-patch\n  dependency-group: dotnet-dependencies\n- dependency-name: Microsoft.Bcl.AsyncInterfaces\n  dependency-version: 10.0.12\n  dependency-type: direct:production\n  update-type: version-update:semver-patch\n  dependency-group: dotnet-dependencies\n- dependency-name: Microsoft.Extensions.Logging.Abstractions\n  dependency-version: 10.0.12\n  dependency-type: direct:production\n  update-type: version-update:semver-patch\n  dependency-group: dotnet-dependencies\n- dependency-name: Microsoft.Extensions.Logging.Abstractions\n  dependency-version: 10.0.12\n  dependency-type: direct:production\n  update-type: version-update:semver-patch\n  dependency-group: dotnet-dependencies\n- dependency-name: System.Diagnostics.DiagnosticSource\n  dependency-version: 10.0.12\n  dependency-type: direct:production\n  update-type: version-update:semver-patch\n  dependency-group: dotnet-dependencies\n- dependency-name: System.Text.Json\n  dependency-version: 10.0.12\n  dependency-type: direct:production\n  update-type: version-update:semver-patch\n  dependency-group: dotnet-dependencies\n- dependency-name: System.Text.Json\n  dependency-version: 10.0.12\n  dependency-type: direct:production\n  update-type: version-update:semver-patch\n  dependency-group: dotnet-dependencies\n- dependency-name: System.Text.Json\n  dependency-version: 10.0.12\n  dependency-type: direct:production\n  update-type: version-update:semver-patch\n  dependency-group: dotnet-dependencies\n...\n\nSigned-off-by: dependabot[bot] <support@github.com>\nCo-authored-by: dependabot[bot] <49699333+dependabot[bot]@users.noreply.github.com>",
          "timestamp": "2026-09-17T20:06:33-04:00",
          "tree_id": "0eb5a85aa412b5373de8d1a95dca089ebf66300d",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/e4caeed69805c89a4b1ff4b91362fcabf5037e9d"
        },
        "date": 1789690183272,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 4858.425192515056,
            "unit": "ns",
            "range": "± 4.122998005793426"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 46520.16970825195,
            "unit": "ns",
            "range": "± 174.49258712207634"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 436561.44547526044,
            "unit": "ns",
            "range": "± 3118.206788429998"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 2633.3811225891113,
            "unit": "ns",
            "range": "± 23.71078716191432"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 25178.453674316406,
            "unit": "ns",
            "range": "± 335.3205843031734"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 304639.22932942706,
            "unit": "ns",
            "range": "± 4551.157967451427"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 5941.872983296712,
            "unit": "ns",
            "range": "± 68.37032525539354"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 55109.15793863932,
            "unit": "ns",
            "range": "± 393.06433188699884"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 553735.9088541666,
            "unit": "ns",
            "range": "± 1945.435747564845"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3607.657667795817,
            "unit": "ns",
            "range": "± 30.436398864818898"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 34242.99537150065,
            "unit": "ns",
            "range": "± 667.8348012822912"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 325193.88728841144,
            "unit": "ns",
            "range": "± 3238.739746554332"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 4691.014485677083,
            "unit": "ns",
            "range": "± 50.69280494202374"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 39915.52917480469,
            "unit": "ns",
            "range": "± 227.76863206072971"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 375631.6748046875,
            "unit": "ns",
            "range": "± 1398.7830658254004"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 2352.1204999287925,
            "unit": "ns",
            "range": "± 20.514434712701945"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 2902.225789388021,
            "unit": "ns",
            "range": "± 37.40269697188232"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 20875.476465861004,
            "unit": "ns",
            "range": "± 161.46807350943294"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 22005.731486002605,
            "unit": "ns",
            "range": "± 220.15438950753162"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 255561.0732421875,
            "unit": "ns",
            "range": "± 3622.8205910871607"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 255508.7080078125,
            "unit": "ns",
            "range": "± 1049.7826064364517"
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
          "id": "8550a90fabb26c0a35302874feab1e424fa083b6",
          "message": "chore(publicapi): record the synthesized record members; per-TFM split for the covariant <Clone>$ lines (#329)\n\n* fix(tfm): ship net5.0/net6.0/net7.0 assemblies — inherited init setters would fail on .NET 5-7\n\nWolfgang.Etl.Abstractions ships per-runtime assemblies, and an init-only setter's IsExternalInit modreq has a different identity in its netstandard2.0 build (internal polyfill) and its net5.0+ builds (System.Runtime). This package's netstandard2.0 assembly is compiled against the former but, on .NET 5/6/7, runs beside the latter, so any write to an inherited options-record property from this assembly throws MissingMethodException — the defect Etl-Csv 0.9.0 hit in its release gate (Chris-Wolfgang/Etl-Csv#287). Same remedy as Abstractions, Etl-DbClient and Etl-Csv.\n\nCompiling for net5.0/net6.0 for the first time exposed two guards that assumed \"NET5_0_OR_GREATER\" meant \"net8.0+\": [RequiresDynamicCode] (42 sites) and StreamReader.ReadLineAsync(CancellationToken) both first shipped in .NET 7 and are now guarded with NET7_0_OR_GREATER; the net5/6 builds take the same path netstandard2.0 does.\n\nCo-Authored-By: Claude Opus 5 <noreply@anthropic.com>\n\n* docs: changelog fragment instead of a CHANGELOG.md edit; framework lists brought in line\n\nCo-Authored-By: Claude Opus 5 <noreply@anthropic.com>\n\n* chore(publicapi): record the synthesized record members that shipped unrecorded; per-TFM split for the covariant <Clone>$ lines\n\nRS0016 is muzzled by the blanket analyzer severity, so the compiler-synthesized members of every shipped record (<Clone>$, copy ctor, Deconstruct, Equals, GetHashCode, ToString, PrintMembers, EqualityContract, ==/!=) have been shipping without an entry in PublicAPI.Shipped.txt. Harvested by raising RS0016 to warning on the project across every target framework and appended to Shipped — they are already public.\n\nThe <Clone>$ of every record that derives from an Abstractions record has a covariant return (the derived type) on net5.0+ but returns the base type on net462 / netstandard2.0, so those lines cannot live in the shared file. Adopts Try-Pattern's layout: PublicApi/modern and PublicApi/legacy PublicAPI.{Shipped,Unshipped}.txt, wired by IsTargetFrameworkCompatible(net5.0) in the csproj; the shared file keeps the TFM-invariant surface. On PublicApiAnalyzers 5.6.0 RS0017 accepts the <Clone>$ lines in the form RS0016 emits, so the blocker recorded in Chris-Wolfgang/Etl-Csv#263 no longer reproduces once the split is in place. Verified: RS0016 raised again reports nothing on any target; plain Release build has no RS0017.\n\nCo-Authored-By: Claude Opus 5 <noreply@anthropic.com>\n\n* chore: changelog fragment (internal) for the PublicAPI backfill\n\nCo-Authored-By: Claude Opus 5 <noreply@anthropic.com>\n\n---------\n\nCo-authored-by: Claude Opus 5 <noreply@anthropic.com>",
          "timestamp": "2026-09-17T20:22:02-04:00",
          "tree_id": "374f97807884c294978e68123f760eea8c77d81f",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/8550a90fabb26c0a35302874feab1e424fa083b6"
        },
        "date": 1789691124026,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 7028.461832682292,
            "unit": "ns",
            "range": "± 74.98666265531769"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 62389.43013509115,
            "unit": "ns",
            "range": "± 644.3611644029629"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 601235.3523763021,
            "unit": "ns",
            "range": "± 5827.911075146016"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 4359.687405904134,
            "unit": "ns",
            "range": "± 41.778378883041874"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 40702.472147623695,
            "unit": "ns",
            "range": "± 75.75809710162713"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 491991.0745442708,
            "unit": "ns",
            "range": "± 3706.7217734406117"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 8225.896825154623,
            "unit": "ns",
            "range": "± 111.87013386253885"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 82796.59442138672,
            "unit": "ns",
            "range": "± 77.44783885634865"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 767933.7913411459,
            "unit": "ns",
            "range": "± 1059.3564439798458"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 5908.459264119466,
            "unit": "ns",
            "range": "± 15.209164491011048"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 54773.84981282552,
            "unit": "ns",
            "range": "± 76.60241277482335"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 574160.35546875,
            "unit": "ns",
            "range": "± 4692.24799849007"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6973.558877309163,
            "unit": "ns",
            "range": "± 25.200038890234815"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 57828.335388183594,
            "unit": "ns",
            "range": "± 324.2535536292037"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 550446.0719401041,
            "unit": "ns",
            "range": "± 1908.431995840356"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3785.430753072103,
            "unit": "ns",
            "range": "± 5.520382045963273"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 4418.077481587728,
            "unit": "ns",
            "range": "± 17.56660974915568"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 34679.83435058594,
            "unit": "ns",
            "range": "± 128.0856655034635"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 35979.56321207682,
            "unit": "ns",
            "range": "± 77.60428313882086"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 391188.1826171875,
            "unit": "ns",
            "range": "± 2193.937449827488"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 391208.18050130206,
            "unit": "ns",
            "range": "± 2663.8903146375933"
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
          "id": "b7cadd54e4152dbcee5aee9daf6456cfa5dd63e3",
          "message": "build: adopt Wolfgang.Etl.Abstractions 0.25.0 (contract tests take the base config through CreateSut) (#387)\n\n* build: adopt Wolfgang.Etl.Abstractions / TestKit / TestKit.Xunit 0.25.0\n\nThe base contract classes now take the base configuration through CreateSut(int itemCount, int maximumItemCount, int skipItemCount, int reportingInterval); every implementer forwards the three values into its options record. Tests that configured a stage through the now-deprecated base setters configure through the record instead; the vestigial CreateSutWithTimer overrides go (Chris-Wolfgang/ETL-Abstractions#372 removes the member next).\n\nThe JSONL example configured skip/max through the base setters; it passes JsonLineExtractorOptions now.\n\nVerified locally: Release build 0 errors; unit suites green on net462 / netcoreapp3.1 / net10.0; coverage gate reproduced with no class below 90 %.\n\nCo-Authored-By: Claude Opus 5 <noreply@anthropic.com>\n\n* build: commit the contract-test, example and changelog changes for the 0.25 adoption\n\nThe adoption commit only captured the csproj bumps; the CreateSut(int, int, int, int) implementations, the dropped CreateSutWithTimer overrides, the JsonMetricsTests record, the JSONL example's move to JsonLineExtractorOptions and the changelog fragment were left in the working tree.\n\nCo-Authored-By: Claude Opus 5 <noreply@anthropic.com>\n\n---------\n\nCo-authored-by: Claude Opus 5 <noreply@anthropic.com>",
          "timestamp": "2026-09-18T21:00:42-04:00",
          "tree_id": "3bfa441fb522042e7e1b84b45b3f4b709665b4db",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/b7cadd54e4152dbcee5aee9daf6456cfa5dd63e3"
        },
        "date": 1789779844691,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6311.487130482991,
            "unit": "ns",
            "range": "± 39.890629060937826"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 56160.53837076823,
            "unit": "ns",
            "range": "± 653.485946483357"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 557839.8818359375,
            "unit": "ns",
            "range": "± 2001.5911040580954"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3337.3417727152505,
            "unit": "ns",
            "range": "± 8.676664687895535"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 31611.087768554688,
            "unit": "ns",
            "range": "± 53.57190784135438"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 382080.84163411456,
            "unit": "ns",
            "range": "± 1278.6539942843963"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 7689.61127726237,
            "unit": "ns",
            "range": "± 21.52804716907895"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 72405.92956542969,
            "unit": "ns",
            "range": "± 1034.568164075126"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 722523.9228515625,
            "unit": "ns",
            "range": "± 6993.616229040784"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 4603.533671061198,
            "unit": "ns",
            "range": "± 18.69549916379152"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 42769.79923502604,
            "unit": "ns",
            "range": "± 82.38364022515002"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 424609.6015625,
            "unit": "ns",
            "range": "± 520.7948310537035"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6229.673403422038,
            "unit": "ns",
            "range": "± 12.253340521014305"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 51253.27968343099,
            "unit": "ns",
            "range": "± 92.80475618496392"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 496435.3098958333,
            "unit": "ns",
            "range": "± 2352.6330993146344"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 2938.2642250061035,
            "unit": "ns",
            "range": "± 0.4875317453942048"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 3560.7446263631186,
            "unit": "ns",
            "range": "± 11.776583934245545"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 25758.632954915363,
            "unit": "ns",
            "range": "± 90.79741916195528"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 26845.09726969401,
            "unit": "ns",
            "range": "± 196.9966541850584"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 322241.1292317708,
            "unit": "ns",
            "range": "± 817.0132304860647"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 334045.5436197917,
            "unit": "ns",
            "range": "± 253.02434920303384"
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
          "id": "6aa890bf0c5ad8297a40b4182084a2c348786077",
          "message": "refactor: options constructor assigns backing fields, not the deprecated setters (#389)\n\n* refactor: options constructor assigns backing fields, not the deprecated setters (#384)\n\nEvery `{ get; [Obsolete] set; }` property now has an explicit backing field the constructor / ApplyOptions writes, so the constructor no longer calls the setters it deprecates and the CS0618 pragma blocks that wrapped those writes are dropped (observation-only reads keep theirs). None of the four stages' deprecated setters validate, so no record-side guard was needed.\n\nVerified locally: Release build 0 errors; unit suites green on net462 / net10.0; coverage gate reproduced with no class below 90 %.\n\nCo-Authored-By: Claude Opus 5 <noreply@anthropic.com>\n\n* docs(pack): add THIRD-PARTY-NOTICES.md and ship it in the package (#298) (#391)\n\nAdds the hand-maintained licence notices for the shipped runtime dependencies (Microsoft.Bcl.AsyncInterfaces, Microsoft.Extensions.Logging.Abstractions, System.Diagnostics.DiagnosticSource, System.Text.Json — all MIT) in the Etl-Csv house format, and packs it unconditionally so a missing file fails `dotnet pack`.\n\nCo-authored-by: Claude Opus 5 <noreply@anthropic.com>\n\n---------\n\nCo-authored-by: Claude Opus 5 <noreply@anthropic.com>",
          "timestamp": "2026-09-18T21:36:19-04:00",
          "tree_id": "b162d6b7878596cd35d8fe7df2c76dfc38def476",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/6aa890bf0c5ad8297a40b4182084a2c348786077"
        },
        "date": 1789781973156,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 5017.847666422526,
            "unit": "ns",
            "range": "± 26.31442737039499"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 44393.62469482422,
            "unit": "ns",
            "range": "± 227.63323367614822"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 424952.32307942706,
            "unit": "ns",
            "range": "± 2020.007777279712"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 2688.459920247396,
            "unit": "ns",
            "range": "± 11.550670911974475"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 25878.08632405599,
            "unit": "ns",
            "range": "± 240.584682443663"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 299081.8974609375,
            "unit": "ns",
            "range": "± 3506.6325607736385"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6289.871325174968,
            "unit": "ns",
            "range": "± 48.92949443695221"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 55903.934814453125,
            "unit": "ns",
            "range": "± 337.6412854936956"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 573781.2747395834,
            "unit": "ns",
            "range": "± 1124.541847205291"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3654.268454233805,
            "unit": "ns",
            "range": "± 34.394319880801596"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 33885.7541809082,
            "unit": "ns",
            "range": "± 317.7703903554657"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 346314.12093098956,
            "unit": "ns",
            "range": "± 2474.5883712736754"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 4632.49947865804,
            "unit": "ns",
            "range": "± 30.440842681714226"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 38875.994120279945,
            "unit": "ns",
            "range": "± 71.06604026272169"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 374101.49918619794,
            "unit": "ns",
            "range": "± 420.43855353287154"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 2345.5519485473633,
            "unit": "ns",
            "range": "± 4.959768050708762"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 2881.510871887207,
            "unit": "ns",
            "range": "± 9.00956414494862"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 20638.18430074056,
            "unit": "ns",
            "range": "± 99.80153709113651"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 21449.68035888672,
            "unit": "ns",
            "range": "± 93.37047396720646"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 248892.5428059896,
            "unit": "ns",
            "range": "± 1965.8450063440605"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 250251.47216796875,
            "unit": "ns",
            "range": "± 1664.2430494250575"
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
          "id": "025748b10ba88c832b0f90832da16a916325ca43",
          "message": "chore(analyzers): S1133 / S3427 off until the 2026-12-15 removal wave (dated src .editorconfig) (#404)\n\nResolves the 7 S1133 and 16 S3427 alerts with two dated, documented project-level exclusions.\n\nVerified locally: Release build 0 errors.\n\nCo-authored-by: Claude Opus 5 <noreply@anthropic.com>",
          "timestamp": "2026-09-19T14:31:00-04:00",
          "tree_id": "33888e8b564fed4664083b09de98e32db1ebec10",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/025748b10ba88c832b0f90832da16a916325ca43"
        },
        "date": 1789843122338,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6975.6612065633135,
            "unit": "ns",
            "range": "± 174.82748527641087"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 63373.18591308594,
            "unit": "ns",
            "range": "± 886.3899306275989"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 625889.1565755209,
            "unit": "ns",
            "range": "± 2686.412370460674"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 4458.788297017415,
            "unit": "ns",
            "range": "± 4.515641680616055"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 42778.4131266276,
            "unit": "ns",
            "range": "± 214.90395460817652"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 473253.3154296875,
            "unit": "ns",
            "range": "± 12635.23600434865"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 8828.964965820312,
            "unit": "ns",
            "range": "± 39.65529599835143"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 78866.88997395833,
            "unit": "ns",
            "range": "± 2315.26856231175"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 779548.2779947916,
            "unit": "ns",
            "range": "± 787.8993552704922"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 5959.204579671224,
            "unit": "ns",
            "range": "± 42.88487593511228"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 56797.16819254557,
            "unit": "ns",
            "range": "± 27.199984225260046"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 583539.1875,
            "unit": "ns",
            "range": "± 27315.63516460052"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 7045.880744934082,
            "unit": "ns",
            "range": "± 164.18879536905456"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 58559.448181152344,
            "unit": "ns",
            "range": "± 623.5807690092747"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 552731.5302734375,
            "unit": "ns",
            "range": "± 2138.0874111153107"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3786.3949495951333,
            "unit": "ns",
            "range": "± 20.690170565916684"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 4567.254264831543,
            "unit": "ns",
            "range": "± 52.89890777931608"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 35437.235900878906,
            "unit": "ns",
            "range": "± 146.21831439346073"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 35432.74133300781,
            "unit": "ns",
            "range": "± 413.0739383331085"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 416420.07177734375,
            "unit": "ns",
            "range": "± 969.7605728350942"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 396622.49039713544,
            "unit": "ns",
            "range": "± 2764.0011306879046"
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
          "id": "0fb6ba69d5a7a3a8303c2e2bfab2dcb5b8a3e942",
          "message": "chore(src): RejectSerializerOptions annotated null-tolerant, IsDryRun summaries, ApplyOptions static (#405)\n\nResolves 14 InspectCode alerts in the six stage classes.\n\nVerified locally: Release build 0 errors; unit suites green on net462 / net10.0.\n\nCo-authored-by: Claude Opus 5 <noreply@anthropic.com>",
          "timestamp": "2026-09-19T15:11:35-04:00",
          "tree_id": "431c66bf198c9267e0d107e746ddaf6a7326112d",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/0fb6ba69d5a7a3a8303c2e2bfab2dcb5b8a3e942"
        },
        "date": 1789845294238,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 5119.38444519043,
            "unit": "ns",
            "range": "± 149.75390501655858"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 45737.1877746582,
            "unit": "ns",
            "range": "± 126.77587221342395"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 441505.2822265625,
            "unit": "ns",
            "range": "± 5168.682290923042"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 2682.90789159139,
            "unit": "ns",
            "range": "± 31.578243087391478"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 25562.12806193034,
            "unit": "ns",
            "range": "± 186.87923137641138"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 305235.6119791667,
            "unit": "ns",
            "range": "± 6274.2586114629985"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6045.012641906738,
            "unit": "ns",
            "range": "± 19.431914540077173"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 55801.529958089195,
            "unit": "ns",
            "range": "± 332.68604102982664"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 577187.2392578125,
            "unit": "ns",
            "range": "± 4928.733798306079"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3452.422294616699,
            "unit": "ns",
            "range": "± 37.89404315212854"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 33992.088999430336,
            "unit": "ns",
            "range": "± 87.60237571208924"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 341835.26481119794,
            "unit": "ns",
            "range": "± 7494.650264419789"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 4573.5956624348955,
            "unit": "ns",
            "range": "± 20.062359592940645"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 37870.730143229164,
            "unit": "ns",
            "range": "± 560.9737321088953"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 386214.95914713544,
            "unit": "ns",
            "range": "± 527.2498974753283"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 2297.8338661193848,
            "unit": "ns",
            "range": "± 20.91647714861147"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 2918.583875020345,
            "unit": "ns",
            "range": "± 37.18054809031269"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 21239.50182088216,
            "unit": "ns",
            "range": "± 185.82065918079533"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 21477.521759033203,
            "unit": "ns",
            "range": "± 289.13322664893064"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 247256.8230794271,
            "unit": "ns",
            "range": "± 769.4076081888049"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 248150.3048502604,
            "unit": "ns",
            "range": "± 145.10780487930793"
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
          "id": "45094976717ebfeb4f7822298aa763f2337435a7",
          "message": "chore: redundant usings removed; explicit test nulls marked deliberate (#406)\n\nResolves 16 InspectCode alerts: 8 redundant usings and 8 redundant-default-argument findings in a test that exists to pass those arguments.\n\nVerified locally: Release build 0 errors; unit suites green on net462 / net10.0.\n\nCo-authored-by: Claude Opus 5 <noreply@anthropic.com>",
          "timestamp": "2026-09-19T15:51:23-04:00",
          "tree_id": "1f9fdb51f06546b941a0d9aef32efc0cda3fc79c",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/45094976717ebfeb4f7822298aa763f2337435a7"
        },
        "date": 1789847675872,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 4710.6879234313965,
            "unit": "ns",
            "range": "± 12.340122457077182"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 44556.08713785807,
            "unit": "ns",
            "range": "± 1925.3147250964928"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 421208.3885904948,
            "unit": "ns",
            "range": "± 7846.938740474552"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 2457.5905532836914,
            "unit": "ns",
            "range": "± 89.81089006836386"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 23534.062306722004,
            "unit": "ns",
            "range": "± 153.09531571380148"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 248297.69246419272,
            "unit": "ns",
            "range": "± 5449.12587063403"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 5235.83425394694,
            "unit": "ns",
            "range": "± 252.7279289817353"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 47992.840799967445,
            "unit": "ns",
            "range": "± 191.41181321904526"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 479653.21915690106,
            "unit": "ns",
            "range": "± 9410.58445470051"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3242.483985900879,
            "unit": "ns",
            "range": "± 30.969583384745356"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 30616.840555826824,
            "unit": "ns",
            "range": "± 467.70056623469475"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 311441.9733072917,
            "unit": "ns",
            "range": "± 9872.278743609826"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 4175.929705301921,
            "unit": "ns",
            "range": "± 32.30940359517639"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 34913.22011311849,
            "unit": "ns",
            "range": "± 928.3710899977533"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 319556.04280598956,
            "unit": "ns",
            "range": "± 4117.92395174947"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 1978.007682800293,
            "unit": "ns",
            "range": "± 18.106245306595724"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 2441.3340695699058,
            "unit": "ns",
            "range": "± 13.23403686783964"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 18560.56121826172,
            "unit": "ns",
            "range": "± 153.252978567055"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 18765.504943847656,
            "unit": "ns",
            "range": "± 170.64873583782833"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 198352.19563802084,
            "unit": "ns",
            "range": "± 2303.4171049156816"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 191443.30924479166,
            "unit": "ns",
            "range": "± 1011.019254737546"
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
          "id": "983d93901ae9386be7451f61b72b67c253f6b83a",
          "message": "fix(pack): ship the generated per-package THIRD-PARTY-NOTICES.md only (#416)\n\nrelease.yaml (previous PR) now renders obj/THIRD-PARTY-NOTICES.md per project\nand Directory.Build.props packs it at the package root; the csproj items that\npacked the repository-wide file to the same path made `dotnet pack` fail with\nNU5118 (warning-as-error, duplicate file) - the release would have died at the\npack step (ETL-Abstractions#633). The PR pipeline never packs, so this only\nshows at release time.\n\nCo-authored-by: Claude Opus 5 <noreply@anthropic.com>",
          "timestamp": "2026-09-21T14:19:45-04:00",
          "tree_id": "60d635e91244bd9c07a4cb54955edb1809d2a1fc",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/983d93901ae9386be7451f61b72b67c253f6b83a"
        },
        "date": 1790014999685,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 7250.90929667155,
            "unit": "ns",
            "range": "± 31.06302460501654"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 67633.30116780598,
            "unit": "ns",
            "range": "± 495.0342458111273"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 614093.0550130209,
            "unit": "ns",
            "range": "± 4203.446635650712"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 4472.230359395345,
            "unit": "ns",
            "range": "± 6.916081818908197"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 44289.78466796875,
            "unit": "ns",
            "range": "± 261.6174233017707"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 494733.83902994794,
            "unit": "ns",
            "range": "± 3572.910391009726"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 8477.92024230957,
            "unit": "ns",
            "range": "± 51.08563084348475"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 83676.8711751302,
            "unit": "ns",
            "range": "± 240.23235823720853"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 829270.3483072916,
            "unit": "ns",
            "range": "± 2233.7548148109904"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 6024.396713256836,
            "unit": "ns",
            "range": "± 9.487706572572815"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 58491.27406819662,
            "unit": "ns",
            "range": "± 125.32044137997688"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 598661.9944661459,
            "unit": "ns",
            "range": "± 1034.58540557831"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 7081.386393229167,
            "unit": "ns",
            "range": "± 5.314519670651964"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 55687.17246500651,
            "unit": "ns",
            "range": "± 432.7377071603143"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 560070.16796875,
            "unit": "ns",
            "range": "± 1372.2021727421873"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3805.6851348876953,
            "unit": "ns",
            "range": "± 0.5813298033404548"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 4554.631744384766,
            "unit": "ns",
            "range": "± 16.293397310555285"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 35164.43916829427,
            "unit": "ns",
            "range": "± 62.54079174023397"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 36181.94226074219,
            "unit": "ns",
            "range": "± 144.4267404324031"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 391176.91748046875,
            "unit": "ns",
            "range": "± 1502.7716006060996"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 405981.9615885417,
            "unit": "ns",
            "range": "± 4937.4179379076195"
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
          "id": "81abff1453615940bd6af53a9610a5e87ed5f20b",
          "message": "build: adopt Wolfgang.Etl.Abstractions / ErrorPolicies / TestKit / TestKit.Xunit 0.26.0 (#417)\n\nDependency bump only: 0.26.0 declares IsTrimmable / IsAotCompatible on net8.0+\nand carries no public API change from 0.25.0, so no source changes are needed.\nRelease build and net10.0 test pass verified locally.\n\nCo-authored-by: Claude Opus 5 <noreply@anthropic.com>",
          "timestamp": "2026-09-22T09:05:13-04:00",
          "tree_id": "b2a69390358683a0626157583cde9bf37c66b6f1",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/81abff1453615940bd6af53a9610a5e87ed5f20b"
        },
        "date": 1790082541408,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 4434.98938369751,
            "unit": "ns",
            "range": "± 37.27248932553455"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 41656.18674723307,
            "unit": "ns",
            "range": "± 566.9626000911154"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 409606.5314941406,
            "unit": "ns",
            "range": "± 1102.4580488703912"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 2383.5018145243325,
            "unit": "ns",
            "range": "± 92.0348121395416"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 22257.143697102863,
            "unit": "ns",
            "range": "± 37.098109714876436"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 228785.43505859375,
            "unit": "ns",
            "range": "± 723.7251699878083"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 4787.623985290527,
            "unit": "ns",
            "range": "± 19.412399541417617"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 51394.21811930338,
            "unit": "ns",
            "range": "± 685.041645542842"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 496899.9303385417,
            "unit": "ns",
            "range": "± 31279.112371837702"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3100.5008252461753,
            "unit": "ns",
            "range": "± 17.730419728163515"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 29372.9379679362,
            "unit": "ns",
            "range": "± 36.51036737626913"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 320651.25406901044,
            "unit": "ns",
            "range": "± 6284.189530128243"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 4326.099822998047,
            "unit": "ns",
            "range": "± 332.04047661837325"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 32621.18940226237,
            "unit": "ns",
            "range": "± 217.9983905446416"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 313756.61116536456,
            "unit": "ns",
            "range": "± 616.8435583618888"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 1933.2411867777507,
            "unit": "ns",
            "range": "± 3.69602842757728"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 2635.974294026693,
            "unit": "ns",
            "range": "± 54.89654042043845"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 18928.977701822918,
            "unit": "ns",
            "range": "± 1135.7974092670227"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 18610.756154378254,
            "unit": "ns",
            "range": "± 1194.6848257793524"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 194966.27612304688,
            "unit": "ns",
            "range": "± 11775.336812427548"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 205323.28190104166,
            "unit": "ns",
            "range": "± 15603.76828791229"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "49699333+dependabot[bot]@users.noreply.github.com",
            "name": "dependabot[bot]",
            "username": "dependabot[bot]"
          },
          "committer": {
            "email": "noreply@github.com",
            "name": "GitHub",
            "username": "web-flow"
          },
          "distinct": true,
          "id": "2ba49f8a48c4dc820d7b46049b01b51690ea8907",
          "message": "build(deps): bump the github-actions group with 4 updates (#420)\n\nBumps the github-actions group with 4 updates: [github/codeql-action/upload-sarif](https://github.com/github/codeql-action), [benchmark-action/github-action-benchmark](https://github.com/benchmark-action/github-action-benchmark), [github/codeql-action/init](https://github.com/github/codeql-action) and [github/codeql-action/analyze](https://github.com/github/codeql-action).\n\n\nUpdates `github/codeql-action/upload-sarif` from 4.38.0 to 4.38.1\n- [Release notes](https://github.com/github/codeql-action/releases)\n- [Changelog](https://github.com/github/codeql-action/blob/main/CHANGELOG.md)\n- [Commits](https://github.com/github/codeql-action/compare/b96794f015dfd88f77b49b1c93e0fa7110f94c63...1c5b675653bb5c22dbe9b12b556ec555138e09fd)\n\nUpdates `benchmark-action/github-action-benchmark` from 1.22.1 to 1.22.2\n- [Release notes](https://github.com/benchmark-action/github-action-benchmark/releases)\n- [Changelog](https://github.com/benchmark-action/github-action-benchmark/blob/master/CHANGELOG.md)\n- [Commits](https://github.com/benchmark-action/github-action-benchmark/compare/v1.22.1...4322e5726e6334590d251fc4f92bec0efafc45dc)\n\nUpdates `github/codeql-action/init` from 4.38.0 to 4.38.1\n- [Release notes](https://github.com/github/codeql-action/releases)\n- [Changelog](https://github.com/github/codeql-action/blob/main/CHANGELOG.md)\n- [Commits](https://github.com/github/codeql-action/compare/b96794f015dfd88f77b49b1c93e0fa7110f94c63...1c5b675653bb5c22dbe9b12b556ec555138e09fd)\n\nUpdates `github/codeql-action/analyze` from 4.38.0 to 4.38.1\n- [Release notes](https://github.com/github/codeql-action/releases)\n- [Changelog](https://github.com/github/codeql-action/blob/main/CHANGELOG.md)\n- [Commits](https://github.com/github/codeql-action/compare/b96794f015dfd88f77b49b1c93e0fa7110f94c63...1c5b675653bb5c22dbe9b12b556ec555138e09fd)\n\n---\nupdated-dependencies:\n- dependency-name: github/codeql-action/upload-sarif\n  dependency-version: 4.38.1\n  dependency-type: direct:production\n  update-type: version-update:semver-patch\n  dependency-group: github-actions\n- dependency-name: benchmark-action/github-action-benchmark\n  dependency-version: 1.22.2\n  dependency-type: direct:production\n  update-type: version-update:semver-patch\n  dependency-group: github-actions\n- dependency-name: github/codeql-action/init\n  dependency-version: 4.38.1\n  dependency-type: direct:production\n  update-type: version-update:semver-patch\n  dependency-group: github-actions\n- dependency-name: github/codeql-action/analyze\n  dependency-version: 4.38.1\n  dependency-type: direct:production\n  update-type: version-update:semver-patch\n  dependency-group: github-actions\n...\n\nSigned-off-by: dependabot[bot] <support@github.com>\nCo-authored-by: dependabot[bot] <49699333+dependabot[bot]@users.noreply.github.com>",
          "timestamp": "2026-09-22T10:11:37-04:00",
          "tree_id": "944c492fa7455c6cd4e5fbcab6edb6fc40d6514e",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/2ba49f8a48c4dc820d7b46049b01b51690ea8907"
        },
        "date": 1790086571040,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 7275.307537078857,
            "unit": "ns",
            "range": "± 187.73713693379634"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 66584.24690755208,
            "unit": "ns",
            "range": "± 662.5858125902577"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 632893.2096354166,
            "unit": "ns",
            "range": "± 1423.9042802067913"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 4535.4146677653,
            "unit": "ns",
            "range": "± 33.834360901892516"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 45073.362548828125,
            "unit": "ns",
            "range": "± 556.1744709274714"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 476413.72672526044,
            "unit": "ns",
            "range": "± 785.5993030694825"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 8336.837819417318,
            "unit": "ns",
            "range": "± 138.8735171908832"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 80076.92907714844,
            "unit": "ns",
            "range": "± 402.77984516167913"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 806311.9599609375,
            "unit": "ns",
            "range": "± 2809.538993964712"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 6101.976028442383,
            "unit": "ns",
            "range": "± 56.32328102514818"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 57488.52606201172,
            "unit": "ns",
            "range": "± 645.1723414023644"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 596550.3225911459,
            "unit": "ns",
            "range": "± 22692.63173605278"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 7043.495211283366,
            "unit": "ns",
            "range": "± 24.18741728141228"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 57942.54384358724,
            "unit": "ns",
            "range": "± 145.12040439714107"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 551489.7613932291,
            "unit": "ns",
            "range": "± 1585.3449747963462"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3825.709416707357,
            "unit": "ns",
            "range": "± 14.902207657790791"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 4556.323893229167,
            "unit": "ns",
            "range": "± 25.22015591666903"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 35274.22017415365,
            "unit": "ns",
            "range": "± 178.6443094728441"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 37299.77956136068,
            "unit": "ns",
            "range": "± 165.8959515692453"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 403650.0953776042,
            "unit": "ns",
            "range": "± 1390.9562255814476"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 418519.708984375,
            "unit": "ns",
            "range": "± 1988.2929603059138"
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
          "id": "8abf7b980740a669ae01f52d331d6eac4193571f",
          "message": "Release v0.10.0 — net5–7 assets, notices fix, Abstractions 0.26.0 (#427)\n\n* release: v0.10.0\n\nMINOR from 0.9.0: the 0.25.0 Abstractions adoption brought the deprecated\nbase-stage setters and the bulk Increment* overloads into this package's\nsurface, and net5.0/net6.0/net7.0 assets now ship. Also the NU5118 notices\npack fix and the 0.26.0 Abstractions adoption (trim/AOT-compatible, no API\nchange). CHANGELOG assembled from the 10 fragments.\n\nVerified locally: dotnet pack at 0.10.0 (ApiCompat clean against the 0.9.0\nbaseline, one THIRD-PARTY-NOTICES.md in the package) and dotnet test -c\nRelease -f net10.0 (530 tests, 0 failures).\n\nCo-Authored-By: Claude Opus 5 <noreply@anthropic.com>\n\n* chore: pre-release clean-up (review findings)\n\n- README: the PR-build badge filtered on event=pull_request_target, which\n  pr.yaml stopped using when it moved to pull_request - the badge would have\n  read \"no status\" forever. Also documents JsonNamedStream /\n  JsonNamedDestination, two public records the feature table never mentioned.\n- compat-suppressions.txt: prune the 0.9.0 ISupportDryRun entry. 0.10.0 makes\n  0.9.0 the comparison baseline and 0.9.0 already lacks the interface, so the\n  rule can no longer match the break it was written for - only a future one of\n  the same shape. The file's own policy says to prune on exactly this event.\n- stryker-config.json: `break` was 0, so the mutation gate could never fail\n  (`low`/`high` are display-only). The last run scored 50.08 %, so the floor\n  becomes 45: a real ratchet that passes today and cannot silently regress.\n  Raising it toward the fleet's 65 is tracked separately.\n- Remove claude-sessions/ (session notes belong in the claude-sessions repo)\n  and the dead .github/license/ policy files, unused since license-audit.yaml\n  moved to .github/license-audit/.\n\nNo source or public-API change.\n\nCo-Authored-By: Claude Opus 5 <noreply@anthropic.com>\n\n* revert: keep .github/license/ — license-audit.yaml still reads it\n\nThe release PR removed .github/license/*, but license-audit.yaml points at\n.github/license/{packages-filter,licenseurl-mappings,allowed-licenses}.json.\nnuget-license threw FileNotFoundException on the missing mappings file and the\nstep's generic handler reported it as \"Disallowed license in Wolfgang.Etl.Json\",\nwhich is not what happened.\n\n.github/license-audit/ exists here but nothing consumes it yet; repointing the\nworkflow is a template change and does not belong in a release PR.\n\nCo-Authored-By: Claude Opus 5 <noreply@anthropic.com>\n\n---------\n\nCo-authored-by: Claude Opus 5 <noreply@anthropic.com>",
          "timestamp": "2026-09-22T15:35:28-04:00",
          "tree_id": "10b03e00df1c3fe777a93a2be38d3041601041c9",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/8abf7b980740a669ae01f52d331d6eac4193571f"
        },
        "date": 1790105943949,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6694.213905334473,
            "unit": "ns",
            "range": "± 2.660892622196014"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 58278.760803222656,
            "unit": "ns",
            "range": "± 129.62170261320472"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 563973.1051432291,
            "unit": "ns",
            "range": "± 2630.526426622572"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3430.221995035807,
            "unit": "ns",
            "range": "± 18.68427999227936"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 32843.76731363932,
            "unit": "ns",
            "range": "± 80.54831460574458"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 401887.3603515625,
            "unit": "ns",
            "range": "± 1235.6699325068519"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 7952.93217976888,
            "unit": "ns",
            "range": "± 16.821208123739964"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 73084.03405761719,
            "unit": "ns",
            "range": "± 115.8943679079655"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 725747.7649739584,
            "unit": "ns",
            "range": "± 1096.5149365695142"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 4854.978154500325,
            "unit": "ns",
            "range": "± 29.738537016768632"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 44834.46278889974,
            "unit": "ns",
            "range": "± 57.705046130382534"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 440098.8108723958,
            "unit": "ns",
            "range": "± 951.5840259192254"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6355.052312215169,
            "unit": "ns",
            "range": "± 2.9035246119938183"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 51988.64619954427,
            "unit": "ns",
            "range": "± 537.1435424131308"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 514634.6748046875,
            "unit": "ns",
            "range": "± 1219.712951780858"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3011.8169174194336,
            "unit": "ns",
            "range": "± 18.607681160849623"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 3698.835380554199,
            "unit": "ns",
            "range": "± 27.583114538302656"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 26219.051951090496,
            "unit": "ns",
            "range": "± 164.2415656192641"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 28134.148030598957,
            "unit": "ns",
            "range": "± 227.81902424921557"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 320449.04606119794,
            "unit": "ns",
            "range": "± 1383.3578795560272"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 323719.0983072917,
            "unit": "ns",
            "range": "± 1407.431613366659"
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
          "id": "f3885e0fa5018142b927eb85549b98888f0ff6f4",
          "message": "docs(changelog): take the template's fragment README (repo-template#630) (#432)\n\nTwo additions: a label added after the fragment check ran does not reach a re-run (the re-run replays the original payload - push a commit instead), and the list of src/ files that never need a fragment (nested .editorconfig, globalconfig/ruleset/DotSettings, PublicAPI baselines) - the rule this repository's changelog.ps1 already implements.\n\nCo-authored-by: Claude Fable 5.1 <noreply@anthropic.com>",
          "timestamp": "2026-09-22T19:00:56-04:00",
          "tree_id": "153a46f68d0dc482e11fb73a1caffa7cb88170b9",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/f3885e0fa5018142b927eb85549b98888f0ff6f4"
        },
        "date": 1790118276613,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6403.32612101237,
            "unit": "ns",
            "range": "± 50.52729623869041"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 57296.515157063805,
            "unit": "ns",
            "range": "± 134.94879838877532"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 562210.6839192709,
            "unit": "ns",
            "range": "± 744.7989543054252"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 3214.678784688314,
            "unit": "ns",
            "range": "± 5.7667535438894255"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 30069.9477335612,
            "unit": "ns",
            "range": "± 42.191499938015546"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonLineLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 354035.4443359375,
            "unit": "ns",
            "range": "± 581.1779273646027"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 6940.172487894694,
            "unit": "ns",
            "range": "± 8.25343712697284"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 64543.285807291664,
            "unit": "ns",
            "range": "± 24.368154437877116"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 670395.1028645834,
            "unit": "ns",
            "range": "± 1535.8820390607"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 4244.113049825032,
            "unit": "ns",
            "range": "± 6.824311859097099"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 41359.14042154948,
            "unit": "ns",
            "range": "± 117.07026690914546"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonMultiStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 394587.5960286458,
            "unit": "ns",
            "range": "± 846.5276776603988"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 10)",
            "value": 5666.6794509887695,
            "unit": "ns",
            "range": "± 8.448197323841152"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 100)",
            "value": 44703.4248046875,
            "unit": "ns",
            "range": "± 134.77719093598012"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamExtractorBenchmarks.ExtractAsync(ItemCount: 1000)",
            "value": 438652.3082682292,
            "unit": "ns",
            "range": "± 951.5155728000078"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 10)",
            "value": 2510.636328379313,
            "unit": "ns",
            "range": "± 1.6974099994440772"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 10)",
            "value": 3255.0889167785645,
            "unit": "ns",
            "range": "± 9.202488323502902"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 100)",
            "value": 22740.18924967448,
            "unit": "ns",
            "range": "± 57.35846500237262"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 100)",
            "value": 23454.34200032552,
            "unit": "ns",
            "range": "± 123.10126641175465"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync(ItemCount: 1000)",
            "value": 255839.2099609375,
            "unit": "ns",
            "range": "± 3454.9002298649266"
          },
          {
            "name": "Wolfgang.Etl.Json.Benchmarks.JsonSingleStreamLoaderBenchmarks.LoadAsync_CamelCase(ItemCount: 1000)",
            "value": 267019.20703125,
            "unit": "ns",
            "range": "± 1921.8903622161836"
          }
        ]
      }
    ]
  }
}