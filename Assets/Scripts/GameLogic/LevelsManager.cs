using UnityEngine;

public class LevelsManager : MonoBehaviour {

    // TODO Consider making smoother transition between levels
    // public Animation Fade;

    public Transform PlayerInstance, CloudInstance;
    public GameLevel[] Levels;

    private int _currentLevel;
    private GameLevel[] _levelsInstances;
    
    private void Start() {
        _currentLevel = 0;
        _levelsInstances = new GameLevel[Levels.Length];

        // Create first level
        _levelsInstances[0] = Instantiate(Levels[0], transform);
        _levelsInstances[0].MovePlayerToTheStart(PlayerInstance);
        _levelsInstances[0].MoveCloudToTheStart(CloudInstance);
        
        EventManager.StartListening(Constants.EVENT_ENTER_REACHED, OnEnterReached);
        EventManager.StartListening(Constants.EVENT_EXIT_REACHED, OnExitReached);
        EventManager.StartListening(Constants.EVENT_RESTART, ReloadLevel);
    }

    // In case player desires to return on the previous level
    private void OnEnterReached()
    {
        CameraSwitcher.ResetCamera(); // Resets Camera on new level enter.
        if (_currentLevel - 1 < 0) {
            print("This is the first level!");
            return;
        }
        Destroy(_levelsInstances[_currentLevel].gameObject);
        
        _currentLevel--;
        _levelsInstances[_currentLevel] = Instantiate(Levels[_currentLevel], transform);
        _levelsInstances[_currentLevel].MovePlayerToTheExit(PlayerInstance);
        _levelsInstances[_currentLevel].MoveCloudToTheExit(CloudInstance);
    }

    private void OnExitReached() {
        CameraSwitcher.ResetCamera(); // Resets Camera on new level enter.
        if (_currentLevel + 1 >= Levels.Length) {
            print("That was the last level!");
            return;
        }
        Destroy(_levelsInstances[_currentLevel].gameObject);
        
        _currentLevel++;
        _levelsInstances[_currentLevel] = Instantiate(Levels[_currentLevel], transform);
        _levelsInstances[_currentLevel].MovePlayerToTheStart(PlayerInstance);
        _levelsInstances[_currentLevel].MoveCloudToTheStart(CloudInstance);
    }

    private void ReloadLevel() {
        // Remove old level instance and create a new one instead
        Destroy(_levelsInstances[_currentLevel].gameObject);
        _levelsInstances[_currentLevel] = Instantiate(Levels[_currentLevel], transform);
        _levelsInstances[_currentLevel].MovePlayerToTheStart(PlayerInstance);
        _levelsInstances[_currentLevel].MoveCloudToTheStart(CloudInstance);
    }

}
