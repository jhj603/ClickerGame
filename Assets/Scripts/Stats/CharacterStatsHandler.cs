using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Unity.Burst;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterStatsHandler : MonoBehaviour
{
    [SerializeField] private CharacterStat baseStat;
    [SerializeField] private float minAttackDelay;
    [SerializeField] private int minAttackPower;

    [SerializeField] private float minSpeed;
    [SerializeField] private int minMaxHealth;

    public CharacterStat CurrentStat { get; private set; }

    private List<CharacterStat> statModifiers = new List<CharacterStat>();

    private Func<float, float, float> operation;

    private void Awake()
    {
        CurrentStat = new CharacterStat();

        UpdateCharacterStat();

        if (null != baseStat.attackData)
        {
            baseStat.attackData = Instantiate(baseStat.attackData);

            CurrentStat.attackData = Instantiate(baseStat.attackData);
        }
    }

    private void UpdateCharacterStat()
    {
        ApplyStatModifier(baseStat);

        foreach (CharacterStat stat in statModifiers.OrderBy(inStat => inStat.statsChangeType))
            ApplyStatModifier(stat);
    }

    private void ApplyStatModifier(CharacterStat modifier)
    {
        operation = modifier.statsChangeType switch
        {
            StatsChangeType.Add => (current, change) => current + change,
            StatsChangeType.Multiple => (current, change) => current * change,
            _ => (current, change) => change
        };

        UpdateBasicStats(operation, modifier);
        UpdateAttackStats(operation, modifier);
    }

    private void UpdateBasicStats(Func<float, float, float> operation, CharacterStat modifier)
    {
        CurrentStat.maxHealth = Mathf.Max((int)operation(CurrentStat.maxHealth, modifier.maxHealth), minMaxHealth);
        CurrentStat.coin = Mathf.Max((int)operation(CurrentStat.coin, modifier.coin), 0);
        CurrentStat.coinDelay = Mathf.Max(operation(CurrentStat.coinDelay, modifier.coinDelay), 0f);
    }

    private void UpdateAttackStats(Func<float, float, float> operation, CharacterStat modifier)
    {
        if ((null == CurrentStat.attackData) || (null == modifier.attackData))
            return;

        var currentAttackData = CurrentStat.attackData;
        var modAttackData = modifier.attackData;

        currentAttackData.power = Mathf.Max((int)operation(currentAttackData.power, modAttackData.power), minAttackPower);
        currentAttackData.delay = Mathf.Max(operation(currentAttackData.delay, modAttackData.delay), minAttackDelay);
    }
}