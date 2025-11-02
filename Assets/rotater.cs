using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotater : MonoBehaviour
{
    public AnimationCurve myCurve;
    private float startY = 0f;

    void Start() {
        startY = transform.position.y;
    }
   
    void Update()
    {
        transform.position = new Vector3(transform.position.x, startY + myCurve.Evaluate((Time.time % myCurve.length)) -0.5f, transform.position.z);
    }
}
