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
    }

    void FixedUpdate()
    {
        if (glory > 4 || glory < -4)
        {
            //the game ends
            if (glory > 4)
            {
                print("You have won");
            }
            else
            {
                print("You have lost");
            }
        }
        else
        {
            if (actionSelected == false)
            {
                print("What would you like to do? | 1 = Attack | 2 = Defend | 3 = Switch Weapon |");
                if (Input.GetKeyDown("1"))
                {
                    Debug.Log("1 key was pressed.");
                    playerAction = 1;
                    actionSelected = true;
                    //if wanting to attack
                    Enemy(playerAction);
                }
                if (Input.GetKeyDown("2"))
                {
                    Debug.Log("2 key was pressed.");
                    playerAction = 2;
                    actionSelected = true;
                    //if wanting to defend
                    Enemy(playerAction);
                }
                if (Input.GetKeyDown("3"))
                {
                    Debug.Log("3 key was pressed.");
                    playerAction = 3;
                    actionSelected = true;
                    //if wanting to swap weapons
                    Inventory();
                }
            }
            actionSelected = false;
        }
    }

    //player making a choice
    /*void playerChoice()
    {
        print("What would you like to do? | 1 = Attack | 2 = Defend | 3 = Switch Weapon |");
        if (Input.GetKeyDown("1"))
        {
            Debug.Log("1 key was pressed.");
            playerAction = 1;
            //if wanting to attack
            Enemy(playerAction);
        }
        if (Input.GetKeyDown("2"))
        {
             playerAction = 2;
             //if wanting to defend
             Enemy(playerAction);
        }
        if (Input.GetKeyDown("3"))
        {
             playerAction = 3;
             //if wanting to swap weapons
             playerWeapon = Inventory();
        }

    } */

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
        print("You decided to open your inventory");
        int x = 0;
        foreach (string a in thePlayer)
        {
            x++;
            print(x + " - " + a);
        }
        /*
        if (Input.GetKeyDown(KeyCode.A))
        {
            //sword
            p = thePlayer.IndexOf("sword");
            print("You have swapped to " + thePlayer[p]);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //hammer
            p = thePlayer.IndexOf("hammer");
            print("You have swapped to " + thePlayer[p]);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            //spear
            p = thePlayer.IndexOf("spear");
            print("You have swapped to " + thePlayer[p]);
        }*/
        if (p > thePlayer.IndexOf("spear"))
        {
            p = thePlayer.IndexOf("sword");
        }

        print("You are currently holding " + thePlayer[p]);
        p = p + 1;

        if (p >= thePlayer.Count)
        {
            p = 0;
        }
        print("You have swapped to " + thePlayer[p]);
        
    }
}
