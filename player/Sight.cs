using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.GetComponent<SpiritRevealer>())
            {
                other.GetComponent<SpiritRevealer>().ChangeColor();
            }
        }
    }
}
