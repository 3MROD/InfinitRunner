using System;
using System.Collections;
using UnityEngine;

public class FlashFx : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private Color originalColour;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        originalColour = GetComponent<Renderer>().material.color;
        EventSystem.Flash += HandleFlash;
        
    }

    private void HandleFlash()
    {
        StartCoroutine(WhiteFlash());
    }
    private IEnumerator WhiteFlash()
    {
        _meshRenderer.material.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        _meshRenderer.material.color = originalColour;
    }
}
