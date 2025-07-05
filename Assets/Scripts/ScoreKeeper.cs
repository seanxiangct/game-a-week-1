using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] private float _swimDistance = 0.0f;

    public float SwimDistance
    {
        get => _swimDistance;
        set => _swimDistance = value;
    }

    private static ScoreKeeper _instance;
    public static ScoreKeeper Instance
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
        }
    }
    
    
}
