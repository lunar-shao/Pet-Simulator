using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour{
    public GameObject flashText;

    // Start is called before the first frame update
    void Start() {
        //Flash text
        InvokeRepeating("flashTheText", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update() {
        //If mouse click load the scene choice
        if (Input.GetMouseButtonUp(0)) {
            SceneManager.LoadScene("Choice");
        }
    }

    // Flash the text
    void flashTheText() {
        if (flashText.activeInHierarchy) {
            flashText.SetActive(false);
        }
        else {
            flashText.SetActive(true);
        }
    }
}
