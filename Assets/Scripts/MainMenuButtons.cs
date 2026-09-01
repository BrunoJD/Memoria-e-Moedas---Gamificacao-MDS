using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuButtons : MonoBehaviour
{

    public void OnLCreateGameEvent(){
        SceneManager.LoadScene("Jogo");
    }

    public void OnExitEvent(){
        Application.Quit();
    }
}
