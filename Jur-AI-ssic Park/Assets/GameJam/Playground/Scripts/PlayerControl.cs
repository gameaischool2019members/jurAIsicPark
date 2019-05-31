using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    private float MovementInputValue;
    private float RotationInputValue;

    public float Speed = 5f;
    public float RotationSpeed = 100f;
    public float AnimationRunSpeed = 5f;

    private bool isDead = false;
    private Rigidbody rigidBody;
    private Animator animator;
    public GameObject playerIndicator;
    private GameObject HumanToEat;
    private bool isEating;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody>();
        animator = this.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void FixedUpdate()
    {
        Vector3 dirToGo = Vector3.zero;
        Vector3 rotateDir = Vector3.zero;

        if (!isDead)
        {
            MovementInputValue = Input.GetAxis("Vertical");
            RotationInputValue = Input.GetAxis("Horizontal");
            Move();
        }

    }

    private void Move()
    {
        // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
        Vector3 movement = transform.forward * MovementInputValue * Speed * Time.deltaTime;

        // Apply this movement to the rigidbody's position.
        rigidBody.MovePosition(rigidBody.position + movement);

        // Determine the number of degrees to be turned based on the input, speed and time between frames.
        float angle = RotationInputValue * RotationSpeed * Time.deltaTime;

        // Make this into a rotation in the y axis.
        Quaternion rotation = Quaternion.Euler(0f, angle, 0f);

        // Apply this rotation to the rigidbody's rotation.
        rigidBody.MoveRotation(rigidBody.rotation * rotation);
    }


    private void Animate()
    {
        if (!isDead)

            // Walk & run animation 
            if (MovementInputValue != 0 || RotationInputValue != 0)
            {
                if (Speed < AnimationRunSpeed)
                    animator.SetBool("isWalking", true);
                else
                    animator.SetBool("isRunning", true);
            }
            else
            {
                if (Speed < AnimationRunSpeed)
                    animator.SetBool("isWalking", false);
                else
                    animator.SetBool("isRunning", false);
            }
    }

    public void OnEaten()
    {
        GameObject.Find("DinoAcademy").GetComponent<DinoAcademy>().AcademyReset();
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(this.transform.gameObject.name);
        if (this.transform.gameObject.name.ToString().Contains("Dino"))
        {
            if (collision.collider.CompareTag("human"))
            {
                isEating = true;
                HumanToEat = collision.gameObject;
                animator.SetBool("isEating", true);
                Invoke("EatHuman", 0.6f);
              
            }

        }

    }

    private void EatHuman()
    {
        if (HumanToEat != null)
            if (HumanToEat.GetComponent<HumanAgent>() != null)
                HumanToEat.GetComponent<HumanAgent>().OnEaten();
     

        isEating = false;
    }
}


