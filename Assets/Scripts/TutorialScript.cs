using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    private GameObject[] tutorialTexts;

    public GameObject[] tutorialTextObjects; // Assign tutorial text objects in the Inspector

    public BoxCollider2D[] tutorialTriggers; // Assign corresponding triggers in the Inspector

    void Start()
    {

        for (int i = 0; i < tutorialTextObjects.Length; i++)
        {
            tutorialTextObjects[i].SetActive(false); // Deactivate all tutorial text objects initially
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            for (int i = 0; i < tutorialTriggers.Length; i++)
            {
                if (collision.gameObject.GetComponent<BoxCollider2D>() == tutorialTriggers[i])
                {
                    tutorialTextObjects[i].SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            for (int i = 0; i < tutorialTexts.Length; i++)
            {
                tutorialTextObjects[i].SetActive(false);
            }
        }
    }
}
