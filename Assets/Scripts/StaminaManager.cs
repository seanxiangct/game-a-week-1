using TMPro;
using UnityEngine;
using UnityEngine.WSA;

public class StaminaManager {

    private PlayerController _playerController;
    private int stamValueArmL, stamValueArmR, stamValueLegL, stamValueLegR, stamValueBreath;
    private float MaxStaminaPenalty => Player.sameLimbUseMaxPenatly;

    public StaminaManager(PlayerController playerController) {
        _playerController = playerController;
        stamValueArmL = 0;
        stamValueArmR = 0;
        stamValueLegL = 0;
        stamValueLegR = 0;
        stamValueBreath = 0;
        StaminaAmountText();
        StaminaLimbUseText();
        _playerController.staminaModified.text = "";
        _playerController.breathModifyStamina.text = "";
    }

    public void StaminaAmountText() {
        _playerController.staminaUI.text = "Stamina: " + Player.Stamina.ToString();
        ChangeStaminaTextColour(Player.Stamina);
    }

    private void ChangeStaminaTextColour(float value) {
        if (value < 20f) {
            _playerController.staminaUI.color = Color.red;
        } else if (value < 40f) {
            _playerController.staminaUI.color = Color.magenta;
        } else if (value < 60f) {
            _playerController.staminaUI.color = Color.yellow;
        } else if (value < 80f) {
            _playerController.staminaUI.color = Color.cyan;
        } else {
            _playerController.staminaUI.color = Color.green;
        }
    }

    public void LimbsStaminaModified(int amount, string text, bool good = false) {
        if (amount == 0) {
            _playerController.staminaModified.text = "";
            return;
        }
        _playerController.staminaModified.color = good ? Color.green : Color.red;
        _playerController.staminaModified.text = text + "\n" + (good ? "+ " : "- ") + (amount != 0 ? amount.ToString() : "");
    }

    public void BreathModifiedText(int amount, string text, bool good = false) {
        if (amount == 0) {
            _playerController.breathModifyStamina.text = "";
            return;
        }
        _playerController.breathModifyStamina.color = good ? Color.green : Color.red;
        _playerController.breathModifyStamina.text =
            (text + "\n") +
            (good ? "+ " : "- ") +
            (amount != 0 ? amount.ToString() : " ");
    }

    public void NeedToBreathText(int amount) {
        _playerController.breathModifyStamina.color = Color.red;
        _playerController.breathModifyStamina.text = "You need to breathe!\n - " + amount;
    }

    public void StaminaLimbUseText() {
        _playerController.stamArmL.text = "Left Arm Penalty: " + stamValueArmL.ToString();
        _playerController.stamArmR.text = "Right Arm Penalty: " + stamValueArmR.ToString();
        _playerController.stamLegL.text = "Left Leg Penalty: " + stamValueLegL.ToString();
        _playerController.stamLegR.text = "Right Leg Penalty: " + stamValueLegR.ToString();
        _playerController.stamBreath.text = "Breath Penalty: " + stamValueBreath.ToString();
    }

    public void LoseStamina(float amount) {
        Player.Stamina -= amount;
        StaminaAmountText();
    }

    public void GainStamina(float amount) {
        Player.Stamina += amount;
        StaminaAmountText();
    }

    public void UseLegs(bool rightLeg) {

        bool textUsed = false;

        Debug.Log(_usedLegsTimer);
        if (_usedLegsTimer < _movingLegsTooFastThreshold && _usedLegsTimer > 0) {
            LoseStamina(1);
            LimbsStaminaModified(1, "Using Legs too fast!");
            textUsed = true;
        }

        if (rightLeg) {
            if (stamValueLegR < MaxStaminaPenalty)
                stamValueLegR++;
                stamValueLegL = 0;
        } else {
            if (stamValueLegL < MaxStaminaPenalty)
                stamValueLegL++;
                stamValueLegR = 0;
        }

        int penalty = Mathf.Max(stamValueLegR, stamValueLegL) - 1;
        if (penalty > 0) {
            LoseStamina(penalty);
            LimbsStaminaModified(penalty, "Same leg used");
        } else {
            if (!textUsed) LimbsStaminaModified(0, "", true);
        }

        StaminaLimbUseText();
        _usedLegsTimer = 0f;
        _usedLegsRecently = true;

    }

