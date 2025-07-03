using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //SINGLETON
    private static GameManager _instance;

    public static GameManager Instance
    {
        get {
            if (_instance == null)
            {
                Debug.LogError("Game manager instance is null.");
            }
            return _instance;
        }
    }
    
    public static event Action OnEndGame;
    public static event Action OnEndStart;

    [SerializeField] private float _initialStamina = 100.00f;
    [SerializeField] private float _initialOxygenLevel = 100.00f;

    public float InitialStamina =>_initialStamina;
    public float InitialOxygenLevel => _initialOxygenLevel;
    
    
    private void Awake()
    {
        if (_instance != null)
        {
            //destroy duplicates
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        Player.GameStarted = true;
    }

    private void Update()
    {
        
    }

    //resets the fields to starting params
    private void InitSetting()
    {
        //todo: set up the initial setting of the game.
    }

    /// <summary>
    /// Starts the game
    /// </summary>
    public void StartGame()
    {
        
    }

    /// <summary>
    /// Ends the game
    /// </summary>
    public static void EndGame()
    {
        OnEndGame?.Invoke();
        Time.timeScale = 0;
    }

    /// <summary>
    /// Pause game time
    /// </summary>
    public void PauseGame()
    {
        
    }

    /// <summary>
    /// Start game time, continue play the game after pausing.
    /// </summary>
    public void UnPauseGame()
    {
        
    }
}
