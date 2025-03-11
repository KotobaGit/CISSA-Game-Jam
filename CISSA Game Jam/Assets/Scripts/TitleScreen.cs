using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Rec Room");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
