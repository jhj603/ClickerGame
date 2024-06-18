using System;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public float CurrentHP { get; private set; }
    public int CurrentCoin { get; private set; }

    private CharacterStatsHandler stats;
    private Animator animator;

    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;

    private float getCoinTime = 0f;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        stats = GetComponent<CharacterStatsHandler>();
    }

    private void Start()
    {
        CurrentHP = stats.CurrentStat.maxHealth;
        CurrentCoin = 0;
    }

    private void Update()
    {
        getCoinTime += Time.deltaTime;

        if (stats.CurrentStat.coinDelay < getCoinTime)
        {
            getCoinTime = 0f;
            CurrentCoin += stats.CurrentStat.coin;
        }
    }

    public void ChangeHealth(float change)
    {
        CurrentHP += change;
        CurrentHP = Mathf.Clamp(CurrentHP, 0, stats.CurrentStat.maxHealth);

        if (0f >= CurrentHP)
        {
            OnDeath?.Invoke();
        }
        if (0f <= change)
            OnHeal?.Invoke();
        else
        {
            OnDamage?.Invoke();
            animator.SetTrigger("Hit");
        }
    }
}