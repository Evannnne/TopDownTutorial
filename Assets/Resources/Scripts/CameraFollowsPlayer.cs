using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowsPlayer : MonoBehaviour
{
    public Transform target;

    private Vector3 m_targetOffset;
    private Quaternion m_targetRotation;
    private void Awake()
    {
        m_targetOffset = transform.position - target.position;
        m_targetRotation = transform.rotation;
    }

    private void Update()
    {
        transform.position = target.position + m_targetOffset;
        transform.rotation = m_targetRotation;
    }
}
