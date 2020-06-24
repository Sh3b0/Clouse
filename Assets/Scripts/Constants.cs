public static class Constants {
    
    // Scene Names
    public const string SN_MENU = "Menu";
    public const string SN_TEST_LEVEL = "Test";
    public const string SN_LEVEL = "Level";
    
    // Event Names
    public const string EVENT_ENTER_REACHED = "EnterReached";
    public const string EVENT_EXIT_REACHED = "ExitReached";
    public const string EVENT_RESTART = "Restart";
    public const string EVENT_DIALOG = "DialogEntered";

    // Objects Tags
    public const string TAG_PLAYER = "Player";
    
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
