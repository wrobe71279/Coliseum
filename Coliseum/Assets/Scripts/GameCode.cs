using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class GameCode : MonoBehaviour
{
    //inventories for both sides
    List<string> thePlayer = new List<string>();
    List<string> theComputer = new List<string>();

    //to maintain what the held weapon is
    int p = 0;
    int c = 0;

    //if glory reaches 5 player wins if reaches -5 ai wins
    int glory = 0;

    /* TIME CONSTRAINT VARIABLES
     * these round time variables and code was found at https://gamedevbeginner.com/how-to-make-countdown-timer-in-unity-minutes-seconds/#convert_to_text
     * this was a very helpful website for learning how to get a float to showcase seconds and then set it up in unity for us in Coliseum we used
     * this to help with our time constraint
    */
    float roundTimer = 10;
    bool availableTime = true;
    float seconds;
    //showcasing time left to the player
    public Text timerText;

    //to trigger SetGlory function in UI
    public GlorySlider glorySlider;

    //to trigger SetGlory function in UI
    public WeaponSwitch weaponSwitch;

    //to display Log in LogText UI
    public Text logText;

    //to display Victory/Defeat Screens
    public GameObject VictoryPanel;
    public GameObject DefeatPanel;

    //move between inventories
    public GameObject ActionPanel;
    public GameObject WeaponPanel;
    public GameObject LogPanel;
    public GameObject TimerPanel;
    public GameObject PredictionPanel;

    //ui for enemy telegraphy
    public GameObject PredictAttack;
    public GameObject PredictDefend;
    public GameObject PredictSwitch;

    //ui for displaying equipped weapons
    public GameObject EquipSwordPanel;
    public GameObject EquipHammerPanel;
    public GameObject EquipSpearPanel;
    public GameObject EnemyEquipSwordPanel;
    public GameObject EnemyEquipHammerPanel;
    public GameObject EnemyEquipSpearPanel;

    //weapon buttons
    public Button swordButton;
    public Button hammerButton;
    public Button spearButton;
    public Sprite brokenSword;
    public Sprite brokenHammer;
    public Sprite brokenSpear;
    public Image swordImageComponent;
    public Image hammerImageComponent;
    public Image spearImageComponent;

    //getting the AI to go first
    int enemyAction;
    int chance;
    int chosen;
    //keeping track of what the ai has done
    public int attack;
    public int defend;
    public int newWeapon;

    //play specific timeline track
    public PlayableDirector cutscene1;
    public PlayableDirector cutscene2;
    public PlayableDirector cutscene3;
    public PlayableDirector cutscene4;
    public PlayableDirector cutscene5;
    public PlayableDirector cutscene6;
    public PlayableDirector cutscene7;
    public PlayableDirector cutscene8;
    public PlayableDirector cutscene9;
    public PlayableDirector cutscene10;
    public PlayableDirector idle;

    //for fuzzy logic
    int turn;

    //stop the player from spam clicking
    public int readyNow;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        idle.Play();

        //for the user
        thePlayer.Add("sword");
        thePlayer.Add("hammer");
        thePlayer.Add("spear");

        //for the ai
        theComputer.Add("sword");
        theComputer.Add("hammer");
        theComputer.Add("spear");

        //making roundTimer to seconds
        seconds = Mathf.FloorToInt(roundTimer % 60);
    }
    // Update is called once per frame
    void Update()
    {
        if (availableTime == true)
        {
            if (roundTimer > 0)
            {
                roundTimer -= Time.deltaTime;
                seconds = Mathf.FloorToInt(roundTimer % 60);
                DisplayTime();
                //checking if the win condition has been met
                if (glory > 4 || glory < -4 || seconds <= 0 || thePlayer.Count <= 0 || theComputer.Count <= 0)
                {
                    //the game ends
                    if (glory > 4 || theComputer.Count <= 0)
                    {
                        //possibly a cutscene
                        //change to victory scene
                        VictoryPanel.SetActive(true);
                        Time.timeScale = 0;
                    }
                    else
                    {
                        availableTime = false;
                        //possibly a cutscene
                        //change to main menu but no menu at this stage
                        DefeatPanel.SetActive(true);
                        Time.timeScale = 0;
                    }
                }

            }
            else
            {
                DefeatPanel.SetActive(true);
                Time.timeScale = 0;
                //losing animation due to time
            }
        }
        if (turn == 0)
        {
            if (seconds >= 9)
            {
                EnemyChoice();
            }
        }
        else
        {
            if (seconds == 5)
            {
                EnemyChoice();
            }
        }
    }
    //to display the time in seconds for the user to see
    public void DisplayTime()
    {
        timerText.text = string.Format("{0:00}:{1:00}", 0, seconds);
    }
    
    //in order to get the enemy to make a choice uses random range
    public void EnemyChoice()
    {
        if (chosen == 0)
        {
            //getting a random descision
            enemyAction = Random.Range(0, 5);
            chance = Random.Range(0, theComputer.Count);
            if (enemyAction == 0 || enemyAction == 3)
            {
                logText.text = ("The enemy seems to be moving towards you. ");
                PredictAttack.SetActive(true);
                PredictDefend.SetActive(false);
                PredictSwitch.SetActive(false);
                enemyAction = 0;
                attack++;
            }
            if (enemyAction == 1 || enemyAction == 4)
            {
                logText.text = ("The enemy is moving away from you. ");
                PredictAttack.SetActive(false);
                PredictDefend.SetActive(true);
                PredictSwitch.SetActive(false);
                enemyAction = 1;
                defend++;
            }
            
            if (enemyAction == 2)
            {
                logText.text = ("It seems that your enemy is changing weapons. ");
                PredictAttack.SetActive(false);
                PredictDefend.SetActive(false);
                PredictSwitch.SetActive(true);
                if (chance == 0)
                {
                    c = 0;
                }
                if (chance == 1)
                {
                    c = 1;
                }
                if (chance == 2)
                {
                    c = 2;
                }
                newWeapon++;
            }
        }
        chosen = 1;
    }

    //the code for respective buttons
    public void Attack()
    {
        //EnemyChoice();
        PlayerAttacks();
        DisplayEnemyWeapon();
        DisplayWeapon();
        turn++;
    }
    public void Defend()
    {
        //EnemyChoice();
        PlayerDefends();
        DisplayEnemyWeapon();
        DisplayWeapon();
        turn++;
    }
    //for swapping weapons
    public void Return()
    {
        ActionPanel.SetActive(true);
        WeaponPanel.SetActive(false);
    }
    //the weapon button code
    public void Sword()
    {
        if (thePlayer.Contains("sword") == true)
        {
            logText.text = ("You switched weapon to a Sword. ");
            weaponSwitch.SelectSword();
            p = thePlayer.IndexOf("sword");
            Return();
            PlayerSwitches();
            DisplayEnemyWeapon();
            DisplayWeapon();
        }
        else
        {
            logText.text = ("Your sword is broken. You cannot use it");            
        }

        
    }
    public void Hammer()
    {
        if (thePlayer.Contains("hammer") == true)
        {
            logText.text = ("You switched weapon to a Hammer. ");
            weaponSwitch.SelectHammer();
            p = thePlayer.IndexOf("hammer");
            Return();
            PlayerSwitches();
            DisplayEnemyWeapon();
            DisplayWeapon();
        }
        else
        {
            logText.text = ("Your hammer is broken. You cannot use it");
        }
    }
    public void Spear()
    {
        if (thePlayer.Contains("spear") == true)
        {
            logText.text = ("You switched weapon to a Spear. ");
            weaponSwitch.SelectSpear();
            p = thePlayer.IndexOf("spear");
            Return();
            PlayerSwitches();
            DisplayEnemyWeapon();
            DisplayWeapon();
        }
        else
        {
            logText.text = ("Your spear is broken. You cannot use it");
        }
    }
    //for when a button is pressed
    public void PlayerAttacks()
    {
        //int enemyAction = Random.Range(0, 2);
        if (enemyAction == 0)
        {
            //both attack
            logText.text = ("Both sides chose to attack. ");
            if (thePlayer[p] == theComputer[c])
            {
                //same weapon
                logText.text += ("Both attacked with " + thePlayer[p] + "s, resulting in a tie.");
                cutscene1.Play();
                StartCoroutine(Cutscene());
                /*
                CutscenePlays();
                PostCutscene();
                */
            }
            if ((thePlayer[p] == "sword" && theComputer[c] == "spear") || (thePlayer[p] == "spear" && theComputer[c] == "hammer") || (thePlayer[p] == "hammer" && theComputer[c] == "sword"))
            {
                //player wins
                glory = glory + 2;
                glorySlider.SetGlory(glory);
                logText.text += ("You used a " + thePlayer[p] + ", while your opponent used a " + theComputer[c] + ". You won that round.");
                cutscene2.Play();
                StartCoroutine(Cutscene());
                /*
                CutscenePlays();
                PostCutscene();
                */
            }
            if ((theComputer[c] == "sword" && thePlayer[p] == "spear") || (theComputer[c] == "spear" && thePlayer[p] == "hammer") || (theComputer[c] == "hammer" && thePlayer[p] == "sword"))
            {
                //player loses
                glory = glory - 2;
                glorySlider.SetGlory(glory);
                logText.text += ("You used a " + thePlayer[p] + ", while your opponent used a " + theComputer[c] + ". You lost that round.");
                cutscene3.Play();
                StartCoroutine(Cutscene());
                /*
                CutscenePlays();
                PostCutscene();
                */
            }
        }
        if (enemyAction == 1)
        {
            //enemy defends
            //player attacks
            logText.text = ("You attack your opponent guard. ");

            if (thePlayer[p] == theComputer[c])
            {
                //same weapon
                glory = glory - 1;
                glorySlider.SetGlory(glory);
                logText.text += ("Both sides used " + thePlayer[p] + "s, but the opponent deflected your attack. You Lost");
                cutscene4.Play();
                StartCoroutine(Cutscene());
                /*
                CutscenePlays();
                PostCutscene();
                */
            }
            if ((thePlayer[p] == "sword" && theComputer[c] == "spear") || (thePlayer[p] == "spear" && theComputer[c] == "hammer") || (thePlayer[p] == "hammer" && theComputer[c] == "sword"))
            {
                //player wins
                glory = glory + 1;
                glorySlider.SetGlory(glory);
                //breaks opponent weapon
                logText.text += ("You used a " + thePlayer[p] + ", breaking your opponent's " + theComputer[c] + ". You won that round");
                cutscene5.Play();
                StartCoroutine(Cutscene());
                /*
                CutscenePlays();
                PostCutscene();
                */
                theComputer.RemoveAt(c);                
            }
            if ((theComputer[c] == "sword" && thePlayer[p] == "spear") || (theComputer[c] == "spear" && thePlayer[p] == "hammer") || (theComputer[c] == "hammer" && thePlayer[p] == "sword"))
            {
                //player loses
                glory = glory - 1;
                glorySlider.SetGlory(glory);
                //breaks own weapon
                logText.text += ("You used a " + thePlayer[p] + ", against your opponent's " + theComputer[c] + ". You broke your " + thePlayer[p] + " and lost.");
                cutscene6.Play();
                StartCoroutine(LongCutscene());
                /*
                CutscenePlays();
                PostCutscene();
                */
                thePlayer.RemoveAt(p);
                WeaponBreak();
            }
        }
        if (enemyAction == 2)
        {
            //the player wins
            glory = glory + 2;
            glorySlider.SetGlory(glory);
            logText.text += ("You used a " + thePlayer[p] + ", while your opponent swapped weapons. You won that round.");
            cutscene5.Play();
            StartCoroutine(Cutscene());
            /*
            CutscenePlays();
            PostCutscene();
            */
        }
        roundTimer = 8;
        chosen = 0;
    }
    public void PlayerDefends()
    {
        //int enemyAction = Random.Range(0, 2);
        if (enemyAction == 0)
        {
            //player defends
            logText.text = ("You try to defend. ");
            if (thePlayer[p] == theComputer[c])
            {
                //same weapon
                glory = glory + 1;
                glorySlider.SetGlory(glory);
                logText.text += ("Both sides used " + thePlayer[p] + "s, but deflected your opponent's attack. You Won");
                cutscene7.Play();
                StartCoroutine(Cutscene());
                /*
                CutscenePlays();
                PostCutscene();
                */
            }

            if ((thePlayer[p] == "sword" && theComputer[c] == "spear") || (thePlayer[p] == "spear" && theComputer[c] == "hammer") || (thePlayer[p] == "hammer" && theComputer[c] == "sword"))
            {
                //player wins
                glory = glory + 1;
                glorySlider.SetGlory(glory);
                //breaks opponent weapon
                logText.text += ("You used a " + thePlayer[p] + ", and broke your opponent's " + theComputer[c] + ". You won that round");
                cutscene8.Play();
                StartCoroutine(LongCutscene());
                /*
                CutscenePlays();
                PostCutscene();
                */
                theComputer.RemoveAt(c);                
            }

            if ((theComputer[c] == "sword" && thePlayer[p] == "spear") || (theComputer[c] == "spear" && thePlayer[p] == "hammer") || (theComputer[c] == "hammer" && thePlayer[p] == "sword"))
            {
                //player loses
                glory = glory - 2;
                glorySlider.SetGlory(glory);
                logText.text += ("You used a " + thePlayer[p] + ", but your opponent's " + theComputer[c] + " defends and breaks your " + thePlayer[p] + ".");
                cutscene9.Play();
                StartCoroutine(Cutscene());
                /*
                CutscenePlays();
                PostCutscene();
                */
                thePlayer.RemoveAt(p);
                WeaponBreak();
            }
        }
        if (enemyAction == 1)
        {
            //nothing happens
            logText.text = ("You both tried to defend. It was quite funny.");
            cutscene10.Play();
            StartCoroutine(Cutscene());
            /*
            CutscenePlays();
            PostCutscene();
            */
        }
        if (enemyAction == 2)
        {
            //nothing happens but opponent gets free switch
            logText.text = ("You tried to defend. Your opponent got a free weapon switch.");
            cutscene10.Play();
            StartCoroutine(Cutscene());
            /*
            CutscenePlays();
            PostCutscene();
            */
        }
        roundTimer = 8;
        chosen = 0;
    }
    public void PlayerSwitches()
    {
        if (enemyAction == 0)
        {
            //player defends
            logText.text = ("You Swapped weapons. ");
            //player loses
            glory = glory - 2;
            glorySlider.SetGlory(glory);
            logText.text += ("You swapped weapons, but your opponent attacked using " + theComputer[c] + ".");
            cutscene9.Play();
            StartCoroutine(Cutscene());
            /*
            CutscenePlays();
            PostCutscene();
            */
        }
        if (enemyAction == 1)
        {
            //nothing happens
            logText.text = ("You got a free weapon swap because your opponent defended.");
            cutscene10.Play();
            StartCoroutine(Cutscene());
            /*
            CutscenePlays();
            PostCutscene();
            */
        }
        if (enemyAction == 2)
        {
            //nothing happens but opponent gets free switch
            logText.text = ("You both swapped weapons and got a free weapon switch.");
            cutscene10.Play();
            StartCoroutine(Cutscene());
            /*
            CutscenePlays();
            PostCutscene();
            */
        }
        roundTimer = 8;
        chosen = 0;
    }
    
    //what happens when the user has a weapon broken
    void WeaponBreak()
    {
        logText.text += ("Choose another weapon.");
        weaponSwitch.HideWeapon();
        //ActionPanel.SetActive(false);
        //WeaponPanel.SetActive(true);

        if (thePlayer.Contains("sword") == false)
        {
            swordButton.interactable = false;
            swordImageComponent.sprite = brokenSword;
        }

        if (thePlayer.Contains("hammer") == false)
        {
            hammerButton.interactable = false;
            hammerImageComponent.sprite = brokenHammer;
        }

        if (thePlayer.Contains("spear") == false)
        {
            spearButton.interactable = false;
            spearImageComponent.sprite = brokenSpear;
        }

    }

    //to display weapon of enemy
    void DisplayEnemyWeapon()
    {
        if(c == theComputer.IndexOf("sword"))
        {
            weaponSwitch.EnemySword();
            EnemyEquipSwordPanel.SetActive(true);
            EnemyEquipHammerPanel.SetActive(false);
            EnemyEquipSpearPanel.SetActive(false);
        }
        else if(c == theComputer.IndexOf("hammer"))
        {
            weaponSwitch.EnemyHammer();
            EnemyEquipSwordPanel.SetActive(false);
            EnemyEquipHammerPanel.SetActive(true);
            EnemyEquipSpearPanel.SetActive(false);
        }
        else if(c == theComputer.IndexOf("spear"))
        {
            weaponSwitch.EnemySpear();
            EnemyEquipSwordPanel.SetActive(false);
            EnemyEquipHammerPanel.SetActive(false);
            EnemyEquipSpearPanel.SetActive(true);
        }
    }

    //display weapon of player
    void DisplayWeapon()
    {
        if (p == thePlayer.IndexOf("sword"))
        {
            EquipSwordPanel.SetActive(true);
            EquipHammerPanel.SetActive(false);
            EquipSpearPanel.SetActive(false);
        }
        else if (p == thePlayer.IndexOf("hammer"))
        {
            EquipSwordPanel.SetActive(false);
            EquipHammerPanel.SetActive(true);
            EquipSpearPanel.SetActive(false);
        }
        else if (p == thePlayer.IndexOf("spear"))
        {
            EquipSwordPanel.SetActive(false);
            EquipHammerPanel.SetActive(false);
            EquipSpearPanel.SetActive(true);
        }
    }

    //methods for the cutscenes to look like cutscenes
    void CutscenePlays()
    {
        ActionPanel.SetActive(false);
        WeaponPanel.SetActive(false);
        //LogPanel.SetActive(false);
        TimerPanel.SetActive(false);
        PredictionPanel.SetActive(false);
    }

    void PostCutscene()
    {
        ActionPanel.SetActive(true);
        WeaponPanel.SetActive(false);
        //LogPanel.SetActive(true);
        TimerPanel.SetActive(true);
        PredictionPanel.SetActive(true);
        idle.Play();
    }

    IEnumerator Cutscene()
    {
        CutscenePlays();

        yield return new WaitForSecondsRealtime(3);

        PostCutscene();
    }

    IEnumerator LongCutscene()
    {
        CutscenePlays();

        yield return new WaitForSecondsRealtime(4);

        PostCutscene();
    }
}
