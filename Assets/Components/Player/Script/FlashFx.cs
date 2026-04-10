using System;
using System.Collections;
using UnityEngine;

public class FlashFx : MonoBehaviour
{
    [SerializeField] private Renderer playerRenderer;
    [SerializeField] private Color flashColour = Color.white;
    [SerializeField] private float flashDuration = 0.1f;
    [SerializeField] private int flashCount = 5;
    private Color originalColour;
    private Material mat;

    private void Start()
    {
        mat = playerRenderer.material;
        originalColour = mat.color;
        EventSystem.Flash += HandleFlash;
        
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
    }
}
