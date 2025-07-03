using TMPro;
using UnityEngine;

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
    }

    public void StaminaAmountText() {
        _playerController.staminaTestText.text = "Stamina: " + Player.Stamina.ToString();
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

    public void UseLegs(bool rightLeg) {

        LoseStamina(1);

        if (rightLeg) {
            if (stamValueLegR < MaxStaminaPenalty)
                stamValueLegR++;
        } else {
            if (stamValueLegL < MaxStaminaPenalty)
                stamValueLegL++;
        }

        int legDifference = Mathf.Abs(stamValueLegR - stamValueLegL);

        if (legDifference > 1) {
            LoseStamina(legDifference);

        }

        if (rightLeg) {
            stamValueLegL = 0;
        } else {
            stamValueLegR = 0;
        }

        if (stamValueLegR == stamValueLegL) {
            stamValueLegR = 0;
            stamValueLegL = 0;
        }

        StaminaLimbUseText();

    }

    public void UseArms(bool rightArm) {

        LoseStamina(1);

        if (rightArm) {
            if (stamValueArmR < MaxStaminaPenalty)
                stamValueArmR++;
        } else {
            if (stamValueArmL < MaxStaminaPenalty)
                stamValueArmL++;
        }

        int armDifference = Mathf.Abs(stamValueArmR - stamValueArmL);

        if (armDifference > 1) {
            LoseStamina(armDifference);
        }

        if (rightArm) {
            stamValueArmL = 0;
        } else {
            stamValueArmR = 0;
        }

        if (stamValueArmR == stamValueArmL) {
            stamValueArmR = 0;
            stamValueArmL = 0;
        }

        StaminaLimbUseText();

    }






}
