using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MainChar : MonoBehaviour
{
    public Joystick joystick;

    public float speed = 2f;

    private CharacterController controller;
    
    private float gravity = 0f;

    Animator m_Animator;
    
    void Start()
    {
        controller = GetComponent<CharacterController> ();
        m_Animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        float x = joystick.Vertical * speed;
        float y = joystick.Horizontal * speed * -1;

        gravity -= 9.81f * Time.deltaTime;
        Vector3 move = new Vector3 (x, gravity, y);
        controller.Move (move * Time.deltaTime);

        var walkingSpeed = map((new Vector3 (x, 0, y)).sqrMagnitude, 0, 4, 0, 0.75f);
        if (walkingSpeed == 0) {
            m_Animator.Play("walking",0,0f);
        }
        m_Animator.SetFloat("Speed", walkingSpeed);

        var nearestEnemy = getNearestEnemy();

        if (nearestEnemy != null) {
            var nearestEnemyPosition = nearestEnemy.transform.position;
            var distance = (nearestEnemyPosition - transform.position).sqrMagnitude;
        
            if (distance < 5) {
                float degree = Mathf.Atan2((nearestEnemyPosition.x - transform.position.x), (nearestEnemyPosition.z - transform.position.z))* Mathf.Rad2Deg;
                Vector3 newRotation = new Vector3(0, degree, 0);
                transform.eulerAngles = newRotation;
            } else {
                changeDirectionAccordingToMovement(x, y);
            }
        } else {
                changeDirectionAccordingToMovement(x, y);
        }


        

        if ( controller.isGrounded ) {
            gravity = 0;
        }
    }

    private void changeDirectionAccordingToMovement(float x, float y) {
        if (x != 0 || y != 0) {
            float degree = Mathf.Atan2(joystick.Vertical, joystick.Horizontal)* Mathf.Rad2Deg + 180;
            Vector3 newRotation = new Vector3(0, degree * -1, 0);
            transform.eulerAngles = newRotation;
        }
    }

    private PlantComponent getNearestEnemy() {
        var pos = transform.position;
        var nearest = PlantComponent.AllPlants.Where(go => (Mathf.Abs(go.transform.position.y - pos.y) < 0.5)).OrderBy(go => (go.transform.position - pos).sqrMagnitude).FirstOrDefault();
        return nearest;
    }

    private float map(float x, float in_min, float in_max, float out_min, float out_max) {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
}
