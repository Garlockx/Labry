using UnityEngine;
using UnityEngine.SceneManagement;

public class ThanksCanvas : MonoBehaviour
{
    public void returnButtonClick()
    {
        SceneManager.LoadScene("WorldSelection");
    }
}
