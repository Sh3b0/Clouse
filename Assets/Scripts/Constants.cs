public static class Constants {
    
    // Scene Names
    public const string SN_MENU = "Menu";
    public const string SN_INTRO = "Intro";
    public const string SN_TEST_LEVEL = "Test-Ahmed";
    public const string SN_LEVEL = "Level 1";

    // Objects Tags
    public const string TAG_PLAYER = "Player";
    public const string TAG_CLOUD = "Cloud";
    public const string TAG_DRAIN = "Drain";
    public const string TAG_BOX = "Box";
    public const string TAG_HELD = "Held";
    public const string TAG_GENERATOR = "Generator";
    public const string TAG_MIRROR = "Mirror";
    
    // Layers
    public const int LAYER_DEFAULT = 0;
    public const int LAYER_WATER = 4;
    public const int LAYER_FLOATING = 8;
    public const int LAYER_FILLABLE = 11;
    
    // Animations
    public const string ANIM_FAST_FLAG = "FlagDescentFast";
    public const string ANIM_FADE_IN = "FadeIn";
    public const string ANIM_FADE_OUT = "FadeOut";
    
    // Dialogs
    public struct DialogEntity {
        public string IconPath;
        public string Text;
    }

    public static readonly DialogEntity[][] Dialogs = {
        // Level 1 Dialog
        new[] {
            new DialogEntity {
                IconPath = "DialogIcons/Knight",
                Text = "Knight: I need your help, Clouse. We need to go on a quick adventure."
            }, 
            new DialogEntity {
                IconPath = "DialogIcons/Clouse",
                Text = "Clouse: No way, Rob. I am still stormy after last time."
            },
            new DialogEntity {
                IconPath = "DialogIcons/Knight",
                Text = "Knight: Come on! Just follow me, my old friend. Someone has stirred the pot." + 
                       " We need to figure out who has done it. Princess Berry asked me to do it for her."
            }, 
            new DialogEntity {
                IconPath = "DialogIcons/Clouse",
                Text = "Clouse: Princess Berry? Why didn’t you say so?! We need to hurry."
            },
            new DialogEntity {
                IconPath = "DialogIcons/Knight",
                Text = "Knight: Nice..."
            }
        },
        
        // Level 2-1 Dialog
        new[] {
            new DialogEntity {
                IconPath = "DialogIcons/Knight",
                Text = "Knight: Could you please help me, bro?"
            }, 
            new DialogEntity {
                IconPath = "DialogIcons/Clouse",
                Text = "Clouse: If you tell Princess Berry the truth I will help you."
            },
            new DialogEntity {
                IconPath = "DialogIcons/Knight",
                Text = "Knight: Why is Princess Berry’s opinion so important for you?"
            }, 
            new DialogEntity {
                IconPath = "DialogIcons/Clouse",
                Text = "Clouse: Rob! You have never mentioned me! I saved your back so many times."
            },
            new DialogEntity {
                IconPath = "DialogIcons/Knight",
                Text = "Knight: I am sorry, Clouse. I will tell her when we come back."
            },
            new DialogEntity {
                IconPath = "DialogIcons/Clouse",
                Text = "Clouse: Let’s see what I can do."
            }
        },
        
        // Level 3 Dialog
        new[] {
            new DialogEntity {
                IconPath = "DialogIcons/Knight",
                Text = "Knight: Clouse, I have found a cave with some strange stuff. Like machines and engines."
            }, 
            new DialogEntity {
                IconPath = "DialogIcons/Clouse",
                Text = "Clouse: I thought there is no engineer in Dreamland."
            },
            new DialogEntity {
                IconPath = "DialogIcons/Knight",
                Text = "Knight: So did I. What’s the point of building machines if we have magic?"
            }, 
            new DialogEntity {
                IconPath = "DialogIcons/Clouse",
                Text = "Clouse: Have you found also any notes or something?"
            },
            new DialogEntity {
                IconPath = "DialogIcons/Knight",
                Text = "Knight: I need to take a look."
            }
        },
        
        // Level 5 Dialog
        new[] {
            new DialogEntity {
                IconPath = "DialogIcons/Knight",
                Text = "Knight: Oh, I should watch my step, there is a hole in the floor... It is too small for me however."
            },
            new DialogEntity {
                IconPath = "DialogIcons/Knight",
                Text = "Knight: But it seems to be enough for water to drain in it - I need something heavy to stop water here..."
            }
        },
        
        // Level 6 Begin Dialog
        new[] {
            new DialogEntity {
                IconPath = "DialogIcons/Knight",
                Text = "Knight: Dear lord, what is that? Do we have to fight it?"
            }, 
            new DialogEntity {
                IconPath = "DialogIcons/Clouse",
                Text = "Clouse: I do not know WHAT is this? But it seems it has some dark aura... Please, be careful with it."
            },
            new DialogEntity {
                IconPath = "DialogIcons/Knight",
                Text = "Knight: It also blocking our way out, so I do not see any other choice..."
            }
        },
        
        // Level 6 Ending Dialog
        new[] {
            new DialogEntity {
                IconPath = "DialogIcons/Knight",
                Text = "Knight: Did you see it? Who... or what was it?"
            }, 
            new DialogEntity {
                IconPath = "DialogIcons/Clouse",
                Text = "Clouse: Somebody who is aggressive and doesn’t look pretty."
            },
            new DialogEntity {
                IconPath = "DialogIcons/Knight",
                Text = "Knight: What was he doing here?"
            }, 
            new DialogEntity {
                IconPath = "DialogIcons/Clouse",
                Text = "Clouse: Maybe he wants us to stay here."
            },
            new DialogEntity {
                IconPath = "DialogIcons/Knight",
                Text = "Knight: Or he hides something... Interesting."
            }
        }
    };

    public static readonly string[] IntroBacks = {
        "Intro1",
        "Intro2"
    };
    
    public static readonly string[] IntroText = {
        "Once upon a time a famous and brave knight by the name of (Robert Smart) was on the edges of Dreamland at the insistence of Princess Berry. Robert always defends the kingdom and obeys royal’s orders.",
        "A couple of days before, inhabitants of one village became gloomy and ugly. The fabulous place became the darkest part of all the kingdom in an instant. The journey starts at a fairy forest where the knight tries to persuade his old friend - Clouse - to come with him."
    };

}
