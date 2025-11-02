using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Crossbow : MonoBehaviour
{
    
    public GameObject bullet;
    public Camera fpsCam;
    public Transform attackPoint;
    public Rigidbody playerRb;
    public float  spread = 0;
    public float shootForce = 10;
    public float shootWaitInterval = 0.5f;
    public static float magazineSize = 10;
    public float currentBulletCount = magazineSize;
    public float currentMagazineCount = 10;
    public TextMeshProUGUI bulletIndicator;

    private float timePassed = 0f;


    void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
         //   Shoot();
        }
    }

    public void Shoot()
    {
        timePassed += Time.deltaTime;
        if(timePassed < shootWaitInterval)
        {
            return;
        }
        timePassed = 0f;
        //Find the exact hit position using a raycast
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //Just a ray through the middle of your current view
        RaycastHit hit;

        //check if ray hits something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75); //Just a point far away from the player

        //Calculate direction from attackPoint to targetPoint
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        //Calculate spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); //Just add spread to last direction

        //Instantiate bullet/projectile
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); //store instantiated bullet in currentBullet
        Arrow arrowObject = currentBullet.GetComponent<Arrow>();
        arrowObject.vanisable = true;
        arrowObject.direction = directionWithoutSpread;
        //Rotate bullet to shoot direction
        currentBullet.transform.forward = directionWithSpread.normalized;

        //Add forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);

        currentBulletCount --;
        if (currentBulletCount == 0) {
            reload();
        }
        updateIndicator();
    }

    public void addArrow(int arrow) {
        currentMagazineCount += arrow;
        updateIndicator();
    }

    private void updateIndicator() {
        bulletIndicator.SetText(currentBulletCount+"/"+currentMagazineCount);
    }

    public void reload() {
        if (currentMagazineCount == 0) {
            return;
        }

        currentMagazineCount --;
        currentBulletCount = magazineSize;
    }
}
