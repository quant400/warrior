using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapTilesetReplace : MonoBehaviour
{
    public string sourcePath;

    public string destPath;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeMap()
    {
        
        string tileLPath = sourcePath + "/newTilesL.png";

        string tileRPath = sourcePath + "/newTilesR.png";



        FileInfo sourceFile = new FileInfo(tileLPath);
        sourceFile.CopyTo(destPath + "/newTilesL.png", true);

        sourceFile = new FileInfo(tileRPath);
        sourceFile.CopyTo(destPath + "/newTilesR.png", true);
        
    }
}
