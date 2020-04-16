using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selection_component : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        var selectedBlock = new MaterialPropertyBlock();
        selectedBlock.SetColor("_BaseColor", Color.red);
        GetComponent<Renderer>().SetPropertyBlock(selectedBlock);
    }

    private void OnDestroy()
    {
        var unselectedBlock = new MaterialPropertyBlock();
        unselectedBlock.SetColor("_BaseColor", Color.white);
        GetComponent<Renderer>().SetPropertyBlock(unselectedBlock);
    }
}
