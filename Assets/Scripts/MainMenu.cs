using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

    [SerializeField] private InputSystem_Actions _inputSystem; // the keybindings for the game
    public InputSystem_Actions InputSystem => _inputSystem;
    [SerializeField] private InputSystem_Actions.PlayerActions _playerActions; // the player's keybinding
    [SerializeField] private Image fadeImage;

    private float waitTime = 0f;

    private bool loadScene = false;

    private void Awake() {
        _inputSystem = new InputSystem_Actions();
        _playerActions = _inputSystem.Player;
    }

    private void Start() {

        FadeIn(2f);

        Debug.Log("Sound and Music tests");
        AudioAssets.PlaySound("switch-1", 0.5f, 1f, 0.1f, true);
        Player.GameStarted = false;
        MusicAssets.PlayTitle();

    }

    private void OnEnable() {
        _playerActions.Enable();
    }

    private void OnDisable() {
        _playerActions.Disable();
    }

    private void OnDestroy() {
        _inputSystem.Dispose();
    }

    private void Update() {

        waitTime += Time.deltaTime;

        if (waitTime > 2f && !loadScene) {
            if (_playerActions.Breathe.WasPressedThisFrame() || Mouse.current.leftButton.wasPressedThisFrame) {
                loadScene = true;
                AudioAssets.PlaySound("switch-1", 0.5f, 1f, 0.1f, true);
                FadeOut(1.5f);
            }
        }

    }

    public void FadeOut(float duration) {
        StartCoroutine(Fade(0f, 1f, duration, true));
    }

    public void FadeIn(float duration) {
        StartCoroutine(Fade(1f, 0f, duration, false));
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration, bool fadeOut) {
        Color color = fadeImage.color;
        float time = 0f;
        while (time < duration) {
            float t = time / duration;
            color.a = Mathf.Lerp(startAlpha, endAlpha, t);
            fadeImage.color = color;
            time += Time.deltaTime;
            yield return null;
        }
        color.a = endAlpha;
        fadeImage.color = color;
        if (fadeOut) {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
