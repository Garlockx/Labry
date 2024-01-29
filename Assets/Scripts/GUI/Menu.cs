using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static bool menuIsActive = true;

    private void Awake()
    {
        if (menuIsActive)
        {
            gameObject.SetActive(true);
            menuIsActive = false;
        } else if (!menuIsActive)
        {
            gameObject.SetActive(false);
        }
    }

    public void activeMenu()
    {
        gameObject.SetActive(true);
    }

    public void deactiveMenu()
    {
        gameObject.SetActive(false);
    }

    public bool getMenuActivity()
    {
        return gameObject.activeInHierarchy;
    }

    public void optionsButton()
    {
        SceneManager.LoadScene("Options");
    }

    public void quitButton()
    {
        Application.Quit();
    }
}
