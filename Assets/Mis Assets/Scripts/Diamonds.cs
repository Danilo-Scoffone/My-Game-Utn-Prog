using TMPro;
using UnityEngine;

public class Diamont : Collectible
{
    [SerializeField] private TextMeshProUGUI textDiamonds;

    protected override void OnCollect(Movement player)
    {
        player._diamonds++;

        // 2. Buscamos el texto de UI en el Canvas por su Tag
        GameObject textObj = GameObject.FindWithTag("TextDiamonds");
        if (textObj != null)
        {
            TextMeshProUGUI textDiamonds = textObj.GetComponent<TextMeshProUGUI>();
            if (textDiamonds != null)
            {
                textDiamonds.text = player._diamonds.ToString();
            }
        }
    }
}

