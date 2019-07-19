using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Timers;

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
        set {_hunger = value;}
    }

    public int happiness {
        get { return _happiness; }
        set { _happiness = value;}
    }

    public string name {
        get { return _name; }
        set { _name = value; }
    }

    /***********************Methods***********************************/

    // Start is called before the first frame update
    void Start(){
        updateStatus();
        if (!PlayerPrefs.HasKey("name")) {
            PlayerPrefs.SetString("name", " Pet");
        }
        else {
            _name = PlayerPrefs.GetString("name");
        }
    }

    // Update is called once per frame
    void Update() {

        //If pet is above certain point, jump
        GetComponent<Animator>().SetBool("jump", gameObject.transform.position.y > -1.6f);

        if (Input.GetMouseButtonUp(0)) {
            //Get mouse location
            Vector2 v = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            //Check if click on pet
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(v), Vector2.zero);

            if (hit) {
                if(hit.transform.gameObject.tag == "pet") {
                    _clickCount++;
                    if(_clickCount >= 3) {
                        _clickCount = 0;
                        updateHappiness(1);
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 100000));
                    }
                }
            }
        }
    }

    //Update status of pet
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

        //For every hour, substract 2 of hunger
        _hunger -= (int)(ts.TotalHours * 2);
        if (_hunger < 0) {
            _hunger = 0;
        }
        // For x of _hunger, divided by total hour, happiness going down
        _happiness -= (int)((100 - _hunger) * (ts.TotalHours / 5));
        if (_happiness < 0) {
            _happiness = 0;
        }

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
        if (_happiness > 100) {
            happiness = 100;
        }
    }

    public void updateHunger(int i) {
        _hunger += i;
        if (_hunger > 100) {
            _hunger = 100;
        }
    }

    //Save of the game
    public void savePet() {
        updateDevice();
        PlayerPrefs.SetInt("_hunger", _hunger);
        PlayerPrefs.SetInt("_happiness", _happiness);
    }

    //When pet is between 1 and 25 of happiness
    public void hurt() {
        if(_happiness < 30) {
            GetComponent<Animator>().SetBool("hurt",true);
        }
        else {
            GetComponent<Animator>().SetBool("hurt", false);
        }
    }

    //When pet is at 0 of hunger
    public void dead() {
        if (_hunger == 0) {
            GetComponent<Animator>().SetBool("hurt", false);
            GetComponent<Animator>().SetBool("dead", true);
        }
        else {
            GetComponent<Animator>().SetBool("dead", false);
        }

    }
   
}
