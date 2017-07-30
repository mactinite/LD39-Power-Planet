using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sizeable : MonoBehaviour
{
    public virtual float getAmount() { return 0; }
    public virtual bool grow() { return false; }
    public virtual bool shrink() { return false; }
}
