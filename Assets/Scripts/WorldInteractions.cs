using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldInteractions : MonoBehaviour
{
    private void OnExit()
    {
       Application.Quit();
    }

    private void OnStartGame()
    {
        SceneManager.LoadScene("Gameplay");
    }
}