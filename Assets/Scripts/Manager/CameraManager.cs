using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Vector3 cameraOriginPos;
    private SelectionManager selectionManager;
    private List<string> spritesNames;

    // Start is called before the first frame update
    void Start()
    {
        cameraOriginPos = gameObject.transform.position;
        selectionManager = FindObjectOfType<SelectionManager>();
        spritesNames = selectionManager.spritesNames;
    }

    // Update is called once per frame
    void Update()
    {
        moveCameraToTile();
    }


    /*
     * Move camera to tile mouseover
     */
    private void moveCameraToTile()
    {
        Vector3Int currentOverTilePosition = selectionManager.grid.WorldToCell(getMousePosition());
        Sprite tileSprite = selectionManager.tilemap.GetSprite(currentOverTilePosition);
        Vector3 tileCenterWorldPos = selectionManager.grid.GetCellCenterWorld(currentOverTilePosition);
        if (tileSprite == null || !spritesNames.Contains(tileSprite.name))
        {
            if (gameObject.transform.position == cameraOriginPos)
            {
                return;
            }
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, cameraOriginPos, 1.3f * Time.deltaTime);
            return;
        }
        tileCenterWorldPos.z = gameObject.transform.position.z;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, tileCenterWorldPos, 1.3f * Time.deltaTime);

    }

    public Vector3 getMousePosition()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0.0f;
        return mouseWorldPosition;
    }
}
