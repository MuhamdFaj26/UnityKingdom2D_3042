using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActive : MonoBehaviour {

    public UnitState State;
    public bool IsAlive;

    public float HealtPoint;
    public float MagicPoint;
    public float MoveSpeed;
    public float StaminaPoint;
    public float AtkSpeed;

    private Animator playerAnim;
    private AudioSource PlayerAudio;
    private AudioSource attackAudio;
    private AudioSource skill_1Audio;
    private Vector2 directPos;

    private float attackTime;
    

    private void Player_Attack ()
    {
        if (attackTime == 0)
        {
            State = UnitState.Attack;
            playerAnim.SetTrigger("Attack");
            attackAudio.Play();
            attackTime = AtkSpeed;
        }
    }
    
    private void Player_Movement(Vector2 moving)
    {
        moving.Normalize();
        playerAnim.SetFloat("AxisX", directPos.x);
        playerAnim.SetFloat("AxisY", directPos.y);
        if (moving.x !=0 || moving.y !=0)
        {
            directPos = moving;
            var xAndy = Mathf.Sqrt(Mathf.Pow(moving.x, 2) +
                                         Mathf.Pow(moving.y, 2));
            var pos_x = moving.x * MoveSpeed * Time.fixedDeltaTime / xAndy;
            var pos_y = moving.y * MoveSpeed * Time.fixedDeltaTime / xAndy;
            var pos_z = transform.position.z;
            transform.Translate(pos_x, pos_y, pos_z, Space.Self);

            playerAnim.SetBool("Is Moving", true);
            if (!PlayerAudio.isPlaying)
            {
                PlayerAudio.Play();
            }
            State = UnitState.Move;
        } else
        {
            playerAnim.SetBool("Is Moving", false);
            PlayerAudio.Stop();
            State = UnitState.Idle;
        }
    }

    private void Player_Death()
    {
        if (HealtPoint <= 0)
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll; 
            Player_Movement(Vector2.zero);
            HealtPoint = 0;
            playerAnim.SetTrigger("DeadFalling");
            State = UnitState.Dead;
            IsAlive = false;

        }
    }

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
        PlayerAudio = GetComponent<AudioSource>();
        attackAudio = transform.Find("Barbarian").GetComponent<AudioSource>();
        skill_1Audio = transform.Find("Abilities").Find("SplashSwing").GetComponent<AudioSource>();

    }

    private void FixedUpdate()
    {
        if (IsAlive)
        {
            var moveaway = Vector2.zero;
            moveaway.x = Input.GetAxis("Horizontal");
            moveaway.y = Input.GetAxis("Vertical");
            Player_Movement(moveaway);

            if  (Input.GetButtonDown("Jump"))
            {
                Player_Attack();
            }

            Player_Death();


        }

        if (attackTime > 0f)
        {
            attackTime -= Time.deltaTime;
        }
        if (attackTime < 0f)
        {
            attackTime = 0f;
            State = UnitState.Attack;
        }
    }

    
}


