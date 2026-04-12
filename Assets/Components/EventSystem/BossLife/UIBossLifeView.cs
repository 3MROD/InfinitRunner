using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBossLifeView : MonoBehaviour
{
    //UI for Boss life; UI Window with text and Slider as child
    //Showed only in ship attack state
    [SerializeField] private GameObject _bossWindow;
    [SerializeField] private TMP_Text _bossLifeText;
    [Header("Slider")]
    [SerializeField] private Slider _bossLifeSlider;
    [SerializeField] private int maxBossLife;
    [SerializeField] private int minBossLife;
    private int _health;
    private ShipAttackState _shipAttackState;
    private void Awake()
    {
        // listen to OnShipLifeUpdate and OnShipStateChange
        EventSystem.OnShipLifeUpdate += HandleShipLifeUpdate;
        EventSystem.OnShipStateChange += HandleShipStateChanger;

    }
    private void Start()
    {
        //Base Amount of life of the Boss
        _health = 5;
    }
    private void OnDestroy()
    {
        EventSystem.OnShipLifeUpdate -= HandleShipLifeUpdate;
        EventSystem.OnShipStateChange -= HandleShipStateChanger;
    }

    private void HandleShipStateChanger(ShipState state)
    {
        //When on ShipState Called if it's ShipAttackState Set window Active in UI and _shipAttackState as true
        if (state is not ShipAttackState shipAttackState)
        {
            _bossWindow.SetActive(false);
            return;
        }
        _bossWindow.SetActive(true);
        _shipAttackState = shipAttackState;    }

    
    private void HandleShipLifeUpdate(int newLifeCount)
    {
        //update Health  with the updated int of ShipLifeUpdate
        _health = newLifeCount ;
        
    }
    
    private void Update()
    {
        // Slider UI display : Cursor, minimum, maximum and boss life text % UI 
        _bossLifeSlider.value = _health;
        _bossLifeSlider.maxValue = maxBossLife;
        _bossLifeSlider.minValue = minBossLife;
        _bossLifeText .text = _health.ToString() + "/" + maxBossLife.ToString();

    }
}