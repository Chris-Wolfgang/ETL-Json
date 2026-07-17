# Security Policy

## Reporting a Vulnerability

If you discover a security vulnerability, please follow these steps:

1. **Do not** create a public issue on this repository.
2. In the top navigation of this repository, click the **Security** tab.
3. In the top right, click the **Report a vulnerability** button.
4. Fill out the provided form with:
   - A description of the vulnerability
   - Steps to reproduce the issue
   - Potential impact
   - Suggested fix (if you have one)

## Response Timeline

We will acknowledge your report within 48 hours and provide an estimated timeline for a fix.

## Thank You

Your help is greatly appreciated!
Responsible disclosure of security vulnerabilities helps protect our entire community.

---

## Release Path & Compromise Scope

This section documents the release pipeline so that the blast radius of a
credential compromise is understood, and so that recovery steps can be executed
quickly under stress.

### How packages reach NuGet

1. A GitHub Release is published (manually, by the maintainer).
2. The `release.yaml` workflow fires on the `release: published` event.
3. The workflow builds, tests, packs, then publishes via **OIDC Trusted
   Publishing**: the `NuGet/login` action exchanges the workflow's short-lived
   GitHub OIDC token for a temporary NuGet API key (valid ~1 hour). No static
   API key secret is stored in this repository.
4. The Trusted Publishing policy on nuget.org is scoped to:
   owner `Chris-Wolfgang`, repository `ETL-Json`, workflow `release.yaml`.

There is no stored `NUGET_API_KEY` secret. The only path to publish is through
a GitHub Actions run of `release.yaml` on this specific repository.

### If the release pipeline is compromised

Because there is no long-lived credential to steal, the attack surface is the
GitHub Actions environment itself during a release run.

1. **Audit recent publishes.** Check <https://www.nuget.org/packages/Wolfgang.Etl.Json/>
   for any unexpected versions published during the incident window.
2. **Revoke the Trusted Publishing policy** if you suspect the OIDC trust has
   been abused. Log in to nuget.org → **Account** → **Publish** → **API Keys**
   → find the Trusted Publisher entry and remove it. Re-add once the workflow
   and repository are confirmed clean.
3. **Unlist (do not delete) any malicious version.** On nuget.org, navigate to
   the affected version → **Manage package** → **Unlist**. Unlisting stops new
   consumers from finding the version while preserving audit history. NuGet
   does not allow hard-deleting a published version.
4. **Publish a patched release.** Increment the version, document the incident
   in the release notes, and publish a clean version as soon as possible.

### If the GitHub account is compromised

A GitHub account compromise is broader: an attacker could modify workflows,
push malicious commits, or trigger a release run that publishes via OIDC.

1. Revoke all active OAuth tokens and Personal Access Tokens in GitHub account
   settings.
2. Enable or re-verify two-factor authentication.
3. Review and revert any recent workflow or source changes.
4. Revoke and re-add the Trusted Publishing policy on nuget.org (see above).
5. Contact GitHub Support at <https://support.github.com> if you believe the
   account itself was taken over.

### Notifying downstream consumers

If a malicious package was published and consumed:

1. Publish a GitHub Security Advisory via **Security → Advisories → New draft**.
   This triggers a Dependabot alert for all consumers using the affected version
   range.
2. Use the following template for the advisory description:

   > **Severity:** [Critical / High / Medium / Low]
   >
   > A compromised credential was used to publish `Wolfgang.Etl.Json` version
   > X.Y.Z between [start time UTC] and [end time UTC]. Consumers who installed
   > this version during that window should treat it as untrusted and upgrade to
   > X.Y.Z+1 or later immediately. No legitimate source changes were included in
   > the compromised version.
   >
   > **Action required:** run `dotnet list package --outdated` and upgrade.

3. Unlist the affected version on nuget.org (see above) so `dotnet add package`
   stops resolving it by default.

### NuGet support contact

For urgent package-level issues (e.g., a compromised version that must be
quarantined faster than the unlist process allows), contact NuGet support at
<https://www.nuget.org/policies/Contact> and select **Report abuse**.

---

## Supply-Chain Verification

This section documents how consumers can verify that a published NuGet package
was genuinely built from this repository by the `release.yaml` workflow.

### SBOM (Software Bill of Materials)

Every release attaches a `Wolfgang.Etl.Json.bom.json` CycloneDX SBOM to the
GitHub Release assets. It lists every NuGet dependency and its version.

To audit the dependency graph:

1. Download `Wolfgang.Etl.Json.bom.json` from the GitHub Release page.
2. Open it in any CycloneDX-compatible tool (e.g.,
   [CycloneDX CLI](https://github.com/CycloneDX/cyclonedx-cli),
   [OWASP Dependency-Track](https://dependencytrack.org/)).
3. Cross-reference component licenses and versions against your own policy.

### SLSA Build Provenance Attestation

Every release generates a **SLSA Build Level 2** provenance attestation signed
via [Sigstore](https://sigstore.dev/) keyless signing through GitHub's OIDC
identity. The attestation proves that the `.nupkg` / `.snupkg` files were
produced by the `release.yaml` workflow at a specific commit in this
repository — with no opportunity for an attacker to inject artifacts without
leaving a verifiable audit trail.

**To verify a package:**

1. Install the [GitHub CLI](https://cli.github.com/) (v2.49.0+).
2. Download the `.nupkg` from NuGet or the GitHub Release page.
3. Run:

   ```sh
   gh attestation verify Wolfgang.Etl.Json.<version>.nupkg \
     --owner Chris-Wolfgang \
     --repo ETL-Json
   ```

4. A successful verification prints the signing workflow, commit SHA, and
   Sigstore transparency log entry. Failure means the artifact cannot be
   traced to a legitimate release run.

### Package Signing (deferred)

NuGet package signing via a code-signing certificate (or Sigstore `cosign`) is
**not yet implemented**. SLSA attestation via `gh attestation verify` provides
an equivalent supply-chain integrity guarantee for most scenarios.

Once implemented, consumers will be able to run `dotnet nuget verify` to check
the embedded signature independently of the GitHub CLI.
