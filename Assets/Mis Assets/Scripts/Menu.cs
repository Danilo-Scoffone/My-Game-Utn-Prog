using NUnit.Framework;
using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Play()
    {
        SceneManager.LoadScene("SceneGame");
    }
    public void Exit()
    {
        Application.Quit();
        Debug.Log("Cerrando juego");
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");

    }
    public void MenuGame()
    {
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        Time.timeScale = 1f;
    }
   
    
}
