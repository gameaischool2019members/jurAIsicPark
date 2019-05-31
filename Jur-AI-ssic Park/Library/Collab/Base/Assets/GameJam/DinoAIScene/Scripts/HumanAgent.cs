using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class HumanAgent : Agent {

    public bool PlayerControl = false;
    public bool respawn;

    public GameObject humanAreaObject;
    [HideInInspector]

    public HumanArea myArea;

    public float Speed = 5f;
    public float RotationSpeed = 100f;
    public float AnimationRunSpeed = 5f;
    public bool contribute;
    public bool useVectorObs;

    private DinoAcademy myAcademy;
    private Rigidbody rigidBody;
    private Animator animator;
    private RayPerception3D rayPer;

    private float MovementInputValue;
    private float RotationInputValue;

    private bool isDead = false;

    public GameObject playerIndicator;

    // Use this for initialization
    void Start () {


        
        if(!PlayerControl)
        playerIndicator.SetActive(false);

        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        rayPer = GetComponent<RayPerception3D>();

        if(humanAreaObject != null)
            myArea = humanAreaObject.GetComponent<HumanArea>();

        myAcademy = FindObjectOfType<DinoAcademy>();
    }
    
    // Update is called ossssnce per frame
    void Update () {
        
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        Act(vectorAction);
    }

    public override void CollectObservations()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        rayPer = GetComponent<RayPerception3D>();

        if (useVectorObs)
        {
            float rayDistance = 50f;
            float[] rayAngles = { 45f, 60f, 75f, 90f, 105f, 120f, 135f, 240f, 255f, 270f, 285f, 300f };
            //float[] rayAngles = { 15f, 30f, 45f, 60f, 75f, 90f, 105f, 120f, 135f, 150f, 165f, 180f };
            string[] detectableObjects = { "human", "dino", "wall", "obstacle" };
            AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f));
            Vector3 localVelocity = transform.InverseTransformDirection(rigidBody.velocity);
            AddVectorObs(localVelocity.x);
            AddVectorObs(localVelocity.z);
        }
    }

    private void Act(float[] act)
    {
        Vector3 dirToGo = Vector3.zero;
        Vector3 rotateDir = Vector3.zero;

        if (!isDead)
        {
            if (PlayerControl || act == null)
            {
                MovementInputValue = Input.GetAxis("Vertical");
                RotationInputValue = Input.GetAxis("Horizontal");
            }
            else
            {
                MovementInputValue = 0;
                RotationInputValue = 0;

                var forwardAxis = (int)act[0];
                var rotateAxis = (int)act[1];

                switch (forwardAxis)
                {
                    case 1:
                        MovementInputValue = 1;
                        break;
                }

                switch (rotateAxis)
                {
                    case 1:
                        RotationInputValue = -1;
                        break;
                    case 2:
                        RotationInputValue = 1;
                        break;
                }
            }
        }
        else
        {
            MovementInputValue = 0;
            RotationInputValue = 0;
        }
    }

    /// <summary>
    /// 
    /// </summary>
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

    public override void AgentReset()
    {

        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        rayPer = GetComponent<RayPerception3D>();

        // rigidBody.velocity = Vector3.zero;
        // Spawn agent in a random position and with random rotation
        transform.position = new Vector3(Random.Range(-myArea.range, myArea.range),
                                            2f, Random.Range(-myArea.range, myArea.range))
                                            + myArea.transform.position;
        transform.rotation = Quaternion.Euler(new Vector3(0f, Random.Range(0, 360)));
    }

    void FixedUpdate()
    {
        Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isDead)
        {
            if (collision.collider.CompareTag("dino"))
            {
                AddReward(-1f);
                isDead = true;

                if (contribute && myAcademy != null)
                    myAcademy.totalScore -= 1;

            }

            if (collision.collider.CompareTag("obstacle")
                || collision.collider.CompareTag("wall"))
            {
                AddReward(-1f);

                if (contribute && myAcademy != null)
                    myAcademy.totalScore -= 1;
            }
        }
    }

    public void OnEaten() {
        animator.SetBool("isDead", true);
        isDead = false;

        // For Game
        myArea.SpawnHuman();
        Destroy(this.gameObject);

        /*
        // For Training
        transform.position = new Vector3(Random.Range(-myArea.range, myArea.range),
                                             transform.position.y + 3f,
                                             Random.Range(-myArea.range, myArea.range));

            //Destroy(gameObject);
        //} */
    }


    public void ActivatePlayerIndicator()
    {
        Debug.Log("Activating Player");
        this.playerIndicator.SetActive(true);
    }
}
