using System.Collections;
using TMPro.EditorUtilities;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject folderPrefab;

    private Computer computerScript;

    private void Awake()
    {
        GameObject computer = GameObject.FindWithTag("Computer");
        computerScript = computer.GetComponent<Computer>();
    }

    void Start()
    {
        StartCoroutine(spawnEnemies());
    }

    IEnumerator spawnEnemies() 
    {
        while(computerScript.health > 0)
        {
            Instantiate(folderPrefab, new Vector3(5, 5, 0), Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
    }
}
