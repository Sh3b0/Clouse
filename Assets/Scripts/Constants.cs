public static class Constants {
    
    // Scene Names
    public const string SN_MENU = "Menu";
    public const string SN_TEST_LEVEL = "Test-Ahmed";
    public const string SN_LEVEL = "Level 1";

    // Objects Tags
    public const string TAG_PLAYER = "Player";
    public const string TAG_DRAIN = "Drain";
    public const string TAG_BOX = "Box";
    public const string TAG_HELD = "Held";
    public const string TAG_GENERATOR = "Generator";
    public const string TAG_MIRROR = "Mirror";
    
    // Dialogs
    public struct DialogEntity {
        public string IconPath;
        public string Text;
    }

    public static readonly DialogEntity[][] Dialogs = {
        // Test Dialog
        new[] {
            new DialogEntity {
                IconPath = "DialogIcons/Grievous Dark",
                Text = "General Grievous: General Kenobi..."
            }, 
            new DialogEntity {
                IconPath = "DialogIcons/ObiWan",
                Text = "Obi-Wan Kenobi: Hello there!"
            }
        } 
    };

}
