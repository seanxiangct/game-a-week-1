using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI testInputText; // to print out input for testing
    [SerializeField] private InputSystem_Actions _inputSystem; // the keybindings for the game
    [SerializeField] private InputSystem_Actions.PlayerActions _playerActions; // the player's keybinding
    private bool keyPressed = false; 
    
    Animator m_Animator;

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

    private void Start()
    {
        m_Animator = gameObject.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        keyPressed = false; //reset this bool for this update.
        testInputText.text = "nothing is pressed";
        UseLeftArm();
        UseRightArm();
        UseLeftLeg();
        UseRightLeg();
        UseBreathe();
    }

    /// <summary>
    /// Left arm swimming action
    /// </summary>
    public void UseLeftArm()
    {
        if (_playerActions.LeftArm.IsPressed())
        {
            testInputText.text = "Use Left Arm";
            keyPressedTrue();
            m_Animator.SetTrigger("LeftArm");
        }
    }
    /// <summary>
    /// Right arm swimming action
    /// </summary>
    public void UseRightArm()
    {
        if (_playerActions.RightArm.IsPressed())
        {
            string newLine = "";
            if (keyPressed)
            {
                newLine = "\n ";
                testInputText.text += newLine + "Use Right Arm";
            }
            else
            {
                testInputText.text = "Use Right Arm";
            }
            
            keyPressedTrue();
            m_Animator.SetTrigger("RightArm");
        }
    }

    /// <summary>
    /// Left leg swimming action
    /// </summary>
    public void UseLeftLeg()
    {
        if (_playerActions.LeftLeg.IsPressed())
        {
            string newLine = "";
            if (keyPressed)
            {
                newLine = "\n ";
                testInputText.text += newLine + "Use Left Leg";
            }
            else
            {
                testInputText.text ="Use Left Leg";
            }
            keyPressedTrue();
            m_Animator.SetTrigger("LeftLeg");
        }
    }
    
    /// <summary>
    /// Right leg swimming action
    /// </summary>
    public void UseRightLeg()
    {
        if (_playerActions.RightLeg.IsPressed())
        {
            string newLine = "";
            if (keyPressed)
            {
                newLine = "\n ";
                testInputText.text += newLine + "Use Right Leg";
            }
            else
            {
                testInputText.text ="Use Right Leg";
            }
            keyPressedTrue();
            m_Animator.SetTrigger("RightLeg");
        }
    }
    
    // need to hold to breathe air or drown.
    public void UseBreathe()
    {
        if (_playerActions.Breathe.IsPressed())
        {
            string newLine = "";
            if (keyPressed)
            {
                newLine = "\n ";
                testInputText.text +=newLine + "Breathe";
            }
            else
            {
                testInputText.text = "Breathe";
            }
            
            keyPressedTrue();
            m_Animator.SetTrigger("Breathe");
        }
    }

    private void keyPressedTrue()
    {
        keyPressed = true;
    }

    ///------------Animation --------------

    private void ResetAllAnimationTrigger()
    {
        m_Animator.ResetTrigger("LeftArm");
    }
    
}
