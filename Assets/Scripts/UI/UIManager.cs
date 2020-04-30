using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public global_selection globalSelection;
    public BossScript BossScript;
    [Header("MainCamera")]
    public Camera MainCamera;
    [Header("DayCircle")]
    public DayCircle dayCircle;
    public Text timeText;
    public Text DateDay;
    public Text DateMonth;
    public Text DateYear;
    [Header("Animations")]
    public Animator RefreshVariables;
    [Header("Sliders")]
    public Slider DaySlider;
    public Slider HourSlider;
    public Slider speedSlider;

    [Header("LeftMenus")]
    
    public float animationTime;
    public GameObject AreaMenu;
    public GameObject buildingMenu;
    public RectTransform buildingRect;
    public bool IsAreaMenuVisible;
    public bool isBuildingMenuVisible;
    public float menuWidth;
    
    [Header("RightMenus")]
    public bool isSaplingsMenuVisible;
    public GameObject saplingsMenu;
    public bool isWoodsMenuVisible;
    public Button Birch;
    public Button Oak;
    public Button Spruce;

    [Header("movement")]
    //one step right is from -46 to -39 so +7
    public GameObject MovementContainer;
    public Vector3 moveTo;
    public Button[] movementbuttons;
    public float baseMovementSpeed;
    public float sliderMultiplier;
    public float normalizedMovementSpeed;
    public float cameraSpeed;
    public float[] cameraBoundaries; //0 -left 1- right 2 - bot 3-top;
    public Text speedText;
    public bool isMovementActive;
    [Header("TopMenus")]
    public Text GameSpeed;
    public float gameSpeed;
    private void Start()
    {

        Invoke("StartUp", .02f);
    }
    private void Update()
    {
        
    }
    private void StartUp()
    {
        
        IsAreaMenuVisible = false;
        isBuildingMenuVisible = false;
        isSaplingsMenuVisible = false;
        baseMovementSpeed = 7f;
        menuWidth = buildingRect.rect.width;
        cameraBoundaries[0] = MapGenerator.mapGenerator.tile_botleft.transform.position.x + 3.5f;
        cameraBoundaries[1] = MapGenerator.mapGenerator.tile_toplight.transform.position.x - 4.5f;
        cameraBoundaries[2] = MapGenerator.mapGenerator.tile_botleft.transform.position.z - 4.5f; //done
        cameraBoundaries[3] = MapGenerator.mapGenerator.tile_toplight.transform.position.z - 4.5f;//140f;
        isMovementActive = true;
        speedText.text = " Camera Speed       " + speedSlider.value.ToString("f2");
    }
    public void UpdateTime()
    {
        
        DaySlider.maxValue = dayCircle.monthDuration;
        DaySlider.wholeNumbers = true;
        DaySlider.value = dayCircle.day;
        DateDay.text = dayCircle.day.ToString();
        DateMonth.text = dayCircle.month.ToString();
        DateYear.text = dayCircle.year.ToString();
    }
    public void ShowMenu()
    {
        AreaMenu.SetActive(IsAreaMenuVisible);
        buildingMenu.SetActive(isBuildingMenuVisible);
        saplingsMenu.SetActive(isSaplingsMenuVisible);
    }
    public void AreaButtonClick()
    {
        isBuildingMenuVisible = false;
        if (!IsAreaMenuVisible)
        {
            
            IsAreaMenuVisible = true;
        }
        else
        {
            IsAreaMenuVisible = false;
            
        }
        ShowMenu();
    }
    public void SaplingsButtonClick()
    {
        
        if (!isSaplingsMenuVisible)
        {

            isSaplingsMenuVisible = true;
        }
        else
        {
            isSaplingsMenuVisible = false;

        }
        ShowMenu();
    }
    public void WoodsButtonClick()
    {
        isSaplingsMenuVisible = false;
        if (!isWoodsMenuVisible)
        {

            isWoodsMenuVisible = true;
        }
        else
        {
            isWoodsMenuVisible = false;

        }
        ShowMenu();
    }
    public void BuildingButtonClick()
    {
        IsAreaMenuVisible = false;
        if (!isBuildingMenuVisible)
        {
            
            isBuildingMenuVisible = true;   
        }
        else
        {
            isBuildingMenuVisible = false;  
        }
        ShowMenu();
    }
    public void MoveCameraToRight()
    {
        sliderMultiplier = speedSlider.value;
        normalizedMovementSpeed = baseMovementSpeed * sliderMultiplier;
        if(MainCamera.transform.position.x + normalizedMovementSpeed < cameraBoundaries[1])
        {
            moveTo = new Vector3(MainCamera.transform.position.x + normalizedMovementSpeed, MainCamera.transform.position.y, MainCamera.transform.position.z);
            MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, moveTo, cameraSpeed);
        }
        else
        {
            moveTo = new Vector3(cameraBoundaries[1], MainCamera.transform.position.y, MainCamera.transform.position.z);
            MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, moveTo, cameraSpeed);
            
        }
        
    }
    public void MoveCameraToLeft()
    {
        sliderMultiplier = speedSlider.value;
        normalizedMovementSpeed = baseMovementSpeed * sliderMultiplier;
        if(MainCamera.transform.position.x - normalizedMovementSpeed > cameraBoundaries[0])
        {
            moveTo = new Vector3(MainCamera.transform.position.x - normalizedMovementSpeed, MainCamera.transform.position.y, MainCamera.transform.position.z);
            MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, moveTo, cameraSpeed);
        }
        else
        {
            moveTo = new Vector3(cameraBoundaries[0], MainCamera.transform.position.y, MainCamera.transform.position.z);
            MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, moveTo, cameraSpeed);
        }
        
    }
    public void MoveCametaToTop()
    {
        sliderMultiplier = speedSlider.value;
        normalizedMovementSpeed = baseMovementSpeed * sliderMultiplier;
        if(MainCamera.transform.position.z + normalizedMovementSpeed < cameraBoundaries[3])
        {
            moveTo = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y , MainCamera.transform.position.z + normalizedMovementSpeed);
            MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, moveTo, cameraSpeed);
        }
        else
        {
            moveTo = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, cameraBoundaries[3]);
            MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, moveTo, cameraSpeed);
        }
        
    }
    public void MoveCameraToBot()
    {
        sliderMultiplier = speedSlider.value;
        normalizedMovementSpeed = baseMovementSpeed * sliderMultiplier;
        if(MainCamera.transform.position.z - normalizedMovementSpeed > cameraBoundaries[2])
        {
            moveTo = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z - normalizedMovementSpeed);
            MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, moveTo, cameraSpeed);
        }
        else
        {
            moveTo = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, cameraBoundaries[2]);
            MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, moveTo, cameraSpeed);
        }
        
    }
    public void ChangeSpeedText()
    {
        speedText.text = "Camera Speed       " + speedSlider.value.ToString("f2");
    }
    public void ShowMovement()
    {
        if (isMovementActive == true)
        {
            isMovementActive = false;

        }
        else isMovementActive = true;

        MovementContainer.SetActive(isMovementActive);
    }
    public void SetToCut()
    {
        globalSelection.SetTreesToCut();
    }
    public void SetToStorage()
    {
        globalSelection.SetToStorage();
    }


}
