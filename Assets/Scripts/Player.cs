using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static bool GameStarted = false; // Indicates if the game has started
    public static bool GameOver = false; // Indicates if the game is over

    [SerializeField] private static float _stamina;
    public static float sameLimbUseMaxPenatly = 4; // Max penalty for using the same limb too much
    public static float breathTooFastTime = 3; // Time in seconds for breathing too fast
    public static float breathTooFastPenalty = 3; // Penalty for breathing too fast
    public static float needToBreathTime = 5; // Time in seconds before the player needs to breathe
    public static float breathStaminaGain = 4; // Stamina gained from breathing

    public static float Stamina
   {
      get => _stamina;
      set => _stamina = value;
   }
   
   [SerializeField] private static float _oxygenLevel;

   public static float OxygenLevel
   {
      get => _oxygenLevel;
      set => _oxygenLevel = value;
   }

   private void OnEnable()
   {
      GameManager.OnEndStart += Reset;
   }

   private void Start()
   {
        GameStarted = true; // Set the game as started
        Reset();
   }

   private void Update()
   {
      //Player dies when they are too tired or drown
      //if (_stamina<=0 && _oxygenLevel<=0)
      if (_stamina<=0)
      {
         Die();
      }
   }

   private static void Die()
   {
        GameOver = true;
        GameManager.EndGame();
   }

   private void Reset()
   {
      _stamina = GameManager.Instance.InitialStamina;
      _oxygenLevel = GameManager.Instance.InitialOxygenLevel;
   }
}
