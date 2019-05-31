using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayGroundCameraScript : MonoBehaviour
{
    public bool followPlayer;

    public bool lockedCamera;

 

    private Vector3 velocity = Vector3.zero;
    private Transform goalTransform;

    private List<GameObject> agentList;

    public Vector3 initialPos;
    public Vector3 initialRot;

    public float smoothing;
    Vector3 offset;

    public Vector3 followCharactersPosition;
    public Vector3 followeCharacterRotation;

    // Start is called before the first frame update
    void Start()
    {

       
     
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            lockedCamera = true;

            this.transform.Rotate(-20, 0.0f, 0.0f);
            this.transform.position = initialPos;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangeTarget();
            lockedCamera = false;

        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            this.transform.Translate(2.0f,0.0f,0.0f);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            this.transform.Translate(-2.0f, 0.0f, 0.0f);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            this.transform.Translate(0.0f, 2.0f, 0.0f);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            this.transform.Translate(0.0f, -2.0f, 0.0f);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            this.transform.Translate(0.0f, 0.0f, 2.0f);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            this.transform.Translate(0.0f, 0.0f, -2.0f);
        }

        


        if (goalTransform == null && !lockedCamera)
        {
            ChangeTarget();
        }



    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (!lockedCamera)
        {


            //   Vector3 goalPos = goalTransform.position;
            //   goalPos.y = transform.position.y;
            //   transform.position = Vector3.SmoothDamp(transform.position, goalPos, ref velocity, smoothing);

            this.transform.position = goalTransform.position + offset;

            // transform.position = Vector3.Lerp(transform.position, goalTransform.position, smoothing * Time.deltaTime);
            //this.GetComponent<Camera>().
        }
    }

    public void ChangeTarget()
    {
        this.transform.position = followCharactersPosition;
        this.transform.Rotate(20, 0.0f, 0.0f);


        agentList = new List<GameObject>();
        agentList.AddRange(GameObject.FindGameObjectsWithTag("dino"));
        agentList.AddRange(GameObject.FindGameObjectsWithTag("human"));

        Random.InitState((int)System.DateTime.Now.Ticks);
        int rand = UnityEngine.Random.Range(0, agentList.Count);


        if (agentList.Count < 1) return;

       // agentList[rand].GetComponentInChildren<Animator>().SetBool("isDead", true);

        goalTransform = agentList[rand].transform;
        offset = this.transform.position - goalTransform.position;

        Debug.Log("Position : " + goalTransform.position.x + " y:" + goalTransform.position.y + "  z: " + goalTransform.position.z);
    }

    public void InitializeCamera()
    {

        this.transform.position = initialPos;
        this.transform.rotation = Quaternion.Euler(initialRot.x, initialRot.y, initialRot.z);
        //  Debug.Log("Initial pos: " + initialPos);
       // this.transform.Rotate(20, 0.0f, 0.0f);


        if (followPlayer)
        {

            this.transform.position = followCharactersPosition;
            this.transform.Rotate(20, 0.0f, 0.0f);

            if(GameObject.Find("PlayerPrefab(Clone)") != null)
            goalTransform = GameObject.Find("PlayerPrefab(Clone)").GetComponent<Transform>();

            else if (GameObject.Find("PlayerDinoPrefab(Clone)") != null)
                goalTransform = GameObject.Find("PlayerDinoPrefab(Clone)").GetComponent<Transform>();


            offset = this.transform.position - goalTransform.position;
        }

        else if (lockedCamera)
        {

        }



        else {

            ChangeTarget();

             }

    }
}
