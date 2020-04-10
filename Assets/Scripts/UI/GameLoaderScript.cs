using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoaderScript : MonoBehaviour
{
    public int WhatLevelToLoad;
    public Animator transition;
    public AsyncOperation loading;
    public Slider slider;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("money: "+GlobalVariables.startMoneyAmount);
            Debug.Log("saplings: "+GlobalVariables.StarSaplingAmount);
            Debug.Log("GR: " + GlobalVariables.StartGrowRate);
        }
    }
   
    public void LoadNextLevel(int i)
    {
        UpdateUI(0);
        
        StartCoroutine(LoadLevel(i));
    }

    private void UpdateUI(float v)
    {
        slider.value = v;
    }

    IEnumerator LoadLevel(int i)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        loading = SceneManager.LoadSceneAsync(i);
        while(!loading.isDone)
        {
            UpdateUI(loading.progress);
            yield return null;
        }

        UpdateUI(loading.progress);
        loading = null;
       
        
       
    }
}
