using UnityEngine;
using System.Collections.Generic;
using Unity.Mathematics; //Para usar diccionario 
public class Store : MonoBehaviour
{
    
    [SerializeField] private int requiredDiamond;

   

  
    private void Start()
    {
        
        
    }
    
    public void BuyItem(string ItemName)
    {
        //Store
        Dictionary<string, int> itemStore = new Dictionary<string, int>();
        itemStore.Add("PowerUpSword", 5);
        itemStore.Add("PowerUpHealth", 8);
        itemStore.Add("PowerUpSpeed", 10);
        Movement playerMovement = FindFirstObjectByType<Movement>();// Buscamos al jugador en la escena
        if (playerMovement != null)// si encuentra recibe los datos del pj
        {
            
            GameObject Panelbuy = playerMovement.GetPanelBuy();
            GameObject PanelPurchaseRejected = playerMovement.GetPanelPurchaseRejected();
            switch (ItemName) //vemos q item selecciono y su precio
            {
                case "PowerUpSword":
                    requiredDiamond = itemStore["PowerUpSword"];
                    break;
                case "PowerUpHealth":
                    requiredDiamond = itemStore["PowerUpHealth"];
                    break;
                case "PowerUpSpeed":
                    requiredDiamond = itemStore["PowerUpSpeed"];
                    break;
            }
            if (playerMovement._diamonds >= requiredDiamond) // verificamos si tiene los suficientes diamantes 
            {
                Panelbuy.SetActive(true);
                Invoke("RemovePanel", 2f);
                playerMovement._diamonds -= requiredDiamond; //restamos los diamantes
               
                switch (ItemName) //vemos q item selecciono 
                {
                    case "PowerUpSword":
                        playerMovement.damage =Mathf.RoundToInt(playerMovement.damage * 1.25f);
                        
                        break;
                    case "PowerUpHealth":
                        playerMovement.MaxHealth =(playerMovement.MaxHealth * 1.50f);
                       
                        break;
                    case "PowerUpSpeed":
                        playerMovement.speed =(playerMovement.speed * 1.50f);
                        break;
                }
                
            }
            else
            {
                PanelPurchaseRejected.SetActive(true);
                Invoke("RemovePanel", 2f);
            }
           

        }

      
    }
    public void RemovePanel()
    {
        Movement playerMovement = FindFirstObjectByType<Movement>();
        if (playerMovement != null)
        {
            GameObject Panelbuy = playerMovement.GetPanelBuy();
            GameObject PanelPurchaseRejected = playerMovement.GetPanelPurchaseRejected();
            if (Panelbuy.activeInHierarchy ) {Panelbuy.SetActive(false);}
            if (PanelPurchaseRejected.activeInHierarchy) {PanelPurchaseRejected.SetActive(false);}
        }
        
          
    }
}



