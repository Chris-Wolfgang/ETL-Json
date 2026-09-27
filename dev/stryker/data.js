window.BENCHMARK_DATA = {
  "lastUpdate": 1790490083973,
  "repoUrl": "https://github.com/Chris-Wolfgang/ETL-Json",
  "entries": {
    "Mutation score": [
      {
        "commit": {
          "author": {
            "name": "Chris Wolfgang",
            "username": "Chris-Wolfgang",
            "email": "210299580+Chris-Wolfgang@users.noreply.github.com"
          },
          "committer": {
            "name": "GitHub",
            "username": "web-flow",
            "email": "noreply@github.com"
          },
          "id": "a5845d1f42527fe7a95acf91780736ec556d1d0a",
          "message": "ci(release): gate the release on SourceLink symbol integrity (#439)\n\n* ci(release): gate the release on SourceLink symbol integrity\n\nAdds the two release-time SourceLink jobs from the ETL-SqlBulkCopy pilot, so a\nmis-packed or un-ingested symbol package fails the release instead of reaching\nconsumers as a silently broken debugging experience.\n\nsourcelink-snupkg (pre-publish, blocking) derives the expected TFM set from the\nruntime .nupkg -- every lib/<tfm>/ shipping the assembly must have a matching PDB\nin the .snupkg -- and checks each PDB is portable. Added to publish-nuget's needs,\nso a failure stops the push to NuGet rather than being found afterwards. Merely\nasserting \"at least one PDB exists\", as Etl-DbClient's original does, passes while\na single TFM is silently missing its symbols.\n\nsourcelink-symbol-server-smoke (post-publish) polls nuget.org's SSQP endpoint with\ndotnet-symbol for up to 30 minutes. Terminal job: it does not gate artifact attach,\nso slow ingestion cannot hold up the release assets.\n\ndotnet-symbol is pinned in .config/dotnet-tools.json rather than installed\nunversioned, and invoked as `dotnet dotnet-symbol` -- `dotnet tool restore`\ninstalls into the manifest and does NOT put the tool on PATH.\n\nNo emitted shell expression contains a backslash: the awk field form is used\ndeliberately, after a sed backreference lost its escape and wrote a 0x01 control\ncharacter into a workflow during the pilot.\n\nBoth files are protected, so this stays homogeneous for the guard. Verified: the\nworkflow parses and the needs wiring reads back as intended; the manifest parses\nas JSON.\n\nCo-Authored-By: Claude Opus 5 <noreply@anthropic.com>\n\n* ci(release): make the SourceLink jobs package-agnostic and identical fleet-wide\n\nDerives the expected symbol set from the package contents instead of a\nhard-coded package name: each lib/<tfm>/<assembly>.dll in the runtime .nupkg\nmust have a matching .pdb in the .snupkg, compared as <tfm>/<assembly> pairs.\n\nTwo reasons. It handles a repo that ships more than one package with no change,\nwhich ETL-Abstractions needs (it ships four). And with the package name gone the\ntwo job bodies are byte-identical in every repo -- verified by hashing them\nacross all six, which yields a single distinct body -- so the jobs are now\ncopy-paste uniform and trivial to keep in sync or fold into the template later.\n\nAlso strictly more thorough than the previous form: it checks every shipped\nassembly, not just the one named package, and the portable-PDB magic check now\ncovers every .pdb in the .snupkg rather than one filename.\n\nVerified on real packages: the pair form reports 8 expected and 8 present for a\nhealthy package with nothing missing.\n\nCo-Authored-By: Claude Opus 5 <noreply@anthropic.com>\n\n* ci(release): fix actionlint findings and probe every shipped package\n\nTwo fixes, both found after the first push.\n\nactionlint failed on all seven PRs, same root cause: shellcheck flagged the\nresult-reporting lines with SC2086 (unquoted $missing / $expected / $actual) and\nSC2116 (the useless `$(echo ...)` used to squash newlines onto one line). They\nnow echo the quoted variables directly over several lines, which is both\nlint-clean and easier to read in a log. Reproduced locally with actionlint 1.7.12\n-- CI's pinned version -- plus shellcheck 0.10.0, since actionlint only runs the\nshellcheck rules when shellcheck is on PATH; without it the workflow lints clean\nand the failure is invisible.\n\nThe post-publish smoke probed only ONE package and picked the lexicographically\nlast DLL, which my comment wrongly described as \"the newest TFM\" (sorting puts\nnetstandard2.0 last, not net10.0). It now collects one probe DLL per shipped\npackage and requires every one to be served, which matters because symbol\ningestion is per symbol package -- a repo shipping four packages had three of\nthem unverified. All packages share a single 30-minute budget rather than one\neach, so adding packages does not multiply worst-case wall clock. The output\ndirectory is emptied before each probe: otherwise a PDB fetched for an earlier\npackage would make the check pass for every package after it.\n\nThe TFM of the probe DLL is irrelevant -- the symbol server is queried by the\nsignature embedded in the assembly -- so the comment now says that instead of\nclaiming a preference it was not implementing.\n\nVerified: actionlint + shellcheck clean in all seven repos, YAML parses, and the\ntwo job bodies still hash to a single distinct body across all seven.\n\nCo-Authored-By: Claude Opus 5 <noreply@anthropic.com>\n\n---------\n\nCo-authored-by: Claude Opus 5 <noreply@anthropic.com>",
          "timestamp": "2026-09-25T23:07:52Z",
          "url": "https://github.com/Chris-Wolfgang/ETL-Json/commit/a5845d1f42527fe7a95acf91780736ec556d1d0a"
        },
        "date": 1790490076591,
        "tool": "customBiggerIsBetter",
        "benches": [
          {
            "name": "Mutation score",
            "value": 50.08,
            "unit": "%"
          }
        ]
      }
    ]
  }
}