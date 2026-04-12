using System;
using UnityEngine;

public class ShipMouvementController : MonoBehaviour
{
    // mouve the boss gameobject in between targets depending of different states 
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
        // _move bool to know when in AttackState
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
        // in ShipAttackState it moves to the _bossPhaseTarget, other wise it goes to the _IdlePhaseTarget
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
