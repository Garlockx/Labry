using UnityEngine;
using UnityEngine.SceneManagement;

public class UILevel : MonoBehaviour
{
    private LevelGenerationManager levelGenerationManager;
    private CameraManager cameraManager;

    private void Start()
    {
        levelGenerationManager = FindObjectOfType<LevelGenerationManager>();
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraManager == null)
        {
            cameraManager = Camera.main.GetComponent<CameraManager>();
        }
        arrowDisplay();
    }

    private void arrowDisplay()
    {
        if (!gameObject.transform.GetChild(0).gameObject.activeInHierarchy && Camera.main.transform.position.y > 0.5f)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (gameObject.transform.GetChild(0).gameObject.activeInHierarchy && Camera.main.transform.position.y <= 0.5f)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }

        if (gameObject.transform.GetChild(1).gameObject.activeInHierarchy && Camera.main.transform.position.y == 0.5f + (3.0f * (levelGenerationManager.getLevelNumber() - 1)))
        {
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
        else if (!gameObject.transform.GetChild(1).gameObject.activeInHierarchy && Camera.main.transform.position.y != 0.5f + (3.0f * (levelGenerationManager.getLevelNumber() - 1)))
        {
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void moveCameraDown()
    {
        Vector3 mainCameraPos = Camera.main.transform.position;
        mainCameraPos.y += -3.0f;
        cameraManager.setCameraOriginPos(mainCameraPos);
        Camera.main.transform.position = mainCameraPos;
    }

    public void moveCameraUp()
    {
        Vector3 mainCameraPos = Camera.main.transform.position;
        mainCameraPos.y += 3.0f;
        cameraManager.setCameraOriginPos(mainCameraPos);
        Camera.main.transform.position = mainCameraPos;
    }

    public void returnToWorldSelection()
    {
        SceneManager.LoadScene(0);
    }
}
