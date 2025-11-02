using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public float x = 1.45f;
    public float z = 0.66f;

    // Update is called once per frame
    void Update () {
        transform.position = player.transform.position + new Vector3(x, 0, z);
    }
}
