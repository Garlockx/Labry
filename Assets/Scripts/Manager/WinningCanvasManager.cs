using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningCanvasManager : MonoBehaviour
{
    public void restartButtonOnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LevelSelectionButtonOnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name.Substring(0, 2), LoadSceneMode.Single);
    }
}
