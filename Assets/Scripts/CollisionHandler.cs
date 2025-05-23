using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] GameObject vfx_explosion;
    [SerializeField] GameObject ship;
    public float loadDelay = 1f;
    void OnTriggerEnter(Collider other)
    {
        StartCrashSequence();
    }
    void StartCrashSequence()
    {
        vfx_explosion.SetActive(true);
        ship.SetActive(false);
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
        Invoke("ReloadScene", loadDelay);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene("Level 0");
    }
}
