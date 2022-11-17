using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinnedMeshRendererBones : MonoBehaviour
{
    /*
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRendererPrefab;
    [SerializeField] private SkinnedMeshRenderer originalSkinnedMeshRenderer;
    [SerializeField] private Transform rootBone;
    */


    [SerializeField] private GameObject currentObject;
    [SerializeField] private GameObject newObjectPrefab;
    [SerializeField] private GameObject originalObjectPrefab;

    [SerializeField] private Transform rootBone;

    private int[] modelSwapped;

    // Start is called before the first frame update
    void Start()
    {
        modelSwapped = new int[15];

        for(int i = 0; i < modelSwapped.Length; i++)
        {
            modelSwapped[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ModelSwapper(int childNum)
    {
        SkinnedMeshRenderer skinnedMeshRendererPrefab;
        SkinnedMeshRenderer originalSkinnedMeshRenderer;

        if(modelSwapped[childNum - 1] == 0)
        {
            skinnedMeshRendererPrefab = newObjectPrefab.transform.GetChild(childNum).transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
            modelSwapped[childNum - 1] = 1;
        }
        else
        {
            skinnedMeshRendererPrefab = originalObjectPrefab.transform.GetChild(childNum).transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
            modelSwapped[childNum - 1] = 0;
        }


        originalSkinnedMeshRenderer = currentObject.transform.GetChild(childNum).transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();

        SkinnedMeshRenderer spawnedSkinnedMeshRenderer = Instantiate(skinnedMeshRendererPrefab, currentObject.transform.GetChild(childNum).transform);
        spawnedSkinnedMeshRenderer.bones = originalSkinnedMeshRenderer.bones;
        spawnedSkinnedMeshRenderer.rootBone = rootBone;

        //originalSkinnedMeshRenderer.gameObject.SetActive(false);

        //currentObject.transform.GetChild(childNum).transform.GetChild(0).gameObject.SetActive(false);

        Destroy(currentObject.transform.GetChild(childNum).transform.GetChild(0).gameObject);
    }

    public void ModelSwapperLegs()
    {
        int childNum = 13;

        SkinnedMeshRenderer skinnedMeshRendererPrefab;
        SkinnedMeshRenderer originalSkinnedMeshRenderer;


        if (modelSwapped[childNum - 1] == 0)
        {
            skinnedMeshRendererPrefab = newObjectPrefab.transform.GetChild(childNum).transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
            originalSkinnedMeshRenderer = currentObject.transform.GetChild(childNum).transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();

            SkinnedMeshRenderer spawnedSkinnedMeshRenderer = Instantiate(skinnedMeshRendererPrefab, currentObject.transform.GetChild(childNum).transform);
            spawnedSkinnedMeshRenderer.bones = originalSkinnedMeshRenderer.bones;
            spawnedSkinnedMeshRenderer.rootBone = rootBone;

            skinnedMeshRendererPrefab = newObjectPrefab.transform.GetChild(childNum).transform.GetChild(1).GetComponent<SkinnedMeshRenderer>();

            spawnedSkinnedMeshRenderer = Instantiate(skinnedMeshRendererPrefab, currentObject.transform.GetChild(childNum).transform);
            spawnedSkinnedMeshRenderer.bones = originalSkinnedMeshRenderer.bones;
            spawnedSkinnedMeshRenderer.rootBone = rootBone;

            modelSwapped[childNum - 1] = 1;

            Destroy(currentObject.transform.GetChild(childNum).transform.GetChild(0).gameObject);
        }
        else
        {
            skinnedMeshRendererPrefab = originalObjectPrefab.transform.GetChild(childNum).transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
            originalSkinnedMeshRenderer = currentObject.transform.GetChild(childNum).transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();

            SkinnedMeshRenderer spawnedSkinnedMeshRenderer = Instantiate(skinnedMeshRendererPrefab, currentObject.transform.GetChild(childNum).transform);
            spawnedSkinnedMeshRenderer.bones = originalSkinnedMeshRenderer.bones;
            spawnedSkinnedMeshRenderer.rootBone = rootBone;

            modelSwapped[childNum - 1] = 0;

            Destroy(currentObject.transform.GetChild(childNum).transform.GetChild(0).gameObject);
            Destroy(currentObject.transform.GetChild(childNum).transform.GetChild(1).gameObject);
        }

        //originalSkinnedMeshRenderer.gameObject.SetActive(false);

        //currentObject.transform.GetChild(childNum).transform.GetChild(0).gameObject.SetActive(false);

        /*
        Transform[] bones = new Transform[newMeshRenderer.bones.Length];
        for (int boneOrder = 0; boneOrder < newMeshRenderer.bones.Length; boneOrder++)
        {
            bones[boneOrder] = System.Array.Find<Transform>(childrens, c => c.name == newMeshRenderer.bones[boneOrder].name);
        }
        GetComponentInChildren<SkinnedMeshRenderer>().bones = bones;
        */
    }
}
