using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    //variable to see what stage of the tutorial the player is at
    int y = 1;

    public GameObject ActionPanel;
    public GameObject WeaponPanel;
    public GameObject TutorialPanel;

    //top get the first text the player sees
    public Text inital;

    //to get each set of dialogue
    public GameObject first;
    public GameObject second;
    public GameObject third;
    public GameObject fourth;
    public GameObject next;

    //to display Log in LogText UI
    public Text logText;

    //buttons for menu and game
    public GameObject menu;
    public GameObject fullgame;


    public void Attack()
    {
        //needs y to be 1
        if (y == 1 || y == 4)
        {
            if (y == 1)
            {
                /*the player wins 
             * starts with sword
             * enemy has a spear
             * dialogue pops up explaining the triangle 
             * what happens if u won or lose when attacking
             * dialogue says to try defending
             */
                logText.text = ("Both sides chose to attack. ");
                logText.text += ("You used a sword, while your opponent used a spear. You won that round.");

                y = 2;

                inital.text = "Good job on winning that round. You had the better weapon.";

                first.SetActive(false);
                second.SetActive(true);
                TutorialPanel.SetActive(true);
            }
            else
            {
                /*player wins
                 * talk about glory
                 * win condistions and lose conditions for glory
                 * and weapon breaking
                 * says its the end of the tutorial
                 * code to send to gamescene
                 * SceneManager.LoadScene(sceneName: "GameScene");
                 */
                logText.text = ("Both sides chose to attack. ");
                logText.text += ("You used a spear, while your opponent used a hammer. You won that round.");

                inital.text = "Good job on winning that round. You have now completed the tutorial.";

                third.SetActive(false);
                fourth.SetActive(true);
                TutorialPanel.SetActive(true);

                //buttons to go to menu or game
                fourth.SetActive(false);
                next.SetActive(false);
                menu.SetActive(true);
                fullgame.SetActive(true);
            }
        }
        else
        {
            /*tells player that this is the wrong button
             */
            inital.text = "That's not the right button";
            TutorialPanel.SetActive(true);
        }
    }

    public void Defend()
    {
        //needs y to be 2
        if (y == 2)
        {
            /* ai switches weapon to a hammer resulting in the player loses
             * dialogue talks about what happened
             * details about losing and winning when defending
             * tells player to switch and choose the spear
             */
            y = 3;
            inital.text = "You're Opponent swapped weapons and therefore beat you";

            first.SetActive(false);
            second.SetActive(false);
            third.SetActive(true);
            TutorialPanel.SetActive(true);
        }
        else
        {
            /*tells player that this is the wrong button
             */

            inital.text = "That's not the right button";
            TutorialPanel.SetActive(true);
        }
    }

    public void Switch()
    {
        //needs y to be 3
        if (y == 3)
        {
            /*allows the player to use the switch button
             * and then also the spear button
             * moves y to 4
             */
            ActionPanel.SetActive(false);
            WeaponPanel.SetActive(true);

            inital.text = "The weapons from left to right are; Hammer, Sword, Spear";

            first.SetActive(false);
            second.SetActive(false);
            third.SetActive(false);
            TutorialPanel.SetActive(true);

            y = 4;
        }
        else
        {
            /*tells player that this is the wrong button
             */

            inital.text = "That's not the right button";
            TutorialPanel.SetActive(true);
        }
    }

    public void Spear()
    {
        logText.text = ("You switched weapon to a Spear. ");
        inital.text = "Now try to attack";
        TutorialPanel.SetActive(true);
    }

    public void Menu()
    {
        SceneManager.LoadScene(sceneName: "MainMenuScene");
    }

    public void Gameplay()
    {
        SceneManager.LoadScene(sceneName: "GameScene");
    }
}
