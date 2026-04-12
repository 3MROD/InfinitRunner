using System;
using TMPro;
using UnityEngine;

public class UIFreeCow : MonoBehaviour
{
    [SerializeField] private TMP_Text _FreecowText;
    private int _freecow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        EventSystem.FreeCowCount += HandleFreeCowCount;
    }

    private void Start()
    {
        _freecow = 0;
    }

    private void HandleFreeCowCount(int newFreecowCount)
    {
        _FreecowText.text = "free cows :" + newFreecowCount;
        _freecow = newFreecowCount;
    }

    private void OnDestroy()
    {
        EventSystem.FreeCowCount -= HandleFreeCowCount;
    }
}
