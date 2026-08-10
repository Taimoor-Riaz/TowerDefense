# Milestone 1 — Risk Register

**Date:** Day 5 handoff  
**Scope:** Foundation complete; risks for M2+ delivery

| ID | Risk | Likelihood | Impact | Mitigation | Owner |
|----|------|------------|--------|------------|-------|
| R1 | Ability rewrite (Option A globals) takes longer than estimated | Med | High | SO stubs + catalog already exist; quarantine done; implement cast service only — do not re-expand tower binding | Eng |
| R2 | Dual EventSystem / AudioListener glitches with additive Hub↔Battle | Med | Med | Hub unloads while Battle loads; smoke after every SceneFlow change | Eng |
| R3 | ~~Config Content vs Resources drift~~ | — | — | **Closed** — single `GameConfigRegistry`; Resources only holds registry | Eng |
| R4 | Naming drift continues (Zeus/Princess folders) | Low | Med | NamingMap frozen; UI uses GDD names; folder renames deferred | Design+Eng |
| R5 | Android device variance (low-end 30 FPS feel) | Med | Med | Quality tiers auto by RAM; Force Low in Editor for testing | Eng+QA |
| R6 | Enemy/wave still scene-serialized — SO tables unused until M2/M3 | High | Med | WaveTable/EnemyDefinition stubs ready; migrate WaveBossManager early in enemy milestone | Eng |
| R7 | Fake meta UI mistaken for finished progression | Med | High | Scope honesty in acceptance; stubs labeled; no fake “complete” claims | Lead |
| R8 | APK not yet smoke-tested on physical device | Med | High | Follow Android_BuildSmoke.md before calling M1 closed with client | QA |
| R9 | Singleton coupling blocks global ability wiring | Med | Med | Prefer GameServices / explicit refs for new cast path | Eng |
| R10 | Scope creep into full 6-skill trees / live-ops in M2 | Med | High | M2 backlog = 6 launch actives only; stretch behind gate | Lead |

## Open actions before client “M1 paid/accepted”

1. Produce Development APK and complete [Android_BuildSmoke.md](Android_BuildSmoke.md) on one device.  
2. Client/design sign [NamingMap.md](NamingMap.md) approval table.  
3. Confirm no Critical/Blocker from Day 3 Editor smoke.
