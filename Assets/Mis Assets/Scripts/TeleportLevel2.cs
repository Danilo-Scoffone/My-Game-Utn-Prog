using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportLevel2 : MonoBehaviour, IChangeScene
{
    
    public void Teleport()
    {
        
        Movement playerMovement = FindFirstObjectByType<Movement>();
        playerMovement.SaveStatsToManager();
        SceneManager.LoadScene("Level 2");
    }
}
