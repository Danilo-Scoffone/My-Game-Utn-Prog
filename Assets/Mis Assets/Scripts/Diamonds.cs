using TMPro;
using UnityEngine;

public class Diamont : MonoBehaviour, ITakeObject
{
    [SerializeField] private TextMeshProUGUI textDiamonds;

    public void TakeObject()
    {
        Movement playermovement = FindFirstObjectByType<Movement>();

        if (playermovement != null)
        {
            
            playermovement._diamonds++;

            // 2. Buscamos el texto de UI en el Canvas por su Tag
            GameObject textObj = GameObject.FindWithTag("TextDiamonds");
            if (textObj != null)
            {
                TextMeshProUGUI textDiamonds = textObj.GetComponent<TextMeshProUGUI>();
                if (textDiamonds != null)
                {
                    textDiamonds.text = playermovement._diamonds.ToString();
                }
            }
        }

        
        Destroy(gameObject);
    }
}

