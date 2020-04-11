using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    private bool RollTimer;
    public float TimeToRespawn;
    private float CurrentTime;

    public CircleCollider2D Col;
    public SpriteRenderer Sr;
    public AudioSource As;

    // Start is called before the first frame update
    void Start()
    {
        Col = GetComponent<CircleCollider2D>();
        Sr = GetComponent<SpriteRenderer>();
        RollTimer = false;
        CurrentTime = TimeToRespawn;
    }

    // Update is called once per frame
    void Update()
    {
        if (RollTimer == true)
        {
            Timer();
        }
    }

    private void Timer()
    {
        if (CurrentTime > 0f)
        {
            CurrentTime -= Time.deltaTime;
        }
        else
        {
            RollTimer = false;
            CurrentTime = TimeToRespawn;
            Sr.enabled = true;
            Col.enabled = true;
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && collider.GetComponent<PlayerControl>().CanDash == false)
        {
            As.Play();
            collider.GetComponent<PlayerControl>().CanDash = true;
            Sr.enabled = false;
            Col.enabled = false;
            RollTimer = true;
        }
    }
}
