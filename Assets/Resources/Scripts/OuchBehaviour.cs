using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class OuchBehaviour : MonoBehaviour
{
    private List<Tuple<MeshRenderer, Material[]>> m_rendererAndMaterials;
    private Coroutine m_currentOuch;
    private static Material s_ouchMaterial;

    private void Awake()
    {
        if (s_ouchMaterial == null)
            s_ouchMaterial = Resources.Load<Material>("Materials/Ouch");

        var mrs = GetComponentsInChildren<MeshRenderer>();
        m_rendererAndMaterials = mrs.Select(r => new Tuple<MeshRenderer, Material[]>(r, r.materials)).ToList();
    }

    public void Ouch(float duration)
    {
        if (m_currentOuch != null) StopCoroutine(m_currentOuch);
        m_currentOuch = StartCoroutine(_Ouch(duration));
    }
    private IEnumerator _Ouch(float duration)
    {
        foreach (var ram in m_rendererAndMaterials)
            ram.Item1.materials = new Material[] { s_ouchMaterial, s_ouchMaterial, s_ouchMaterial };
        yield return new WaitForSeconds(duration);
        foreach (var ram in m_rendererAndMaterials)
            ram.Item1.materials = ram.Item2;
    }
}
