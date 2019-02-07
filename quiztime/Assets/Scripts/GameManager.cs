using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private string truth;
    public LevelManager levelmgr; // you only need to instantiate a levelmgr for level changes not caused by button clicks
    public Scene currentscene;
    
    public Question[] question_data;
    private static List<Question> questions_unused;
    private Question current_question;

    [SerializeField] private Text question_text;
    [SerializeField] private Text[] answers;
    private string[] nullcheck = new string[4];

    void Start() {
        question_data = CSVReader.questions.ToArray();
        StaticsMgr.SetScore(0);
        if (questions_unused == null || questions_unused.Count == 0)
        {
            questions_unused = question_data.ToList();
        }

        currentscene = SceneManager.GetActiveScene();
        GetRndQuestion();
    }
    
    public void GetRndQuestion()
    {
        if (questions_unused.Count > 0)
        {
            int i = Random.Range(0, questions_unused.Count);
            current_question = questions_unused[i];
            questions_unused.RemoveAt(i);

            if (currentscene.name == "MCQ_Game")
            {
                question_text.text = current_question.question;
                LoadAnswers();
            }
            else if(currentscene.name == "TF_Game")
            {
                question_text.text = GetTFText();
            }
        }
    }

    public string GetTFText()
    {
        string chosen_answer = "init";

        double rand = Random.value;
        if(rand <= 0.4)
        {
            chosen_answer = current_question.correct;
            truth = "True";
        }
        else if(rand <= 0.6)
        {
            chosen_answer = current_question.wrong1;
            truth = "False";
        }
        else if (rand <= 0.8)
        {
            chosen_answer = current_question.wrong2;
            truth = "False";
        }
        else if (rand <= 1.0)
        {
            chosen_answer = current_question.wrong3;
            truth = "False";
        }

        string text = current_question.question + " = " + chosen_answer;
        return text;
    }

    void LoadAnswers()
    {
        //set all nullchecker fields to null, just in case
        for (int i = 0; i < 4; ++i){ nullcheck[i] = null; }

        //choose one of the four slots for the correct answer; assign correct_index
        double rnd = Random.value;
        if (rnd <= 0.25) {
            nullcheck[3] = "not null";
            answers[3].text = current_question.correct;
        }
        else if (rnd <= 0.50) {
            nullcheck[2] = "not null";
            answers[2].text = current_question.correct;
        }
        else if (rnd <= 0.75) {
            nullcheck[1] = "not null";
            answers[1].text = current_question.correct;
        }
        else {
            nullcheck[0] = "not null";
            answers[0].text = current_question.correct;
        }

        // find empty slots in answers and fill with wrong answers
        int beingAssigned = 1; // start with the first wrong answer; increment as empty slots are found and filled
        for (int i = 0; i < 4; i++)
        {
            if(nullcheck[i] == null){
                if (beingAssigned == 1)
                {
                    nullcheck[i] = "not null";
                    answers[i].text = current_question.wrong1;
                    beingAssigned++;
                }
                else if (beingAssigned == 2)
                {
                    nullcheck[i] = "not null";
                    answers[i].text = current_question.wrong2;
                    beingAssigned++;
                }
                else if (beingAssigned == 3)
                {
                    nullcheck[i] = "not null";
                    answers[i].text = current_question.wrong3;
                }
            }
        }
    }

    public void UserSelection(int i)
    {
        if (answers[i].text == current_question.correct || answers[i].text == truth)
        {
            StaticsMgr.UpdateScore(2);
            if (questions_unused.Any())
            {
                GetRndQuestion();
            }
            else
                levelmgr.LoadLevel("EndMenu");
        }
        else{
            StaticsMgr.UpdateScore(-1);
        }
    }
}
