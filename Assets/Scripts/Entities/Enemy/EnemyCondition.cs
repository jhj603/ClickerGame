using System;
using UnityEngine;

public class EnemyCondition : MonoBehaviour
{
    public float CurrentHP { get; private set; }

    private CharacterStatsHandler stats;
    private Animator animator;

    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        stats = GetComponent<CharacterStatsHandler>();
    }

    private void Start()
    {
        CurrentHP = stats.CurrentStat.maxHealth;
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