    public void UseArms(bool rightArm) {

        bool textUsed = false;

        if (_usedArmsTimer < _movingArmsTooFastThreshold && _usedArmsTimer > 0) {
            LoseStamina(1);
            LimbsStaminaModified(1, "Using Arms too fast!");
            textUsed = true;
        }

        if (rightArm) {
            if (stamValueArmR < MaxStaminaPenalty)
                stamValueArmR++;
                stamValueArmL = 0;
        } else {
            if (stamValueArmL < MaxStaminaPenalty)
                stamValueArmL++;
                stamValueArmR = 0;
        }
        int penalty = Mathf.Max(stamValueArmR, stamValueArmL) - 1;
        if (penalty > 0) {
            LoseStamina(penalty);
            LimbsStaminaModified(penalty, "Same arm used");
        } else {
            if (!textUsed) LimbsStaminaModified(0, "", true);
        }
        StaminaLimbUseText();
        _usedArmsTimer = 0f;
        _usedArmsRecently = true;

    }

    private float _breathTimer = 0f;
    private int _breathDesperationCount = 1;

    public void UseBreath() {

        if (_breathTimer < Player.breathTooFastTime) {
            LoseStamina(Player.breathTooFastPenalty);
            BreathModifiedText((int)Player.breathTooFastPenalty, "Breathing too fast!");
        } else {
            if (Player.Stamina < 100) {
                GainStamina(Player.breathStaminaGain);
                BreathModifiedText((int)Player.breathStaminaGain, "", true);
            }
        }

        _breathTimer = 0f;
        _breathDesperationCount = 1;
    }


    public void BreathingUpdate() {

        //Debug.Log("Breathing Update called, timer: " + _breathTimer + ", desperation count: " + _breathDesperationCount);

        _breathTimer += Time.deltaTime;

        if (_breathTimer >= Player.needToBreathTime) {
            LoseStamina(_breathDesperationCount);
            NeedToBreathText(_breathDesperationCount);
            _breathDesperationCount++;
            _breathTimer = 4f;            
        }

    }

    private bool _usedArmsRecently = false;
    private bool _usedLegsRecently = false;
    private float _usedArmsTimer = -10f;
    private float _usedLegsTimer = -10f;
    private float _gainStaminaTimer = 0f;
    private float _notMovingTimer = -10f;
    private int _notMovingCount = 0;
    private float _movingArmsTooFastThreshold = 0.35f;
    private float _movingLegsTooFastThreshold = 0.15f;
    private float _needToUseAllLimbsThreshold = 4f;
    private float _needToUseAllLimbsTimer = 0f;

    public void NeedToUseAllLimbsUpdate() {

        if (_usedArmsRecently) {
            _usedArmsTimer += Time.deltaTime;
            if (_usedArmsTimer >= 5f) {
                _usedArmsRecently = false;
            }
        }
        if (_usedLegsRecently) {
            _usedLegsTimer += Time.deltaTime;
            if (_usedLegsTimer >= 5f) {
                _usedLegsRecently = false;
            }
        }

        if (_usedArmsRecently && _usedLegsRecently) {
            _gainStaminaTimer += Time.deltaTime;
            _needToUseAllLimbsTimer = 0f;
        } else {
            _needToUseAllLimbsTimer += Time.deltaTime;
        }


        if (_gainStaminaTimer >= 2f) {
            GainStamina(1);
            LimbsStaminaModified(1, "", true);
            _gainStaminaTimer = 0f;
        }

        if (!_usedArmsRecently && !_usedLegsRecently) {
            _notMovingTimer += Time.deltaTime;
            if (_notMovingTimer >= 3f) {
                LoseStamina(1 + _notMovingCount);
                LimbsStaminaModified(1 + _notMovingCount, "Not Moving");
                _notMovingTimer = 0f;
                _notMovingCount++;
            }
        } else if (_needToUseAllLimbsTimer >= _needToUseAllLimbsThreshold) {
            LoseStamina(3);
            LimbsStaminaModified(3, "Not using all limbs!");
            _needToUseAllLimbsTimer = 3f;
        } else {
            _notMovingTimer = 0f;
        }
    }

}
