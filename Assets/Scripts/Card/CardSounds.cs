using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class CardSounds : MonoBehaviour
{
    public static CardSounds Sounds;

    [Header("Sounds")]
    public AudioClip pickSound;
    public AudioClip putSound;
    public AudioClip spawnSound;
    public AudioClip removeSound;
    public AudioClip editSound;
    public AudioClip clickSound;

    private AudioSource Source;

    public void Awake()
    {
        Source = GetComponent<AudioSource>();
        Singleton();
    }

    private void Singleton()
    {
        if (!Sounds)
        {
            Sounds = this;
        }
        else
        {
            if (Sounds != this)
            {
                Debug.Log("Destroy " + name);
                Destroy(Sounds.gameObject);
                Sounds = this;
            }
        }
    }
    
    public void SoundClick()
    {
        SoundPlay(clickSound);
    }

    public void SoundPick()
    {
        SoundPlay(pickSound);
    }

    public void SoundPut()
    {
        SoundPlay(putSound);
    }

    public void SoundSpawn()
    {
        SoundPlay(spawnSound);
    }

    public void SoundRemove()
    {
        SoundPlay(removeSound);
    }

    public void SoundEdit()
    {
        SoundPlay(editSound);
    }
    
    private void SoundPlay(AudioClip clip)
    {
        if(!clip) {Debug.LogError("Clip is null"); return; }

        Source.pitch = Random.Range(0.8f, 1.2f);
        Source.PlayOneShot(clip, Random.Range(0.8f, 1.0f));
    }
}
