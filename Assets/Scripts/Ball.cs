using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    bool canInteract=true;
    [SerializeField] Material ballSelected;
    [SerializeField] Material incorrectMaterial;
    [SerializeField] Material correctMaterial;

    public int ballLevelIndex;


    public void SetBallIndex(int index) {
        ballLevelIndex = index;
    }

    private void OnTriggerEnter(Collider other) {


        if (!GameManager.instance.interaction && canInteract) {

            // Only react when the player enters
            if (!other.CompareTag("Player"))
                return;
            canInteract = false;
            Debug.Log("Player entered ball: " + gameObject.name);
            SetBallMaterial(ballSelected);
            GameManager.instance.PlayerInteractedWithABall(ballLevelIndex);
        }


    }

    public void SetBallCorrectMaterial() {
        if (QuestionManager.instance.answer) {
            //correct answer
            SetBallMaterial(correctMaterial);
        } else {
            SetBallMaterial(incorrectMaterial);
        }
    }
    public void SetBallMaterial(Material mat) {
        this.GetComponent<MeshRenderer>().material = mat;
    }
    
}
