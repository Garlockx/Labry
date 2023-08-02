using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SelectionManager : MonoBehaviour
{
    public Tilemap tilemap;

    public Grid grid;
    private BoundsInt.PositionEnumerator area;

    // Define grow scale for world tiles in scene
    private float growScale = 1.1f;
    // Define max distance between tiles and mouse where tiles get shrinked 
    private float maxDistance = 0.5f;
    // List of tile sprite names who can be a selection world (can be shrink and select)
    public List<string> spritesNames = new List<string>
    {
        "Tileset_Labirynth_4",
    };

    // Tile that can be clicked on to select a world
    private List<TileData> worldSelectionTiles = new List<TileData>();

    private SceneLoaderManager sceneLoaderManager;

    private struct TileData
    {
        public Tile tile;
        public Vector3Int tilePosition;
        public string sceneName;

        public TileData(Tile tile, Vector3Int tilePosition, string sceneName)
        {
            this.tile = tile;
            this.tilePosition = tilePosition;
            this.sceneName = sceneName;
        }
    }

    private void Start()
    {
        grid = gameObject.GetComponent<Grid>();
        area = tilemap.cellBounds.allPositionsWithin;
        sceneLoaderManager = FindObjectOfType<SceneLoaderManager>();
        fillWorldSelectionTiles();
        foreach(TileData tile in worldSelectionTiles)
        {
            tilemap.SetTileFlags(tile.tilePosition, TileFlags.None);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            selectWorld();
            return;
        }
        foreach (TileData tile in worldSelectionTiles)
        {
            resizeTileSprite(Camera.main.gameObject.GetComponent<CameraManager>().getMousePosition(), tile.tilePosition);
        }
    }

    /*
     * Change scene to selected world
     */
    private void selectWorld()
    {
        Vector3Int currentOverTilePosition = grid.WorldToCell(Camera.main.gameObject.GetComponent<CameraManager>().getMousePosition());
        if (tilemap.GetSprite(currentOverTilePosition) != null && !spritesNames.Contains(tilemap.GetSprite(currentOverTilePosition).name)) {
            return;
        }
        foreach (TileData tile in worldSelectionTiles)
        {
            if (tile.tilePosition == currentOverTilePosition)
            {
                sceneLoaderManager.loadScene(tile.sceneName);
            }
        }
    }

    private void fillWorldSelectionTiles()
    {
        int scenedNumber = 0;
        foreach (Vector3Int tilePosition in area)
        {
            if (tilemap.GetSprite(tilePosition) != null && spritesNames.Contains(tilemap.GetSprite(tilePosition).name))
            {
                if (grid.gameObject.GetComponent<WorldGenerationManager>() != null)
                {
                    worldSelectionTiles.Add(new TileData(tilemap.GetTile<Tile>(tilePosition), tilePosition, "W" + scenedNumber));
                } else if (grid.gameObject.GetComponent<LevelGenerationManager>() != null) {
                    worldSelectionTiles.Add(new TileData(tilemap.GetTile<Tile>(tilePosition), tilePosition, SceneManager.GetActiveScene().name + "L" + scenedNumber));
                }
                scenedNumber += 1;
            }
        }
    }

    /*
     * Resize tile in terms of distance between tile and mouse
     */
    private void resizeTileSprite(Vector3 mouseWorldPos, Vector3Int tilePosition)
    {
        Vector3 tileWorldPosition = tilemap.GetCellCenterWorld(tilePosition);
        float distance = Vector3.Distance(mouseWorldPos, tileWorldPosition);
        if (distance > maxDistance)
        {
            tilemap.SetTransformMatrix(tilePosition, Matrix4x4.Inverse(Matrix4x4.Scale(new Vector3(growScale + maxDistance, growScale + maxDistance, growScale + maxDistance))));
            return;
        }
        tilemap.SetTransformMatrix(tilePosition, Matrix4x4.Inverse(Matrix4x4.Scale(new Vector3(growScale + distance, growScale + distance, growScale + distance))));
    }
}
