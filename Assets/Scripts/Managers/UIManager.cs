using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TextMeshProUGUI coinText;

    [SerializeField] private Slider playerHPGauge;
    [SerializeField] private TextMeshProUGUI playerHPText;

    [SerializeField] private Slider enemyHPGauge;
    [SerializeField] private TextMeshProUGUI enemyHPText;

    [SerializeField] private Button autoAttackBtn;

    PlayerCondition playerCondition;
    CharacterStatsHandler playerStat;

    EnemyCondition enemyCondition;
    CharacterStatsHandler enemyStat;

    private void Start()
    {
        playerCondition = GameManager.Instance.Player.Condition;
        playerStat = GameManager.Instance.Player.GetComponent<CharacterStatsHandler>();

        enemyCondition = GameManager.Instance.CurrentEnemy.Condition;
        enemyStat = GameManager.Instance.CurrentEnemy.GetComponent<CharacterStatsHandler>();

        playerCondition.OnDamage += UpdatePlayerHPUI;
        playerCondition.OnHeal += UpdatePlayerHPUI;

        enemyCondition.OnDamage += UpdateEnemyHPUI;
        enemyCondition.OnHeal += UpdateEnemyHPUI;

        UpdatePlayerHPUI();
        UpdateEnemyHPUI();
    }

    private void Update()
    {
        coinText.text = GameManager.Instance.Player.Condition.CurrentCoin.ToString();
    }

    private void UpdatePlayerHPUI()
    {
        playerHPGauge.value = playerCondition.CurrentHP / playerStat.CurrentStat.maxHealth;

        playerHPText.text = $"{playerCondition.CurrentHP} / {playerStat.CurrentStat.maxHealth}";
    }

    private void UpdateEnemyHPUI()
    {
        enemyHPGauge.value = enemyCondition.CurrentHP / enemyStat.CurrentStat.maxHealth;

        enemyHPText.text = $"{enemyCondition.CurrentHP} / {enemyStat.CurrentStat.maxHealth}";
    }

    public void OnAutoAttack()
    {
        GameManager.Instance.Player.Controller.AutoAttack();

        autoAttackBtn.interactable = false;
    }

    public void ResetAutoBtn()
    {
        autoAttackBtn.interactable = true;
    }
}