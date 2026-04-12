using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashFx : MonoBehaviour
{
    // Flash effect for when hit to change the player material for few seconds with a coroutine
    [SerializeField] private SkinnedMeshRenderer playerRenderer;
    [SerializeField] private Color flashColour = Color.white;
    [SerializeField] private float flashDuration = 0.1f;
    [SerializeField] private int flashCount = 5;
    private Color originalColour;
    private Material mat;
  

    private void Start()
    {
        playerRenderer = GetComponent<SkinnedMeshRenderer>();
        List<Material> materials = new List<Material>();
        playerRenderer.GetMaterials(materials);
        mat = playerRenderer.material;
        originalColour = mat.color;
        EventSystem.Flash += HandleFlash;
        
    }

    private void OnDestroy()
    {
        EventSystem.Flash -= HandleFlash;
    }

    private void HandleFlash()
    {
        StartCoroutine(WhiteFlash());
    }
    private IEnumerator WhiteFlash()
    {
        for (int i = 0; i < flashCount; i++)
        {
            mat.color = flashColour;
            yield return new WaitForSeconds(flashDuration);
            mat.color = originalColour;
            yield return new WaitForSeconds(flashDuration);
        }
        Debug.Log("flashed");
    }
}
