using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBasedVersion : MonoBehaviour
{
    //variables
    bool actionSelected = false;
    int playerAction;

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
        //welcoming the player to the game
        print("Welcome to Coliseum");

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

    private void Update()
    {
        if (availableTime == true)
        {
            if (roundTimer > 0)
            {
                roundTimer -= Time.deltaTime;
                seconds = Mathf.FloorToInt(roundTimer % 60);
            }
        }
        
    }

    void FixedUpdate()
    {
        //checking if the win condition has been met
        if (glory > 4 || glory < -4 || seconds <= 0)
        {
            //the game ends
            if (glory > 4)
            {
                print("You have won");
                //possibly a cutscene

                //change to victory scene
            }
            else
            {
                print("You have lost");
                availableTime = false;
                //possibly a cutscene
                //change to main menu but no menu at this stage
                Application.Quit();
            }
        }
        else
        {
            if (actionSelected == false)
            {
                roundTimer = 60;
                print("What would you like to do? | 1 = Attack | 2 = Defend | 3 = Switch Weapon |");
                if (Input.GetKeyDown("1"))
                {
                    playerAction = 1;
                    actionSelected = true;
                    //if wanting to attack
                    Enemy(playerAction);
                }
                if (Input.GetKeyDown("2"))
                {
                    playerAction = 2;
                    actionSelected = true;
                    //if wanting to defend
                    Enemy(playerAction);
                }
                if (Input.GetKeyDown("3"))
                {
                    playerAction = 3;
                    actionSelected = true;
                    //if wanting to swap weapons
                    Inventory();
                }
            }
            actionSelected = false;
        }
    }

    //used for anything enemy related
    void Enemy(int player)
    {
        EnemyChoice();
        if (player == 1)
        {
            print("You chose to attack");
            WeaponTriangle(player);

        }
        if (player == 2)
        {
            print("You decided to try and defend");
            WeaponTriangle(player);
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

    void WeaponTriangle(int action)
    {
        //if action = 1 it is attack if 2 then defend
        int enemyAction = Random.Range(0, 1);
        if (enemyAction == 0)
        {
            //enemy attacks
            if (action == 1)
            {
                //player attacks
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
        }
        else
        {
            //enemy defends
            if (action == 1)
            {
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
            else
            {
                //player defends
                //due to both sides defending nothing happens.
                print("You both defended resulting in nothing happening");
            }
        }
    }

    //used for the inventory
    void Inventory()
    {
        //telling the user that they have opened their inventory
        print("You decided to open your inventory");
        //used to help people keep track of how many weapons they have
        int x = 0;
        //goes through each string in the list and does the loop for each one
        foreach (string a in thePlayer)
        {
            x++;
            print(x + " - " + a);
        }
        //trying to keep the player's weapon viable
        if (p >= thePlayer.Count)
        {
            p = 0;
        }
        //telling the player what they are currently holding
        print("You are currently holding " + thePlayer[p]);
        p = p + 1;
        //similar to the if before it is used to maintain the weapon in the list
        if (p >= thePlayer.Count)
        {
            p = 0;
        }
        //telling the player what their new weapon is
        print("You have swapped to " + thePlayer[p]);
        
    }

    //methods for the button presses
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
