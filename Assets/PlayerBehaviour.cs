using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float moveSpeed = 4;
    public float sprintModifier = 2;

    public float attackDamage = 100;
    public float attackRadius = 0.5f;

    public float health = 100;
    public float maxHealth = 100;

    private Animator m_animator;
    private Rigidbody m_rigidbody;
    private LayerMask m_groundMask;
    private OuchBehaviour m_ouch;

    void Awake()
    {
        m_animator = GetComponentInChildren<Animator>();
        m_rigidbody = GetComponentInChildren<Rigidbody>();
        m_ouch = GetComponentInChildren<OuchBehaviour>();
        m_groundMask = LayerMask.GetMask("Ground");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
