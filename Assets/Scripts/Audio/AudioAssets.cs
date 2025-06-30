using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioAssets : MonoBehaviour {

    public static AudioAssets instance;
    public AudioClip[] SoundCollection;
    private Dictionary<string, AudioClip> soundDictionary;
    [SerializeField] private SoundObject soundObjectPrefab;

    private void Awake() {

        if (instance != null) {
            Destroy(this);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);

        Initialise();

    }

    public void Initialise() {
        soundDictionary = new Dictionary<string, AudioClip>();
        foreach (AudioClip sound in SoundCollection) {
            if (!soundDictionary.ContainsKey(sound.name)) {
                soundDictionary.Add(sound.name, sound);
            }

        }
    }

    private AudioClip GetSound(string soundName) {
        if (soundDictionary == null) {
            Initialise();
        }
        if (soundDictionary.TryGetValue(soundName, out AudioClip sound)) {
            return sound;
        } else {
            Debug.LogWarning($"Sound '{soundName}' not found in the collection.");
            return null;
        }
    }

    public static void PlaySound(string soundName, float volume = 1f, float pitch = 1f, float pitchShift = 0f, bool spatial = true) {

        AudioClip sound = instance.GetSound(soundName);
        if (sound == null) {
            return;
        }
        SoundObject soundObject = Instantiate(instance.soundObjectPrefab);

        soundObject.audioSource.clip = sound;
        soundObject.audioSource.volume = volume;
        float randomizedPitch = pitch;
        if (pitchShift > 0f) {
            randomizedPitch = Random.Range(pitch - pitchShift, pitch + pitchShift);
        }
        soundObject.audioSource.pitch = randomizedPitch;
        soundObject.audioSource.spatialBlend = spatial ? 1f : 0f;

        soundObject.Play();
    }
}

public static class PlayAudio {

    public static void Play(string soundName, float volume = 1f, float pitch = 1f, float pitchShift = 0f, bool spatial = true) {

        if (AudioAssets.instance == null) {
            Debug.LogWarning("AudioAssets instance is not initialized.");
            return;
        }

        AudioAssets.PlaySound(soundName, volume, pitch, pitchShift, spatial); // Fixed: Use the type name instead of instance reference
    }
}