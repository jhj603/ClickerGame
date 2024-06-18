public class GameManager : Singleton<GameManager>
{
    public Player Player { get; set; }
    public Enemy CurrentEnemy { get; set; }
    public ObjectPool ObjectPool { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        ObjectPool = GetComponent<ObjectPool>();
    }
}