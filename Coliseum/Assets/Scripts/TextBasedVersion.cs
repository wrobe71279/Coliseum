using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBasedVersion : MonoBehaviour
{
    //variables
    bool actionSelected = false;
    string playerWeapon = "sword";
    string enemyWeapon;
    int playerAction;

    //if glory reaches 5 player wins if reaches -5 ai wins
    int glory = 0;

    // Start is called before the first frame update
    void Start()
    {
        //welcoming the player to the game
        print("Welcome to Coliseum");
    }

    void Update()
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
                    playerWeapon = Inventory();
                }
            }
            actionSelected = false;
            //nothing happens
            /* if (Input.GetKeyDown(KeyCode.Space))
            {
                playerChoice();
            }*/
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
        string enemyWeapon = EnemyChoice();
        if (player == 1)
        {
            print("You chose to attack");
            WeaponTriangle(player, playerWeapon, enemyWeapon);

        }
        if (player == 2)
        {
            print("You decided to try and defend");
            WeaponTriangle(player, playerWeapon, enemyWeapon);
        }
    }

    public string EnemyChoice()
    {
        int chance = Random.Range(0, 2);
        if (chance == 0)
        {
            enemyWeapon = "sword";
        }
        if (chance == 1)
        {
            enemyWeapon = "hammer";
        }
        if (chance == 2)
        {
            enemyWeapon = "spear";
        }
        return enemyWeapon;
    }

    void WeaponTriangle(int action, string player, string enemy)
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
                if (player == enemy)
                {
                    //same weapon
                    print("Both sides tried to attack with " + player + "s, resulting in a clash");
                }

                if (player == "sword" && enemy == "spear" || player == "spear" && enemy == "hammer" || player == "hammer" && enemy == "sword")
                {
                    //player wins
                    glory = glory + 2;
                    print("You used a " + player + ", while your opponent used a " + enemy);
                }

                if (enemy == "sword" && player == "spear" || enemy == "spear" && player == "hammer" || enemy == "hammer" && player == "sword")
                {
                    //player loses
                    glory = glory - 2;
                    print("You used a " + player + ", while your opponent used a " + enemy);
                }
            }
            else
            {
                //player defends
                print("You defended from the opponent's attack");
                if (player == enemy)
                {
                    //same weapon
                    glory = glory - 1;
                    print("Both sides used " + player + "s, resulting in a clash");
                }

                if (player == "sword" && enemy == "spear" || player == "spear" && enemy == "hammer" || player == "hammer" && enemy == "sword")
                {
                    //player wins
                    glory = glory + 1;
                    //breaks opponent weapon
                    print("You used a " + player + ", while your opponent used a " + enemy);
                }

                if (enemy == "sword" && player == "spear" || enemy == "spear" && player == "hammer" || enemy == "hammer" && player == "sword")
                {
                    //player loses
                    glory = glory - 2;
                    print("You used a " + player + ", while your opponent used a " + enemy);
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
                if (player == enemy)
                {
                    //same weapon
                    glory = glory - 1;
                    print("Both sides used " + player + "s, resulting in a clash");
                }

                if (player == "sword" && enemy == "spear" || player == "spear" && enemy == "hammer" || player == "hammer" && enemy == "sword")
                {
                    //player wins
                    glory = glory + 1;
                    //breaks opponent weapon
                    print("You used a " + player + ", while your opponent used a " + enemy);
                }

                if (enemy == "sword" && player == "spear" || enemy == "spear" && player == "hammer" || enemy == "hammer" && player == "sword")
                {
                    //player loses
                    glory = glory - 1;
                    //breaks own weapon
                    print("You used a " + player + ", while your opponent used a " + enemy);
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
    public string Inventory()
    {
        string weapon = "";
        print("You decided to open your inventory");
        print("| 4 = sword | 5 = hammer | 6 = spear |");
        if (Input.GetKeyDown("4"))
        {
            weapon = "sword";
            print("You know are using " + weapon);
        }
        if (Input.GetKeyDown("5"))
        {
            weapon = "hammer";
            print("You know are using " + weapon);
        }
        if (Input.GetKeyDown("6"))
        {
            weapon = "spear";
            print("You know are using " + weapon);
        }
        return weapon;
    }
}
