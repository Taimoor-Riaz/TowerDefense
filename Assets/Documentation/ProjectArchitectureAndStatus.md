# Competitive Survival Merge Defense
## Revised Technical Audit, Repair Plan & MVP Development Proposal

**Document type:** Client decision package (updated audit)  
**Source of truth:** `Full Game Design Concept V2.docx` (MVP GDD)  
**Codebase reviewed:** `Assets/Script`, `Assets/_Prefabs`, `Assets/Scenes`, `Assets/Resources`, `Assets/Documentation`  
**Original audit date:** 2026-08-01  
**Revision date:** 2026-08-03  
**Revision reason:** Client feedback — product direction, continue vs rebuild, repair plan, roadmap, commercial proposal, acceptance criteria  

---

## 1. Executive Summary

This project is a **playable gameplay prototype / vertical slice**, not a finished MVP.

The **core match loop** is real and valuable: 6-unit deck → summon → merge → auto-combat → waves/bosses → leak ends the match. Art, prefabs, combat feedback, and hub UI shells already exist.

Relative to the GDD, overall completion remains approximately **35–40%**. The largest gaps are: global active-ability loadout, enemy/boss special mechanics, progression economy, leagues/indirect PvP, and durable meta systems.

### Client decision answer (short)

**Yes — the project can realistically be repaired and completed into a proper MVP.**  
We recommend **continue + partial refactor**, **not** a full greenfield rebuild. Keep the match foundation; rebuild the ability product model and content data layer; implement missing GDD systems on top.

| Area | Rough GDD MVP completion |
|------|--------------------------|
| Core match loop (board/summon/merge/mana/combat) | **65–75%** |
| Unit roster + first-pass combat identities | **55–65%** |
| Active ability system (GDD: 2 of 8 loadout abilities) | **10–15%** (wrong model today) |
| Enemy roster + special mechanics | **20–30%** |
| Meta progression (levels, cards, runes, leagues) | **5–15%** |
| Competitive layer (replay opponent, medals) | **~0–5%** |
| Meta UI shells (shop/gift/quest/events/rank) | **25–40%** UI / **~5%** real systems |
| **Overall GDD MVP** | **~35–40%** |

---

## 2. Product Direction (Locked — Option A)

Per client direction, development will follow **Option A** from the GDD.

### 2.1 Confirmed product rules

1. **6-unit deck selection before the match** (already present; keep and harden).
2. **2 global active abilities selected before the match** from an unlocked ability pool.
3. **Active abilities are independent of the selected tower.** In-match Ability / Ability_2 buttons cast the pre-selected global actives only.
4. **Unit abilities remain passive / identity-based** (on-hit, aura, interval, drag-specials such as Shapeshifter / Light Fairy). Units do not own the global active loadout.
5. The current **tower-selected ability button system** (`HeroAbilityButtonController` binding to the selected tower) is **temporary prototype UX only**. It is **not** the final implementation and must not be expanded as the product ability system.

### 2.2 Separation of systems going forward

| Layer | Purpose | Examples |
|-------|---------|----------|
| Global Active Abilities | Pre-match loadout (pick 2), manual cast, cooldowns | Meteor Strike, Frost Nova, Mana Surge, Radiant Cleanse, Arcane Overclock, Execution Sigil (+ Gravity Well / Barrier Pulse if schedule allows) |
| Unit Identity / Passive Skills | Always on or auto-triggered from the unit | Frost slow on hit, Fire AoE on hit, Poison DoT, Enchantress aura, Gold Spirit mana ticks, Zeus chain on hit |
| Board Utilities | Special drag interactions | Shapeshifter copy, Light Fairy upgrade |

### 2.3 Prototype ability system — explicit status

- **Keep:** unit passive scripts and combat hooks (`Tower.AttackHit`, status effects, VFX pools) as the base for identity skills.
- **Do not expand:** tower-selection → ability-button binding as a feature path.
- **Replace for MVP:** pre-match 2-ability picker + global cast controller wired to the existing two battle HUD buttons.

---

## 3. Continue vs Rebuild — Clear Recommendation

### 3.1 Direct answers

| Question | Answer |
|----------|--------|
| Should the project continue? | **Yes.** |
| Should it be partially refactored? | **Yes — targeted refactor of ability UX, content data layer, enemy behaviors, and progression foundations.** |
| Should anything be rebuilt? | **Yes — rebuild the active-ability product system and enemy special-mechanic framework.** Do **not** rebuild the whole game from zero. |

