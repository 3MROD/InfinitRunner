using System;
using TMPro;
using UnityEngine;

public class UIBossLifeView : MonoBehaviour
{
    [SerializeField] private TMP_Text _bossLifeText;
    private int _health;
    [SerializeField] private GameObject _bossWindow;
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
        _health = newLifeCount ;    }

    private void Start()
    {
        _health = 30;
    }
}
