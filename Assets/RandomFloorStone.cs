using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFloorStone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var stones = getChildGameObject();
        foreach (Transform stone in stones)
        {
            Vector3 newRotation = new Vector3(0, getRandom(), 0);
            stone.transform.eulerAngles = newRotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private float getRandom() {
        int rand = Random.Range(1, 5);
        float value = 0f;
        if (rand == 1) {
            value = 0f;
        } else if (rand == 2) {
            value = 90f;
        } else if (rand == 3) {
            value = 180f;
        } else if (rand == 4) {
            value = 270f;
        }
        return value;// + Random.Range(-5, 5);
    }

    private Transform[] getChildGameObject() {
		Transform[] ts = gameObject.transform.GetComponentsInChildren<Transform>(true); 
        return ts;
    }
}
