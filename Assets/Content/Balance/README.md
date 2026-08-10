# Balance

**Canonical asset:** `Assets/Content/Balance/GameBalanceConfig.asset`  
**Access:** `GameServices.Instance.Config.GameBalance` (via `GameConfigRegistry`)

Do **not** duplicate this under Resources. Change `startingMana`, summon costs, etc. here only.

Summon cost is battle state on `ManaManager` (resets each battle). It is not a wallet currency.
