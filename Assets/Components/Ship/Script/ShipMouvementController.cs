using System;
using UnityEngine;

public class ShipMouvementController : MonoBehaviour
{
    [SerializeField] private GameObject _bossPhaseTarget;
    [SerializeField] private GameObject _IdlePhaseTarget;
    [SerializeField] private GameObject _ship;
    [SerializeField] private bool _move;
    private void Start()
    {
        EventSystem.OnShipStateChange += HandelShipStateChange;
        
    }

    private void OnDestroy()
    {
       EventSystem.OnShipStateChange -= HandelShipStateChange;
    }

    private void HandelShipStateChange(ShipState shipState)
    {
        if (shipState is not ShipAttackState shipAttackState)
        {
            _move = false;
        }
        else
        {
            _move = true;
        }
    }
    

    void Update()
    {
        if (!_move)
        {
            _ship.transform.position = Vector3.MoveTowards(_ship.transform.position, _IdlePhaseTarget.transform.position, 0.1f);
        }
        else
        {
            _ship.transform.position = Vector3.MoveTowards(_ship.transform.position, _bossPhaseTarget.transform.position, 0.1f);
        }
    }
}
