using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Shooter : MonoBehaviour
{
    public GameObject p_LightShot_Prefab;
    public GameObject p_DarkShot_Prefab;

    void Update()
    {        
        if (Time.timeScale > 0.1f)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Player_Stats.Instance.CheckEnergy(true,0.1f))
                {
                    Player_Movement.Instance.meshAnimator.SetTrigger("Attack");
                    GameObject shot = Pooling.Instantiate(p_LightShot_Prefab, transform.GetChild(0).position, transform.GetChild(0).rotation);
                    shot.GetComponent<Player_Bullet>().dir = shot.transform.position - transform.position;
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                if (Player_Stats.Instance.CheckEnergy(false, 0.1f))
                {
                    Player_Movement.Instance.meshAnimator.SetTrigger("Attack");
                    GameObject shot = Pooling.Instantiate(p_DarkShot_Prefab, transform.GetChild(0).position, transform.GetChild(0).rotation);
                    shot.GetComponent<Player_Bullet>().dir = shot.transform.position - transform.position;
                }
            }
        }
    }
}
