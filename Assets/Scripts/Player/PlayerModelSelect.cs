using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelSelect : MonoBehaviour
{
    //public GameObject playerModelFBX;

    //Dictionary<string, GameObject> playerModelFBX = new Dictionary<string, GameObject>();

    [SerializeField]
    private GameObject[] playerModelFBX;

    private Animator playerAnimator;

    //private int selectedModel;

    // Start is called before the first frame update
    void Start()
    {
        int randomNum;

        randomNum = UnityEngine.Random.Range(0, playerModelFBX.Length - 1);

        spawnModel(randomNum);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void spawnModel(int modelNum)
    {
        Debug.Log(modelNum  + ". "+ playerModelFBX[modelNum].name);

        playerModelFBX[modelNum].transform.localScale = new Vector3(gameObject.transform.parent.transform.localScale.y, gameObject.transform.parent.transform.localScale.x, gameObject.transform.parent.transform.localScale.z);

        playerModelFBX[modelNum].transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.x, gameObject.transform.position.x);

        GameObject playerModel = Instantiate(playerModelFBX[modelNum]);

        playerAnimator = gameObject.GetComponent<Animator>();


        playerModel.gameObject.transform.GetChild(0).SetParent(gameObject.transform);
        playerModel.gameObject.transform.GetChild(0).SetParent(gameObject.transform);

        playerAnimator.avatar = playerModelFBX[modelNum].GetComponent<Animator>().avatar;

        Destroy(playerModel);
    }
}
