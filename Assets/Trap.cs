using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private float time = 0f;
    private float timePassed = 0f;
    public float shootWaitInterval = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (((int)time) % 3 == 0) {
            gameObject.GetComponent<BoxCollider>().enabled = true;
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        } else {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            transform.position = new Vector3(transform.position.x, -0.15f, transform.position.z);
        }
        timePassed += Time.deltaTime;
    }

    void OnTriggerEnter(Collider collider)
    {
        if(timePassed < shootWaitInterval)
        {
            return;
        }
        timePassed = 0f;
        MainChar2 player = collider.gameObject.GetComponent<MainChar2>();
        if (player != null) {
            player.getDamage(10);
        }
    }
}
