window.BENCHMARK_DATA = {
  "lastUpdate": 1784146322269,
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
      }
    ]
  }
}