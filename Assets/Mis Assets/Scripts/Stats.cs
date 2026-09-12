using TMPro;
using UnityEngine;

public class Stats : MonoBehaviour
{
    
    
    [SerializeField] private TextMeshProUGUI textMaxHealth;
    [SerializeField] private TextMeshProUGUI textDamage;
    [SerializeField] private TextMeshProUGUI textSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement playerMovement = FindFirstObjectByType<Movement>();// Buscamos al jugador en la escena
        textMaxHealth.text = playerMovement.MaxHealth.ToString();
        textDamage.text = playerMovement.damage.ToString();
        textSpeed.text = playerMovement.speed.ToString();
    }
}
