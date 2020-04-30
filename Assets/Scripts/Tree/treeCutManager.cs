using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeCutManager : MonoBehaviour
{
    public static treeCutManager TDM;
    public BossScript bosscript;
    public Queue<GameObject> treeQueue= new Queue<GameObject>();
    [Range(1, 10)]
    public int refreshTime;
    void Start()
    {
        TDM = this;
        StartCoroutine(checkIfStartedCutting(refreshTime));
    }
    public IEnumerator checkIfStartedCutting(int time)
    {
       while(true)
       {
            yield return new WaitForSeconds(time);
            if(treeQueue.Count>0)
            {
                StartCoroutine(startCutting());
            }
            
       }
    }
    public IEnumerator startCutting()
    {
        
        yield return new WaitForSeconds(4);
        if(treeQueue.Count!=0)
        {
            GameObject treetodestroy = treeQueue.Dequeue();
            treetodestroy.GetComponent<Tree>().addResources();
            treetodestroy.GetComponent<Tree>().releaseSoil();
            Destroy(treetodestroy);
        }
        else
        {
            StopCoroutine(startCutting());
        }
        
        
    }
   
}
