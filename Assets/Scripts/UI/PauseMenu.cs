using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject PausePanel;
    
    private bool _inMenu;

    private void Start() {
        OnResume();
    }

    private void Update() {
        if (Input.GetKeyDown("r")) OnRestart();
        if (!Input.GetButtonDown("Cancel")) return;
        if (_inMenu) OnResume();
        else OnPause();
    }

    public void OnResume() {
        _inMenu = false;
        PausePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    public void OnRestart() {
        OnResume();
        CheckpointsManager.LoadCheckpoint();
    }

    public void OnExitToMenu() {
        SceneManager.LoadScene(Constants.SN_MENU);
    }
    
    private void OnPause() {
        _inMenu = true;
        PausePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
}
