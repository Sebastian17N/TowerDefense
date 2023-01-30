using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private float attackRange = 3f;
    public Enemy CurrentEnemyTarget { get; set; }
    public TurretUpgrade TurretUpgrade { get; set; }
    public float AttackRange => attackRange;
    private bool _gameStarted;
    private List<Enemy> _enemies;
    private void Start()
    {
        _gameStarted = true;
        _enemies = new List<Enemy>();
        TurretUpgrade = GetComponent<TurretUpgrade>();

    }
    private void Update()
    {
        GetCurrentEnemyTarget();
        RotateTowardsTarget();
    }
    private void GetCurrentEnemyTarget()
    {
        if (_enemies.Count <= 0)
        {
            CurrentEnemyTarget = null;
            return;
        }
        CurrentEnemyTarget = _enemies[0];

    }
    private void RotateTowardsTarget()
    {
        if (CurrentEnemyTarget == null)
            return;

        Vector3 taretPos = CurrentEnemyTarget.transform.position - transform.position;
        float angle = Vector3.SignedAngle(transform.up, taretPos, transform.forward);
        transform.Rotate(0, 0, angle);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy newEnemy = collision.GetComponent<Enemy>();
            _enemies.Add(newEnemy);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (_enemies.Contains(enemy))
            {
                _enemies.Remove(enemy);
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (!_gameStarted)
        {
            GetComponent<CircleCollider2D>().radius = attackRange;
        }
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
