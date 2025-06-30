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
    public void EndGame()
    {
        
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
