using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public GameObject floor;
    public GameObject chunkDestroyable;
    public GameObject chunkNotDestroyable;

    public int mapWidth;
    public int mapDepth;

    int chunkSize = 10;
    int chunkOffset;

    List<GameObject> mapChunks = new List<GameObject>();
    void Start()
    {
        chunkOffset = chunkSize / 2;
        InstantiateMap();
    }

    void InstantiateMap()
    {
        GameObject go= Instantiate(floor, new Vector3(mapWidth/2.0f*chunkSize,0, mapDepth/2.0f*chunkSize), Quaternion.identity);
        go.transform.localScale = new Vector3(mapWidth,1, mapDepth);

        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapDepth; j++)
            {
                int aux = Random.Range(0, 3);

                if ((i % 2 == 0 && (j+1) % 2 != 0)) aux = 0;
                else if (i % mapWidth == 0 || (i + 1) % mapWidth == 0 || j % mapDepth == 0 || (j + 1) % mapDepth == 0) aux = 0;
                else aux = 2;

                switch (aux)
                {
                    case 0:
                        mapChunks.Add(Instantiate(chunkNotDestroyable, new Vector3(i * chunkSize + chunkOffset, chunkOffset, j * chunkSize + chunkOffset), Quaternion.identity));
                        break;
                    case 1:
                        mapChunks.Add(Instantiate(chunkDestroyable, new Vector3(i * chunkSize + chunkOffset, chunkOffset, j * chunkSize + chunkOffset), Quaternion.identity));
                        break;
                    case 2:
                        mapChunks.Add(null);
                        break;
                }
            }
        }
    }

}
