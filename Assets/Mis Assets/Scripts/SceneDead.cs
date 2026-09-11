using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDead : MonoBehaviour, IChangeScene
{
    public float timer;
    public void Update()
    {
        timer= timer + Time.deltaTime;
    }
    public void Teleport()
    {
        if (timer>=1)
        {
            SceneManager.LoadScene("SceneDead");
            timer = 0;
        }
    }
}
