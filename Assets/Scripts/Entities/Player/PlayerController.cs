using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private CharacterStatsHandler stats;

    private bool attacking;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        stats = GetComponent<CharacterStatsHandler>();
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && (InputActionPhase.Started == context.phase))
        {
            Attack();
        }
    }

    private bool Attack()
    {
        if (!attacking)
        {
            attacking = true;
            animator.SetTrigger("Attack");

            GameManager.Instance.CurrentEnemy.Condition.ChangeHealth(-stats.CurrentStat.attackData.power);

            StartCoroutine(CanAttack());

            return true;
        }

        return false;
    }

    private IEnumerator CanAttack()
    {
        yield return new WaitForSeconds(stats.CurrentStat.attackData.delay);

        attacking = false;
    }
    
    public void AutoAttack()
    {
        StartCoroutine(AutoAttackCor());
    }

    private IEnumerator AutoAttackCor()
    {    
        int count = 0;

        while (true)
        {
            yield return null;

            if (10 <= count)
                break;

            if (Attack())
                ++count;
        }

        UIManager.Instance.ResetAutoBtn();
    }
}
