using System.Collections;
using UnityEngine;

public class MenuEffect : MonoBehaviour
{
    public GameObject menuCharacter;

    void Start()
    {
        for(int i = 0; i < 20; i++)
        {
            int randX = Random.Range(-10, 10);
            float randSpeed = Random.Range(.2f, .8f);
            float randOpacity = Random.Range(20, 200);
            float randSize = Random.Range(.2f, 1.5f);
            float y = 5;
            for(int j = 0; j < Random.Range(3, 15); j++)
            {
                Vector3 position = new Vector3(randX, y, 0);
                GameObject newChar = Instantiate(menuCharacter, position, Quaternion.identity);
                MenuCharacter newCharScript = newChar.GetComponent<MenuCharacter>();
                newCharScript.Init(randSpeed, randSize);
                y += randSize/2;
            }
        }
        StartCoroutine(spawnLetters());
    }

    IEnumerator spawnLetters()
    {
        while (true)
        {
            yield return new WaitForSeconds(.35f);
            int randX = Random.Range(-10, 10);
            float randSpeed = Random.Range(.2f, .8f);
            float randOpacity = Random.Range(20, 200);
            float randSize = Random.Range(.2f, 1.5f);
            float y = 5;

            for (int j = 0; j < Random.Range(3, 15); j++)
            {
                Vector3 position = new Vector3(randX, y, 0);
                GameObject newChar = Instantiate(menuCharacter, position, Quaternion.identity);
                MenuCharacter newCharScript = newChar.GetComponent<MenuCharacter>();
                newCharScript.Init(randSpeed, randSize);
                y += randSize / 2;
            }
        }
    }
}
