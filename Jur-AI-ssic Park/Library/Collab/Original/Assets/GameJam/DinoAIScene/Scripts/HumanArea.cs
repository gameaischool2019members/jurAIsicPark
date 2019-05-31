using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class HumanArea : Area
{
    public List<GameObject> humans;
    public GameObject dino;
    public GameObject obstacle;
    public GameObject playerPrefab;

    public Transform playerZone;

    public int numHumans;
    public int numDinos;
    public int numObstacles;
    public bool respawnHumans;
    public float range;
    public bool randomPlayer;
    private DinoAcademy myAcademy;

    // Use this for initialization
    void Start()
    {
        myAcademy = FindObjectOfType<DinoAcademy>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateHuman()
    {
        for (int i = 0; i < numHumans; i++)
        {
            //UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);

            GameObject human = humans[Random.Range(0, (humans.Count - 1))];
                
            GameObject Hum = Instantiate(human, new Vector3(Random.Range(-range, range), 1f,
                                                              Random.Range(-range, range)) + transform.position,
                                          Quaternion.Euler(new Vector3(0f, Random.Range(0f, 360f))));
            Hum.GetComponent<HumanAgent>().respawn = respawnHumans;
            Hum.GetComponent<HumanAgent>().myArea = this;
        }
    }

    void CreateDino()
    {
        for (int i = 0; i < numDinos; i++)
        {
            GameObject Dino = Instantiate(dino, new Vector3(Random.Range(-range, range), 1f,
                                                              Random.Range(-range, range)) + transform.position,
                                          Quaternion.Euler(new Vector3(0f, Random.Range(0f, 360f))));
            Dino.GetComponent<DinoAgent>().myArea = this;
        }
    }

    void CreateObstacle()
    {
        for (int i = 0; i < numObstacles; i++)
        {
            GameObject Obs = Instantiate(obstacle, new Vector3(Random.Range(-range, range), 1f,
                                                              Random.Range(-range, range)) + transform.position,
                                          Quaternion.Euler(new Vector3(0f, Random.Range(0f, 360f))));
            Obs.GetComponent<ObstacleLogic>().myArea = this;
        }
    }

    public void ResetHumanArea()
    {
        UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
        GameObject[] dinos = GameObject.FindGameObjectsWithTag("dino");
        GameObject[] humans = GameObject.FindGameObjectsWithTag("human");

        /*
        // For Game

        foreach(GameObject d in dinos)
        {
            Destroy(d);
        }

        foreach(GameObject h in humans)
        {
            Destroy(h);
        }
        
        CreateHuman();
        CreateDino();
        */
        
        // For Training
        GameObject[] agents;
        if (myAcademy.trainedObjectTag == "human")
            agents = humans;
        else
            agents = dinos;

        foreach (GameObject agent in agents)
        {
            if (agent.transform.parent == gameObject.transform)
            {
                agent.transform.position = new Vector3(Random.Range(-range, range), 2f,
                                                        Random.Range(-range, range))
                                                        + transform.position;
                agent.transform.rotation = Quaternion.Euler(new Vector3(0f, Random.Range(0, 360)));
            }

        }
        
        CreateObstacle();


        if (GameObject.Find("GameManager") != null)
        {

           var  playingAs = GameObject.Find("GameManager").GetComponent<IntroSceneScript>().playingAs;


            if (playingAs == "human")
            {
                GameObject player = Instantiate(playerPrefab, playerZone);
                GameObject.Find("OverviewCamera").GetComponent<PlayGroundCameraScript>().followPlayer = true;
                GameObject.Find("OverviewCamera").GetComponent<PlayGroundCameraScript>().lockedCamera = false;
                GameObject.Find("OverviewCamera").GetComponent<PlayGroundCameraScript>().InitializeCamera();

            }
        }
        else
            GameObject.Find("OverviewCamera").GetComponent<PlayGroundCameraScript>().InitializeCamera();
    }

    public void SpawnHuman()
    {
        //  UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
        var human = humans[Random.Range(0, (humans.Count - 1))];

        GameObject Hum = Instantiate(human, new Vector3(Random.Range(-range, range), 1f,
                                                            Random.Range(-range, range)) + transform.position,
                                        Quaternion.Euler(new Vector3(0f, Random.Range(0f, 360f))));
        Hum.GetComponent<HumanAgent>().respawn = respawnHumans;
        Hum.GetComponent<HumanAgent>().myArea = this;
    }

    public override void ResetArea()
    {
    }
}
