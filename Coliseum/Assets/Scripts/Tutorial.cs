using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Playables;

public class Tutorial : MonoBehaviour
{
    //variable to see what stage of the tutorial the player is at
    int y = 1;

    public GameObject ActionPanel;
    public GameObject WeaponPanel;
    public GameObject TutorialPanel;
    public GameObject TimerPanel;
    public GameObject PredictionPanel;
    //public GameObject logPanel;

    //glory ui
    public GlorySlider glorySlider;
    int glory = 0;

    //top get the first text the player sees
    public Text inital;

    //to get each set of dialogue
    public GameObject first;
    public GameObject second;
    public GameObject third;
    public GameObject fourth;
    public GameObject next;

    //to display Log in ////logText UI
    //public Text ////logText;

    //buttons for menu and game
    public GameObject menu;
    public GameObject fullgame;

    //playable animation sequences
    public PlayableDirector cutscene1;
    public PlayableDirector cutscene2;
    public PlayableDirector cutscene3;
    public PlayableDirector idle;

    //weapon and weapon ui of enemy
    public GameObject enemySpear;
    public GameObject enemySpearIcon;
    public GameObject enemyHammer;

    //weapon and weapon ui of player
    public GameObject playerSword;
    public GameObject playerSwordIcon;
    public GameObject playerSpear;

    private void Start()
    {
        idle.Play();
    }

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
                ////logText.text = ("Both sides chose to attack. ");
                ////logText.text += ("You used a sword, while your opponent used a spear. You won that round.");

                y = 2;

                glory = glory + 2;
                glorySlider.SetGlory(glory);

                cutscene1.Play();
                StartCoroutine(Cutscene());
                inital.text = "Good job on winning that round. You had the better weapon.";

                //moved this script to coroutine
                //first.SetActive(false);
                //second.SetActive(true);
                //TutorialPanel.SetActive(true);
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
                ////logText.text = ("Both sides chose to attack. ");
                //logText.text += ("You used a spear, while your opponent used a hammer. You won that round.");

                glory = glory + 2;
                glorySlider.SetGlory(glory);

                cutscene1.Play();
                StartCoroutine(Cutscene());
                inital.text = "Good job on winning that round. You have now completed the tutorial.";
                
                //moved to coroutine
                //third.SetActive(false);
                //fourth.SetActive(true);
                //TutorialPanel.SetActive(true);

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

            //logText.text = ("You defended from the opponent's attack. ");
            //logText.text += ("You used a sword, while your opponent used a spear. You lost that round.");
            glory = glory + 1;
            glorySlider.SetGlory(glory);

            cutscene2.Play();
            StartCoroutine(LongCutscene());

            inital.text = "When defending, you gain less glory but can break your opponent's weapon";

            //moved this script to coroutine
            //first.SetActive(false);
            //second.SetActive(false);
            //third.SetActive(true);
            //TutorialPanel.SetActive(true);
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

            inital.text = "The weapons flowing clockwise are: Hammer, Sword, Spear";

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
        playerSword.SetActive(false);
        playerSpear.SetActive(true);
        playerSwordIcon.SetActive(false);

        glory = glory - 2;
        glorySlider.SetGlory(glory);

        cutscene3.Play();
        StartCoroutine(Cutscene());
        //logText.text = ("You switched weapon to a Spear. But your opponent attacked you");
        inital.text = "Be careful when you swap weapons because your opponent can still attack you. Now let's get revenge, press the attack button.";
        
        //moved to coroutine
        //TutorialPanel.SetActive(true);
        //WeaponPanel.SetActive(false);
        //ActionPanel.SetActive(true);
    }

    //methods for the cutscenes to look like cutscenes
    void CutscenePlays()
    {
        ActionPanel.SetActive(false);
        WeaponPanel.SetActive(false);
        //logPanel.SetActive(false);
        TimerPanel.SetActive(false);
        PredictionPanel.SetActive(false);

    }

    void PostCutscene()
    {
        ActionPanel.SetActive(true);
        WeaponPanel.SetActive(false);
        //logPanel.SetActive(true);
        TimerPanel.SetActive(true);
        PredictionPanel.SetActive(true);
        idle.Play();

        if (y==2)
        {
            first.SetActive(false);
            second.SetActive(true);
            TutorialPanel.SetActive(true);
        }
        else if(y==3)
        {
            first.SetActive(false);
            second.SetActive(false);
            third.SetActive(true);
            TutorialPanel.SetActive(true);
            enemyHammer.SetActive(true);
            enemySpear.SetActive(false);
            enemySpearIcon.SetActive(false);
        }
        else if(y==4)
        {
            third.SetActive(false);
            fourth.SetActive(false);
            TutorialPanel.SetActive(true);
        }
        else
        {
            TutorialPanel.SetActive(true);
            WeaponPanel.SetActive(false);
            ActionPanel.SetActive(true);
        }
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

    public void Menu()
    {
        SceneManager.LoadScene(sceneName: "MainMenuScene");
    }

    public void Gameplay()
    {
        SceneManager.LoadScene(sceneName: "GameScene");
    }
}
