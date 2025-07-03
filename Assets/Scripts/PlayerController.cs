using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI testInputText; // to print out input for testing
    [SerializeField] public TextMeshProUGUI staminaUI; // shows stamina
    [SerializeField] public TextMeshProUGUI staminaModified; // shows modified stamina
    [SerializeField] public TextMeshProUGUI breathModifyStamina; // shows stamina modified by breath
    [SerializeField] public TextMeshProUGUI stamArmL, stamArmR, stamLegL, stamLegR, stamBreath; // stamina for each limb
    [SerializeField] private InputSystem_Actions _inputSystem; // the keybindings for the game
    public InputSystem_Actions InputSystem => _inputSystem;
    [SerializeField] private InputSystem_Actions.PlayerActions _playerActions; // the player's keybinding
    [SerializeField] private float armForce = 5f; // Force magnitude for arm strokes
    [SerializeField] private float legForce = 4f; // Force magnitude for leg strokes
    [SerializeField] private float waterDrag = 2f; // Water drag coefficient
    private bool keyPressed = false;
    private bool isRightArm = false;
    private bool isLeftArm = false;
    private bool isRightLeg = false;
    private bool isLeftLeg = false;
    private bool isBreathe = false;
    private Rigidbody2D rb;
    Animator m_Animator;

    private StaminaManager _staminaManager;

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
        _staminaManager = new(this);
    }

    private void FixedUpdate()
    {
        ApplyWaterDrag();
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
        _staminaManager.BreathingUpdate();
        _staminaManager.NeedToUseAllLimbsUpdate();
    }

    /// <summary>
    /// Left arm swimming action
    /// </summary>
    public void UseLeftArm()
    {
        if (_playerActions.LeftArm.WasPressedThisFrame())

        {
            testInputText.text = "Use Left Arm";
        }
        if (_playerActions.LeftArm.WasPressedThisFrame() && !isLeftArm)
        {
            keyPressedTrue();
            _staminaManager.UseArms(false); // Use left arm stamina
            m_Animator.SetTrigger("LeftArm");
            isLeftArm = true;
        }

        if (!_playerActions.LeftArm.WasPressedThisFrame())
        {
            isLeftArm = false;
        }

    }

    /// <summary>
    /// Right arm swimming action
    /// </summary>
    public void UseRightArm()
    {
        if (_playerActions.RightArm.WasPressedThisFrame())
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
            _staminaManager.UseArms(true); // Use right arm stamina
        }

        if (_playerActions.RightArm.WasPressedThisFrame() && !isRightArm)
        {
            m_Animator.SetTrigger("RightArm");
            isRightArm = true;
        }

        if (!_playerActions.RightArm.WasPressedThisFrame())
        {
            isRightArm = false;
        }



    }

    /// <summary>
    /// Left leg swimming action
    /// </summary>
    public void UseLeftLeg()
    {
        if (_playerActions.LeftLeg.WasPressedThisFrame())
        {
            string newLine = "";
            if (keyPressed)
            {
                newLine = "\n ";
                testInputText.text += newLine + "Use Left Leg";
            }
            else
            {
                testInputText.text = "Use Left Leg";
            }
            keyPressedTrue();
            _staminaManager.UseLegs(false); // Use left leg stamina
        }

        if (_playerActions.LeftLeg.WasPressedThisFrame() && !isLeftLeg)
        {
            m_Animator.SetTrigger("LeftLeg");
            isLeftLeg = true;
        }

        if (!_playerActions.LeftLeg.WasPressedThisFrame())
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
        if (_playerActions.RightLeg.WasPressedThisFrame())
        {
            string newLine = "";
            if (keyPressed)
            {
                newLine = "\n ";
                testInputText.text += newLine + "Use Right Leg";
            }
            else
            {
                testInputText.text = "Use Right Leg";
            }
            keyPressedTrue();
            _staminaManager.UseLegs(true); // Use right leg stamina
        }

        if (_playerActions.RightLeg.WasPressedThisFrame() && !isRightLeg)
        {

            m_Animator.SetTrigger("RightLeg");
            isRightLeg = true;
        }

        if (!_playerActions.RightLeg.WasPressedThisFrame())
        {
            isRightLeg = false;
        }

    }

    // need to hold to breathe air or drown.
    public void UseBreathe()
    {
        //debug only
        if (_playerActions.Breathe.WasPressedThisFrame())
        {
            string newLine = "";
            if (keyPressed)
            {
                newLine = "\n ";
                testInputText.text += newLine + "Breathe";
            }
            else
            {
                testInputText.text = "Breathe";
            }
            keyPressedTrue();
            _staminaManager.UseBreath(); // Use breath
        }

        if (_playerActions.Breathe.WasPressedThisFrame() && !isBreathe)
        {

            m_Animator.SetTrigger("Breathe");
            isBreathe = true;
        }

        if (!_playerActions.Breathe.WasPressedThisFrame())
        {
            isBreathe = false;
        }
    }

    private void keyPressedTrue()
    {
        keyPressed = true;
    }

    private void ApplyWaterDrag()
    {
        if (rb != null)
        {
            // Simple drag: reduce velocity each physics step
            rb.linearVelocity *= Mathf.Clamp01(1f - waterDrag * Time.fixedDeltaTime);
        }
    }


    ///----------------- Event handlers ---------------

    /// <summary>
    /// Called by animation event to apply left arm force
    /// </summary>
    public void ApplyLeftArmForce()
    {
        if (rb != null)
        {
            Vector2 forceDir = (-transform.right + transform.up).normalized;
            rb.AddForce(forceDir * armForce, ForceMode2D.Impulse);
        }
    }

    /// <summary>
    /// Called by animation event to apply right arm force
    /// </summary>
    public void ApplyRightArmForce()
    {
        if (rb != null)
        {
            Vector2 forceDir = (transform.right + transform.up).normalized;
            rb.AddForce(forceDir * armForce, ForceMode2D.Impulse);
        }
    }

    public void ApplyLeftLegForce()
    {
        if (rb != null)
        {
            Vector2 forceDir = (-transform.right + transform.up).normalized;
            rb.AddForce(forceDir * legForce, ForceMode2D.Impulse);
        }
    }

    public void ApplyRightLegForce()
    {
        if (rb != null)
        {
            Vector2 forceDir = (transform.right + transform.up).normalized;
            rb.AddForce(forceDir * legForce, ForceMode2D.Impulse);
        }
    }

    ///------------Animation --------------


}
