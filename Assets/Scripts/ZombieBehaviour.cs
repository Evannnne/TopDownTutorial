using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieBehaviour : MonoBehaviour
{
    public Material ouchMaterial;
    public float health = 100;
    public float maxHealth = 100;

    public float attackDamage = 10;

    [SerializeField] private NavMeshAgent m_agent;
    [SerializeField] private Animator m_animator;
    [SerializeField] private Rigidbody m_rigidbody;
    [SerializeField] private Collider m_collider;
    [SerializeField] private OuchBehaviour m_ouch;

    private GameObject m_player;

    public void Start() => m_player = GameObject.Find("Player");
    public void Update()
    {
        if(m_agent.enabled) m_agent.SetDestination(m_player.transform.position);
    }

    public void Damage(float damage)
    {
        m_ouch.Ouch(0.2f);
        health -= damage;
        if(health <= 0)
        {
            m_animator.SetTrigger("Die");
            gameObject.AddComponent<Despawner>();
            Destroy(m_rigidbody);
            Destroy(m_agent);
            Destroy(m_collider);
            Destroy(this);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();
        if(player != null)
        {
            player.Damage(attackDamage);
        }
    }
}
