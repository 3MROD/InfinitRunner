using System;
using UnityEngine;

public class Luciole : MonoBehaviour
{
    private void OnDestroy()
    {
        EventSystem.OnLucioleCollision?.Invoke();
    }
}
