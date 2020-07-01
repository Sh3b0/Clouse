using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public static class CheckpointsManager {

    public static bool InitialCheckpointSaved = false;
    public static LevelState.SavedBox[] PlayerBoxes;
    public static readonly Collection<int> CheckpointsVisited = new Collection<int>(); // Considering no more than one per level
    
    private static int _checkpointLevel;
    private static Vector3 _savedPlayerPos, _savedCloudPos;
    
    // Updated only when checkpoint is created, used when CP is loaded or to load level state (after trying temp)
    private static readonly Dictionary<int, LevelState> SavedLevelStates = new Dictionary<int, LevelState>();
    
    // Updated every time player exits any level, used every time player enters visited level, or to create checkpoint
    private static readonly Dictionary<int, LevelState> TempLevelStates = new Dictionary<int, LevelState>();

    public static void CreateCheckpoint() {
        _checkpointLevel = LevelsManager.CurrentLevel;
        _savedPlayerPos = Player.me.transform.position;
        _savedCloudPos = Cloud.me.position;

        // Make all temp states saved, delete temp
        foreach (var tempSaveLevelIndex in TempLevelStates.Keys) {
            SavedLevelStates[tempSaveLevelIndex] = TempLevelStates[tempSaveLevelIndex];
        }
        TempLevelStates.Clear();

        // Notify player about checkpoint save
        MessageController.ShowMessage();
    }

    public static void LoadCheckpoint() {
        // Re-load scene
        GameLevel.CurrentLevelInstance.SaveLevelState(false);
        LevelsManager.LoadLevel(_checkpointLevel);
        
        Player.me.transform.position = _savedPlayerPos;
        Cloud.me.position = _savedCloudPos;
        
        // Return all objects on scene to their saved place, delete temp states
        GameLevel.CurrentLevelInstance.RecreateLevelState(SavedLevelStates[_checkpointLevel]);
        TempLevelStates.Clear();
    }

    // Create temp level state
    public static void SaveLevelState(int levelIndex, LevelState newLevelState) {
        TempLevelStates[levelIndex] = newLevelState;
    }
    
    // Try to load level state
    // If not temp, then try saved (null is returned in case of no luck at both)
    public static LevelState LoadLevelState(int levelIndex) {
        if (TempLevelStates.ContainsKey(levelIndex)) return TempLevelStates[levelIndex];
        if (SavedLevelStates.ContainsKey(levelIndex)) return SavedLevelStates[levelIndex];
        return null;
    }
    
}
