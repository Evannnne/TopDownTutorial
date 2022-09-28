using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieBehaviour : MonoBehaviour
{
    private NavMeshAgent m_agent;
    private Animator m_animator;
    private OuchBehaviour m_ouch;

    private GameObject m_player;

    private void Awake()
    {
        m_agent = GetComponentInChildren<NavMeshAgent>();
        m_animator = GetComponentInChildren<Animator>();
        m_ouch = GetComponentInChildren<OuchBehaviour>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (m_agent.enabled) m_agent.SetDestination(m_player.transform.position);
    }
}
