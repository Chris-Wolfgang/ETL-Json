# Reproducing the ETL-Json Build

This document explains how any third party can independently rebuild
`Wolfgang.Etl.Json` from source and verify that the assemblies published to
NuGet.org were built from the tagged commit.

---

## The guarantee

For a given commit built **with the same .NET SDK**, the following outputs are
byte-for-byte identical, **on Linux and Windows alike**:

- the compiled assembly `Wolfgang.Etl.Json.dll` (every target framework),
- its debug symbols `Wolfgang.Etl.Json.pdb` (every target framework).

> **The package files are not byte-reproducible.** The `.nupkg` / `.snupkg` are
> zip containers, and NuGet stamps every entry with the time the package was
> packed and writes platform-specific metadata into it. Packing the same commit
> twice, even on one machine, gives two different package files. What is inside
> them is stable: compare the assemblies and symbols, not the package file.

The `reproducible-build.yaml` workflow checks this: it builds every target
framework on `ubuntu-latest` and `windows-latest`, hashes each `.dll` and `.pdb`,
and **fails** if any hash differs between the two.

## How it is achieved

| Setting | Effect |
| --- | --- |
| `Deterministic` (SDK default `true`) | No timestamps or other run-specific data in the assembly. |
| `ContinuousIntegrationBuild=true` (`Directory.Build.props`, set when `CI=true`) | Enables `DeterministicSourcePaths`: absolute source paths become stable `/_/` tokens. Pass `-p:ContinuousIntegrationBuild=true` when you build locally. |
| `Microsoft.SourceLink.GitHub` + `EmbedUntrackedSources` | Maps source paths to the repository, so the PDB does not depend on where the code was checked out. |
| `NormalizeGeneratedLineEndings` target (`Directory.Build.targets`) | The SDK writes its generated sources (`*.AssemblyInfo.cs`, `*.AssemblyAttributes.cs`) with the OS newline. The PDB embeds them, so the PDB, and through its checksum the DLL, differed per OS. The target rewrites them to LF before compilation. |
| `Microsoft.NETFramework.ReferenceAssemblies` (`Directory.Build.props`, net4x TFMs) | The PDB records every referenced assembly. Windows would otherwise compile against the machine's installed targeting pack and Linux against this NuGet package, which are different builds. Referencing the package makes every OS use it. |

### What still has to match

- **The SDK, and the runtime it runs the compiler on.** The PDB records the
  compiler version and the runtime version that executed it, so use the SDK
  version recorded in the release build log. If several runtime patches of the
  same major version are installed, the newest one runs the compiler.
- **Targeting packs.** A machine with an old, separately installed targeting
  pack (for example `packs/Microsoft.NETCore.App.Ref/5.0.0` from a .NET 5 SDK
  installer) compiles that TFM against a different build of the reference
  assemblies than the NuGet package of the same version. Its output for that
  TFM differs from a machine that downloads the package.

---

## Prerequisites

| Tool | Version |
|------|---------|
| .NET SDK | The exact version recorded in the release build log |
| OS | Linux (Ubuntu 22.04+) or Windows 10+ |
| PowerShell | 7+ (for build scripts) |

---

## Steps

1. **Clone the repository at the release tag.**

   ```sh
   git clone https://github.com/Chris-Wolfgang/ETL-Json.git
   git -C ETL-Json checkout v<version>
   ```

2. **Install the .NET SDK** recorded in the release run's build log (also in
   the `HostEnvironmentInfo` field of the `.bom.json` attached to the release).

3. **Build in Release configuration, deterministically.**

   ```sh
   dotnet build ETL-Json/src/Wolfgang.Etl.Json/Wolfgang.Etl.Json.csproj \
     --configuration Release -p:ContinuousIntegrationBuild=true
   ```

4. **Extract the published package.** Download `Wolfgang.Etl.Json.<version>.nupkg`
   from NuGet.org (or the GitHub Release) and unzip it; the assemblies are under
   `lib/<tfm>/`. The `.pdb` files are in the `.snupkg` the same way.

5. **Compare the hashes per target framework.**

   Linux / macOS:
   ```sh
   sha256sum ETL-Json/src/Wolfgang.Etl.Json/bin/Release/net8.0/Wolfgang.Etl.Json.dll \
             published/lib/net8.0/Wolfgang.Etl.Json.dll
   ```

   Windows PowerShell:
   ```powershell
   Get-FileHash ETL-Json/src/Wolfgang.Etl.Json/bin/Release/net8.0/Wolfgang.Etl.Json.dll,
                published/lib/net8.0/Wolfgang.Etl.Json.dll -Algorithm SHA256
   ```

   If the hashes match, the published assembly was built from exactly this
   source. Repeat for the other target frameworks and for the `.pdb` files.

The `reproducible-build-manifest.json` attached to each GitHub Release lists the
SHA-256 of the released `.nupkg` / `.snupkg` files. Those hashes identify the
exact files that were released; they cannot be reproduced by re-packing,
because the package embeds its pack time (see above).

---

## Known non-determinism

- **Package files**: `.nupkg` / `.snupkg` differ on every pack (see above).
- **Restore**: every `PackageReference` pins an exact version and nuget.org
  packages are immutable, so a normal `dotnet restore` resolves the same
  packages as the release run. There is no lock file.

---

## Filing a Discrepancy

If your reproduced hash does not match the published manifest:

1. Open a GitHub issue titled
   **"Reproducible build mismatch — vX.Y.Z"**.
2. Include:
   - Your OS, .NET SDK version, and exact hash values
   - The mismatched artifact filename
   - The `reproducible-build-manifest.json` hash you were comparing against
3. Label the issue `security` — it will be triaged within 48 hours.

---

## Additional Verification

Beyond reproducibility, every release also provides:

- **SLSA provenance attestation** — verify with:
  ```sh
  gh attestation verify Wolfgang.Etl.Json.<version>.nupkg \
    --owner Chris-Wolfgang --repo ETL-Json
  ```
- **CycloneDX SBOM** — attached as `Wolfgang.Etl.Json.bom.json` to each
  GitHub Release.

See [`SECURITY.md`](../SECURITY.md) for full details on supply-chain
verification.
