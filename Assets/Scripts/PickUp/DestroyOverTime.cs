using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    public float LifeTime;

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, LifeTime);
    }
}
