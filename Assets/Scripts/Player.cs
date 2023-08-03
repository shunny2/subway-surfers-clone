using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private float jumpVelocity;
    public float speed;
    public float jumpHeight;
    public float gravity; //no characterController é obrigado colocar a gravidade.
    public float horizontalSpeed;
    public float rayRadius;
    public LayerMask layer;
    public LayerMask coinLayer;
    private bool isMovingLeft;
    private bool isMovingRight;
    public Animator anim;
    public bool isDead;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 direction = Vector3.forward * speed;

        if(controller.isGrounded) { //verifica se esta enconstando no chão.
            if(Input.GetKeyDown(KeyCode.Space)) {
                jumpVelocity = jumpHeight;
            }

            if(Input.GetKeyDown(KeyCode.RightArrow) && transform.position.x < 3f && !isMovingRight) { //personagem nao pode passar da posicao 3 em x (isMoving) para limitar que o jogador fique pressionando mt vezes a tecla.
                isMovingRight = true;
                StartCoroutine(RightMove());
            }

            if(Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.x > -3f && !isMovingLeft) {
                isMovingLeft = true;
                StartCoroutine(LeftMove());
            }
        }else {
            jumpVelocity -= gravity;
        }

        OnCollision();

        direction.y = jumpVelocity;

        controller.Move(direction * Time.deltaTime);

    }

    IEnumerator LeftMove() //uma corotina pode ser pausada(controlada por tempo)
    {
        for(float i = 0; i < 10; i += 0.2f) {
            controller.Move(Vector3.left * Time.deltaTime * horizontalSpeed);
            yield return null; // sai do for
        }
        isMovingLeft = false;
    }

    IEnumerator RightMove()
    {
        for(float i = 0; i < 10; i+= 0.2f) {
            controller.Move(Vector3.right * Time.deltaTime * horizontalSpeed);
            yield return null;
        }
        isMovingRight = false;
    }

    void OnCollision()
    {
        RaycastHit hit;
        //forma uma linha de raio invisivel para detectar colisao
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayRadius, layer) && !isDead) { //origem do raio, destino do raio, armazenando objeto, tamanho do raio.
            anim.SetTrigger("die");
            speed = 0;
            jumpHeight = 0;
            horizontalSpeed = 0;
            Invoke("GameOver", 3f);
            isDead = true;
        }

        RaycastHit coinHit;

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward + new Vector3(0,1f,0)), out coinHit, rayRadius, coinLayer)) {
            GameController.instance.AddCoin();
            Destroy(coinHit.transform.gameObject);
        }
    }

    void GameOver()
    {
        GameController.instance.ShowGameOver();
    }
}
