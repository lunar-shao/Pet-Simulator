using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Timers;

public class GameManager : MonoBehaviour {

    /*********************************************VALUES*****************************************************/
    public GameObject happinessText;
    public GameObject hungerText;

    public GameObject namePanel;
    public GameObject nameInput;
    public GameObject nameText;

    public GameObject pet;
    public GameObject petPanel;
    public GameObject[] petList;

    public GameObject homePanel;
    public Sprite[] homeTileSprites;
    public GameObject[] homeTiles;

    public GameObject background;
    public Sprite[] backgroundOptions;

    public GameObject foodPanel;
    public GameObject food;
    public Sprite[] foodIcons;

    public GameObject replayPanel;

    public AudioSource musicSource;
    public AudioClip[] musicList;

    private Timer tmr;
    private int timerCount;


    /***********************Methods***********************************/

    // Start is called before the first frame update
    void Start() {
        if (!PlayerPrefs.HasKey("looks")) {
            PlayerPrefs.SetInt("looks", 0);
        }
        createPet(PlayerPrefs.GetInt("looks"));

        if (!PlayerPrefs.HasKey("tiles")) {
            PlayerPrefs.SetInt("tiles", 0);
        }
        changeTiles(PlayerPrefs.GetInt("tiles"));

        if (!PlayerPrefs.HasKey("background")) {
            PlayerPrefs.SetInt("background", 0);
        }
        changeBackground(PlayerPrefs.GetInt("background"));

        playMusic(0);
    }

    // Update is called once per frame
    void Update() {
        happinessText.GetComponent<Text>().text = "" + pet.GetComponent<Pet>().happiness;
        hungerText.GetComponent<Text>().text = "" + pet.GetComponent<Pet>().hunger;
        nameText.GetComponent<Text>().text = pet.GetComponent<Pet>().name;

        //Check if pet is hurt (happiness level)
        pet.GetComponent<Pet>().hurt();
        //Check if pet is dead (hungry level)
        pet.GetComponent<Pet>().dead();

        string hunger = "" + pet.GetComponent<Pet>().hunger;
        if (hunger == "0" && timerCount == 0) {
            Timer tm = new Timer();
            tm.Interval = 5000;
            tm.Elapsed += (o,e) => replayPanel.SetActive(!replayPanel.activeInHierarchy);
            tm.Start();
            timerCount++;
        }
    }

    //Replay of the 
    public void replay(bool b) {
        if (b) {
            PlayerPrefs.DeleteAll();
            createPet(0);
            Start();
            timerCount = 0;
        }
        else {
            Application.Quit();
        }
    }

    //Show panel to change name of robot
    public void triggerNamePanel(bool b) {
        namePanel.SetActive(!namePanel.activeInHierarchy);

        //If panel is "on", change name of robot
        if (b) {
            pet.GetComponent<Pet>().name = nameInput.GetComponent<InputField>().text;
            PlayerPrefs.SetString("name", pet.GetComponent<Pet>().name);
        }
    }

    //Creation of pet
    public void createPet(int i) {
        if (pet) {
            Destroy(pet);
        }
        pet = Instantiate(petList[i], Vector3.zero, Quaternion.identity) as GameObject;

        toggle(petPanel);
        PlayerPrefs.SetInt("looks", i);
    }

    //Manager of bottom buttons in application
    public void buttonBehavior(int i) {
        switch (i) {
            //For the looks
            case (0):
            default:
                petPanel.SetActive(!petPanel.activeInHierarchy);
                break;
            //For the background
            case (1):
                homePanel.SetActive(!homePanel.activeInHierarchy);
                break;
            //For the food
            case (2):
                foodPanel.SetActive(!foodPanel.activeInHierarchy);
                break;
            //For the music
            case (3):
                funMusic();
                break;
            //Quit the game
            case (4):
                pet.GetComponent<Pet>().savePet();
                Application.Quit();
                break;
        }
    }

    //Show desired panel in parameters
    public void toggle(GameObject g) {
        if (g.activeInHierarchy) {
            g.SetActive(false);
        }
    }

    //Change the tiles (sort of ground) of application
    public void changeTiles(int t) {
        for (int i = 0; i < homeTiles.Length; i++) {
            homeTiles[i].GetComponent<SpriteRenderer>().sprite = homeTileSprites[t];
        }
        toggle(homePanel);
        PlayerPrefs.SetInt("tiles", t);
    }

    //Change background image of application
    public void changeBackground(int i) {
        background.GetComponent<SpriteRenderer>().sprite = backgroundOptions[i];
        toggle(homePanel);
        PlayerPrefs.SetInt("background", i);
    }

    //Choice of food
    public void selectFood(int i) {

        if(foodIcons[i].name == "candy") {
            pet.GetComponent<Pet>().updateHunger(2);
            pet.GetComponent<Pet>().updateHappiness(3);
        }
        else {
            pet.GetComponent<Pet>().updateHunger(4);
            pet.GetComponent<Pet>().updateHappiness(2);
        }
        toggle(foodPanel);
    }

    //Play the main music
    public void playMusic(int i) {
        System.Random r = new System.Random();
        DateTime now = DateTime.Now;
        int month = now.Month;
        switch (month) {
            case (1):
            case (2):
            case (3):
                int winter = r.Next(10,14);
                musicSource.clip = musicList[winter];
                musicSource.PlayDelayed(i);
                break;
            case (4):
            case (5):
            case (6):
                int spring = r.Next(1, 4);
                musicSource.clip = musicList[spring];
                musicSource.PlayDelayed(i);
                break;
            case (7):
            case (8):
            case (9):
                int summer = r.Next(4, 7);
                musicSource.clip = musicList[summer];
                musicSource.PlayDelayed(i);
                break;
            case (10):
            case (11):
            case (12):
                int autumn = r.Next(7, 10);
                musicSource.clip = musicList[autumn];
                musicSource.PlayDelayed(i);
                break;
        }
    }

    //Put music for pet
    public void funMusic() {
        musicSource.clip = musicList[0];
        musicSource.PlayOneShot(musicSource.clip);
        pet.GetComponent<Pet>().updateHappiness(6);

        playMusic(15);
    }
}
