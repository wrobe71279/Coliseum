using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //variables for round timings
    float roundTimer = 60;
    bool availableTime = true;
    float seconds;

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
                //checking if the win condition has been met
                if (glory > 4 || glory < -4 || seconds <= 0 || thePlayer.Count <= 0 || theComputer.Count <= 0)
                {
                    //the game ends
                    if (glory > 4 || theComputer.Count <= 0)
                    {
                        //possibly a cutscene
                        //change to victory scene
                        Debug.Log("Victory");
                    }
                    else
                    {
                        availableTime = false;
                        //possibly a cutscene
                        //change to main menu but no menu at this stage
                        Debug.Log("Defeat");
                        Application.Quit();
                    }
                }
            }
            else
            {
                Debug.Log("Defeat");
                Application.Quit();
                //losing animation due to time
            }
        }
    }
    void EnemyChoice()
    {
        int chance = Random.Range(0, 2);
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
        p = thePlayer.IndexOf("sword");
    }

    public void Hammer()
    {
        p = thePlayer.IndexOf("hammer");
    }

    public void Spear()
    {
        p = thePlayer.IndexOf("spear");
    }

    public void PlayerAttacks()
    {
        int enemyAction = Random.Range(0, 1);
        if (enemyAction == 0)
        {
            //both attack
            print("Both sides chose to attack");
            if (thePlayer[p] == theComputer[c])
            {
                //same weapon
                print("Both sides tried to attack with " + thePlayer[p] + "s, resulting in a clash. Nothing was won");
            }
            if (thePlayer[p] == "sword" && theComputer[c] == "spear" || thePlayer[p] == "spear" && theComputer[c] == "hammer" || thePlayer[p] == "hammer" && theComputer[c] == "sword")
            {
                //player wins
                glory = glory + 2;
                print("You used a " + thePlayer[p] + ", while your opponent used a " + theComputer[c] + ". You won that round.");
            }
            if (theComputer[c] == "sword" && thePlayer[p] == "spear" || theComputer[c] == "spear" && thePlayer[p] == "hammer" || theComputer[c] == "hammer" && thePlayer[p] == "sword")
            {
                //player loses
                glory = glory - 2;
                print("You used a " + thePlayer[p] + ", while your opponent used a " + theComputer[c] + ". You lost that round.");
            }
        }
        else
        {
            //enemy defends
            //player attacks
            print("You attacked while your opponent defended");

            if (thePlayer[p] == theComputer[c])
            {
                //same weapon
                glory = glory - 1;
                print("Both sides used " + thePlayer[p] + "s, resulting in your opponent defended from your attack. You Lost");
            }
            if (thePlayer[p] == "sword" && theComputer[c] == "spear" || thePlayer[p] == "spear" && theComputer[c] == "hammer" || thePlayer[p] == "hammer" && theComputer[c] == "sword")
            {
                //player wins
                glory = glory + 1;
                //breaks opponent weapon
                theComputer.RemoveAt(c);
                print("You used a " + thePlayer[p] + ", while your opponent used a " + theComputer[c] + " which you managed to break. You won that round");
            }
            if (theComputer[c] == "sword" && thePlayer[p] == "spear" || theComputer[c] == "spear" && thePlayer[p] == "hammer" || theComputer[c] == "hammer" && thePlayer[p] == "sword")
            {
                //player loses
                glory = glory - 1;
                //breaks own weapon
                thePlayer.RemoveAt(p);
                print("You used a " + thePlayer[p] + ", while your opponent used a " + theComputer[c] + ". Although your weapon broke, you managed to lose");
            }
        }
    }
    public void PlayerDefends()
    {
        int enemyAction = Random.Range(0, 1);
        if (enemyAction == 0)
        {
            //player defends
            print("You defended from the opponent's attack");
            if (thePlayer[p] == theComputer[c])
            {
                //same weapon
                glory = glory - 1;
                print("Both sides used " + thePlayer[p] + "s, resulting in a clash. Nothing was won");
            }

            if (thePlayer[p] == "sword" && theComputer[c] == "spear" || thePlayer[p] == "spear" && theComputer[c] == "hammer" || thePlayer[p] == "hammer" && theComputer[c] == "sword")
            {
                //player wins
                glory = glory + 1;
                //breaks opponent weapon
                theComputer.RemoveAt(c);
                print("You used a " + thePlayer[p] + ", while your opponent used a " + theComputer[c] + " which you managed to break. You won that round");
            }

            if (theComputer[c] == "sword" && thePlayer[p] == "spear" || theComputer[c] == "spear" && thePlayer[p] == "hammer" || theComputer[c] == "hammer" && thePlayer[p] == "sword")
            {
                //player loses
                glory = glory - 2;
                print("You used a " + thePlayer[p] + ", while your opponent used a " + theComputer[c] + ". You lost that round.");
            }
        }
        else
        {
            //nothing happens
        }

    }
}
