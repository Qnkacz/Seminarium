using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaplingSnap : MonoBehaviour
{
    public BoxCollider collider;
    public GameObject targettile;
    public Soil targetsoil;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="tile")
        {
            targettile =other.gameObject;
        }
    }
    private void Start()
    {
        targettile = this.gameObject.transform.parent.gameObject;
        Soil s = targettile.GetComponent<Soil>();
        if (s.child == null || s.child.tag == "Storage")
        {
            this.gameObject.transform.position = targettile.transform.position;
            this.gameObject.transform.parent = targettile.transform;
            targetsoil = targettile.GetComponent<Soil>();
            Destroy(targetsoil.child);
            targetsoil.child = this.gameObject;
            GlobalMoneymanager.GMM.ChangeMoney(GlobalMoneymanager.GMM.cost_Sapling);
        }
        else
        {
            GlobalMoneymanager.GMM.ChangeMoney(-GlobalMoneymanager.GMM.cost_Sapling);
            Destroy(this.gameObject);
        }
    }
    public void Snap()
    {
        
    }
}
