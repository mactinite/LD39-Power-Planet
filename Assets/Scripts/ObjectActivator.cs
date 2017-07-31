using System.Collections;
using System.Collections.Generic;
using DigitalRuby.LightningBolt;
using UnityEngine;

public class ObjectActivator : MonoBehaviour {

    public bool active = false;
    public BatteryRotate br;
    public Transform particles;

    public bool sendMessage = true;
    public Transform toActivate;

    private void Update()
    {
        if (active)
        {
            br.enabled = true;
            particles.gameObject.SetActive(true);
        }
        else
        {
            br.enabled = false;
            particles.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player Projectile"))
        {
                if (sendMessage)
                {
                    toActivate.SendMessage("Activate", !active);
                }
                else
                {
                    toActivate.gameObject.SetActive(!active);
                }
            active = !active;
        }
    }

}
