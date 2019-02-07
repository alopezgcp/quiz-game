using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    private string line;
    private string[] fields;
    private List<string> all_answers;
    public static List<Question> questions;
    private Question current_question;

    private string[] pronouns = {"","yo", "tu", "el/ella", "nosotros/nosotras", "ustedes", "ellos/ellas",
                                 "I", "you", "he/she", "we", "you all", "they"};

    void Start()
    {
        questions = new List<Question>();
        all_answers = new List<string>();

        using (StreamReader sr = new StreamReader("Assets/Files/testing.csv"))
        {
            line = sr.ReadLine(); // discard pronouns for now
            // each line produces six question-correct answer pairs
            while(!(sr.EndOfStream))
            {
                line = sr.ReadLine();
                fields = line.Split(',');

                for(int i = 7; i <= 12; ++i)
                {
                    current_question = new Question();
                    current_question.question = pronouns[i] + " " + fields[i];
                    current_question.correct = pronouns[i - 6] + " " + fields[i - 6];
                    all_answers.Add(current_question.correct);
                    questions.Add(current_question);
                }
            }
        }
        foreach(Question question in questions)
        {
            int rand_index;
            int beingAssigned = 1;

            while(beingAssigned <= 3)
            {
                rand_index = Random.Range(0, all_answers.Count - 1);
                if(all_answers[rand_index] != question.correct)
                {
                    if(beingAssigned == 1)
                        question.wrong1 = all_answers[rand_index];
                    else if (beingAssigned == 2)
                        question.wrong2 = all_answers[rand_index];
                    else if (beingAssigned == 3)
                        question.wrong3 = all_answers[rand_index];
                    beingAssigned++;
                }
            }
        }
    }
}
