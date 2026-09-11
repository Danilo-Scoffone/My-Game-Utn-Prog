using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportLevel2 : MonoBehaviour, IChangeScene
{
    Movement playerMovement = FindFirstObjectByType<Movement>();
    public void Teleport()
    {
        Movement playerMovement = FindFirstObjectByType<Movement>();
        playerMovement.SaveStatsToManager();
        SceneManager.LoadScene("Level 2");
    }
}
