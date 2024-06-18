using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator animator;
    private CharacterStatsHandler stats;

    private bool attacking;

    private float attackTime = 0f;

    [SerializeField] private float attackDelay;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        stats = GetComponent<CharacterStatsHandler>();
    }

    private void Update()
    {
        attackTime += Time.deltaTime;

        if (attackDelay < attackTime)
        {
            attackTime = 0f;
            Attack();
        }
    }

    private void Attack()
    {
        if (!attacking)
        {
            attacking = true;
            animator.SetTrigger("Attack");

            GameManager.Instance.Player.Condition.ChangeHealth(-stats.CurrentStat.attackData.power);

            StartCoroutine(CanAttack());
        }
    }

    private IEnumerator CanAttack()
    {
        yield return new WaitForSeconds(stats.CurrentStat.attackData.delay);

        attacking = false;
    }
}