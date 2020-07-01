using System.Collections.Generic;
using UnityEngine;

public class LevelState {

    // Box saving model
    public struct SavedBox {
        public readonly bool IsMetal;
        public Vector3 LocalPos;
        
        public SavedBox(bool isMetal, Vector3 localPos) {
            IsMetal = isMetal;
            LocalPos = localPos;
        }
    }
    
    // Mirror saving model
    public struct SavedMirror {
        public Quaternion Rotation;
        public int Mode;
        public bool Dir;
        
        public SavedMirror(Quaternion rotation, int mode, bool dir) {
            Rotation = rotation;
            Mode = mode;
            Dir = dir;
        }
    }
    
    public SavedBox[] Boxes;
    public Dictionary<Vector3, SavedMirror> MirrorsByPos;
    public Dictionary<Vector3, bool> GeneratorsActivatedByPos;
    // TODO Add support for water

}
