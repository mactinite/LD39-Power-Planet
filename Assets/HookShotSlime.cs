using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShotSlime : MonoBehaviour {


    public Transform Stunned;
    public Transform Moving;
    public float stunTime = 3;
    bool stunned = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player Projectile") && !stunned)
        {
            StartCoroutine(Stun());
        }
    }


    IEnumerator Stun()
    {
        stunned = true;
        Moving.gameObject.SetActive(false);
        Stunned.gameObject.SetActive(true);
        GetComponent<MovingPlatform>().active = false;
        GetComponent<SphereCollider>().isTrigger = true;
        this.gameObject.tag = "Hookshot";
        yield return new WaitForSeconds(stunTime);
        stunned = false;
        this.gameObject.tag = "Enemy";
        GetComponent<MovingPlatform>().active = true;
        GetComponent<SphereCollider>().isTrigger = false;
        Moving.gameObject.SetActive(true);
        Stunned.gameObject.SetActive(false);
    }
}
