using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SkinShop : MonoBehaviour
{
    public Material birdMaterial;

    public Material normalMat;

    public Material blackMat;

    public Material goldMat;

    public Material redMat;

    public Material obstacleMat;

    public Material oldMat;
    
    private UIDocument _uiDocument;

    public Global global;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateAchievementUnlocks();
        
        bool blackUnlocked = PlayerPrefs.GetInt("blackUnlocked", 0) == 1;
        bool goldUnlocked =  PlayerPrefs.GetInt("goldUnlocked", 0) == 1;
        bool redUnlocked =  PlayerPrefs.GetInt("redUnlocked", 0) == 1;
        bool obstacleUnlocked =   PlayerPrefs.GetInt("obstacleUnlocked", 0) == 1;
        bool oldUnlocked =   PlayerPrefs.GetInt("oldUnlocked", 0) == 1;
        
        _uiDocument = GetComponent<UIDocument>();
        VisualElement root = _uiDocument.rootVisualElement;
        
        VisualElement plain =  root.Q<VisualElement>("Plain");
        VisualElement black = root.Q<VisualElement>("Black");
        VisualElement gold = root.Q<VisualElement>("Gold");
        VisualElement red = root.Q<VisualElement>("Red");
        VisualElement obstacle = root.Q<VisualElement>("Obstacle");
        VisualElement old = root.Q<VisualElement>("Old");
        
        global.coins = PlayerPrefs.GetInt("coins", 0);
        print("you have: " + global.coins + " coins");
        
        UpdateUIForPlane(plain, true, -1, OnPlainSelectClicked);
        UpdateUIForPlane(black, blackUnlocked, 100, OnBlackSelectClicked);
        UpdateUIForPlane(gold, goldUnlocked, 9999, OnGoldSelectClicked);
        UpdateUIForPlane(red, redUnlocked, -1, OnRedSelectClicked);
        UpdateUIForPlane(obstacle, obstacleUnlocked, -1,  OnObstacleSelectClicked);
        UpdateUIForPlane(old, oldUnlocked, -1, OnOldSelectClicked);
    }

    void UpdateAchievementUnlocks()
    {
        int highscore = PlayerPrefs.GetInt("highscore", 0);
        int playTimes =  PlayerPrefs.GetInt("playTimes", 0);
        if (highscore > 100)
        {
            PlayerPrefs.SetInt("redUnlocked", 1);
        }
        if (playTimes > 20)
        {
            PlayerPrefs.SetInt("obstacleUnlocked", 1);
        }
        if (playTimes > 50)
        {
            PlayerPrefs.SetInt("oldUnlocked", 1);
        }
    }

    void UpdateUIForPlane(VisualElement plane, bool unlocked, int coinsNeeded, EventCallback<ClickEvent> buttonFunction)
    {
        Label unlockLabel = plane.Q<Label>("unlockText");
        Button selectButton = plane.Q<Button>("SelectButton");
        unlockLabel.visible = !unlocked;
        selectButton.SetEnabled(unlocked);
        selectButton.RegisterCallback(buttonFunction);  
        if (coinsNeeded < 0) return;
        if (global.coins > coinsNeeded)
        {
            selectButton.SetEnabled(true);
        }
        
    }

    void OnPlainSelectClicked(ClickEvent evt)
    {
        print("plain");
        birdMaterial.CopyPropertiesFromMaterial(normalMat);
        SceneManager.LoadScene(0);
    }

    void OnBlackSelectClicked(ClickEvent evt)
    {
        if (PlayerPrefs.GetInt("blackUnlocked", 0) != 1)
        {
            PlayerPrefs.SetInt("blackUnlocked", 1);
            global.coins -= 100;
            global.SaveCoins();
        }
        birdMaterial.CopyPropertiesFromMaterial(blackMat);
        SceneManager.LoadScene(0);
    }

    void OnGoldSelectClicked(ClickEvent evt)
    {
        if (PlayerPrefs.GetInt("goldUnlocked", 0) != 1)
        {
            PlayerPrefs.SetInt("goldUnlocked", 1);
            global.coins -= 9999;
            global.SaveCoins();
        }
        birdMaterial.CopyPropertiesFromMaterial(goldMat);
        SceneManager.LoadScene(0);
    }

    void OnRedSelectClicked(ClickEvent evt)
    {
        birdMaterial.CopyPropertiesFromMaterial(redMat);
        SceneManager.LoadScene(0);
    }

    void OnObstacleSelectClicked(ClickEvent evt)
    {
        birdMaterial.CopyPropertiesFromMaterial(obstacleMat);
        SceneManager.LoadScene(0);
    }

    void OnOldSelectClicked(ClickEvent evt)
    {
        birdMaterial.CopyPropertiesFromMaterial(oldMat);
        SceneManager.LoadScene(0);
    }
}
