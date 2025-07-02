using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI testInputText; // to print out input for testing
    [SerializeField] private InputSystem_Actions _inputSystem; // the keybindings for the game
    public InputSystem_Actions InputSystem => _inputSystem;
    [SerializeField] private InputSystem_Actions.PlayerActions _playerActions; // the player's keybinding
    [SerializeField] private float armForce = 5f; // Force magnitude for arm strokes
    private bool keyPressed = false;
    private bool isRightArm = false;
    private bool isLeftArm = false;
    private bool isRightLeg = false;
    private bool isLeftLeg = false;
    private bool isBreathe = false;
    private Rigidbody2D rb;
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
        rb = GetComponent<Rigidbody2D>();
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
        } 
        if (_playerActions.LeftArm.IsPressed() && !isLeftArm)
        {
            keyPressedTrue();
            m_Animator.SetTrigger("LeftArm");
            isLeftArm = true;
            // Apply force to the top-left
            if (rb != null)
            {
                rb.AddForce(new Vector2(-1, 1).normalized * armForce, ForceMode2D.Impulse);
            }
        }

        if (!_playerActions.LeftArm.IsPressed())
        {
            isLeftArm = false;
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
        }
        
        if (_playerActions.RightArm.IsPressed() && !isRightArm)
        {
            m_Animator.SetTrigger("RightArm");
            isRightArm = true;
            // Apply force to the top-right
            if (rb != null)
            {
                rb.AddForce(new Vector2(1, 1).normalized * armForce, ForceMode2D.Impulse);
            }
        }

        if (!_playerActions.RightArm.IsPressed())
        {
            isRightArm = false;
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
        }

        if (_playerActions.LeftLeg.IsPressed() && !isLeftLeg)
        {
           m_Animator.SetTrigger("LeftLeg");
            isLeftLeg = true;
        }

        if (!_playerActions.LeftLeg.IsPressed())
        {
            isLeftLeg = false;
        }
    }
    
    /// <summary>
    /// Right leg swimming action
    /// </summary>
    public void UseRightLeg()
    {
        //debug
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
        }

        if (_playerActions.RightLeg.IsPressed() && !isRightLeg)
        {
          
            m_Animator.SetTrigger("RightLeg");
            isRightLeg = true;
        }

        if (!_playerActions.RightLeg.IsPressed())
        {
            isRightLeg = false;
        }
    }
    
    // need to hold to breathe air or drown.
    public void UseBreathe()
    {
        //debug only
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
        }
        
        if (_playerActions.Breathe.IsPressed() && !isBreathe)
        {
          
            m_Animator.SetTrigger("Breathe");
            isBreathe = true;
        }

        if (!_playerActions.Breathe.IsPressed())
        {
            isBreathe = false;
        }
    }

    private void keyPressedTrue()
    {
        keyPressed = true;
    }

    ///------------Animation --------------



}
