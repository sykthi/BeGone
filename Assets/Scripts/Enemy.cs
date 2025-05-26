using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject explosionVFX;
    [SerializeField] GameObject HitVFX;
    [SerializeField] Transform Container;
    [SerializeField] int scoreValue = 10;
    [SerializeField] int hitpoint = 20;

    GameManager scorekeeper;

    [System.Obsolete]
    private void Start()
    {
        scorekeeper = FindObjectOfType<GameManager>();
    }
    private void OnParticleCollision(GameObject other)
    { 
        Hit();
        if (hitpoint == 0)
        {
            Killenemy();
        }
    }

    void Hit()
    {
        GameObject explosion = Instantiate(HitVFX, transform.position, Quaternion.identity);
        explosion.transform.parent = Container;
        hitpoint = hitpoint - 5;
    }

    private void Killenemy()
    {
        GameObject explosion = Instantiate(explosionVFX, transform.position, Quaternion.identity);
        explosion.transform.parent = Container;
        Destroy(gameObject);
        scorekeeper.IncreaseScore(scoreValue);
    }
}