### 3.2 Recommendation summary

**Continue the existing project with a controlled partial refactor + selective rebuild.**

A full rewrite would discard working summon/merge/combat, 11 heroes × 6 levels of prefabs, routes, combat juice, audio, and hub art/UI — months of recoverable value. The current codebase is prototype-shaped, but the **match spine is solid enough** to carry an MVP if Option A is enforced early.

### 3.3 Systems to KEEP (as-is or with light cleanup)

- Board / cells / placement ownership (`TowerBoardCell`, `BoardTower`)
- Summon loop (`SummonManager`)
- Merge rules & drag (`MergeManager`, `BoardTowerDrag`)
- Mana + rising summon cost (`ManaManager`)
- Core tower combat + bullets (`Tower`, `Bullet`)
- Enemy path following + leak end condition
- Wave timing shell (`WaveBossManager` as orchestrator, later data-fed)
- Combat feedback / pooling / status presentation
- `UnitData` ScriptableObjects + level 1–6 prefab pipeline
- Unit passive identity abilities (reclassified as passives; remove manual-global misuse)
- Battle flow gating (`BattleFlowState`)
- Game over stats presentation shell
- Scenes, art, animations, audio assets, hub visual canvases

### 3.4 Systems to REFACTOR

- Pre-match flow: extend deck select → also select **2 global actives**
- Ability button controller: stop binding to selected tower; bind to match loadout
- `TowerAbilityBase` split: passive unit skills vs global active ability runtime
- Currency / mana separation (Water vs in-match mana clarity)
- Naming map: GDD names ↔ prefabs ↔ roles (Zeus/Thunder Oracle, Princess/Shield Priestess, etc.)
- `WaveBossManager`: move compositions/scaling into ScriptableObject data
- Hub UI controllers: replace fake timers/`Debug.Log` claims with real state where MVP requires it
- Singleton coupling hotspots that block Option A wiring

### 3.5 Systems to REBUILD (new or replace)

- **Global Active Ability system** (catalog, loadout, cast, cooldown, upgrades later)
- **Enemy behavior framework** (shield, split, mana leech, elite casts, boss phases)
- **GDD enemy/boss mechanic set** (even simplified telegraph versions)
- **Progression foundation** (Gold/Gems/Unit Cards/Runes, local save model)
- **Minimal league + opponent comparison** (async/fake replay first is acceptable for MVP)
- **Unit out-of-match progression slice** (start with Skill 1–3 unlocks; full 6-skill tree can be post-MVP if needed)

---

## 4. Repair Plan (Practical)

### 4.1 Fix first (immediate / Phase 0–1)

1. Lock Option A in docs, scenes, and sprint backlog (done in this revision).
2. Freeze naming map: GDD unit/enemy/boss ↔ project assets.
3. Inventory all current “manual” unit abilities and mark each as Passive-only vs Temporary-prototype-button.
4. Disable or isolate tower-selected ability binding from the product path (keep code only if needed for temporary demos).
5. Stabilize BattleScene / Main_UI flow, null refs, summon lock timing, and Android smoke build checklist.
6. Separate wallet currencies from in-match mana conceptually in code and UI labels.

### 4.2 Refactor next

1. Pre-match loadout UI/data: 6 units + 2 actives.
2. Global ability cast service + HUD buttons.
3. Reclassify existing unit scripts as identity/passives (Skill 1 equivalents).
4. Introduce ScriptableObjects for Active Abilities, Enemy Definitions, Wave Tables.
5. Slim `Enemy` / `Tower` responsibilities where new systems attach.

### 4.3 Remain as-is for now

- Merge randomness rules and max level 6
- Core projectile combat feel
- Existing combat VFX/audio presentation
- Hub page navigation shells (Home/Heroes/Guild/Rank) as visual containers
- Gift/Quest/Event cosmetic canvases (unless a milestone explicitly wires real rewards)
- Full ranked backend / live multiplayer (out of MVP repair scope)

### 4.4 Do NOT expand until redesigned

- Tower-selected ability UX as a product feature
- More one-off ability MonoBehaviours duplicated across all 6 level prefabs without a data/trigger plan
- Fake meta claims presented as finished progression
- Full 6-skill-per-unit endgame trees before Skill 1–3 + global actives exist
- Live-ops season tools, IAP economy depth, or authoritative anti-cheat ranked servers

---

## 5. MVP Roadmap (Phased)

