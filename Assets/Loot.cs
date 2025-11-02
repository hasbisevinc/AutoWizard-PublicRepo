using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public int value = 1;
    public ItemType type = ItemType.ARROW;
    // Start is called before the first frame update
    void Start()
    {
        if (type == ItemType.ARROW) {
            getChildGameObject("arrow").SetActive(true);
        } else if (type == ItemType.HEALTH) {
            getChildGameObject("heart").SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        MainChar2 player = collider.gameObject.GetComponent<MainChar2>();
        if (player != null) {
            giveValue(player);
            Destroy(this.gameObject);
        }
    }

    private void giveValue(MainChar2 player) {
        if (type == ItemType.ARROW) {
            player.addArrow(value);
        } else if (type == ItemType.HEALTH) {
            player.heal(value);
        }
    }

     private GameObject getChildGameObject(string withName) {
		Transform[] ts = gameObject.transform.GetComponentsInChildren<Transform>(true); 
		foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
        return null;
    }

    public enum ItemType {
        ARROW,
        HEALTH
    }
}
    
