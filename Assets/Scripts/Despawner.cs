using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawner : MonoBehaviour
{
    public float despawnTime = 5;
    public void Start() => StartCoroutine(_Despawn());
    private IEnumerator _Despawn()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }
}
