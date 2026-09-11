using TMPro;
using UnityEngine;

public class Potions : MonoBehaviour, ITakeObject
{
    
    [SerializeField] private TextMeshProUGUI textPotions;
    public void TakeObject()
    {
        Movement playermovement = FindFirstObjectByType<Movement>();
        if (playermovement != null) {

            playermovement.healtPotions++;
            textPotions.text = playermovement.healtPotions.ToString();
            Destroy(gameObject);
            
        }
        
    }
}
