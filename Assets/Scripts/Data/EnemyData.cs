using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "TD/Data/Enemy")]
public class EnemyData : ScriptableObject
{
    public int hp = 10;
    public float moveSpeed = 2f;
}
