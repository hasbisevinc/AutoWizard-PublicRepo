using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject effect;
    public GameObject prizeEffect;
    public GameObject prizePreFab;

    private bool open = false;
    private Animator animator;
    private GameObject prize;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

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
        GameObject currentBullet = Instantiate(effect, transform.position, Quaternion.identity);
        animator.SetBool("isOpen", true);
        StartCoroutine("showPrize", 0.5F);
    }

    IEnumerator showPrize()
    {
        yield return new WaitForSeconds(0.5f);
        prize = Instantiate(prizePreFab, (transform.position + new Vector3(0, 1, 0)), Quaternion.identity);
        StartCoroutine("getPrize", 1.0F);
    }

    IEnumerator getPrize()
    {
        yield return new WaitForSeconds(1);
        Instantiate(prizeEffect, (prize.transform.position), Quaternion.identity);
        Destroy(prize);
    }
}
