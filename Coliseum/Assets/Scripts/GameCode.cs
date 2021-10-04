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

    // Start is called before the first frame update
    void Start()
    {
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
                    }
                    else
                    {
                        availableTime = false;
                        //possibly a cutscene
                        //change to main menu but no menu at this stage
                        DefeatPanel.SetActive(true);
                    }
                }
            }
            else
            {
                DefeatPanel.SetActive(true);
                Application.Quit();
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
        int chance = Random.Range(0, (theComputer.Count + 1));
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
    public void Sword()
    {
        logText.text = ("You switched weapon to a Sword. ");
        p = thePlayer.IndexOf("sword");
    }

    public void Hammer()
    {
        logText.text = ("You switched weapon to a Hammer. ");
        p = thePlayer.IndexOf("hammer");
    }

    public void Spear()
    {
        logText.text = ("You switched weapon to a Spear. ");
        p = thePlayer.IndexOf("spear");
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
                logText.text += ("Both sides tried to attack with " + thePlayer[p] + "s, resulting in a clash. Nothing was won");
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
            logText.text = ("You attacked while your opponent defended. ");

            if (thePlayer[p] == theComputer[c])
            {
                //same weapon
                glory = glory - 1;
                glorySlider.SetGlory(glory);
                logText.text += ("Both sides used " + thePlayer[p] + "s, resulting in your opponent defended your attack. You Lost");
            }
            if ((thePlayer[p] == "sword" && theComputer[c] == "spear") || (thePlayer[p] == "spear" && theComputer[c] == "hammer") || (thePlayer[p] == "hammer" && theComputer[c] == "sword"))
            {
                //player wins
                glory = glory + 1;
                glorySlider.SetGlory(glory);
                //breaks opponent weapon
                theComputer.RemoveAt(c);
                logText.text += ("You used a " + thePlayer[p] + ", while your opponent used a " + theComputer[c] + " which you managed to break. You won that round");
            }
            if ((theComputer[c] == "sword" && thePlayer[p] == "spear") || (theComputer[c] == "spear" && thePlayer[p] == "hammer") || (theComputer[c] == "hammer" && thePlayer[p] == "sword"))
            {
                //player loses
                glory = glory - 1;
                glorySlider.SetGlory(glory);
                //breaks own weapon
                thePlayer.RemoveAt(p);
                logText.text += ("You used a " + thePlayer[p] + ", while your opponent used a " + theComputer[c] + ". Although your weapon broke, you managed to lose");
            }
        }
        roundTimer = 20;
    }
    public void PlayerDefends()
    {
        int enemyAction = Random.Range(0, 2);
        if (enemyAction == 0)
        {
            //player defends
            logText.text = ("You defended from the opponent's attack. ");
            if (thePlayer[p] == theComputer[c])
            {
                //same weapon
                glorySlider.SetGlory(glory);
                logText.text += ("Both sides used " + thePlayer[p] + "s, resulting in a clash. Nothing was won");
            }

            if (thePlayer[p] == "sword" && theComputer[c] == "spear" || thePlayer[p] == "spear" && theComputer[c] == "hammer" || thePlayer[p] == "hammer" && theComputer[c] == "sword")
            {
                //player wins
                glory = glory + 1;
                glorySlider.SetGlory(glory);
                //breaks opponent weapon
                theComputer.RemoveAt(c);
                logText.text += ("You used a " + thePlayer[p] + ", while your opponent used a " + theComputer[c] + " which you managed to break. You won that round");
            }

            if (theComputer[c] == "sword" && thePlayer[p] == "spear" || theComputer[c] == "spear" && thePlayer[p] == "hammer" || theComputer[c] == "hammer" && thePlayer[p] == "sword")
            {
                //player loses
                glory = glory - 2;
                glorySlider.SetGlory(glory);
                logText.text += ("You used a " + thePlayer[p] + ", while your opponent used a " + theComputer[c] + ". You lost that round.");
            }
        }
        else
        {
            //nothing happens
            logText.text += ("You both tried to defend. It was quite funny.");
        }
        roundTimer = 20;
    }
}
