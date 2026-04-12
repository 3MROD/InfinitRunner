using UnityEngine;

public class ChunkController : MonoBehaviour
// to add to the chunks so an Empty can be placed at the end of the prefab to tell the where the next chunk must be instaciated
{
    [SerializeField] private Transform _endAnchor;
    
    public Vector3 EndAnchor => _endAnchor.position;

   // to indicate if the chunk is behind the player to destroy 
    public bool IsBehindPlayer()
    {
        return EndAnchor.z <= 0;
    }
}
