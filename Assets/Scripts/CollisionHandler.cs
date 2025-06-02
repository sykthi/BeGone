using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] GameObject vfx_explosion;
    [SerializeField] GameObject ship;
    public float loadDelay = 0.5f;

    SoundManager _soundManager;

    private void Awake()
    {
        _soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
    }
    void OnTriggerEnter(Collider other)
    {        
        StartCrashSequence();
    }
    void StartCrashSequence()
    {
        _soundManager.PlaySFX(_soundManager.Explosion2);
        UI.IsGameEnded = true;
        vfx_explosion.SetActive(true);
        ship.SetActive(false);
        Invoke("stopGame", loadDelay);
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
    }

    void stopGame()
    {
        Time.timeScale = 0f;
    }
    void ReloadScene()
    {
        SceneManager.LoadScene("Level 0");
    }
}
