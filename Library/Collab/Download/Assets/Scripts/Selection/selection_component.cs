using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selection_component : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        var selectedBlock = new MaterialPropertyBlock();
    }

    private void OnDestroy()
    {
        var unselectedBlock = new MaterialPropertyBlock();
    }
}
