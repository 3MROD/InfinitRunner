using UnityEngine;

public class ShipCollisionController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EventSystem.OnShipCollision?.Invoke();
        Debug.Log("ship hit");
        Destroy(other.gameObject);
    }
}
