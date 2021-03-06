using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActions : MonoBehaviour {

	public delegate void pauseDel(bool paused);
    public static event pauseDel onPause;
    public static void pause(bool paused) {onPause(paused);}

    public delegate void restartDel();
    public static event restartDel onRestart;
    public static void restart() {onRestart();}
    
    public delegate void startDel();
    public static event startDel onStart;
    public static void start() {onStart();}


    public delegate void newScoreDel(int value);
    public static event newScoreDel onNewScore;
    public static void newScore(int value) {onNewScore(value);}

    public delegate void jumpDel ();
    public static event jumpDel onJump;
    public static void jump() {onJump();}
    
    public delegate void screenPassDel ();
    public static event screenPassDel onScreenPass;
    public static void screenPass() {onScreenPass();}

    public delegate void secondChanceDel ();
    public static event secondChanceDel onSecondChance;
    public static void secondChance() {onSecondChance();}

    public delegate void deathDel ();
    public static event deathDel onDeath;
    public static void death() {onDeath();}
}

