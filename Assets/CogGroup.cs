using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogGroup : MonoBehaviour
{
    public int speed = 50;
    // Start is called before the first frame update
    private Transform parent;
    void Start()
    {
        parent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(parent.position, Vector3.up, speed * Time.deltaTime);
    }
}
