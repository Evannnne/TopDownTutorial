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

    private void FixedUpdate()
    {
        Vector3 move = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) move += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) move += Vector3.back;
        if (Input.GetKey(KeyCode.A)) move += Vector3.left;
        if (Input.GetKey(KeyCode.D)) move += Vector3.right;
        move = move.normalized;
        move *= Time.fixedDeltaTime;
        move *= moveSpeed;

        m_rigidbody.MovePosition(m_rigidbody.position + move);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, m_groundMask))
            transform.LookAt(hit.point, Vector3.up);
    }
}
