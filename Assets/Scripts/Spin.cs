using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {

    public float rotSpeed;

    // Update is called once per frame
    void Update () {
        //transform.Rotate(transform.forward, rotSpeed * Time.deltaTime, Space.World);
    }

    public void SpinMeOverTime(Vector3 angles, float t)
    {
        StartCoroutine(RotateMe(angles, t));
    }

    public void SpinMe(Vector3 angles)
    {
        transform.Rotate(angles * Time.deltaTime);
    }

    IEnumerator RotateMe(Vector3 byAngles, float inTime)
    {
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.Rotate(byAngles * t);
            yield return null;
        }
    }

}
