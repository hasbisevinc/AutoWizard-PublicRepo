using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    Animator doorAnimator;

    void Start()
    {
        doorAnimator = gameObject.GetComponent<Animator>();
    }

    
    void Update()
    {
        
    }

    public void OpenDoor() {
        doorAnimator.SetBool("isOpen", true);
    }
}
