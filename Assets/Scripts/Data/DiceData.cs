using UnityEngine;

[CreateAssetMenu(fileName = "DiceData", menuName = "TD/Data/Dice")]
public class DiceData : ScriptableObject
{
    public int damage = 1;
    public float attackSpeed = 1f;
    public float range = 2.5f;
}
