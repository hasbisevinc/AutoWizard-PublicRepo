using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateSwitch : MonoBehaviour
{
    private bool open = false;
    public GameObject gate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (open) return;
        MainChar2 character = collider.gameObject.GetComponent<MainChar2>();
        if (character == null) {
            return;
        }
        open = true;
        Destroy(getChildGameObject("area"));
        GameObject stick = getChildGameObject("stick");
        Animator stickAnimator = stick.GetComponent<Animator>();
        stickAnimator.SetBool("isOpen", true);
        gate.GetComponent<Gate>().OpenDoor();
    }

    private GameObject getChildGameObject(string withName) {
		Transform[] ts = gameObject.transform.GetComponentsInChildren<Transform>(true); 
		foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
        return null;
    }
}
