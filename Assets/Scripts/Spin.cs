using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {

    public float rotSpeed;

    // Update is called once per frame
    void Update () {
        transform.Rotate(transform.forward, rotSpeed * Time.deltaTime, Space.World);
    }
}
