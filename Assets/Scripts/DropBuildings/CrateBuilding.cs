using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateBuilding : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        
            if (other.gameObject.tag == "tree")
            {
                other.gameObject.GetComponent<Tree>().SetAbleToCut();
            }
       
    }
    
}
