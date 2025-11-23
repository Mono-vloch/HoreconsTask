using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour {

    public static QuestionManager instance;
    public event EventHandler OnPlayerAnswer;

    public GameObject questionaireBoard;
    [SerializeField] Question questions;
    [SerializeField] TextMeshProUGUI questionText;

    public bool answer;
    public int questionIndex;
    public Image questionImage;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    private void Start() {
        GameManager.instance.OnPlayerPickedABall += PlayerPickedABall;
    }
    private void OnDisable() {
        GameManager.instance.OnPlayerPickedABall -= PlayerPickedABall;
    }
    private void PlayerPickedABall(object sender, EventArgs e) {
        SetQuestion();
    }
    public void PlayerAnswer(bool playerAnswer) {

        if (playerAnswer == questions.questions[questionIndex].correctAnswerIsYes) {
            //player answered correctly
            answer = true;
            Debug.Log("the player answered correctly");
        } else {
            answer = false;
        }

        OnPlayerAnswer?.Invoke(this,EventArgs.Empty);


    }
    public void SetQuestion() {

        questionText.text = questions.questions[questionIndex].questionText;
        questionImage.sprite = questions.questions[questionIndex].questionImage;
    }

    public int GetMaxQuestions() {
        return questions.questions.Length;
    }
}



[Serializable]
public class QuestionEntry {
    [TextArea]
    public string questionText;
    public bool correctAnswerIsYes;
    public Sprite questionImage;
}
