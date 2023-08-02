using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathExplosion : MonoBehaviour
{
    public float force = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        Explode();
    }

    private void Explode()
    {
        foreach (Transform child in transform)
        {
            Rigidbody2D rigidbody2D = child.gameObject.AddComponent<Rigidbody2D>();
            rigidbody2D.gravityScale = 0;
            Vector3 explodeDirection = child.localPosition * 10.0f;
            rigidbody2D.AddForce(explodeDirection * force, ForceMode2D.Impulse);
        }
    }

}
