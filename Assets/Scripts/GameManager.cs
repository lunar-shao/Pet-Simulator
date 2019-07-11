using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    /*********************************************VARIABLES*****************************************************/
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
    public Sprite[] foodIcons;


    // Start is called before the first frame update
    void Start() {
        //triggerNamePanel(true);
    }

    // Update is called once per frame
    void Update() {
        
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

    //Show desired panel in parameters
    public void toggle(GameObject g) {
        if (g.activeInHierarchy) {
            g.SetActive(false);
        }
    }
}
