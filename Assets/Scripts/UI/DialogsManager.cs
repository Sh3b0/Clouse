using UnityEngine;
using UnityEngine.UI;

public class DialogsManager : MonoBehaviour {

    public Image Icon;
    public Text Text;
    public GameObject DialogLines;
    public Button Next, Finish;
    
    private int _currentDialogIndex, _currentLineIndex;
    private Constants.DialogEntity[] _currentDialog;
    
    private void Start() {
        _currentDialogIndex = 0;
        EventManager.StartListening(Constants.EVENT_DIALOG, OnDialog);
    }

    public void OnNextLine() {
        _currentLineIndex++;
        
        Icon.sprite = Resources.Load<Sprite>(_currentDialog[_currentLineIndex].IconPath);
        Text.text = _currentDialog[_currentLineIndex].Text;

        if (_currentLineIndex < _currentDialog.Length - 1) return;
        Next.gameObject.SetActive(false);
        Finish.gameObject.SetActive(true);
    }

    public void OnFinish() {
        DialogLines.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Player.playerActive = true;
    }

    private void OnDialog() {
        _currentDialog = Constants.Dialogs[_currentDialogIndex];
        
        Player.playerActive = false;
        DialogLines.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Next.gameObject.SetActive(true);
        Finish.gameObject.SetActive(false);

        Icon.sprite = Resources.Load<Sprite>(_currentDialog[0].IconPath);
        Text.text = _currentDialog[0].Text;

        _currentLineIndex = 0;
        _currentDialogIndex++;
    }

}
