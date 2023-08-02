using UnityEngine;

public class W0L2Rules : MonoBehaviour
{
    bool isWinningInvoke = false;

    // Update is called once per frame
    void Update()
    {
        if (isWinningInvoke == true)
        {
            return;
        }
        if (GameObject.FindGameObjectWithTag("PlayerCollider") != null && GameObject.FindGameObjectWithTag("PlayerCollider").transform.position == new Vector3(0.5f, -0.5f, 0.0f))
        {
            FindObjectOfType<TileMapManager>().invokeWinning();
            isWinningInvoke = true;
        }
    }
}
