using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathCanvasManager : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            restartButtonOnClick();
        }
    }

    public void restartButtonOnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
