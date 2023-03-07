using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerTimeAddScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject timeAddPrompt;

    private bool timeAdditionPromptCoroutine;

    void Start()
    {
        timeAdditionPromptCoroutine = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chicken"))
        {
            PlayerStats.Instance.timeLeft += 15;

            timeAddPrompt.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "+" + (15).ToString() + " SECONDS";
            timeAddPrompt.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "+" + (15).ToString() + " SECONDS";

            Destroy(collision.gameObject);

            if(!timeAdditionPromptCoroutine)
            {
                StartCoroutine(TimeAdditionPrompt(1.5f));
            }
            
        }
    }

    IEnumerator TimeAdditionPrompt(float secs)
    {
        timeAdditionPromptCoroutine = true;

        GameObject parentObject;

        Vector3 parentPosition;

        GameObject playerObject;

        Vector3 playerObjectPosition;

        playerObject = GameObject.FindGameObjectWithTag("PlayerBody");

        parentObject = timeAddPrompt.transform.parent.gameObject;

        parentPosition = parentObject.transform.localPosition;

        playerObjectPosition = parentObject.transform.position;

        parentObject.transform.SetParent(null);

        parentObject.transform.position = playerObjectPosition + parentPosition;

        timeAddPrompt.SetActive(true);


        yield return new WaitForSeconds(secs);


        parentObject.transform.SetParent(playerObject.transform);

        parentObject.transform.localPosition = parentPosition;

        timeAddPrompt.SetActive(false);

        timeAdditionPromptCoroutine = false;
    }
}