Target MVP cut (aligned with GDD §19, schedule-aware):

- Core combat loop hardened
- 11 units with clear passive identity (Skill 1 focus)
- Pre-match 6 units + 2 global actives
- Launch ability pool: **6 core actives** first (Meteor Strike, Frost Nova, Mana Surge, Radiant Cleanse, Arcane Overclock, Execution Sigil); Gravity Well + Barrier Pulse as stretch
- Enemy set: 6 normals + 3 elites + 3 bosses with **readable special mechanics** (simplified OK)
- Simple account economy + unit/ability upgrade slice
- Basic medals/league + opponent comparison (recorded/fake curve acceptable for v1)
- Android playable build

### Phase 1 — Project stabilization / cleanup (Week 1–2)

- Naming map freeze
- Option A enforcement in backlog
- Build/scene stability, null-safety, input edge cases
- Currency/mana cleanup
- Prototype ability path quarantined
- Baseline Android build

### Phase 2 — Ability system correction (Week 3–5)

- ActiveAbility ScriptableObject catalog
- Pre-match pick 2 actives
- Global cast + cooldown UI on existing Ability buttons
- Implement 6 launch actives
- Keep unit passives; remove dependence on selected tower for globals

### Phase 3 — Enemy & boss mechanics (Week 5–8)

- EnemyDefinition + behavior components
- Normal mechanics: shield, split/swarm pressure, mana leech, speed/tank roles
- Elites: suppressor, rank drain, herald aura (simplified telegraphs)
- Bosses: tank check, summon/adds check, debuff/disable check
- Wave composition tables

### Phase 4 — Unit system improvements (Week 7–9)

- Role clarity pass for all 11 units
- Passive identity tuning + missing role coverage (boss-killer / protection)
- Optional merge-level thresholds for Skill-style spikes (lightweight)
- Prefab/data consistency across levels 1–6

### Phase 5 — Progression foundation (Week 9–11)

- Inventory: Gold, Gems, Unit Cards, Runes
- Local save model (JSON/Scriptable save; replace critical PlayerPrefs usage)
- Out-of-match unit level + unlock Skill 1–3
- Active ability upgrade ladder (milestone levels)
- Medals + league movement from match outcome vs opponent score
- Opponent comparison v1 (preauthored or recorded curve)

### Phase 6 — UI integration (Week 11–13)

- Pre-match loadout screens polished
- In-match HUD: mana, summon, 2 actives, wave/boss, opponent comparison readout
- Heroes upgrade UI wired to real data
- Rank/league UI wired to real medals
- Game over rewards grant real inventory
- Stub-only pages clearly marked or lightly wired

### Phase 7 — Android testing & polish (Week 13–16)

- Device performance (URP mobile), heat/frame budget
- Touch UX, safe area, resolution variants
- Soft-lock/crash pass
- Balance smoke tests
- Acceptance pack + known-issues list
- MVP candidate build

Phases intentionally overlap slightly where dependencies allow (enemies can start while abilities finish).

---

## 6. Development Proposal

### 6.1 Estimated timeline

| Plan | Duration | Notes |
|------|----------|-------|
| **Recommended MVP repair** | **14–16 weeks** | Option A + enemies + progression foundation + Android polish |
| Aggressive / reduced content | 10–12 weeks | 6 actives, simplified elites/bosses, thinner progression |
| Full GDD maximalist (all 8 actives + full 6-skill trees + deep live-ops) | 20+ weeks | Not recommended as first MVP gate |

Assumes focused delivery with the team in §7, timely art/design answers, and no major scope adds mid-flight.

### 6.2 Estimated development cost

Costs below are **engineering + implementation QA** estimates for the Recommended MVP repair plan. Art creation beyond existing assets, original music packs, backend servers, store publishing fees, and user-acquisition are excluded unless added by change order.

| Item | Estimate |
|------|----------|
| Stabilization + Option A ability correction | 20–25% of total |
| Enemy/boss mechanics + unit identity pass | 25–30% |
| Progression + leagues/opponent v1 | 20–25% |
| UI integration + Android polish + acceptance | 20–25% |
| **Total effort** | **~560–720 productive hours** |
| **Commercial estimate band** | **USD $14,000 – $22,000** |

**Rate basis used for the band:** blended delivery rate approximately **USD $25–$35/hr** (Unity gameplay + systems + mobile QA).  
Final quote can be fixed-price per milestone after kickoff scope sign-off. If the client prefers a different commercial region/rate card, the hour estimate still applies and price scales linearly.

