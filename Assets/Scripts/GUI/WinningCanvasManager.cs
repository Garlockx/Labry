using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningCanvasManager : MonoBehaviour
{
    public void replayButtonOnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void backButtonOnClick()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName.Substring(0, sceneName.IndexOf("L")), LoadSceneMode.Single);
    }
}
