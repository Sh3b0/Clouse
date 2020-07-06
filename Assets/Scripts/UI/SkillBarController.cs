using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SkillBarController : MonoBehaviour {

    public Image[] SkillsIcons;
    
    public void Initialize() {
        for (var i = 0; i < SkillsIcons.Length; i++) {
            DeactivateSkill(i);
        }
    }

    public void ActivateSkill(int skillIndex) {
        SkillsIcons[skillIndex].color = new Color32(47, 196, 237, 255);
    }

    private void DeactivateSkill(int skillIndex) {
        SkillsIcons[skillIndex].color = new Color32(47, 196, 237, 90);
    }
}