### 6.3 Milestone breakdown, deliverables, inclusion / exclusion

#### Milestone M0 — Kickoff & Stabilization (Week 1–2)

**Deliverables**
- Revised audit accepted (this document)
- Naming map spreadsheet/doc
- Option A technical design note (active vs passive)
- Stable debug Android build of current prototype
- Risk register + sprint plan

**Included:** cleanup, build pipeline check, quarantine of tower-selected ability UX  
**Excluded:** new GDD features beyond stabilization

**Acceptance criteria**
- Project opens/builds without blocking errors
- Match loop playable on at least one Android device/profile
- Naming map signed off
- Tower-selected ability path documented as non-product / disabled in MVP scenes

#### Milestone M1 — Ability System Correction (Week 3–5)

**Deliverables**
- Pre-match: select 6 units + 2 global actives
- 6 launch active abilities implemented and castable
- HUD buttons independent of tower selection
- Unit passives still function without owning global buttons

**Included:** Meteor Strike, Frost Nova, Mana Surge, Radiant Cleanse, Arcane Overclock, Execution Sigil (v1 values)  
**Excluded:** full ability upgrade tree UI depth; Gravity Well/Barrier Pulse unless ahead of schedule

**Acceptance criteria**
- Feature selectable in-game (pre-match loadout)
- Both actives visible, cooldown-readable, and testable in battle
- Casting does not require selecting a tower
- Works on Android build
- QA verified against a written ability checklist

#### Milestone M2 — Enemy & Boss Mechanics (Week 5–8)

**Deliverables**
- Behavior framework + data hooks
- 6 normal + 3 elite + 3 boss mechanic passes (simplified telegraphs allowed)
- Wave composition driven by data tables

**Included:** readable specials that force targeting/CC/boss-DPS/hybrid decisions  
**Excluded:** every advanced secondary boss behavior from long-form GDD lore text

**Acceptance criteria**
- Each roster entry has an identifiable mechanic or role pressure
- Elites/bosses are visually/priority distinct
- Match remains completable and failable by leak
- Android build + QA checklist signed

#### Milestone M3 — Unit System Improvements (Week 7–9)

**Deliverables**
- Role clarity for all 11 units
- Passive identity balance pass
- Prefab/data consistency for levels 1–6
- Lightweight merge-threshold hooks if required by design

**Included:** Skill-1 quality bar for MVP  
**Excluded:** full permanent Skill 4–6 endgame kit for every unit

**Acceptance criteria**
- Each unit has a clear combat purpose in-game
- No broken level prefab references for 1–6
- Android + QA verified

#### Milestone M4 — Progression Foundation (Week 9–11)

**Deliverables**
- Inventory currencies: Gold, Gems, Unit Cards, Runes
- Local durable save
- Unit upgrade + ability upgrade slice
- Medals/leagues v1 + opponent comparison v1
- Rewards granted from match end

**Included:** earnable progression loop sufficient for retention demo  
**Excluded:** full live backend, season pass, IAP catalog depth, guild systems

**Acceptance criteria**
- Progression values persist between sessions
- Upgrades visibly affect match power or unlocks
- League/medal state changes from match outcome
- Android + QA verified

#### Milestone M5 — UI Integration (Week 11–13)

**Deliverables**
- Integrated pre-match, battle HUD, heroes, rank, results screens
- Opponent comparison visible in/after match
- Stub pages either wired minimally or clearly non-MVP

**Included:** GDD-critical UX readability goals for combat and loadout  
**Excluded:** full shop/gift/quest/event live-ops depth

**Acceptance criteria**
- Critical flows selectable and understandable without debug keys
- Visible and testable end-to-end
- Android + QA verified

#### Milestone M6 — Android Testing & MVP Candidate (Week 13–16)

**Deliverables**
- Performance/polish pass
- Crash/soft-lock pass
- Balance smoke
- MVP candidate build + known issues + acceptance pack

**Included:** shippable prototype-quality Android MVP candidate  
**Excluded:** store ASO, soft-launch live-ops staffing, backend ranked integrity hardening

**Acceptance criteria**
- MVP feature set from signed scope playable on target Android devices
- No Critical/Blocker open bugs without waiver
- QA verified + client playtest sign-off

### 6.4 What’s included in the Recommended MVP package

- Option A ability architecture
- 6 global actives (8th-pool stretch optional)
- 11 units with passive identities
- Enemy roster mechanics for MVP set
- Progression foundation + leagues/opponent v1
- Android MVP candidate build
- Documentation / acceptance packs per milestone

