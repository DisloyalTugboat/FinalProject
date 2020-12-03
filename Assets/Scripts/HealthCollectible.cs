using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{


    public AudioClip collectedClip;



    public bool isHealth;
    public bool isCog;
    public bool isPower;


    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {

            if(isHealth)

            {
                if (controller.health < controller.maxHealth)
                {
                    controller.ChangeHealth(1);
                    Destroy(gameObject);

                    controller.PlaySound(collectedClip);
                }
            }
            else if(isCog)
            {
                controller.ChangeCogs(4);
                Destroy(gameObject);

                controller.PlaySound(collectedClip);
            }

            else if(isPower)
            {
                controller.GivePower();
                Destroy(gameObject);

                controller.PlaySound(collectedClip);
            }


        }
    }

}
