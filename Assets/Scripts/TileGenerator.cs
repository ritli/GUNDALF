using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
public class TileGenerator : MonoBehaviour {

    public int xMax;
    public int yMax;

    public GameObject objectToSpawn;
    public bool spawnTiles = false;
    
    void OnValidate()
    {
        if (spawnTiles)
        {
            Transform parent = Instantiate(new GameObject(), transform).transform;
            parent.transform.position = transform.position;

            for (int x = 0; x < xMax; x++)
            {
                for (int y = 0; y < yMax; y++)
                {
                    Instantiate(objectToSpawn, transform.position + new Vector3(x, y), Quaternion.identity, parent);
                }
            }
            spawnTiles = false;
        }
    }
}
#endif
