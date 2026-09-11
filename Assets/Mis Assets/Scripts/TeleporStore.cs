using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleporStore : MonoBehaviour, IChangeScene
{
    

    public void Teleport()
    {
        Movement playerMovement = FindFirstObjectByType<Movement>();
        playerMovement.SaveStatsToManager();
        SceneManager.LoadScene("Store");
        
    }
}
