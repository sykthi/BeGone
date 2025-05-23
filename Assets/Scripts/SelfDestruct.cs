using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float TimeToLive = 2f;

    private void Start()
    {
        Destroy(gameObject, TimeToLive);
    }
}
