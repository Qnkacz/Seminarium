using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ObstacleInfo : MonoBehaviour
{
    public int x;
    public int y;
    [Header("UI")]
    public Canvas canvas;
    public TextMesh cost;
    public GameObject boulder;
    public Soil soil;
    public void setCoords(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public void Start()
    {
        cost.text = "Cost: " + (GlobalMoneymanager.GMM.cost_obstacle_stone*-1).ToString();
    }
    public void AwakeCanvas()
    {
        canvas.gameObject.SetActive(true);
        StartCoroutine(hideAfterperiod());
    }
    public void hideCanvas()
    {
        canvas.gameObject.SetActive(false);
    }
    public void DestroyObstacle()
    {
        GlobalMoneymanager.GMM.ChangeMoney(GlobalMoneymanager.GMM.cost_obstacle_stone);
        soil.child = null;
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="tile")
        {
            soil=other.gameObject.GetComponent<Soil>();
        }
    }
    IEnumerator hideAfterperiod()
    {
        yield return new WaitForSeconds(5f);
        hideCanvas();
    }
}
