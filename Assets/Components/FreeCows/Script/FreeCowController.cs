using UnityEngine;

public class FreeCowController : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _cowPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventSystem.FreeCow += HandlerFreeCow;
    }

    private void HandlerFreeCow()
    {
        InstantiateCow();
    }

    private void InstantiateCow()
    {
        Instantiate(_cowPrefab, _spawnPoint.position, _spawnPoint.rotation);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
