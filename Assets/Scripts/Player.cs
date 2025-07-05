using System;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
   public static bool GameStarted = false; // Indicates if the game has started
   public static bool GameOver = false; // Indicates if the game is over

   [SerializeField] private static float _stamina;
   public static float sameLimbUseMaxPenatly = 4; // Max penalty for using the same limb too much
   public static float breathTooFastTime = 3; // Time in seconds for breathing too fast
   public static float breathTooFastPenalty = 3; // Penalty for breathing too fast
   public static float needToBreathTime = 5; // Time in seconds before the player needs to breathe
   public static float breathStaminaGain = 5; // Stamina gained from breathing

   public static Action OnDeath; // Action to call when the player dies

   private enum Death
   {
      Tired,
      DuckCollision
   }

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

   private void Awake()
   {
      Debug.Log("Player Awake: Instance created");
   }

   private void OnEnable()
   {
      Debug.Log("Player OnEnable: Subscribing to GameManager.OnEndStart");
      GameManager.OnEndStart += Reset;
   }

   private void Start()
   {
      Debug.Log("Player Start: GameStarted set true, calling Reset");
      GameStarted = true; // Set the game as started
      Reset();
   }

   private void Update()
   {
      //Player dies when they are too tired or drown
      //if (_stamina<=0 && _oxygenLevel<=0)
      if (_stamina <= 0)
      {
         Debug.Log("Player Update: Stamina depleted, calling Die");
         Die(Death.Tired);
      }
   }

   private static void Die(Death deathType)
   {
      Debug.Log($"Player Die: DeathType={deathType}");
      // GameOver = true;
      // GameManager.EndGame();

      OnDeath?.Invoke(); // Invoke the death action

      SceneManager.LoadScene("SampleScene"); // Load the GameOver scene
   }

   private void Reset()
   {
      Debug.Log("Player Reset: Resetting stamina and oxygen");
      _stamina = GameManager.Instance.InitialStamina;
      _oxygenLevel = GameManager.Instance.InitialOxygenLevel;
   }

   private void OnCollisionEnter2D(Collision2D collision)
   {
      if (collision.gameObject.CompareTag("duck"))
      {
         Debug.Log("Collided with duck! Calling Die.");
         Die(Death.DuckCollision);
      }
   }

}
