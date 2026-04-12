using System;
using TMPro;
using UnityEngine;

public class UILucioleController : MonoBehaviour
{
    [SerializeField] private TMP_Text LucioleText;
    private int LucioleNumber;
    // this script goes on the UILucioleCount to show Count
    private void Awake()
    {
        //listen to LucioleUpdate
        EventSystem.LucioleUpdate += HandleLucioleUpdate;
    }

    private void Start()
    {
        
        LucioleNumber = 0;
    }

    private void HandleLucioleUpdate(int newLucioleNumber)
    {
        //When LucioleUpdate is called Update UI Text
       LucioleText.text = "Luciole: " + newLucioleNumber;
       LucioleNumber = newLucioleNumber;
    }


    private void OnDestroy()
    {
        EventSystem.LucioleUpdate -= HandleLucioleUpdate;
    }
}
