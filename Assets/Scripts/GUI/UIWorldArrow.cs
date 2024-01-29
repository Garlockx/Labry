using UnityEngine;

public class UIWorldArrow : MonoBehaviour
{
    private WorldGenerationManager worldGenerationManager;
    private CameraManager cameraManager;

    private void Start()
    {
        worldGenerationManager = FindObjectOfType<WorldGenerationManager>();
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
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
        if (!gameObject.transform.GetChild(0).gameObject.activeInHierarchy && Camera.main.transform.position.x > 1.5f)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        } else if (gameObject.transform.GetChild(0).gameObject.activeInHierarchy && Camera.main.transform.position.x <= 1.5f)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }

        if (gameObject.transform.GetChild(1).gameObject.activeInHierarchy && Camera.main.transform.position.x == 1.5f + (3.0f * (worldGenerationManager.getWorldNumber() - 1)))
        {
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
        } else if (!gameObject.transform.GetChild(1).gameObject.activeInHierarchy && Camera.main.transform.position.x != 1.5f + (3.0f * (worldGenerationManager.getWorldNumber() - 1)))
        {
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void moveCameraToLeft()
    {
        Vector3 mainCameraPos = Camera.main.transform.position;
        mainCameraPos.x += -3.0f;
        cameraManager.setCameraOriginPos(mainCameraPos);
        Camera.main.transform.position = mainCameraPos;
    }

    public void moveCameraToRight()
    {
        Vector3 mainCameraPos = Camera.main.transform.position;
        mainCameraPos.x += 3.0f;
        cameraManager.setCameraOriginPos(mainCameraPos);
        Camera.main.transform.position = mainCameraPos;
    }
}
