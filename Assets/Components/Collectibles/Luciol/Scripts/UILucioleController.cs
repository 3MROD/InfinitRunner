using System;
using TMPro;
using UnityEngine;

public class UILucioleController : MonoBehaviour
{
    [SerializeField] private TMP_Text LucioleText;
    private int LucioleNumber;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        EventSystem.LucioleUpdate += HandleLucioleUpdate;
    }

    private void Start()
    {
        LucioleNumber = 0;
    }

    private void HandleLucioleUpdate(int newLucioleNumber)
    {
       LucioleText.text = "Luciole: " + newLucioleNumber;
       LucioleNumber = newLucioleNumber;
    }


    private void OnDestroy()
    {
        EventSystem.LucioleUpdate -= HandleLucioleUpdate;
    }
}
