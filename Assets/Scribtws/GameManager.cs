using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int p1Score;
    public TextMeshProUGUI p1ScoreText;

    public int p2Score;
    public TextMeshProUGUI p2ScoreText;

    public GameObject[] goals;
    public GameObject ball;
    [HideInInspector] public GameObject ballGameObject;
    public Transform ballSpawnPos;


    private void Awake()
    {
        instance = this;

        
    }
    // Start is called before the first frame update
    void Start()
    {
        ballGameObject = Instantiate(ball, ballSpawnPos);
        ballGameObject.name = "Ball";
    }

    // Update is called once per frame
    void Update()
    {
        p1ScoreText.text = p1Score.ToString();
        p2ScoreText.text = p2Score.ToString();

        if (ballGameObject == null)
        {
            ballGameObject = Instantiate(ball, ballSpawnPos);
        }
    }
}