### 6.5 What’s excluded (unless added by change order)

- Full greenfield rewrite
- Real-time multiplayer
- Production backend / anti-cheat ranked servers
- Complete 6-skill trees for all units at launch depth
- Deep monetization/IAP/shop live economy
- Full quest/event/gift live-ops
- New full art/animation overhaul beyond wiring existing assets
- UA/marketing/store publishing operations

### 6.6 Payment structure (proposed)

| Stage | % | Trigger |
|-------|---|---------|
| Kickoff / M0 start | **30%** | Contract + kickoff |
| M1 complete (Ability correction) | **20%** | Acceptance criteria met |
| M2–M3 complete (Enemies + Units) | **20%** | Acceptance criteria met |
| M4–M5 complete (Progression + UI) | **20%** | Acceptance criteria met |
| M6 MVP candidate accepted | **10%** | Final acceptance / waiver list signed |

Fixed-price milestone contracts preferred. Scope changes use written change orders with timeline/cost impact before work starts.

---

## 7. Team

### 7.1 Who would work on the project

Proposed delivery team for the repair-to-MVP plan:

| Role | Responsibility |
|------|----------------|
| **Lead Unity / Gameplay Engineer** | Architecture decisions, Option A ability system, combat/enemy frameworks, code reviews |
| **Gameplay / Systems Programmer** | Active abilities, enemy behaviors, progression, ScriptableObject content wiring |
| **UI / Client Integration** | Pre-match loadout, battle HUD, heroes/rank/results integration |
| **Mobile QA (shared)** | Android builds, acceptance checklists, regression, device smoke tests |

Team size flexes 2–3 active engineers most weeks, with QA concentrated around milestone exits.

### 7.2 Unity / mobile game experience

The delivery team works in **Unity (URP)** for **portrait mobile** games, including:

- Session-based midcore loops
- Touch-first controls and safe-area UI
- Android build, profiling, and device validation
- Prototype-to-MVP hardening (turning vertical slices into shippable systems)

### 7.3 ScriptableObjects & data-driven architecture experience

Relevant practice for this project’s refactor:

- Content catalogs via ScriptableObjects (units, abilities, enemies, waves, rewards)
- Separating **data / rules / presentation / persistence**
- Editor tooling to batch-wire prefabs and validate content
- Avoiding “one unique script per content piece” where shared triggers suffice

### 7.4 Domain systems experience (matched to this GDD)

| Domain | Relevance |
|--------|-----------|
| Ability systems | Global cooldowns, loadouts, targeted/AoE casts, upgrade ladders |
| Status effects | Slow, poison, stun, cleanse, attack-speed debuffs, shields |
| Progression | Soft currencies, cards/runes, unlock gates, persistent save |
| URP mobile optimization | Draw-call/overdraw awareness, pooling, UI canvas hygiene, frame-budget passes |

This is exactly the skill mix required to repair the current prototype into Option A without discarding the working match spine.

---

## 8. Acceptance Criteria (Standard Gate for Every Milestone)

Unless a milestone lists extra rules, **every milestone must meet all of the following:**

1. **Feature selectable in-game** through normal player UI (no hidden debug-only path for core acceptance features).
2. **Visible and testable** — QA can observe the feature, reproduce it from a written checklist, and capture evidence (video/screenshots/logs).
3. **Works on Android build** — verified on at least one agreed target device/API profile for that milestone.
4. **QA verified** — checklist signed; Critical/Blocker defects fixed or explicitly waived by client in writing.
5. **Scope honesty** — excluded items are not presented as complete; stubs remain labeled if still present.
6. **No regression of the core loop** — summon, merge, combat, leak-fail still function.

Milestone-specific criteria are listed under §6.3.

---

## 9. Project Structure & Architecture Snapshot (Reference)

### 9.1 Folder map

```
Assets/
├── Animation/
├── Design/
├── Documentation/      ← this audit package
├── Editor/
├── Plans/
├── Resources/
├── Scenes/             BattleScene, Main_UI
├── Script/             gameplay & UI modules
├── Settings/           URP
├── Sprite/
├── Tile_Asset_Map/
└── _Prefabs/           Units, Enemies, HeroAbilities, Bullets
```

### 9.2 Current architecture style

