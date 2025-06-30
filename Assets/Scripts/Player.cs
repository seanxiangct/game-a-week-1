using System;
using UnityEngine;

public class Player : MonoBehaviour
{
   [SerializeField] private static float _stamina;

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
      Reset();
   }

   private void Update()
   {
      //Player dies when they are too tired or drown
      if (_stamina<=0 && _oxygenLevel<=0)
      {
         Die();
      }
   }

   private static void Die()
   {
      GameManager.EndGame();
   }

   private void Reset()
   {
      _stamina = GameManager.Instance.InitialStamina;
      _oxygenLevel = GameManager.Instance.InitialOxygenLevel;
   }
}
