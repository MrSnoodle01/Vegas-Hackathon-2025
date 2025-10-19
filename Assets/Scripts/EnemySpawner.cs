using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro.EditorUtilities;
using UnityEditor.Build.Content;
using UnityEditor.Timeline;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject folderPrefab;
    public GameObject hackerPrefab;
    public GameObject filePrefab;
    public float gameSpeed = 1f;

    private List<string> currentHackerPositions = new List<string>();
    private string[] possibleHackerPositions = { "botLeft", "botRight", "topLeft", "topRight" };
    private Computer computerScript;
    private float timeElapsed = 0f;
    private bool hasSpawnedFiles = false;
    private bool hasSpawnedHackers = false;


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

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed > 30)
        {
            gameSpeed += Time.deltaTime / 100f;
        }

        if (timeElapsed > 15 && !hasSpawnedHackers){
            hasSpawnedHackers = true;
            //StartCoroutine(spawnHackerEnemies());
        }
        if(timeElapsed > 30 && !hasSpawnedFiles)
        {
            hasSpawnedFiles = true;
            StartCoroutine(spawnFileEnemies());
        }
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
            yield return new WaitForSeconds(1.5f / gameSpeed);
        }
    }

    IEnumerator spawnFileEnemies()
    {
        while (computerScript.health > 0)
        {
            Instantiate(filePrefab, new Vector3(5, 5, 0), Quaternion.identity);
            yield return new WaitForSeconds(2.5f / gameSpeed);
        }
    }

    IEnumerator spawnHackerEnemies()
    {
        while (computerScript.health > 0)
        {
            yield return new WaitForSeconds(5 / gameSpeed);

            if (currentHackerPositions.Count < 4)
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
        }
    }
}
