using UnityEngine;
using UnityEditor.SearchService;
using System.Collections.Generic;
using UnityEngine.Rendering; //Para usar diccionario 
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    
    // Variables que transportamos entre escenas
    public float diamonds;
    public float healthPotions;
    public float savedHealth = 100f;
    public float maxHealt = 100f;
    public float savedSpeed = 4;
    public int savedDamage = 25;
    public float scene;
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
