type: internal

The OpenSSF Scorecard workflow's top-level `permissions:` is `read-all` again (Scorecard's documented setting, kept in step with repo-template), which supersedes the 0.8.1 entry about replacing it with `contents: read`; each job still grants only the write scopes it needs.
