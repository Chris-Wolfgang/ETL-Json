# Reproducing the ETL-Json Build

This document explains how any third party can independently reproduce the
`Wolfgang.Etl.Json` NuGet package from source and verify that it matches
the artifact published to NuGet.org.

---

## Prerequisites

| Tool | Version |
|------|---------|
| .NET SDK | 10.0.x (exact patch pinned in release run) |
| OS | Linux (Ubuntu 22.04+) or Windows 10+ |
| PowerShell | 7+ (for build scripts) |

The `release.yaml` CI job runs on `windows-latest`. For a byte-identical
reproduction, use **Windows**. Linux builds produce functionally identical
output but may have minor differences in PE timestamp handling; CI logs this
as an informational warning, not a failure.

---

## Steps

1. **Clone the repository at the release tag.**

   ```sh
   git clone https://github.com/Chris-Wolfgang/ETL-Json.git
   git -C ETL-Json checkout v0.3.0   # substitute the version you are verifying
   ```

2. **Install the .NET SDK.**

   Use the same SDK version recorded in the release run. The expected SDK
   version is listed in each GitHub Release's build log and in the
   `HostEnvironmentInfo` field of any `.bom.json` attached to the release.

3. **Restore and build in Release configuration.**

   ```sh
   dotnet restore ETL-Json/
   dotnet build ETL-Json/ --no-restore --configuration Release
   ```

4. **Pack the NuGet package.**

   ```sh
   dotnet pack ETL-Json/src/Wolfgang.Etl.Json/Wolfgang.Etl.Json.csproj \
     --no-build --configuration Release --output ./repro-output
   ```

5. **Compute the SHA-256 hash of the output.**

   Linux / macOS:
   ```sh
   sha256sum ./repro-output/*.nupkg ./repro-output/*.snupkg
   ```

   Windows PowerShell:
   ```powershell
   Get-ChildItem ./repro-output/*.nupkg, ./repro-output/*.snupkg |
     ForEach-Object { Get-FileHash $_.FullName -Algorithm SHA256 } |
     Select-Object Hash, Path
   ```

6. **Compare against the manifest.**

   Each GitHub Release includes a `reproducible-build-manifest.json` asset.
   Download it and compare:

   ```json
   {
     "version": "0.3.0",
     "artifacts": [
       { "name": "Wolfgang.Etl.Json.0.3.0.nupkg", "sha256": "..." },
       { "name": "Wolfgang.Etl.Json.0.3.0.snupkg", "sha256": "..." }
     ]
   }
   ```

   If your hashes match, the build is reproducible. If they differ, see
   [Filing a Discrepancy](#filing-a-discrepancy) below.

---

## Known non-determinism

- **PE timestamps**: On Windows, `dotnet build` may embed a timestamp in the
  PE header. The `<Deterministic>true</Deterministic>` property in
  `Directory.Build.props` suppresses this for cross-run comparison on the
  same OS. Cross-OS comparison (Windows build vs. Linux build) may still
  show a byte difference in the PE section; this is logged as a warning by
  the `reproducible-build.yaml` CI check.

- **Restore caching**: `dotnet restore` must use the same NuGet packages as
  the CI run. The NuGet lock file is checked in; use `--locked-mode` to
  force exact restoration:

  ```sh
  dotnet restore ETL-Json/ --locked-mode
  ```

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
