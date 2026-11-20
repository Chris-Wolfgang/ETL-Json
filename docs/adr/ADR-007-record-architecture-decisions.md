# ADR-007 Record architecture decisions

- **Status**: Accepted
- **Date**: 2026-11-17

---

## Context

`Wolfgang.Etl.Json` makes several choices that the code alone does not explain: it uses `System.Text.Json` and not Newtonsoft.Json, every stage has a second set of overloads for source-generated type information, the assembly version is pinned while the package version moves, and blocking APIs are banned by an analyzer. Without the reason next to it, each of these reads as an accident that a later change might "tidy up".

The repository has kept such records in `docs/adr` since ADR-001. Two of the first three have since been superseded, and the way that was recorded (the old ADR kept in place, with its status changed and an update section added) is the practice. The practice itself was never written down, which is why this record carries a later number than the decisions it describes.

---

## Decision

We record each significant, non-obvious design decision as an Architecture Decision Record in `docs/adr`.

- One Markdown file per decision, named `ADR-NNN-short-title.md` with the next free number, copied from [TEMPLATE.md](TEMPLATE.md).
- Every ADR has a row in [index.md](index.md).
- An ADR starts as **Proposed** and becomes **Accepted** when the decision is agreed.
- An ADR is not deleted or rewritten when the decision changes. It stays in place with its status changed to **Superseded**, and says what replaced it.

---

## Considered Options

### Option A: Leave the reasoning in commit messages and pull requests

- Pro: No extra file to write.
- Con: The reason is far from the code and has to be dug out of history, if it was written down at all.

### Option B: Keep ADRs next to the code (chosen)

- Pro: The reason is versioned with the code.
- Pro: Reversing a decision is explicit, and the earlier reasoning survives.
- Con: A decision worth recording costs a few minutes to write up.

---

## Consequences

**Easier:**

- A contributor can find out why something is the way it is without reading the history.

**Harder:**

- A record written after the fact, as this one and ADR-004 to ADR-006 are, reconstructs the reasoning and can miss part of it.
- The index has to be kept in step with the files by hand.
