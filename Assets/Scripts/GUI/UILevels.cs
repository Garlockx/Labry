using UnityEngine;
using UnityEngine.SceneManagement;

public class UILevels : MonoBehaviour
{
    public void returnToWorld()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName.Substring(0, sceneName.IndexOf("L")));
    }
}
