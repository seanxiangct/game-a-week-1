using UnityEngine;

public class MusicAssets : MonoBehaviour {

    public static MusicAssets instance;
    public AudioClip titleMusic;
    public AudioClip swimmingMusic;
    public AudioClip drowningMusic;

    [SerializeField] private AudioSource audioSource;

    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        audioSource.loop = true;
        audioSource.spatialBlend = 0f;
        audioSource.playOnAwake = false;
    }

    public static void PlayTitle() {
        instance.audioSource.clip = instance.titleMusic;
        instance.PlayMusic();
    }

    public static void PlaySwim() {
        instance.audioSource.clip = instance.swimmingMusic;     
        instance.PlayMusic();
    }

    public static void PlayDrown() {
        instance.audioSource.clip = instance.drowningMusic;
        instance.PlayMusic();
    }

    public void PlayMusic(float volume = 1f) {
        audioSource.volume = volume;
        audioSource.Play();
    }

    public void StopMusic() {
        audioSource.Stop();
    }

    private void Update() {
        if (!Player.GameStarted) return;

        if (!Player.GameOver) {
            if (Player.Stamina > 50) {
                if (instance.audioSource.clip == instance.swimmingMusic) return; // Already playing swimming music
                Debug.Log("Playing swimming music");
                PlaySwim();
            } else {
                if (instance.audioSource.clip == instance.drowningMusic) return; // Already playing drowning music
                Debug.Log("Playing drowning music");
                PlayDrown();
            }

        } else {

        }
    }

}








