using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    public void resetButtonClick()
    {
        PlayerPrefs.SetInt("World", 1);
        PlayerPrefs.SetInt("Level", 1);
        PlayerPrefs.SetString("W1", "");
    }

    public void returnButtonClick()
    {
        SceneManager.LoadScene("WorldSelection");
    }
}
