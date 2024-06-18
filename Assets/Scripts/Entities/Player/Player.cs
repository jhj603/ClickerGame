using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController Controller { get; private set; }
    public PlayerCondition Condition { get; private set; }
    private void Awake()
    {
        GameManager.Instance.Player = this;

        Controller = GetComponent<PlayerController>();
        Condition = GetComponent<PlayerCondition>();
    }
}