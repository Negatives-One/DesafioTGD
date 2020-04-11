using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform Trans;
    private Vector2 pos;

    private void Start()
    {
        Trans = GetComponent<Transform>();
        pos = Trans.position;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.GetComponent<PlayerControl>().StartPosition = pos;
        }
    }
}
