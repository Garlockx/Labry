using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoaderManager : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1.0f;

    public void loadScene(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }

    IEnumerator LoadScene(string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
    }
    
}
