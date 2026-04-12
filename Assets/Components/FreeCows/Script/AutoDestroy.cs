using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
// selfdestroy cow after instanteated
    void Start()
    {
        Destroy(gameObject, 5.0f);
    }

   
}
