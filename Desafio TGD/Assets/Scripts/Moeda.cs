using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moeda : MonoBehaviour
{

    public BoxCollider2D Col;
    public SpriteRenderer Sr;
    public AudioSource aud;

    // Start is called before the first frame update
    void Start()
    {
        Col = GetComponent<BoxCollider2D>();
        Sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.GetComponent<PlayerControl>().moedas++;
            aud.Play();
            Sr.enabled = false;
            Col.enabled = false;
        }
    }
}
