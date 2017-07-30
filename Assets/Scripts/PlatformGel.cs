using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGel : Sizeable
{
    public float amount = 25;
    public float capacity = 50;
    public Vector3 growAxis = Vector3.one;
    private Vector3 startScale;

    public void Start()
    {
        startScale = transform.localScale;
        float delta = Mathf.InverseLerp(0, capacity, amount);
        transform.localScale = startScale + (growAxis * delta);
    }


    public override float getAmount()
    {
        return amount;
    }

    public override bool grow()
    {
        if (amount <= capacity)
        {
            amount++;
            float delta = Mathf.InverseLerp(0, capacity, amount);
            transform.localScale = startScale + (growAxis * delta);
            return true;
        }
        return false;
    }

    public override bool shrink()
    {
        if (amount >= 0)
        {
            amount--;
            float delta = Mathf.InverseLerp(0, capacity, amount);
            transform.localScale = startScale + (growAxis * delta);
            return true;
        }
        return false;
    }
}
