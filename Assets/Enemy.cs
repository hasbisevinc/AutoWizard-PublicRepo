using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public float speed = 1.8f;

    private CharacterController controller;

    Animator m_Animator;
    
    private float gravity = 0f;

    public float maxHealth = 100;
    public float currentHealth = 0;

    public Slider healthBar;

    public float attackPower = 10f;
    public float attackInterval = 0.5f;
    private float latestAttack = 0f;
    public bool isActive = false;
    private bool isAttacking = false;
    private bool isDead = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        m_Animator = gameObject.GetComponent<Animator>();
        if (healthBar != null) {
            healthBar.maxValue = maxHealth;
            healthBar.value = maxHealth;
        }
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && !isDead) {
            var pos = GameObject.Find("RogueHooded").transform.position;
            var x = (pos.x - transform.position.x) * speed;
            var z = (pos.z - transform.position.z) * speed;

            var x2 = x*x;
            var z2 = z*z;
            var amp = Mathf.Sqrt(x2+z2);
            var diff = speed/amp;
            x = x * diff;
            z = z * diff;

            
            gravity -= 9.81f * Time.deltaTime;
            Vector3 move = new Vector3 (x, gravity, z);
            controller.Move (move * Time.deltaTime);

            changeDirectionAccordingToMovement(x, z);

            if ( controller.isGrounded ) {
                gravity = 0;
            }
        }
    }

    public void getDamage(float damage) {
        currentHealth -= damage;
        healthBar.value = currentHealth;
        if (currentHealth < 1) {
            isDead = true;
            m_Animator.SetBool("dead", true);
            controller.enabled = false;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            var rigit = gameObject.AddComponent<Rigidbody>();
            if (rigit != null) {
                rigit.drag=10f;
            }
            StartCoroutine("removeComponent", 4.0F);
        }
    }

    public void getForce(Vector3 direction) {
        controller.Move (direction * 5* Time.deltaTime);
    }

    public float attack() {
        latestAttack += Time.deltaTime;
        if (latestAttack < attackInterval) {
            return 0f;
        }
        latestAttack = 0f;
        if (!isAttacking) {
            StartCoroutine("disableAttackingAnimation", 2.0F);
            m_Animator.SetBool("IsAttacking", true);
        }
        isAttacking = true;

        return attackPower;
    }

    private void changeDirectionAccordingToMovement(float x, float y) {
        if (x != 0 || y != 0) {
            float degree = Mathf.Atan2(y, x)* Mathf.Rad2Deg + 270;
            Vector3 newRotation = new Vector3(0, degree * -1, 0);
            transform.eulerAngles = newRotation;
        }
    }

    IEnumerator disableAttackingAnimation()
    {
        yield return new WaitForSeconds(1);
        m_Animator.SetBool("IsAttacking", false);
        isAttacking = false;
    }

    IEnumerator removeComponent()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
}
