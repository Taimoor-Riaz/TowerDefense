# Android Build & Smoke (M1 Day 5)

## Player Settings baseline (applied)

| Setting | Value |
|---------|--------|
| Company | CompetitiveSurvival |
| Product | Merge Defense |
| Package | `com.competitivesurvival.mergedefense` |
| Min SDK | 25 |
| Target SDK | 34 |
| Bundle version | 1.0 / versionCode 1 |
| Orientation | Portrait (project default) |
| First scene | `Bootstrap` |

Validate in Editor: **Game → Foundation → Validate Android Player Settings**

## Build steps (Unity Editor)

1. Switch platform: **File → Build Settings → Android → Switch Platform**
2. Confirm scenes:
   - `Assets/Scenes/Bootstrap.unity`
   - `Assets/Scenes/Main_UI.unity`
   - `Assets/Scenes/BattleScene.unity`
3. **Player Settings** — confirm package id above; use Debug (Development Build) for smoke.
4. **Build** APK to `Build_Apk/MergeDefense_M1.apk`
5. Install on device: `adb install -r Build_Apk/MergeDefense_M1.apk`

> APK binary is produced on your machine (Unity + Android SDK). This repo delivers settings, quality service, changelog, and checklist — run the build locally to generate the artifact.

## Device smoke checklist

- [ ] App launches to Hub via Bootstrap (no black screen hang)
- [ ] Console/logcat shows `[MobileQuality] Applied Low|Mid|High ...`
- [ ] Battle loads additively; summon/merge/combat work
- [ ] Footer mana updates on kill with `TopUIRoot` hidden; summon cost 50 → 60
- [ ] Enemies spawn / exit at route `Rp` points; footer stays clear of notch / home indicator
- [ ] Ability buttons do **not** bind to selected tower (Option A quarantine)
- [ ] Leak → Game Over → Exit returns to Hub
- [ ] No Critical crash on one full match loop
- [ ] On a low-RAM device, tier is Low (30 FPS target) unless prefs override

## Known non-blockers (M1)

- Global actives not implemented yet (M2)
- Enemy specials incomplete
- Development/debug signing only for smoke
