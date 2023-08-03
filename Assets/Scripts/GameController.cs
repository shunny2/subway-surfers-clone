using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject gameOver;
    public static GameController instance; //Variavel estatica.
    public float score;
    public Text scoreText;
    public int scoreCoin;
    public Text scoreCoinText;

    private Player player;

    void Start() {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); //Procura um objeto com a tag player na classe player
    }

    void Update() {
        if(!player.isDead) { //Enquanto player estiver vivo.
            score += Time.deltaTime * 5f;
            scoreText.text = Mathf.Round(score).ToString() + "m"; //Arredonda o score transformando de float para int e convertendo para texto
        }
    }

    public void ShowGameOver()
    {
        gameOver.SetActive(true);
    }

    public void AddCoin()
    {
        scoreCoin ++;
        scoreCoinText.text = scoreCoin.ToString();
    }
}
