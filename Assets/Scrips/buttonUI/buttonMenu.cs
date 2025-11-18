using UnityEngine;
using UnityEngine.SceneManagement;
public class buttonMenu : MonoBehaviour
{

    public void loadSceen(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void onCLickQuit()
    {
        Application.Quit();
    }
}
