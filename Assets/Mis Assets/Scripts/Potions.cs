using TMPro;
using UnityEngine;

public class Potions : Collectible
{
    
    [SerializeField] private TextMeshProUGUI textPotions;
    protected override void OnCollect(Movement player)
    {
        
        if (player != null)
        {
            player.healtPotions++;
            textPotions.text = player.healtPotions.ToString();
            

        }
    }
}
