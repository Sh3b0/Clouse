using System.Collections.Generic;
using UnityEngine.SceneManagement;

public static class LevelsManager {

    public static int CurrentLevel;
    public static bool EnteredFromLeft = true;

    private static readonly Dictionary<int, string> LevelIndexToScene = new Dictionary<int, string>() {
        {0, "Menu"},
        {1, "Level 1"},
        {2, "Level 2-1"},
        {3, "Level 2-2"},
        {4, "Level 3"},
        {5, "Level 4"},
        {6, "Level 5"},
        {7, "Level 6"}
    };

    public static bool CurrentLevelIsLast() {
        return CurrentLevel == LevelIndexToScene.Count - 1;
    }
    
    // In case player desires to return on the previous level
    public static void OnEnterReached() {
        EnteredFromLeft = false;
        GameLevel.CurrentLevelInstance.SaveLevelState(true);
        SceneManager.LoadScene(LevelIndexToScene[CurrentLevel - 1]);
    }

    public static void OnExitReached() {
        EnteredFromLeft = true;
        GameLevel.CurrentLevelInstance.SaveLevelState(true);
        SceneManager.LoadScene(LevelIndexToScene[CurrentLevel + 1]);
    }

    public static void LoadLevel(int levelIndex) {
        SceneManager.LoadScene(LevelIndexToScene[levelIndex]);
    }

}
