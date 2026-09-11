using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    // Variables que transportamos entre escenas
    public int diamonds;
    public int healthPotions;
    public float savedHealth = 100f;
    public int savedSpeed = 4;
    public int savedDamage = 25;

    private void Awake()
    {
        if(Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
