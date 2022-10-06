using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiSwapSprite : MonoBehaviour
{
    private Image objectImage;

    public Sprite onImage;

    public Sprite offImage;

    // Start is called before the first frame update
    void Start()
    {
        objectImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSprite()
    {
        if(objectImage.sprite == onImage)
        {
            objectImage.sprite = offImage;
        }
        else
        {
            objectImage.sprite = onImage;
        }
    }
}
