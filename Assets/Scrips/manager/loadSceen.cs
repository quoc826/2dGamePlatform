using UnityEngine;
using UnityEngine.SceneManagement;
public class loadSceen : MonoBehaviour
{
    public void OnClickExit()
    {
        Application.Quit();
    }

    public void OnClickPlay(string level)
    {
        SceneManager.LoadScene(level);
    }
}
