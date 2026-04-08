using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBossLifeView : MonoBehaviour
{
    [SerializeField] private TMP_Text _bossLifeText;
    private int _health;
    [SerializeField] private GameObject _bossWindow;
    [SerializeField] private Slider _bossLifeSlider;
    [SerializeField] private int maxBossLife;
    [SerializeField] private int minBossLife;
    private ShipAttackState _shipAttackState;
    private void Awake()
    {
        EventSystem.OnShipLifeUpdate += HandleShipLifeUpdate;
        EventSystem.OnShipStateChange += HandleShipStateChanger;

    }

    private void HandleShipStateChanger(ShipState state)
    {
        if (state is not ShipAttackState shipAttackState)
        {
            _bossWindow.SetActive(false);
            return;
        }
        _bossWindow.SetActive(true);
        _shipAttackState = shipAttackState;    }

    private void OnDestroy()
    {
        EventSystem.OnShipLifeUpdate -= HandleShipLifeUpdate;
        EventSystem.OnShipStateChange -= HandleShipStateChanger;
    }

    private void HandleShipLifeUpdate(int newLifeCount)
    {
        _bossLifeText.text = "Life: " + newLifeCount;
        _health = newLifeCount ;
        
    }

    private void Start()
    {
        _health = 5;
    }

    private void Update()
    {
        _bossLifeSlider.value = _health;
        _bossLifeSlider.maxValue = maxBossLife;
        _bossLifeSlider.minValue = minBossLife;
        _bossLifeText .text = _health.ToString() + "/" + maxBossLife.ToString();

    }
}