using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI testInputText; // to print out input for testing
    [SerializeField] private InputSystem_Actions _inputSystem; // the keybindings for the game
    [SerializeField] private InputSystem_Actions.PlayerActions _playerActions; // the player's keybinding

    private void Awake()
    {
        _inputSystem = new InputSystem_Actions();
        _playerActions = _inputSystem.Player;
    }

    private void OnEnable()
    {
        _playerActions.Enable();
    }

    private void OnDisable()
    {
        _playerActions.Disable();
    }

    private void OnDestroy()
    {
        _inputSystem.Dispose();
    }
    
    void Update()
    {
        testInputText.text = "nothing is pressed";
        UseLeftArm();
        UseRightArm();
        UseLeftLeg();
        UseRightLeg();
        UseBreathe();
    }

    public void UseLeftArm()
    {
        if (_playerActions.LeftArm.IsPressed())
        {
            testInputText.text = "Use Left Arm";
        }
    }
    public void UseRightArm()
    {
        if (_playerActions.RightArm.IsPressed())
        {
            testInputText.text = "Use Right Arm";
        }
    }

    public void UseLeftLeg()
    {
        if (_playerActions.LeftLeg.IsPressed())
        {
            testInputText.text = "Use Left Leg";
        }
    }
    public void UseRightLeg()
    {
        if (_playerActions.RightLeg.IsPressed())
        {
            testInputText.text = "Use Right Leg";
        }
    }
    
    public void UseBreathe()
    {
        if (_playerActions.Breathe.IsPressed())
        {
            testInputText.text = "Breathe";
        }
    }
}
