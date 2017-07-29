using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShotTarget : MonoBehaviour {

    public ParticleSystem ps;

    public void Active(bool state)
    {
        ps.gameObject.SetActive(state);
    }

}
