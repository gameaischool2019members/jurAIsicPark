using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneScript : MonoBehaviour
{

    public string playingAs = "";
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LoadDinosaur() {

        playingAs = "dino";
        SceneManager.LoadScene(1);
    }

    public void LoadHuman()
    {
        playingAs = "human";
        SceneManager.LoadScene(1);
    }

    public void LoadAI()
    {
        playingAs = "AI";
        SceneManager.LoadScene(1);
    }
}
