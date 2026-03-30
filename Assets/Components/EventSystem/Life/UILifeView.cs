using System;
using TMPro;
using UnityEngine;

public class UILifeView : MonoBehaviour
{
    [SerializeField] private TMP_Text _lifeText;
    public GameObject gameOver, heart0, heart1,heart2,heart3;
    private int health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        EventSystem.OnPlayerLifeUpdate += HandlePlayerLifeUpdated;
       
    }

 

    private void HandlePlayerLifeUpdated(int newLifeCount)
    {
        _lifeText.text = "Life: " + newLifeCount;
        health = newLifeCount;
    }

    private void Update()
    {
        switch (health)
        {
            case 4:
                heart0.gameObject.SetActive(true);
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(true);
                break;
            case 3:
                heart0.gameObject.SetActive(true);
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(false);
                break;
            case 2:
                heart0.gameObject.SetActive(true);
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                break;

            case 1:
                heart0.gameObject.SetActive(true);
                heart1.gameObject.SetActive(false);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                break;
            default:
                heart0.gameObject.SetActive(false);
                heart1.gameObject.SetActive(false);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                break;
        }

    }

    private void OnDestroy()
    {
        EventSystem.OnPlayerLifeUpdate -= HandlePlayerLifeUpdated;
    }
 
}