Classic Unity **MonoBehaviour + singleton managers**, partial ScriptableObject data (`UnitData`), event hooks for kills/leaks/hits. Suitable for a prototype; MVP needs a stronger **data-driven** layer for actives, enemies, and waves (refactor path above), not a full engine rewrite.

### 9.3 Current vs Option A (important)

| Topic | Today | Option A target |
|-------|-------|-----------------|
| Pre-match | 6 units | 6 units + 2 global actives |
| Ability buttons | Bind to selected tower | Bind to match loadout only |
| Unit scripts | Mixed passive + manual | Passive / identity only |
| Enemy specials | Mostly stats/visuals | Behavior components + data |
| Progression | Thin PlayerPrefs wallet | Cards/runes/levels/leagues foundation |

---

## 10. GDD Feature Matrix (Condensed)

| Feature | Status |
|---------|--------|
| 5×5 board, summon, merge Lv1–6, mana, leak end | **Done / strong** |
| Pre-match 6-unit deck | **Done** |
| Pre-match 2 global actives | **Missing — rebuild under Option A** |
| Unit passive identities | **Partial — keep & improve** |
| 8 GDD global actives | **Missing** |
| Enemy specials / elites / designed bosses | **Partial assets, missing mechanics** |
| Leagues / opponent comparison | **Missing / placeholders** |
| Economy (cards/runes/upgrades) | **Missing / thin** |
| Meta UI shells | **Partial visuals, mostly stubs** |

---

## 11. Final Recommendation

### Can this project be repaired into a proper MVP, or should it be rebuilt?

**Recommendation: Repair and complete — do not full-rebuild.**

### Technical reasoning

1. **The expensive, high-risk gameplay spine already works.** Board, summon, merge, combat, pathing, leak-fail, unit prefab ladder, and combat presentation would all be recreated at high cost in a rewrite, with no guarantee of equal feel.
2. **The failures are concentrated and redesignable.** The wrong ability product model, missing enemy behaviors, and stubbed progression are serious — but they are **bounded subsystems**, not proof that the whole codebase is unusable.
3. **Option A gives a clear correction path.** By quarantining tower-selected abilities and building global actives + passives separation, the team stops digging the prototype hole and starts matching the GDD.
4. **A full rebuild would reset calendar time** while reintroducing the same design risks (merge RNG, balance, mobile performance) that this prototype has already partially solved.
5. **MVP is reachable in one focused program (about 14–16 weeks)** if scope stays honest: 6 launch actives, simplified enemy telegraphs, progression foundation, Android candidate — not every live-ops system in the long GDD.

### Conditions for success

- Client locks Option A (done in this document).
- No expansion of the temporary tower-selected ability UX.
- Milestone acceptance is enforced (§8).
- Stretch goals (extra actives, deep skill trees, live backend) stay behind the MVP gate.

### Bottom line for the client

**Continue development on this project.**  
**Partially refactor** architecture and content pipelines.  
**Rebuild** the active-ability system and enemy-mechanic framework.  
**Keep** the match foundation and content assets.  

This is the fastest credible path to a GDD-aligned, Android-playable MVP.

---

## Appendix A — File Quick Reference

| Concern | Start here |
|---------|------------|
| Match phase | `Assets/Script/Battle/BattleFlowState.cs` |
| Waves / bosses / leak | `Assets/Script/Enemy/Boss/WaveBossManager.cs` |
| Summon | `Assets/Script/Summon/SummonManager.cs` |
| Merge | `Assets/Script/Merge/MergeManager.cs` |
| Unit combat | `Assets/Script/Unit/Tower.cs`, `Bullet.cs` |
| Unit data | `Assets/Script/Unit/UnitData.cs` |
| Current abilities (prototype) | `Assets/Script/Abilities/*` |
| Tower-selected buttons (temporary) | `Assets/Script/Abilities/HeroAbilityButtonController.cs` |
| Enemy status/combat | `Assets/Script/Enemy/Enemy.cs` |
| Deck select | `Assets/Script/Deck/DeckSelectionManager.cs` |
| Wallet | `Assets/Script/Currency/CurrencyManager.cs` |
| Hub UI | `Assets/Script/MainMenu/MainMenuUI.cs` |

## Appendix B — Document control

| Version | Date | Notes |
|---------|------|-------|
| v1 | 2026-08-01 | Initial architecture/status audit |
| v2 | 2026-08-03 | Client feedback revision — Option A lock, continue/repair recommendation, repair plan, phased MVP roadmap, commercial proposal, team, acceptance criteria, final recommendation |
