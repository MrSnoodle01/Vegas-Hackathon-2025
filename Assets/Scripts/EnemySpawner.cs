using System.Collections;
using TMPro.EditorUtilities;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject folderPrefab;
    void Start()
    {
        StartCoroutine(spawnEnemies());
    }

    IEnumerator spawnEnemies() 
    {
        for(int i = 0; i < 10; i++)
        {
            Instantiate(folderPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
    }
}
