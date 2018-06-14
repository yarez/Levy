using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day_Operation : MonoBehaviour {
    private int[,] charScriptInd = { { 1, 1, 1 }, { 6, 3, 4} , {1, 4, 6}, {4, 6, 0}, { 4, 0, -1 } } ;
    private int[,] charScriptDest = { { 1, 1, 1 }, { 5, 3, 4 }, {2, 2, 3}, { 3, 3, 4 }, { 2, 2, -1 } };
    public GameManager Gmanager;

    // Use this for initialization
    void Start() {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Random_Ding(Char_Movement_Controller[] Chars)
    {
        int charChoice = Select_Char(Chars);
        checkFloor(charChoice, Chars);
        while (Chars[charChoice].did_script && Gmanager.day < 3 || !Chars[charChoice].getIsAlive())
        {
            charChoice = Select_Char(Chars);
        }
        int dest = Select_Dest(Chars[charChoice]);
        
        Chars[charChoice].Ding(dest);
    }

    public void Script(int scriptCount, int day, Char_Movement_Controller[] Chars)
    {
        Char_Movement_Controller curChar = Chars[charScriptInd[day, scriptCount]];
        checkFloor(curChar.ar_id, Chars);
        while ((!curChar.getIsAlive() || curChar.did_script) && Gmanager.day < 3)
        {
            Debug.Log("Char Dead, reselecting");
            curChar = Chars[charScriptInd[day, ++scriptCount]];
        }
        curChar.script_char = true;
        curChar.Ding(charScriptDest[day, scriptCount]);
    }

    private int Select_Char(Char_Movement_Controller[] Chars)
    {
        int choice = 0;
        do
        {
            choice = Random.Range(0, Chars.Length);
        } while (!Chars[choice].getIsAlive() && !Chars[choice].did_script);

        return choice;
    }

    private int Select_Dest(Char_Movement_Controller Char)
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        int choice = 1;
        do
        {
            choice = Random.Range(1,6);
        } while (choice == Char.getFloor());
        return choice;
    }

    private void checkFloor(int charChoice, Char_Movement_Controller[] Chars)
    {
        for(int i = 0; i < 3; i++)
        {
            if (charChoice == charScriptInd[Gmanager.day, i])
            {
                if (Chars[charChoice].getFloor() == charScriptDest[Gmanager.day, i]){
                    Chars[charChoice].did_script = true;
                    Debug.Log("Character "+charChoice+" already on target floor");
                }
            } 
        }
    }
}
