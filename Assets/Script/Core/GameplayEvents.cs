using System;
using UnityEngine;

/// <summary>
/// Typed gameplay event bus. Publish-only — no gameplay logic.
/// New passives/actives should subscribe here instead of coupling to managers.
/// </summary>
public static class GameplayEvents
{
    public static event Action BattleStarted;
    public static event Action BattleEnded;
    public static event Action<int> WaveStarted;
    public static event Action<Enemy> EnemySpawned;
    public static event Action<Enemy> EnemyKilled;
    public static event Action<Enemy> EnemyReachedEnd;
    public static event Action<UnitData, int> UnitSummoned;
    public static event Action<UnitData, int> UnitMerged;
    public static event Action<Tower, Enemy, float> DamageDealt;
    public static event Action<Enemy> BossSpawned;
    public static event Action<Enemy, string> StatusApplied;

    public static void RaiseBattleStarted() => BattleStarted?.Invoke();
    public static void RaiseBattleEnded() => BattleEnded?.Invoke();
    public static void RaiseWaveStarted(int waveIndex) => WaveStarted?.Invoke(waveIndex);
    public static void RaiseEnemySpawned(Enemy enemy) => EnemySpawned?.Invoke(enemy);
    public static void RaiseEnemyKilled(Enemy enemy) => EnemyKilled?.Invoke(enemy);
    public static void RaiseEnemyReachedEnd(Enemy enemy) => EnemyReachedEnd?.Invoke(enemy);
    public static void RaiseUnitSummoned(UnitData unit, int level) => UnitSummoned?.Invoke(unit, level);
    public static void RaiseUnitMerged(UnitData unit, int level) => UnitMerged?.Invoke(unit, level);
    public static void RaiseDamageDealt(Tower source, Enemy target, float damage) =>
        DamageDealt?.Invoke(source, target, damage);
    public static void RaiseBossSpawned(Enemy boss) => BossSpawned?.Invoke(boss);
    public static void RaiseStatusApplied(Enemy target, string statusId) =>
        StatusApplied?.Invoke(target, statusId);

    /// <summary>Clears all subscribers (e.g. domain reload / tests). Prefer not calling in production matches.</summary>
    public static void ClearAll()
    {
        BattleStarted = null;
        BattleEnded = null;
        WaveStarted = null;
        EnemySpawned = null;
        EnemyKilled = null;
        EnemyReachedEnd = null;
        UnitSummoned = null;
        UnitMerged = null;
        DamageDealt = null;
        BossSpawned = null;
        StatusApplied = null;
    }
}
