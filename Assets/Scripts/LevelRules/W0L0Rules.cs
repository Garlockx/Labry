using UnityEngine;

public class W0L0Rules : MonoBehaviour
{
    GameObject player;
    TileMapManager tileMapManager;
    bool endTuto = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerCollider");
        tileMapManager = FindObjectOfType<TileMapManager>();
    }

    void Update()
    {
        if(endTuto == true)
        {
            return;
        }
        checkRightMove();
    }

    private void checkRightMove()
    {
        if (player.gameObject.transform.localPosition == new Vector3(2.0f, 0.0f, 0.0f))
        {
            tileMapManager.invokeWinning();
            endTuto = true;
        }
    }
}
