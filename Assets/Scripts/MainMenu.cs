using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [SerializeField] private InputSystem_Actions _inputSystem; // the keybindings for the game
    public InputSystem_Actions InputSystem => _inputSystem;
    [SerializeField] private InputSystem_Actions.PlayerActions _playerActions; // the player's keybinding

    private bool loadScene = false;

    private void Awake() {
        _inputSystem = new InputSystem_Actions();
        _playerActions = _inputSystem.Player;
    }

    private void Start() {

        Debug.Log("Sound and Music tests");
        AudioAssets.PlaySound("switch-1", 0.5f, 1f, 0.1f, true);
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
        if (_playerActions.Breathe.WasPressedThisFrame()) {
            loadScene = true;
            AudioAssets.PlaySound("switch-1", 0.5f, 1f, 0.1f, true);
            SceneManager.LoadScene("SampleScene");

        }
    }


}
