using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class TeleporStore : MonoBehaviour, IChangeScene
{
   
    public void Teleport()
    {

        Movement playerMovement = FindFirstObjectByType<Movement>();
        playerMovement.SaveStatsToManager();
        SceneManager.LoadScene("Store");
        
    }
}
