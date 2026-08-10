# Runtime config

| Asset | Role |
|-------|------|
| `GameConfigRegistry.asset` | Single source of truth — refs all other configs |
| `SceneFlowConfig.asset` | Hub/Battle scene names |

Runtime loads `Resources/GameConfigRegistry.asset`, which points at these Content assets (and Balance/Quality/Abilities/Waves). Do not copy individual configs into Resources.
