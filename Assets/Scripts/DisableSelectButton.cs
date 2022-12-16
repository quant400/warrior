using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableSelectButton : MonoBehaviour
{
    [SerializeField]
    private Button select;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        DisablePlay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisablePlay()
    {
        select.interactable = false;
    }
}
