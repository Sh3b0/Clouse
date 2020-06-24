using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public GameObject LevelsButtons;
    public Animation[] OpeningAnimations;
    
    // List of availible levels 
    private static readonly Dictionary<int, string> Levels = new Dictionary<int, string>() {
        { 0, Constants.SN_TEST_LEVEL },
        { 1, Constants.SN_LEVEL }
    };

    private void Start() {
        LevelsButtons.SetActive(false);
    }

    public void OnStartClick() {
        foreach (var anim in OpeningAnimations) {
            anim.Play();
        }
        LevelsButtons.SetActive(true);
    }

    public void OnLevelClick(int levelNum) {
        SceneManager.LoadScene(Levels[levelNum]);
    }
    
    public void OnExitClick() {
        Application.Quit(0);
    }
    
}
