using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public static string GAME_SCENE = "SampleScene";

    [SerializeField] Button yesAnswerBtn, noAnswerBtn;

    Color normalColor = Color.blue;
    Color correctColor = Color.green;
    Color incorrectColor = Color.red;

    private void Awake() {
        if (Instance==null) {
            Instance = this;
        }
    }

    private void Start() {
        GameManager.instance.OnPlayerPickedABall += PlayerPickedABall;
        QuestionManager.instance.OnPlayerAnswer += PlayerAnswer;
    }

    private void OnDisable() {
        GameManager.instance.OnPlayerPickedABall -= PlayerPickedABall;
        QuestionManager.instance.OnPlayerAnswer -= PlayerAnswer;
    }

    private void PlayerPickedABall(object sender,EventArgs e) {
        ResetAnswersButtons();
    }

    private void ResetAnswersButtons() {
        yesAnswerBtn.interactable = true;
        yesAnswerBtn.image.color = normalColor;
        noAnswerBtn.interactable = true;
        noAnswerBtn.image.color = normalColor;
    }
    private void PlayerAnswer(object sender, EventArgs e) {
        if (QuestionManager.instance.answer) {
            //answer is correct
            yesAnswerBtn.image.color = correctColor;
            noAnswerBtn.image.color = correctColor;
        } else {
            //wrong answer
            yesAnswerBtn.image.color = incorrectColor;
            noAnswerBtn.image.color = incorrectColor;
        }

        yesAnswerBtn.interactable = false;
        noAnswerBtn.interactable = false;

    }

    public void Answer(bool playerAnswer) {

        Debug.Log("the player is " + playerAnswer);
        QuestionManager.instance.PlayerAnswer(playerAnswer);
    }


    public void RestartLevel() {
        SceneManager.LoadScene(GAME_SCENE);
    }


}
