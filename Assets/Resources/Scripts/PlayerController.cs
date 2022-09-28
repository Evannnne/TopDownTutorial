using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1;
    public float sprintModifier = 2;
    public float attackDamage = 20;
    public float attackRadius = 0.5f;
    public float attackPushback = 0.5f;

    public float maxHealth = 100;
    public float health = 100;
    public float invincibilityPeriod = 5;

    public Transform shootingOrigin;

    private float hitTime = -99999999;

    private Animator m_animator;
    private Rigidbody m_rigidbody;
    private LayerMask m_groundMask;
    private OuchBehaviour m_ouch;

    private void Awake()
    {
        m_animator = GetComponentInChildren<Animator>();
        m_rigidbody = GetComponentInChildren<Rigidbody>();
        m_ouch = GetComponentInChildren<OuchBehaviour>();
        m_groundMask = LayerMask.GetMask("Ground");
    }

    private IEnumerator _Attack()
    {
        yield return new WaitForFixedUpdate();
        RaycastHit hit;
        Debug.DrawLine(shootingOrigin.transform.position, shootingOrigin.transform.position + shootingOrigin.transform.forward * 100, Color.red, 1);
        if (Physics.SphereCast(shootingOrigin.transform.position, attackRadius, shootingOrigin.transform.forward, out hit))
        {
            Debug.Log("Hit at " + Time.time);

            var hitGameObject = hit.collider.gameObject;
            var zombie = hitGameObject.GetComponent<ZombieBehaviour>();
            if (zombie != null)
            {
                zombie.Damage(attackDamage);
            }

            var rigid = hitGameObject.GetComponent<Rigidbody>();
            if (rigid != null)
            {
                var direction = (rigid.transform.position - shootingOrigin.position).normalized;
                rigid.transform.position += direction * attackPushback;
            }
        }
    }
    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_animator.SetTrigger("Fire");
            StartCoroutine(_Attack());
        }
    }
    private void MoveInCardinalDirections()
    {
        Vector3 move = Vector2.zero;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) move += Vector3.forward;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) move += Vector3.back;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) move += Vector3.left;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) move += Vector3.right;
        move = move.normalized;
        move *= Time.fixedDeltaTime;
        move *= moveSpeed;

        bool run = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        if (run) move *= 2;
        m_animator.SetFloat("RunSpeed", run ? 2 : 1);

        m_rigidbody.MovePosition(m_rigidbody.position + move);
        m_animator.SetBool("Moving", move != Vector3.zero);
    }
    private void LookAtMouse()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, m_groundMask))
            transform.LookAt(hit.point, Vector3.up);
    }
 
    public void Damage(float damage)
    {
        if (Time.time - hitTime > invincibilityPeriod)
        {
            health -= damage;
            m_ouch.Ouch(invincibilityPeriod);
            hitTime = Time.time;
        }
    }

    public void Update()
    {
        Attack();
    }
    public void FixedUpdate()
    {
        MoveInCardinalDirections();
        LookAtMouse();
    }
}