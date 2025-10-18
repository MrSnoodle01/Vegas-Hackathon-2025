using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro.EditorUtilities;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject folderPrefab;
    public GameObject hackerPrefab;

    private List<string> currentHackerPositions = new List<string>();
    private string[] possibleHackerPositions = { "botLeft", "botRight", "topLeft", "topRight" };
    private Computer computerScript;

    private void Awake()
    {
        GameObject computer = GameObject.FindWithTag("Computer");
        computerScript = computer.GetComponent<Computer>();
    }

    void Start()
    {
        StartCoroutine(spawnFolderEnemies());
        StartCoroutine(spawnHackerEnemies());
    }

    public void removeHackerPosition(string oldPosition)
    {
        currentHackerPositions.Remove(oldPosition);
    }

    IEnumerator spawnFolderEnemies() 
    {
        while(computerScript.health > 0)
        {
            Instantiate(folderPrefab, new Vector3(5, 5, 0), Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator spawnHackerEnemies()
    {
        while (computerScript.health > 0)
        {
            if(currentHackerPositions.Count < 4)
            {
                int randomPosition = Random.Range(0, possibleHackerPositions.Length);
                while (currentHackerPositions.Contains(possibleHackerPositions[randomPosition])){
                    randomPosition = Random.Range(0, possibleHackerPositions.Length);
                }

                currentHackerPositions.Add(possibleHackerPositions[randomPosition]);

                GameObject newHacker = Instantiate(hackerPrefab, new Vector3(5, 5, 0), Quaternion.identity);
                Hacker hackerScript = newHacker.GetComponent<Hacker>();
                hackerScript.Init(possibleHackerPositions[randomPosition]);
            }

            yield return new WaitForSeconds(5);
        }
    }

}
