using System;
using TMPro;
using UnityEngine;

public class UIFreeCow : MonoBehaviour
{
    //UI for Cow Count Text
    [SerializeField] private TMP_Text _FreecowText;
    private int _freecow;
  
    private void Awake()
    {
        // Listen to freeCowCount
        EventSystem.FreeCowCount += HandleFreeCowCount;
    }

    private void Start()
    {
        //set Cow count to 0 at Start
        _freecow = 0;
        _FreecowText.text = "Free cows : " + _freecow;
    }

    private void HandleFreeCowCount(int newFreecowCount)
    {
        //When free CowCount is called Update int to _freeCow var / Update UI Text 
        _freecow = newFreecowCount;
        _FreecowText.text = "Free cows : " + _freecow;
        
    }

    private void OnDestroy()
    {
        EventSystem.FreeCowCount -= HandleFreeCowCount;
    }
}
