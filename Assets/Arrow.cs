using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private float timePassed = 0f;
    public float shootWaitInterval = 10f;
    public bool vanisable = false;

    public Vector3 direction;

    public float damage = 10;

    void Start()
    {
    }

    void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null) {
            enemy.getForce(direction);
            enemy.getDamage(damage);
        }
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!vanisable) return;
        timePassed += Time.deltaTime;
        if(timePassed < shootWaitInterval)
        {
            return;
        }
        Destroy(this.gameObject);
    }
}
