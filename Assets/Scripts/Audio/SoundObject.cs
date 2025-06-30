using UnityEngine;

public class SoundObject : MonoBehaviour {

    public AudioSource audioSource;

    public void Play() {
        audioSource.Play();
        Invoke(nameof(Remove), audioSource.clip.length + .25f);
    }

    private void Remove() {
        Destroy(this);
    }

}
