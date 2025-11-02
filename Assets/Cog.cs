using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cog : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage = 1;
    public float shootWaitInterval = 0.5f;
    public float timePassed = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider collider)
    {
        timePassed += Time.deltaTime;
        if(timePassed < shootWaitInterval)
        {
            return;
        }
        timePassed = 0;
        Enemy enemy = collider.gameObject.GetComponent<Enemy>();
        print("asdasd");
        if (enemy != null) {
            enemy.getDamage(damage);
        }
    }
}
