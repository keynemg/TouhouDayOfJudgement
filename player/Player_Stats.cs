using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player_Stats : Entities
{

    public Image HpBar;
    public GameObject mesh;

    public AudioClip hittedSFX;

    public Image yinImg;
    public Image yangImg;

    public float p_Yin = 1f;
    public float p_Yang = 1f;

    private static Player_Stats instance;
    public static Player_Stats Instance { get { return instance; } }

    public override void Awake()
    {
        instance = this;
        base.Awake();
        currentSp = maxSp;
    }

    void FixedUpdate()
    {
        AttEnergy();
        HpBar.fillAmount = currentHp / maxHp;
    }

    public void AttEnergy()
    {
        if (yinImg.fillAmount != p_Yin)
        {
            yinImg.fillAmount = Mathf.Lerp(yinImg.fillAmount, p_Yin, Time.deltaTime * 10);
        }
        if (yangImg.fillAmount != p_Yang)
        {
            yangImg.fillAmount = Mathf.Lerp(yangImg.fillAmount, p_Yang, Time.deltaTime * 10);
        }
    }


    public bool CheckEnergy(bool _Energy, float _cost)
    {
        if (_Energy)
        {
            if (p_Yang > 0.00f)
            {
                p_Yang -= _cost;
                if (p_Yang <= 0.01f)
                {
                    p_Yang = 0;
                }
                p_Yin += _cost;
                if (p_Yin >= 1)
                {
                    p_Yin = 1;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (p_Yin > 0.00f)
            {
                p_Yin -= _cost;
                if (p_Yin <= 0.01f)
                {
                    p_Yin = 0;
                }
                p_Yang += _cost;
                if (p_Yang >= 1)
                {
                    p_Yang = 1;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public override void RecieveDamage(float damage)
    {
        if (immortal || !IsAlive)
        {
            return;
        }
        currentHp -= damage;
        Player_Movement.Instance.meshAnimator.SetTrigger("TookDamage");
        OnDamage();
        iTween.PunchScale(mesh, Vector3.one / 3, 0.5f);
        AudioManager.instance.PlaySingle(hittedSFX);
        if (!IsAlive)
        {
            currentHp = 0;
            OnDeath();
        }
    }

    public override void OnDeath()
    {
        base.OnDeath();
        Player_Movement.Instance.meshAnimator.SetBool("Dead",true);
        gameObject.GetComponent<Collider>().enabled = false;
        Destroy(gameObject,1.5f);
        GameManager.Instance.DefeatScreen.SetActive(true);
        GameManager.Instance.defeated = true;
    }


}
