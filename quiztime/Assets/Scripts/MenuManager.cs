using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    [SerializeField] public string main_text;
    [SerializeField] public string rules_text;
    
    [SerializeField] private Text maintextbox;
    
    // Use this for initialization
    void Start () {
        Scene currentscene = SceneManager.GetActiveScene();
        if(currentscene.name == "EndMenu")
        {
            maintextbox.text = "Final Score: " + StaticsMgr.GetScore();
        }
        else if(currentscene.name == "HomeMenu"){
            main_text = "It's quiz time, baby.";
            rules_text = "Click an answer to select it.\n " +
                         "Plus two points for right answers.\n" +
                         "Minus one point for wrong answers.";
        }
	}

    public void ToggleRules()
    {
        if (maintextbox.text == rules_text)
        {
            maintextbox.text = main_text;
        }
        else if (maintextbox.text == main_text)
        {
            maintextbox.text = rules_text;
        }
    }
}
