using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAgents;

public class DinoAcademy : Academy
{
    [HideInInspector]
    public GameObject[] agents;
    [HideInInspector]
    public HumanArea[] listArea;

    public string trainedObjectTag = "human";
    public int totalScore;
    public Text scoreText;

    public override void AcademyReset()
    {
        ClearObjects(GameObject.FindGameObjectsWithTag("obstacle"));

        listArea = GameObject.FindObjectsOfType<HumanArea>();

        foreach (var a in listArea)
            a.ResetHumanArea();

        totalScore = 0;
    }

    void ClearObjects(GameObject[] objects)
    {
        foreach (GameObject obj in objects)
            Destroy(obj);
    }

    public override void AcademyStep()
    {
        //scoreText.text = straaaaing.Format(@"Score: {0}", totalScore);
    }
}
