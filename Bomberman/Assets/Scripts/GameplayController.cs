using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public GameObject floor;
    public GameObject chunkDestroyable;
    public GameObject chunkNotDestroyable;
    public GameObject enemy;
    public GameObject door;
    Player3rdPersonControl player;

    public int mapWidth;
    public int mapDepth;

    public int enemyCurrentAmount = 0;
    public int enemyTargetAmount;
    bool doorCreated = false;

    public static int chunkSize = 10;
    public static int chunkOffset;

    List<GameObject> mapChunks = new List<GameObject>();

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player3rdPersonControl>();
        chunkOffset = chunkSize / 2;
        InstantiateMap();
        InstantiateEnemies();
    }

    void InstantiateMap()
    {
        GameObject go = Instantiate(floor, new Vector3(mapWidth / 2.0f * chunkSize, 0, mapDepth / 2.0f * chunkSize), Quaternion.identity);
        go.transform.localScale = new Vector3(mapWidth, 1, mapDepth);

        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapDepth; j++)
            {
                int aux = Random.Range(1, 4);

                if ((i % 2 == 0 && (j + 1) % 2 != 0)) aux = 0;
                else if (i % mapWidth == 0 || (i + 1) % mapWidth == 0 || j % mapDepth == 0 || (j + 1) % mapDepth == 0) aux = 0;

                if ((i == 1 && j == 1) || (i == 2 && j == 1) || i == 1 && j == 2) aux = 2;

                if (aux == 1 && !doorCreated && Random.Range(0, 7) == 0)
                {
                    Instantiate(door, new Vector3(i * chunkSize + chunkOffset, 1, j * chunkSize + chunkOffset), Quaternion.identity).GetComponent<checkWin>().checkIfWin=CheckGameOver;
                    
                    doorCreated = true;
                }

                switch (aux)
                {
                    case 0:
                        mapChunks.Add(Instantiate(chunkNotDestroyable, new Vector3(i * chunkSize + chunkOffset, chunkOffset, j * chunkSize + chunkOffset), Quaternion.identity));
                        break;
                    case 1:
                        mapChunks.Add(Instantiate(chunkDestroyable, new Vector3(i * chunkSize + chunkOffset, chunkOffset, j * chunkSize + chunkOffset), Quaternion.identity));
                        break;
                    case 2:
                    case 3:
                        mapChunks.Add(null);
                        break;
                }
            }
        }
    }
    void InstantiateEnemies()
    {
        List<int> freeSpaces = new List<int>();
        for (int i = 0; i < mapChunks.Count; i++)
        {
            if (mapChunks[i] == null)
            {
                if (i != (1 * mapDepth + 1) && i != (1 * mapDepth + 2) && i != (2 * mapDepth + 1))
                    freeSpaces.Add(i);
            }
        }
        if (freeSpaces.Count / 2 < enemyTargetAmount) enemyTargetAmount = freeSpaces.Count / 2;
        for (int i = 0; i < enemyTargetAmount; i++)
        {
            int aux = Random.Range(0, freeSpaces.Count - 1);
            GameObject go= Instantiate(enemy, new Vector3(chunkSize * (freeSpaces[aux] / mapDepth) + chunkOffset, chunkOffset, chunkSize * (freeSpaces[aux] % mapDepth) + chunkOffset), Quaternion.identity);
            enemyCurrentAmount++;
            go.GetComponent<Enemy>().updateEnemyAmount = SubtractEnemyAmount;
            freeSpaces.RemoveAt(aux);
        }
    }

    void SubtractEnemyAmount()
    {
        enemyCurrentAmount--;
    }

    void CheckGameOver()
    {
        if (enemyCurrentAmount == 0)
        {
            GameManager.Get().GameOver(1);
        }
    }

}
