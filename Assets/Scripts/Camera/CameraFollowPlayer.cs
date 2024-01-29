using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("PlayerCollider");
        if (player != null)
        {
            followPlayer(player);
        }
    }

    private void followPlayer(GameObject player)
    {
        Vector3 newCameraPos = transform.position;
        newCameraPos.x = player.transform.position.x;
        newCameraPos.y = player.transform.position.y;
        transform.position = newCameraPos;
    }
}
