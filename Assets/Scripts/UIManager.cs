using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
   [SerializeField] private PlayerController _playerController;

    [SerializeField] private TextMeshProUGUI _leftArm;
    [SerializeField] private TextMeshProUGUI _rightArm;
    [SerializeField] private TextMeshProUGUI _leftLeg;
    [SerializeField] private TextMeshProUGUI _rightLeg;
    [SerializeField] private TextMeshProUGUI _breath;

    [SerializeField] private string _leftArmKey = "F";
    [SerializeField] private string _RightArmKey = "J";
    [SerializeField] private string _leftLegKey = "Z";
    [SerializeField] private string _rightLegKey = "/";
    [SerializeField] private string _breatheKey = "Space";

    [SerializeField] private Color defaultTextColour = Color.gray;
    [SerializeField] private Color pressedTextColour = Color.black;

    private void Start()
    {
        _leftArm.text = _leftArmKey;
        _rightArm.text = _RightArmKey;
        _leftLeg.text = _leftLegKey;
        _rightLeg.text = _rightLegKey;
        _breath.text = _breatheKey;
    }

    private void Update()
    {
        resetTextColourToDefault();
        if (_playerController.InputSystem.Player.LeftArm.IsPressed())
        {
            Debug.Log("hello leftarm is pressed");
            _leftArm.color = pressedTextColour;
        }
        if (_playerController.InputSystem.Player.RightArm.IsPressed())
        {
            _rightArm.color = pressedTextColour;
        }
        if (_playerController.InputSystem.Player.LeftLeg.IsPressed())
        {
            _leftLeg.color = pressedTextColour;
        }
        if (_playerController.InputSystem.Player.RightLeg.IsPressed())
        {
            _rightLeg.color = pressedTextColour;
        }
        if (_playerController.InputSystem.Player.Breathe.IsPressed())
        {
            _breath.color = pressedTextColour;
        }
    }

    private void resetTextColourToDefault()
    {
        _leftArm.color = defaultTextColour;
        _rightArm.color = defaultTextColour;
        _leftLeg.color = defaultTextColour;
        _rightLeg.color = defaultTextColour;
        _breath.color = defaultTextColour;
    }
}
