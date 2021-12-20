using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatform : MonoBehaviour
{
    public List<GameObject> platforms = new List<GameObject>(); //lista de objetos para armazena as plataformas
    public List<Transform> currentPlatforms = new List<Transform>(); //lista para adicionar os objetos dos prefabs

    public int offset; //controla as distancia das plataformas

    private Transform player;
    private Transform currentPlatformPoint;
    private int platformIndex;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; //procura um objeto com a tag player e armazena o transform dele

        for(int i = 0; i < platforms.Count; i++) {
            Transform p = Instantiate(platforms[i], new Vector3(0,0,i * 86), transform.rotation).transform; //Criando copia das plataformas
            currentPlatforms.Add(p);
            offset += 86; //offset recebe a posição inicial das plataformas.
        }

        currentPlatformPoint = currentPlatforms[platformIndex].GetComponent<Platform>().point; //vai receber o ponto(game object) da plataforma A
    }

    void Update()
    {
        float distance = player.position.z - currentPlatformPoint.position.z;

        if(distance >= 5) {
            Recycle(currentPlatforms[platformIndex].gameObject); //Acessando os marcadores(objetos) da plataforma e reciclando.
            platformIndex++; //vai pra proxima plataforma

            if(platformIndex > currentPlatforms.Count - 1) { //se platformIndex > 2
                platformIndex = 0;
            }
            
            currentPlatformPoint = currentPlatforms[platformIndex].GetComponent<Platform>().point; //recebe o novo index.
        }
    }

    public void Recycle(GameObject platform)
    {
        platform.transform.position = new Vector3(0,0, offset);
        offset += 86;
    }
}
