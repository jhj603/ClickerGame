using UnityEngine;

[CreateAssetMenu(fileName = "DefaultAttacKSO", menuName = "Data/Attacks/Default", order = 0)]
public class DefaultAttackSO : ScriptableObject
{
    public float delay;
    public int power;
    public LayerMask target;
}
