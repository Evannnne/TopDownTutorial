using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSpawner : MonoBehaviour
{
    public float interval = 1;
    public float rampUp = 0.01f;
    public float minInterval = 0.2f;
    public float ringRange = 10;

    public GameObject prefab;

    private float m_elapsed = 0;
    public void Update()
    {
        interval -= rampUp * Time.deltaTime;
        interval = Mathf.Max(minInterval, interval);

        m_elapsed += Time.deltaTime;
        if(m_elapsed >= interval)
        {
            m_elapsed -= interval;
            var instance = Instantiate(prefab);
            Vector3 pos = Random.insideUnitCircle.normalized * ringRange;
            pos = new Vector3(pos.x, 0, pos.y);
            UnityEngine.AI.NavMeshHit hit;
            if (UnityEngine.AI.NavMesh.SamplePosition(pos, out hit, 10, int.MaxValue))
                instance.transform.position = hit.position;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 1);
    }
}
