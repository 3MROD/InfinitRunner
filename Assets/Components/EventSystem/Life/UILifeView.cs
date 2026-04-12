using System;
using TMPro;
using UnityEngine;

public class UILifeView : MonoBehaviour
{
    // Life UI controller, text and Sprites
    [SerializeField] private TMP_Text _lifeText;
    public GameObject gameOver, heart0, heart1,heart2;
    private int health;
    void Awake()
    {
        //listen to onplayerlifeUpdate for current life
        EventSystem.OnPlayerLifeUpdate += HandlePlayerLifeUpdated;
       
    }

    private void Start()
    {
        // Life at start 
       health = 2;
    }

    private void HandlePlayerLifeUpdated(int newLifeCount)
    {
        //When PlayerLifeUpdated is called set int as healt and update the text UI
        health = newLifeCount ;
        _lifeText.text = "Life: " + health;
        
    }

    private void Update()
    {
        // the switch Statement based on health will update the Sprite life UI depending on the health
        switch (health)
        {
            
            case 3:
                heart0.gameObject.SetActive(true);
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
              
                break;
            case 2:
                heart0.gameObject.SetActive(true);
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(false);
             
                break;

            case 1:
                heart0.gameObject.SetActive(true);
                heart1.gameObject.SetActive(false);
                heart2.gameObject.SetActive(false);
               
                break;
            default:
                heart0.gameObject.SetActive(false);
                heart1.gameObject.SetActive(false);
                heart2.gameObject.SetActive(false);
               
                break;
        }

    }

    private void OnDestroy()
    {
        EventSystem.OnPlayerLifeUpdate -= HandlePlayerLifeUpdated;
    }
 
}
