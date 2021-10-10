using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    float roundTimer = 20;
    bool availableTime = true;
    float seconds;
    //showcasing time left to the player
    public Text timerText;

    //to trigger SetGlory function in UI
    public GlorySlider glorySlider;

    //to display Log in LogText UI
    public Text logText;

    //to display Victory/Defeat Screens
    public GameObject VictoryPanel;
    public GameObject DefeatPanel;

    //move between inventories
    public GameObject ActionPanel;
    public GameObject WeaponPanel;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        
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
    }

    public void DisplayTime()
    {
        timerText.text = string.Format("{0:00}:{1:00}", 0, seconds);
    }
    void EnemyChoice()
    {
        int chance = Random.Range(0,theComputer.Count);
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
    }

    public void Attack()
    {
        EnemyChoice();
        PlayerAttacks();
    }

    public void Defend()
    {
        EnemyChoice();
        PlayerDefends();
    }

    //for swapping weapons
    public void Return()
    {
        ActionPanel.SetActive(true);
        WeaponPanel.SetActive(false);
    }

    public void Sword()
    {
        if (thePlayer.Contains("sword") == true)
        {
            logText.text = ("You switched weapon to a Sword. ");
            p = thePlayer.IndexOf("sword");
            Return();
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
            p = thePlayer.IndexOf("hammer");
            Return();
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
            p = thePlayer.IndexOf("spear");
            Return();
        }
        else
        {
            logText.text = ("Your spear is broken. You cannot use it");
        }
    }

    public void PlayerAttacks()
    {
        int enemyAction = Random.Range(0, 2);
        if (enemyAction == 0)
        {
            //both attack
            logText.text = ("Both sides chose to attack. ");
            if (thePlayer[p] == theComputer[c])
            {
                //same weapon
                logText.text += ("Both attacked with " + thePlayer[p] + "s, resulting in a tie.");
            }
            if ((thePlayer[p] == "sword" && theComputer[c] == "spear") || (thePlayer[p] == "spear" && theComputer[c] == "hammer") || (thePlayer[p] == "hammer" && theComputer[c] == "sword"))
            {
                //player wins
                glory = glory + 2;
                glorySlider.SetGlory(glory);
                logText.text += ("You used a " + thePlayer[p] + ", while your opponent used a " + theComputer[c] + ". You won that round.");
            }
            if ((theComputer[c] == "sword" && thePlayer[p] == "spear") || (theComputer[c] == "spear" && thePlayer[p] == "hammer") || (theComputer[c] == "hammer" && thePlayer[p] == "sword"))
            {
                //player loses
                glory = glory - 2;
                glorySlider.SetGlory(glory);
                logText.text += ("You used a " + thePlayer[p] + ", while your opponent used a " + theComputer[c] + ". You lost that round.");
            }
        }
        else
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
            }
            if ((thePlayer[p] == "sword" && theComputer[c] == "spear") || (thePlayer[p] == "spear" && theComputer[c] == "hammer") || (thePlayer[p] == "hammer" && theComputer[c] == "sword"))
            {
                //player wins
                glory = glory + 1;
                glorySlider.SetGlory(glory);
                //breaks opponent weapon
                logText.text += ("You used a " + thePlayer[p] + ", breaking your opponent's " + theComputer[c] + ". You won that round");
                theComputer.RemoveAt(c);                
            }
            if ((theComputer[c] == "sword" && thePlayer[p] == "spear") || (theComputer[c] == "spear" && thePlayer[p] == "hammer") || (theComputer[c] == "hammer" && thePlayer[p] == "sword"))
            {
                //player loses
                glory = glory - 1;
                glorySlider.SetGlory(glory);
                //breaks own weapon
                logText.text += ("You used a " + thePlayer[p] + ", against your opponent's " + theComputer[c] + ". You broke your " + thePlayer[p] + " and lost.");
                thePlayer.RemoveAt(p);                
            }
        }
        roundTimer = 10;
    }
    public void PlayerDefends()
    {
        int enemyAction = Random.Range(0, 2);
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
            }

            if ((thePlayer[p] == "sword" && theComputer[c] == "spear") || (thePlayer[p] == "spear" && theComputer[c] == "hammer") || (thePlayer[p] == "hammer" && theComputer[c] == "sword"))
            {
                //player wins
                glory = glory + 1;
                glorySlider.SetGlory(glory);
                //breaks opponent weapon
                logText.text += ("You used a " + thePlayer[p] + ", and broke your opponent's " + theComputer[c] + ". You won that round");
                theComputer.RemoveAt(c);                
            }

            if ((theComputer[c] == "sword" && thePlayer[p] == "spear") || (theComputer[c] == "spear" && thePlayer[p] == "hammer") || (theComputer[c] == "hammer" && thePlayer[p] == "sword"))
            {
                //player loses
                glory = glory - 2;
                glorySlider.SetGlory(glory);
                logText.text += ("You used a " + thePlayer[p] + ", but your opponent's " + theComputer[c] + " defends and breaks your " + thePlayer[p] + ".");
                thePlayer.RemoveAt(p);
            }
        }
        else
        {
            //nothing happens
            logText.text = ("You both tried to defend. It was quite funny.");
        }
        roundTimer = 10;
    }
}
