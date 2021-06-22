using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float x;
    public float z;
    public float walkSpeed;
    public float dashSpeed;
    public float endDash;
    public float dashCooldown;
    public bool dashing, canDash;
    public Rigidbody RB;
    public Animator meshAnimator;
    public AudioClip dashSFX;
    public float lookdir;

    float multF = 0;
    float multR = 0;
    float multB = 0;
    float multL = 0;

    float animZ = 0;
    float animX = 0;


    private static Player_Movement instance;
    public static Player_Movement Instance { get { return instance; } }

    private void Awake()
    {
        dashCooldown = 0.1f;
        meshAnimator = transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Animator>();
        RB = GetComponent<Rigidbody>();
        instance = this;
        dashing = false;
        canDash = true;
    }

    void Update()
    {
        if (Player_Stats.Instance.currentHp > 0)
        {
            //Movement Controls
            if (dashing == false)
            {
                x = Input.GetAxisRaw("Horizontal");
                z = Input.GetAxisRaw("Vertical");
                Vector3 mov = new Vector3(x, 0, z).normalized * walkSpeed;
                mov.y = RB.velocity.y;
                RB.velocity = mov;

                if (Player_Mouse_Control.Instance)
                {
                    lookdir = Player_Mouse_Control.Instance.transform.rotation.eulerAngles.y;
                }

                SmoothRotation();
            }

            //DashControls
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash == true && (x != 0 || z != 0))
            {
                if ((z == 1 && x == 0) || (z == 1 && x == -1) || (z == 0 && x == -1) || (z == -1 && x == -1))
                {
                    if (Player_Stats.Instance.CheckEnergy(true, 0.1f))
                    {
                        AudioManager.instance.PlaySingle(dashSFX);
                        dashing = true;
                        transform.GetChild(2).gameObject.GetComponent<ParticleSystem>().Play();
                        canDash = false;
                        float speedY = RB.velocity.y;
                        RB.velocity = new Vector3(x, RB.velocity.y, z).normalized * dashSpeed;
                        RB.velocity = new Vector3(RB.velocity.x, speedY, RB.velocity.z);
                    }
                }
                if ((z == 1 && x == 1) || (z == 0 && x == 1) || (z == -1 && x == 1) || (z == -1 && x == 0))
                {
                    if (Player_Stats.Instance.CheckEnergy(false, 0.1f))
                    {
                        AudioManager.instance.PlaySingle(dashSFX);
                        dashing = true;
                        transform.GetChild(2).gameObject.GetComponent<ParticleSystem>().Play();
                        canDash = false;
                        float speedY = RB.velocity.y;
                        RB.velocity = new Vector3(x, RB.velocity.y, z).normalized * dashSpeed;
                        RB.velocity = new Vector3(RB.velocity.x, speedY, RB.velocity.z);
                    }
                }
            }
            if (dashing == true)
            {
                endDash -= Time.deltaTime;
            }
            if (endDash <= 0)
            {
                if (dashing)
                {
                    RB.velocity = new Vector3(0, RB.velocity.y, 0);
                }
                dashing = false;
                dashCooldown -= Time.deltaTime;
            }
            if (dashCooldown <= 0)
            {
                endDash = 0.3f;
                canDash = true;
                dashCooldown = 0.1f;
            }
        }
    }



    void SmoothRotation()
    {
        if (lookdir < 45 || lookdir > 315)
        {
            if (lookdir < 45)
            {
                multF = Mathf.InverseLerp(-45, 45, lookdir);
            }
            else
            {
                if (lookdir > 315)
                {
                    multF = Mathf.InverseLerp(315, 405, lookdir);
                }
            }
            multF -= 0.5f;
            multF *= 2;
        }
        else
        {
            multF = 0;
        }

        if (lookdir > 45 && lookdir < 135)
        {
            multR = Mathf.InverseLerp(45, 135, lookdir);
            multR -= 0.5f;
            multR *= 2;
        }
        else
        {
            multR = 0;
        }

        if (lookdir > 135 && lookdir < 225)
        {
            multB = Mathf.InverseLerp(135, 225, lookdir);
            multB -= 0.5f;
            multB *= 2;
        }
        else
        {
            multB = 0;
        }

        if (lookdir > 225 && lookdir < 315)
        {
            multL = Mathf.InverseLerp(225, 315, lookdir);
            multL -= 0.5f;
            multL *= 2;
        }
        else
        {
            multL = 0;
        }

        if (lookdir < 45 || lookdir > 315)
        {
            animZ = (z * (1 - (Mathf.Abs(multF))) + (x * multF));
            animX = (z * -multF) + (x * (1 - (Mathf.Abs(multF))));

            meshAnimator.SetFloat("X", animX);
            meshAnimator.SetFloat("Y", animZ);
        }
        else
        {
            if (lookdir > 45 && lookdir < 135)
            {
                animZ = (z * (1 - (Mathf.Abs(multR))) + (x * multR));
                animX = (z * multR) + (x * (1 - (Mathf.Abs(multR))));

                meshAnimator.SetFloat("X", -z);
                meshAnimator.SetFloat("Y", x);
            }
            else
            {
                if (lookdir > 135 && lookdir < 225)
                {
                    animZ = (z * (1 - (Mathf.Abs(multB))) + (x * multB));
                    animX = (z * -multB) + (x * (1 - (Mathf.Abs(multB))));

                    meshAnimator.SetFloat("X", -animX);
                    meshAnimator.SetFloat("Y", -animZ);
                }
                else
                {
                    animZ = (z * (1 - (Mathf.Abs(multL))) + (x * multL));
                    animX = (z * multL) + (x * (1 - (Mathf.Abs(multL))));

                    meshAnimator.SetFloat("X", z);
                    meshAnimator.SetFloat("Y", -x);
                }
            }
        }
    }
}
