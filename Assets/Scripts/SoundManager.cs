using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource SfxSource;
    [SerializeField] AudioSource SfxSource1;

    [Header("AUDIO CLIPS")]
    public AudioClip Theme;
    public AudioClip Lazer;
    public AudioClip Engine1;
    public AudioClip Engine2;
    public AudioClip Explosion1;
    public AudioClip Explosion2;
    public AudioClip Button;

    public float LazerVolume = 0.5f;

    private void Start()
    {
        MusicSource.clip = Theme;
        MusicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SfxSource.PlayOneShot(clip);
    }
    public void PlaySFX1()
    {
        SfxSource.PlayOneShot(Lazer,LazerVolume);
    }

    public void ButtonSFX()
    {
        SfxSource.PlayOneShot(Button);
    }
}
