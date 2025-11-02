using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;

public class MainChar2 : MonoBehaviour
{
    public Joystick joystick;

    public Crossbow weapon;

    public float speed = 2f;

    private CharacterController controller;
    
    private float gravity = 0f;

    private float maxHealth = 1000f;
    private float currentHealth = 0f;

    Animator m_Animator;

    public Slider healthBar;

    public float attackRange = 10f;
    public float damageRange = 2f;

    private GameObject rig;
    private bool isDead = false;
    
    void Start()
    {
        controller = GetComponent<CharacterController> ();
        controller.enabled = true;
        m_Animator = gameObject.GetComponent<Animator>();
        currentHealth = maxHealth;
        if (healthBar != null) {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
        rig = getChildGameObject("Rig");
    }

    private Vector3 calculateMaxSpeed(float x, float y, float gravity) {
        var mag = Math.Sqrt(x*x + y*y);
        if (mag != 0) {
            float a = speed/((float)Math.Sqrt(x*x + y*y));
            x = x*a;
            y = y*a;
        }
        return new Vector3 (x, gravity, y);
    }

    void Update()
    {
        if (isDead) return;

        float x = joystick.Vertical;
        float y = joystick.Horizontal * -1;

        gravity -= 9f * Time.deltaTime;
        Vector3 move = calculateMaxSpeed(x, y, gravity);
        controller.Move (move * Time.deltaTime);

        var walkingSpeed = map((new Vector3 (x, 0, y)).sqrMagnitude, 0, 4, 0, 1f);
        if (walkingSpeed == 0) {
            m_Animator.SetBool("isWalking", false);
        } else {
            m_Animator.SetBool("isWalking", true);
        }

        var nearestEnemy = getNearestEnemy();

        if (nearestEnemy != null) {
            var nearestEnemyPosition = nearestEnemy.transform.position;
            var distance = (nearestEnemyPosition - transform.position).sqrMagnitude;
        
            if (distance <= attackRange) {
                float degree = Mathf.Atan2((nearestEnemyPosition.x - transform.position.x), (nearestEnemyPosition.z - transform.position.z))* Mathf.Rad2Deg + 22f;
                Vector3 newRotation = new Vector3(0, degree, 0);
                rig.transform.eulerAngles = newRotation;
                m_Animator.SetBool("shooting", true);
                weapon.Shoot();
                if (distance <= damageRange) {
                    var enemyScript = nearestEnemy.GetComponentInParent<Enemy>();
                    if (enemyScript != null) {
                        var damage = enemyScript.attack();
                        getDamage(damage);
                    }
                }
            } else {
                m_Animator.SetBool("shooting", false);
                changeDirectionAccordingToMovement(x, y);
            }
        } else {
            m_Animator.SetBool("shooting", false);
            changeDirectionAccordingToMovement(x, y);
        }

        activateNearEnemies();

        if ( controller.isGrounded ) {
            gravity = 0;
        }
    }

    public void addArrow(int arrow) {
        weapon.addArrow(arrow);
    }

    public void heal(int value) {
        changeHealth(value);
    }

    public void getDamage(float damage) {
        changeHealth(damage * -1);

        if (currentHealth < 1) {
            m_Animator.SetLayerWeight (1, 0);
            m_Animator.SetLayerWeight (2, 0);
            m_Animator.SetBool("dead", true);
            isDead = true;
            controller.enabled = false;
            //Destroy(this.gameObject);
        }
    }

    private void changeHealth(float value) {
        if (currentHealth + value > maxHealth) {
            currentHealth = maxHealth;
        } else {
            currentHealth += value;
        }
        healthBar.value = currentHealth;
    }

    private void changeDirectionAccordingToMovement(float x, float y) {
        if (x != 0 || y != 0) {
            float degree = Mathf.Atan2(joystick.Vertical, joystick.Horizontal)* Mathf.Rad2Deg + 180;
            Vector3 newRotation = new Vector3(0, degree * -1, 0);
            rig.transform.eulerAngles = newRotation;
        }
    }

    private PlantComponent getNearestEnemy() {
        var pos = transform.position;
        var nearest = PlantComponent.AllPlants.Where(go => (Mathf.Abs(go.transform.position.y - pos.y) < 0.5)).OrderBy(go => (go.transform.position - pos).sqrMagnitude).FirstOrDefault();
        return nearest;
    }

    private void activateNearEnemies() {
        var enemies = getNearInactiveEnemy();
        for (int i = 0; i < enemies.Count; i ++) {
            var enemyScript = enemies[i].GetComponentInParent<Enemy>();
            enemyScript.isActive = true;
        }
    }

    private List<PlantComponent> getNearInactiveEnemy() {
        var pos = transform.position;
        var nearest = PlantComponent.AllPlants.Where(go => (Mathf.Abs(go.transform.position.y - pos.y) < 0.5 && (go.transform.position - pos).sqrMagnitude < 10)).ToList();
        return nearest;
    }

    private float map(float x, float in_min, float in_max, float out_min, float out_max) {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }

    private GameObject getChildGameObject(string withName) {
		Transform[] ts = gameObject.transform.GetComponentsInChildren<Transform>(true); 
		foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
        return null;
    }
}
