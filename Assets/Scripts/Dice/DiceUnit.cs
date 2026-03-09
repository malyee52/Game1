using UnityEngine;

public class DiceUnit : MonoBehaviour
{
    [SerializeField] private DiceData data;

    private float attackCooldown;

    public void Initialize(DiceData diceData)
    {
        data = diceData;
    }

    private void Update()
    {
        if (data == null)
        {
            return;
        }

        attackCooldown -= Time.deltaTime;
        if (attackCooldown > 0f)
        {
            return;
        }

        Enemy target = FindTargetInRange();
        if (target == null)
        {
            return;
        }

        target.TakeDamage(data.damage);
        attackCooldown = 1f / Mathf.Max(0.01f, data.attackSpeed);
    }

    private Enemy FindTargetInRange()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Enemy nearest = null;
        float nearestDist = float.MaxValue;

        foreach (Enemy enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist <= data.range && dist < nearestDist)
            {
                nearest = enemy;
                nearestDist = dist;
            }
        }

        return nearest;
    }

    private void OnDrawGizmosSelected()
    {
        if (data == null)
        {
            return;
        }

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, data.range);
    }
}
