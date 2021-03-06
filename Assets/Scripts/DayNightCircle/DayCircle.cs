﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayCircle : MonoBehaviour
{
    /// <summary>
    /// Things for the 24h timers
    /// </summary>
    /// 
    public Text timeText;
    //public Text DateDay;
    //public Text DateMonth;
    //public Text DateYear;
    public const float REALS_SECONDS_PER_INGAME_DAY = 60F;
    public static float rday;
    public  float hours;
    public  float minutes;
    private string HoursString;
    private string MinutesString;
    public float hoursperDay = 24f;
    public float minutesperday = 60f;
    float dayNormalized;
    public UIManager UIManager;
    public float timeScale;

    /// <summary>
    /// things for the date
    /// </summary>
    public int day, month, year;
    public int monthDuration, lutyDuration;
    [Header("Referencess")]
    public GlobalVariables GV;
    public UIManager UImanager;
    public BossScript BossScript;

    private void Start()
    {
        
        day = 30;
        month = 5;
        year = 1997;
        monthduration();
        
        UIManager.UpdateTime();
        GV.currDay = day;
        GV.currMonth = month;
        GV.currYear = year;
        timeScale = 1;
        UImanager.gameSpeed = timeScale;
    }
    private void Update()
    {
        timeChanger();
        
    }

    void timeChanger()
    {
        rday += Time.deltaTime / REALS_SECONDS_PER_INGAME_DAY;
        dayNormalized = rday % 1f;
        hours = (dayNormalized * hoursperDay);
        HoursString = Mathf.Floor(hours).ToString("00");
        UIManager.HourSlider.value = rday;
        MinutesString = Mathf.Floor(((dayNormalized * hoursperDay) % 1f) * minutesperday).ToString("00");
        timeText.text = HoursString + " : " + MinutesString;
        if(rday>1)
        { 
            rday = 0;
            day += 1;
            DateChanger();
        }
    }
    private static bool przestepny(int rok)
    {
        return ((rok % 4 == 0) && (rok % 100 != 0)) || (rok % 400 == 0);
    }
    void DateChanger()
    {
        if(month==2)
        {
            if(day>lutyDuration)
            {
                day = 1;
                month += 1;
            }
        }
        else
        {
            if(day>monthDuration)
            {
                day = 1;
                month += 1;
            }
        }

        if(month>13)
        {
            year += 1;
            month = 1;
        }
        //DateDay.text = day.ToString();
        //DateMonth.text = month.ToString();
        //DateYear.text = year.ToString();
        monthduration();
        UIManager.UpdateTime();
        GV.currDay = day;
        GV.currMonth = month;
        GV.currYear = year;
    }
    void monthduration()
    {
        if (przestepny(year))
        {
            lutyDuration = 29;
        }
        else
        {
            lutyDuration = 28;
        }
        if (month % 2 == 0)
        {
            monthDuration = 30;
        }
        else
        {
            monthDuration = 31;
        }
    }

    public void SetTimeScale_pause()
    {
        Time.timeScale = 0;
        timeScale = 0;
        if(UImanager.gameSpeed>timeScale)
        {
            UImanager.RefreshVariables.SetTrigger("R_sub");
        }
        else
        {
            UIManager.RefreshVariables.SetTrigger("R_add");
        }
        UImanager.gameSpeed = timeScale;
        BossScript.currGrowRT = (int)timeScale;
    }
    public void SetTimeScale_To1()
    {
        Time.timeScale = 1;
        timeScale = 1;
        if (UImanager.gameSpeed > timeScale)
        {
            UImanager.RefreshVariables.SetTrigger("R_sub");
        }
        else
        {
            UIManager.RefreshVariables.SetTrigger("R_add");
        }
        UImanager.gameSpeed = timeScale;
        BossScript.currGrowRT = (int)timeScale;
    }
    public void SetTimeScale_To4()
    {
        Time.timeScale = 4;
        timeScale = 4;
        if (UImanager.gameSpeed > timeScale)
        {
            UImanager.RefreshVariables.SetTrigger("R_sub");
        }
        else
        {
            UIManager.RefreshVariables.SetTrigger("R_add");
        }
        UImanager.gameSpeed = timeScale;
        BossScript.currGrowRT = (int)timeScale;
    }
    public void SetTimeScale_To10()
    {
        Time.timeScale = 10;
        timeScale = 10;
        if (UImanager.gameSpeed > timeScale)
        {
            UImanager.RefreshVariables.SetTrigger("R_sub");
        }
        else
        {
            UIManager.RefreshVariables.SetTrigger("R_add");
        }
        UImanager.gameSpeed = timeScale;
        BossScript.currGrowRT = (int)timeScale;
    }
}
