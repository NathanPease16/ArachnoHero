using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    chasing,
    attacking
}

public class Enemy : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float health;
    [SerializeField] private float damage;
    [SerializeField] private float chaseDistance;
    [SerializeField] private float attackDistance;
    [SerializeField] private float speed;

    [Header("States")]
    private EnemyState state;

    [Header("References")]
    private new Rigidbody rigidbody;
    private Transform player;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player").transform;

        state = EnemyState.idle;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        
        if (health <= 0)
            Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        if (dist <= attackDistance)
            state = EnemyState.attacking;
        else if (dist <= chaseDistance)
            state = EnemyState.chasing;
        else
            state = EnemyState.idle;
    }
}
