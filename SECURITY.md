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
3. The workflow builds, tests, packs, and pushes to NuGet.org using the
   `NUGET_API_KEY` secret stored in the repository's GitHub Actions secrets.
4. Packages are signed by the workflow runner (not by a local certificate).

The only credential that can publish a new package version is `NUGET_API_KEY`.
No other path to NuGet.org exists in this repository.

### If `NUGET_API_KEY` is compromised

1. **Revoke the key immediately.** Log in to <https://www.nuget.org/account/apikeys>
   and delete the affected key. GitHub Actions will fail on the next release
   attempt until a new key is configured.
2. **Rotate and re-add the key.** Create a new API key scoped to
   `Wolfgang.Etl.Json` (push + unlist only; do not grant delete). Add it to
   the repository's Actions secrets as `NUGET_API_KEY`.
3. **Audit recent publishes.** Check <https://www.nuget.org/packages/Wolfgang.Etl.Json/>
   for any unexpected versions published during the window of compromise.
4. **Unlist (do not delete) any malicious version.** On nuget.org, navigate to
   the affected version → **Manage package** → **Unlist**. Unlisting stops new
   consumers from finding the version while preserving audit history. NuGet
   does not allow hard-deleting a published version.
5. **Publish a patched release.** Increment the version, document the incident
   in the release notes, and publish a clean version as soon as possible.

### If the GitHub account is compromised

A GitHub account compromise is broader: an attacker could modify workflows,
push malicious commits, or alter release artifacts.

1. Revoke all active OAuth tokens and Personal Access Tokens in GitHub account
   settings.
2. Enable or re-verify two-factor authentication.
3. Review and revert any recent workflow or source changes.
4. Follow the `NUGET_API_KEY` rotation steps above, since the key may have
   been read from Actions secrets.
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
