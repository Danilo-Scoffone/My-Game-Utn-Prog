using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneVictory : MonoBehaviour,IChangeScene
{
    public void Teleport()
    {
        SceneManager.LoadScene("SceneVictory");
    }
}
