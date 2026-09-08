using UnityEngine;
using System.Collections.Generic; //Para usar diccionario 
public class Store : MonoBehaviour
{
    [SerializeField] private int DiamonsStore;
    [SerializeField] private int requiredDiamond=0;
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
            DiamonsStore = playerMovement.GetDiamonds();
            int damage = playerMovement.Getdamage();
            float maxHelath = playerMovement.GetmaxHealt();
            int speed = playerMovement.GetSpeed();
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
            if (DiamonsStore >= requiredDiamond) // verificamos si tiene los suficientes diamantes 
            {
                Panelbuy.SetActive(true);
                Invoke("RemovePanel", 2f);
                DiamonsStore -= requiredDiamond; //restamos los diamantes
                playerMovement.SetDiamonds(DiamonsStore);// le mandamos al pj los diamantes restantes
                switch (ItemName) //vemos q item selecciono 
                {
                    case "PowerUpSword":
                        damage = Mathf.RoundToInt(damage * 1.25f);
                        playerMovement.Setdamage(damage);
                        break;
                    case "PowerUpHealth":
                        maxHelath = Mathf.RoundToInt(maxHelath * 1.50f);
                        playerMovement.SetmaxHealt(maxHelath);
                        break;
                    case "PowerUpSpeed":
                        speed = Mathf.RoundToInt(speed * 1.25f);
                        playerMovement.SetSpeed(speed);
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



