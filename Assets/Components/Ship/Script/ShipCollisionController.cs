using System;
using UnityEngine;

public class ShipCollisionController : MonoBehaviour
{
    private bool _inShipAttackState;
    private void Start()
    {
        EventSystem.OnShipStateChange += HandelShipAttackState;
        _inShipAttackState = false;
    }

    private void OnDestroy()
    {
        EventSystem.OnShipStateChange -= HandelShipAttackState;
    }

    private void HandelShipAttackState(ShipState shipState)
    {
        if (shipState is ShipAttackState)
        {
            _inShipAttackState = true;
            Debug.Log("attack possible");
        }
        else
        {
            _inShipAttackState = false;
            Debug.Log("attack not possible");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_inShipAttackState)
        {
            EventSystem.OnShipCollision?.Invoke();
            Debug.Log("ship hit");
            Destroy(other.gameObject);
        }
        Destroy(other.gameObject);
    }
}
