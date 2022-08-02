using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerModelSelect : MonoBehaviour
{
    public GameObject playerModelFBX;

    //Dictionary<string, GameObject> playerModelFBX = new Dictionary<string, GameObject>();

    /*
    [SerializeField]
    private GameObject[] playerModelFBX;
    */


    private Animator playerAnimator;

    private string chosenNFTName;

    //private int selectedModel;

    // Start is called before the first frame update
    void Start()
    {
        //chosenNFTName = NameToSlugConvert(gameplayView.instance.chosenNFT.name);

        chosenNFTName = "a-rod";

        playerModelFBX = Resources.Load(Path.Combine("SinglePlayerPrefabs/DisplayModels", chosenNFTName)) as GameObject;

        /*
        int randomNum;

        randomNum = UnityEngine.Random.Range(0, playerModelFBX.Length - 1);
        */

        spawnModel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void spawnModel()
    {
        Debug.Log(playerModelFBX.name);

        playerModelFBX.transform.localScale = new Vector3(gameObject.transform.parent.transform.localScale.y, gameObject.transform.parent.transform.localScale.x, gameObject.transform.parent.transform.localScale.z);

        playerModelFBX.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.x, gameObject.transform.position.x);

        GameObject playerModel = Instantiate(playerModelFBX);

        playerAnimator = gameObject.GetComponent<Animator>();


        playerModel.gameObject.transform.GetChild(0).SetParent(gameObject.transform);
        playerModel.gameObject.transform.GetChild(0).SetParent(gameObject.transform);

        playerAnimator.avatar = playerModelFBX.GetComponent<Animator>().avatar;

        Destroy(playerModel);
    }

    string NameToSlugConvert(string name)
    {
        string slug;
        slug = name.ToLower().Replace(".", "").Replace("'", "").Replace(" ", "-");
        return slug;

    }
}
