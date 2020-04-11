using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mace : MonoBehaviour
{
    private Rigidbody2D Rigid;
    private Transform Trans;
    public LayerMask PlayerLayer;
    private Animator anim;

    private bool sobe = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Rigid = GetComponent<Rigidbody2D>();
        Trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Trans.position.y <= 0.18)
        {
            sobe = true;
        }
        if(Trans.position.y > 6.21)
        {
            sobe = false;
        }
        PodeCair();
        if (sobe == true)
         {
            Rigid.gravityScale = 0f;
            Trans.position += new Vector3(0f, 0.05f, 0f);
         }

    }

    private void PodeCair()
    {
        Debug.DrawRay(new Vector2(Trans.position.x - 1f, Trans.position.y), Trans.TransformDirection(Vector2.down) * 8f, Color.red);
        Debug.DrawRay(new Vector2(Trans.position.x + 1f, Trans.position.y), Trans.TransformDirection(Vector2.down) * 8f, Color.red);
        RaycastHit2D PodeCair;
        RaycastHit2D PodeCair2;
        PodeCair = Physics2D.Raycast(new Vector2(Trans.position.x - 1f, Trans.position.y), Trans.TransformDirection(Vector2.down), 8f, PlayerLayer);
        PodeCair2 = Physics2D.Raycast(new Vector2(Trans.position.x + 1f, Trans.position.y), Trans.TransformDirection(Vector2.down), 8f, PlayerLayer);
        if (PodeCair || PodeCair2)
        {
            Rigid.gravityScale = 7f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerControl>().morreu();
        }
    }
}
