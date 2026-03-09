using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event Action<Enemy> Spawned;
    public static event Action<Enemy> Died;
    public static event Action<Enemy> Escaped;

    [SerializeField] private EnemyData data;

    private int currentHp;
    private float moveSpeed;
    private Transform[] waypoints;
    private int currentWaypointIndex;
    private bool initialized;

    public void Initialize(EnemyData enemyData, Transform[] path)
    {
        data = enemyData;
        waypoints = path;

        currentHp = data.hp;
        moveSpeed = data.moveSpeed;
        currentWaypointIndex = 0;
        initialized = true;

        if (waypoints != null && waypoints.Length > 0)
        {
            transform.position = waypoints[0].position;
            currentWaypointIndex = 1;
        }

        Spawned?.Invoke(this);
    }

    private void Update()
    {
        if (!initialized || waypoints == null || waypoints.Length == 0)
        {
            return;
        }

        if (currentWaypointIndex >= waypoints.Length)
        {
            ReachGoal();
            return;
        }

        Transform target = waypoints[currentWaypointIndex];
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) <= 0.01f)
        {
            currentWaypointIndex++;
        }
    }

    public void TakeDamage(int damage)
    {
        if (!initialized)
        {
            return;
        }

        currentHp -= damage;
        if (currentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Died?.Invoke(this);
        Destroy(gameObject);
    }

    private void ReachGoal()
    {
        Escaped?.Invoke(this);
        Destroy(gameObject);
    }
}
