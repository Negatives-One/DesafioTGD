using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public int moedas = 0;
    public float Speed;
    public float JumpForce;
    public Vector2 StartPosition;
    private Vector3 m_Velocity;
    public float m_MovementSmoothing = .05f;
    private float dirPlayer = 1;

    public bool CanMove;
    public bool IsJumping;
    public LayerMask groundLayers;

    public float dashSpeed;
    public float dashTime;
    private float startDashTime;
    public bool CanDash;
    private bool RollTimer;

    public GameObject TelaFinal;
    public AudioSource AudioDash;
    public AudioSource AudioJump;
    private TrailRenderer Trail;
    private CircleCollider2D Col;
    private Transform Trans;
    private Animator anim;
    private Rigidbody2D rig;
    private SpriteRenderer sr;

    //Ready
    void Start()
    {
        Trail = GetComponent<TrailRenderer>();
        Col = GetComponent<CircleCollider2D>();
        Trans = GetComponent<Transform>();
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        startDashTime = dashTime;
        CanMove = true;
        CanDash = true;
        RollTimer = false;
        StartPosition = Trans.position;
    }

    //process
    void Update()
    {
        if (RollTimer == true)
        {
            Timer();
        }
        IsOnFloor();
        Dash();
        Move();
        Jump();
        YAnimations();
        if (CanDash)
        {
            sr.color = Color.white;
        }
        else
        {
            sr.color = Color.yellow;
        }
    }
    //physics_process
    void FixedUpdate()
    {
        
    }

    //Movimenta?es + Anim?es Run
    void Move()
    {
        //ReceberInput + movimenta?o Horizontal
        if (CanMove)
        {
            var Direcao = Input.GetAxisRaw("Horizontal");
            Vector3 targetVelocity = new Vector2(Direcao * Speed, rig.velocity.y);
            rig.velocity = Vector3.SmoothDamp(rig.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        }

        //Anima?o andar pra direita
        if(Input.GetAxisRaw("Horizontal") > 0f)
        {
            anim.SetBool("Run", true);
            dirPlayer = 1f;
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        //Anima?o andar pra esquerda
        if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            anim.SetBool("Run", true);
            dirPlayer = -1f;
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        //Anima?o Idle
        if(Input.GetAxis("Horizontal") == 0f)
        {
            anim.SetBool("Run", false);
        }
    }

    //Fun?o do Pulo
    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.C) && !IsJumping)
        {
            AudioJump.Play();
            rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
        }
    }

   //Fun?o Dash, 12 Quadros Full diagonal, 10 horizontal
    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.X) && CanDash)
        {
            AudioDash.Play();
            Trail.emitting = true;
            CanMove = false;
            CanDash = false;
            rig.gravityScale = 0f;
            rig.velocity = Vector2.zero;
            Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if(dir == new Vector2(0f, 0f))
            {
                dir = new Vector2(dirPlayer, 0);
            }
            rig.AddForce(dir.normalized * dashSpeed, ForceMode2D.Impulse);
            RollTimer = true;
        }
    }

    void Timer()
    {
        if(startDashTime > 0f)
        {
            startDashTime -= Time.deltaTime;
        }
        else
        {
            Trail.emitting = false;
            rig.gravityScale = 3f;
            CanMove = true;
            RollTimer = false;
            startDashTime = dashTime;
            rig.velocity = Vector2.zero;
        }
    }

    //Anima?es de Pulo e queda
    void YAnimations()
    {
        var velocity = rig.velocity;
        anim.SetFloat("VelocityY", velocity.y);
    }


    private bool isGrounded()
    {
        bool isGrounded;
        isGrounded = Physics2D.OverlapArea(new Vector2(Trans.position.x - 0.3f, Trans.position.y - 0.5f),
               new Vector2(Trans.position.x + 0.3f, Trans.position.y - 0.6f), groundLayers);
        Debug.DrawLine(new Vector2(Trans.position.x - 0.3f, Trans.position.y - 0.5f),
               new Vector2(Trans.position.x + 0.3f, Trans.position.y - 0.6f));
        return isGrounded;
    }

    private void IsOnFloor()
    {
        if (isGrounded())
        {
            IsJumping = false;
            CanDash = true;
            anim.SetBool("IsOnFloor", true);
        }
        else
        {
            IsJumping = true;
            anim.SetBool("IsOnFloor", false);
        }
    }

    public void morreu()
    {
        Trail.emitting = false;
        Trans.position = StartPosition;
        rig.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Boat")
        {
            TelaFinal.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Boat")
        {
            TelaFinal.SetActive(false);
        }
    }
}
