using UnityEngine;

public enum StatsChangeType
{
    Add,
    Multiple,
    Override
}

[System.Serializable]
public class CharacterStat
{
    public StatsChangeType statsChangeType;

    [Range(0, 100)] public int maxHealth;
    public int coin;
    public float coinDelay;
    public DefaultAttackSO attackData;
}