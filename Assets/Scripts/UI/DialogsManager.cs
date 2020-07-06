using UnityEngine;
using UnityEngine.UI;

public class DialogsManager : MonoBehaviour {

    // Singleton
    public static DialogsManager Instance { get; private set; }

    public Image Icon;
    public Text Text;
    public GameObject DialogLines;
    public Button Next, Finish, Skip;
    
    private int _currentLineIndex;
    private Constants.DialogEntity[] _currentDialog;
    
    private void Start() {
        // Initialization of manager on scene
        Instance = this;
        Next.onClick.AddListener(OnNextLine);
        Finish.onClick.AddListener(OnFinish);
        Skip.onClick.AddListener(OnFinish);
    }

    public void OnDialog(int dialogIndex) {
        _currentDialog = Constants.Dialogs[dialogIndex];
        
        Player.me.gameObject.GetComponent<Player>().anim.Move(0.0f);
        Player.playerActive = false;
        DialogLines.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Next.gameObject.SetActive(true);
        Finish.gameObject.SetActive(false);

        Icon.sprite = Resources.Load<Sprite>(_currentDialog[0].IconPath);
        Text.text = _currentDialog[0].Text;

        _currentLineIndex = 0;
    }
    
    private void OnNextLine() {
        _currentLineIndex++;
        
        Icon.sprite = Resources.Load<Sprite>(_currentDialog[_currentLineIndex].IconPath);
        Text.text = _currentDialog[_currentLineIndex].Text;

        if (_currentLineIndex < _currentDialog.Length - 1) return;
        Next.gameObject.SetActive(false);
        Finish.gameObject.SetActive(true);
    }

    private void OnFinish() {
        DialogLines.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Player.playerActive = true;
    }

}
