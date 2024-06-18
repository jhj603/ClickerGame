using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyController Controller { get; private set; }
    public EnemyCondition Condition { get; private set; }

    private void Awake()
    {
        GameManager.Instance.CurrentEnemy = this;

        Controller = GetComponent<EnemyController>();
        Condition = GetComponent<EnemyCondition>();
    }
}