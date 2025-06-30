using UnityEngine;

public class MainMenu : MonoBehaviour {

    private void Start() {

        Debug.Log("Sound and Music tests");
        AudioAssets.PlaySound("switch-1", 0.5f, 1f, 0.1f, true);
        MusicAssets.PlayTitle();

    }


}
