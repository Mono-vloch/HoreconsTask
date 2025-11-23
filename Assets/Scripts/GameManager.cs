using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public event EventHandler OnPlayerPickedABall;

    public List<Ball> levelBalls = new List<Ball>();

    [Header("References")]
    public Renderer planeRenderer;      // drag your Plane here (its Renderer)
    public GameObject ball;   // drag your ball prefab here
    public Transform ballsContainer;

    [Header("Settings")]
    public int numberOfBalls = 10;
    public float ballYOffset = 0.2f;    // how high above the plane to spawn

    public bool interaction;

    private void Awake() {
        if (instance==null) {
            instance = this;
        }
    }

    private void Start() {
        QuestionManager.instance.OnPlayerAnswer += PlayerAnswer;
    }
    private void OnDisable() {
        QuestionManager.instance.OnPlayerAnswer -= PlayerAnswer;
    }

    private void PlayerAnswer(object sender, EventArgs e) {

            levelBalls[QuestionManager.instance.questionIndex].SetBallCorrectMaterial();
        interaction = false;
    }
    public void GenerateLevel() {
        if (planeRenderer == null) {
            Debug.LogError("GameManager: planeRenderer is not assigned!");
            return;
        }


        // 1) Get plane bounds
        Bounds bounds = planeRenderer.bounds;

        float minX = bounds.min.x;
        float maxX = bounds.max.x;
        float minZ = bounds.min.z;
        float maxZ = bounds.max.z;

        // (Optional) If you need width/height numbers:
        float width = bounds.size.x;
        float height = bounds.size.z;
        Debug.Log($"Plane size: width={width}, height={height}");

        // 2) Spawn balls
        for (int i = 0; i < numberOfBalls; i++) {

            float x = UnityEngine.Random.Range(minX, maxX);
            float z = UnityEngine.Random.Range(minZ, maxZ);
            float y = bounds.max.y + ballYOffset;

            Vector3 spawnPos = new Vector3(x, y, z);
            GameObject ballObj = Instantiate(ball, spawnPos, Quaternion.identity, ballsContainer);
            Ball currentBall = ballObj.GetComponent<Ball>();
            levelBalls.Add(currentBall);
            currentBall.SetBallIndex(i);        
        }
    }

    public void PlayerInteractedWithABall(int ballIndex) {

        if (!interaction) {
            QuestionManager.instance.questionIndex = ballIndex;
            OnPlayerPickedABall?.Invoke(this, EventArgs.Empty);
            interaction = true;
        }
      

    }

}


