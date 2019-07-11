using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pet : MonoBehaviour {

    /**************Values********/
    [SerializeField]
    private int _hunger;
    [SerializeField]
    private int _happiness;
    [SerializeField]
    private string _name;

    private int _clickCount;


    /*********************Getters and setters********************/
    public int hunger {
        get { return _hunger; }
        set {
            _hunger = value;
            if (_hunger < 0) {
                _hunger = 0;
            }
            if (_hunger > 100) {
                _hunger = 100;
            }

        }
    }

    public int happiness {
        get { return _happiness; }
        set { _happiness = value;
            if (_happiness < 0) {
                _happiness = 0;
            }
            if (_happiness > 100) {
                _happiness = 100;
            }

        }
    }

    public string name {
        get { return _name; }
        set { _name = value; }
    }

    /***********************Methods***********************************/

    // Start is called before the first frame update
    void Start(){
        PlayerPrefs.SetString("then", "07/10/2019 7:54:00");
        updateStatus();
    }

    // Update is called once per frame
    void Update() {
        
    }

    //Update status of robot
    void updateStatus() {
        if (!PlayerPrefs.HasKey("_hunger")) {
            _hunger = 100;
            PlayerPrefs.SetInt("_hunger", _hunger);
        }
        else {
            _hunger = PlayerPrefs.GetInt("_hunger");
        }

        if (!PlayerPrefs.HasKey("_happiness")) {
            _happiness = 100;
            PlayerPrefs.SetInt("_happiness", _happiness);
        }
        else {
            _happiness = PlayerPrefs.GetInt("_happiness");
        }

        //If dont have "then", set now as TimeStamp
        if (!PlayerPrefs.HasKey("then")) {
            PlayerPrefs.SetString("then", getStringTime());
        }

        TimeSpan ts = getTimeSpan();

        //For every hour, substrac 2 of hunger
        _hunger -= (int)(ts.TotalHours * 2);
        if (_hunger < 0) {
            _hunger = 0;
        }
        // For x of _hunger, divided by total hour, happiness going down
        _happiness -= (int)((100 - _hunger) * (ts.TotalHours / 5));
        /*if (_happiness < 0) {
            _happiness = 0;
        }*/

        Debug.Log(getTimeSpan().ToString() + "debug updateStatus");
        //Debug.Log(getTimeSpan().TotalHours);

        //Check time based on device, can create a loophole
        InvokeRepeating("updateDevice", 0f, 30f);
    }

    void updateDevice() {
        PlayerPrefs.SetString("then", getStringTime());
    }

    TimeSpan getTimeSpan() {
        return DateTime.Now - Convert.ToDateTime(PlayerPrefs.GetString("then"));
    }

    //Return time in a string
    string getStringTime() {
        DateTime now = DateTime.Now;
        return now.Day + "/" + now.Month + "/" + now.Year + " " + now.Hour + ":" + now.Minute + ":" + now.Second;
    }

    public void updateHappiness(int i) {
        _happiness += i;
        /*if (_happiness > 100) {
            happiness = 100;
        }*/
    }

    public void updateHunger(int i) {
        _hunger += i;
        /*if (_happiness > 100) {
            happiness = 100;
        }*/
    }

    //Save of the game
    public void saveRobot() {
        updateDevice();
        PlayerPrefs.SetInt("_hunger", _hunger);
        PlayerPrefs.SetInt("_happiness", _happiness);
    }
}
