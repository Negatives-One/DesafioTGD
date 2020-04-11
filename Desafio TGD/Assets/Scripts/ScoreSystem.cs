using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public GameObject player;
    private int score;
    public GameObject text;

    private Text mesh;
    // Start is called before the first frame update
    void Start()
    {
        mesh = text.GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        score = player.GetComponent<PlayerControl>().moedas;
        mesh.text = score.ToString();
    }
}
