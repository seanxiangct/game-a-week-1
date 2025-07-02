using UnityEngine;

public class MusicAssets : MonoBehaviour {

    public static MusicAssets instance;
    public AudioClip titleMusic;
    public AudioClip swimmingMusic;
    public AudioClip gameMusic;

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

    public static void PlayShark() {
        instance.audioSource.clip = instance.gameMusic;
        instance.PlayMusic();
    }

    public void PlayMusic(float volume = 1f) {
        audioSource.volume = volume;
        audioSource.Play();
    }

    public void StopMusic() {
        audioSource.Stop();
    }
}