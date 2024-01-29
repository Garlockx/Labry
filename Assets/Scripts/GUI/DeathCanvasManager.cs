using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathCanvasManager : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            replayButtonOnClick();
        }
    }

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
