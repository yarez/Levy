using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamesSelector : MonoBehaviour {

    public Texture generalAlive;
    public Texture generalDead;

    public Texture[] Day_01;
    public Texture[] Day_02_Alive;
    public Texture[] Day_02_Dead;
    public Texture[] Day_03_Alive;
    public Texture[] Day_03_Dead;
    public Texture[] Day_04_Alive;
    public Texture[] Day_04_Dead;

    public AudioClip A_generalAlive;
    public AudioClip A_generalDead;

    public AudioClip[] A_Day_01;
    public AudioClip[] A_Day_02_Alive;
    public AudioClip[] A_Day_02_Dead;
    public AudioClip[] A_Day_03_Alive;
    public AudioClip[] A_Day_03_Dead;
    public AudioClip[] A_Day_04_Alive;
    public AudioClip[] A_Day_04_Dead;

    public Texture select(int day, bool isAlive, int index)
    {
        if(day == 1)
        {
            if(index < 3)
            {
                return Day_01[index];
            }else if(isAlive)
            {
                return generalAlive;
            }
            return generalDead;
        } else if(day == 2)
        {
            if (isAlive)
            {
                if (index < 3)
                    return Day_02_Alive[index];
                return generalAlive;
            }else if(index < 3)
            {
                return Day_02_Dead[index];
            }
            return generalDead;
        }else if (day == 3)
        {
            if (isAlive)
            {
                if (index < 3)
                    return Day_03_Alive[index];
                return generalAlive;
            }
            else if (index < 3)
            {
                return Day_03_Dead[index];
            }
            return generalDead;
        }
        else if (day == 4)
        {
            if (isAlive)
            {
                if (index < 3)
                    return Day_04_Alive[index];
                return generalAlive;
            }
            else if (index < 3)
            {
                return Day_04_Dead[index];
            }
            return generalDead;
        }
        return null;
    }


    /*----------------------Audio Describe Selection-------------------*/
    public AudioClip selectAudio(int day, bool isAlive, int index)
    {
        if (day == 1)
        {
            if (index < 3)
            {
                return A_Day_01[index];
            }
            else if (isAlive)
            {
                return A_generalAlive;
            }
            return A_generalDead;
        }
        else if (day == 2)
        {
            if (isAlive)
            {
                if (index < 3)
                    return A_Day_02_Alive[index];
                return A_generalAlive;
            }
            else if (index < 3)
            {
                return A_Day_02_Dead[index];
            }
            return A_generalDead;
        }
        else if (day == 3)
        {
            if (isAlive)
            {
                if (index < 3)
                    return A_Day_03_Alive[index];
                return A_generalAlive;
            }
            else if (index < 3)
            {
                return A_Day_03_Dead[index];
            }
            return A_generalDead;
        }
        else if (day == 4)
        {
            if (isAlive)
            {
                if (index < 3)
                    return A_Day_04_Alive[index];
                return A_generalAlive;
            }
            else if (index < 3)
            {
                return A_Day_04_Dead[index];
            }
            return A_generalDead;
        }
        return null;
    }
}
