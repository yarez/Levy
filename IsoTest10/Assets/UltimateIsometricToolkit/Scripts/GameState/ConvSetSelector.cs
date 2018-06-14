using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class ConvSetSelector : MonoBehaviour {

    public GameManager Gmanager;

    public Texture[] Day_01_LeonAkira;
    public Texture[] Day_01_MinnieJodee;
    public Texture[] Day_01_WilliamJodee;
    public Texture[] Day_01_ZahraAkira;
    public Texture[] Day_02_Dead_AkiraMinnie;
    public Texture[] Day_02_Dead_WilliamAkira;
    public Texture[] Day_02_Dead_ZahraJodee;
    public Texture[] Day_02_Alive_AkiraMinnie;
    public Texture[] Day_02_Alive_WilliamLeon;
    public Texture[] Day_02_Alive_ZahraJodee;
    public Texture[] Day_03_Dead_LeonJodee;
    public Texture[] Day_03_Dead_WilliamJodee;
    public Texture[] Day_03_Dead_ZahraMinnie;
    public Texture[] Day_03_Alive_AkiraLeon;
    public Texture[] Day_03_Alive_WilliamJodee;
    public Texture[] Day_03_Alive_ZahraMinnie;
    public Texture[] Day_04_Dead_LeonAkira;
    public Texture[] Day_04_Dead_WilliamJodee;
    public Texture[] Day_04_Alive_LeonMinnie;
    public Texture[] Day_04_Alive_WilliamJodee;
    public Texture[] Day_04_Alive_ZahraAkira;

    public AudioClip[] A_Day_01_LeonAkira;
    public AudioClip[] A_Day_01_MinnieJodee;
    public AudioClip[] A_Day_01_WilliamJodee;
    public AudioClip[] A_Day_01_ZahraAkira;
    public AudioClip[] A_Day_02_Dead_AkiraMinnie;
    public AudioClip[] A_Day_02_Dead_WilliamAkira;
    public AudioClip[] A_Day_02_Dead_ZahraJodee;
    public AudioClip[] A_Day_02_Alive_AkiraMinnie;
    public AudioClip[] A_Day_02_Alive_WilliamLeon;
    public AudioClip[] A_Day_02_Alive_ZahraJodee;
    public AudioClip[] A_Day_03_Dead_LeonJodee;
    public AudioClip[] A_Day_03_Dead_WilliamJodee;
    public AudioClip[] A_Day_03_Dead_ZahraMinnie;
    public AudioClip[] A_Day_03_Alive_AkiraLeon;
    public AudioClip[] A_Day_03_Alive_WilliamJodee;
    public AudioClip[] A_Day_03_Alive_ZahraMinnie;
    public AudioClip[] A_Day_04_Dead_LeonAkira;
    public AudioClip[] A_Day_04_Dead_WilliamJodee;
    public AudioClip[] A_Day_04_Alive_LeonMinnie;
    public AudioClip[] A_Day_04_Alive_WilliamJodee;
    public AudioClip[] A_Day_04_Alive_ZahraAkira;

    bool Jodee = false;
    bool Akira = false;
    bool Leon = false;
    bool William = false;
    bool Minnie = false;
    bool Zahra = false;

    public Texture[] Select(int day, int char1, int char2)
    {
        voidChars();
        learnCharNames(char1, char2);
        Texture[] ConvFinal = null;
        if (day == 1)
        {
            ConvFinal = findSet_Day_01();
        }else if(day == 2 && !Gmanager.Chars[3].isAlive)
        {
            ConvFinal = findSet_Day_02_Dead();
        }
        else if (day == 2)
        {
            ConvFinal = findSet_Day_02_Alive();
        }
        else if (day == 3 && !Gmanager.Chars[1].isAlive)
        {
            ConvFinal = findSet_Day_03_Dead();
        }
        else if (day == 3)
        {
            ConvFinal = findSet_Day_03_Alive();
        }
        else if (day == 4 && !Gmanager.Chars[6].isAlive)
        {
            ConvFinal = findSet_Day_04_Dead();
        }
        else if (day == 4)
        {
            ConvFinal = findSet_Day_04_Alive();
        }
        return ConvFinal;
    }

    private void learnCharNames(int char1, int char2)
    {
        if (char1 == 0 || char2 == 0)
            Jodee = true;

        if (char1 == 1 || char2 == 1)
            Akira = true;

        if (char1 == 3 || char2 == 3)
            Leon = true;

        if (char1 == 4 || char2 == 4)
            William = true;

        if (char1 == 5 || char2 == 5)
            Minnie = true;

        if (char1 == 6 || char2 == 6)
            Zahra = true;
    }

    private void voidChars()
    {
        Jodee = false;
        Akira = false;
        Leon = false;
        William = false;
        Minnie = false;
        Zahra = false;
    }

    private Texture[] findSet_Day_01()
    {
        if (Leon && Akira)
        {
            return Day_01_LeonAkira;
        }
        else if (Minnie && Jodee)
        {
            return Day_01_MinnieJodee;
        }
        else if (William && Jodee)
        {
            return Day_01_WilliamJodee;
        }
        else if (Zahra && Akira)
        {
            return Day_01_ZahraAkira;
        }
        Debug.LogError("How did this even fail");
        return null;
    }

    private Texture[] findSet_Day_02_Alive()
    {
        if (Minnie && Akira)
        {
            return Day_02_Alive_AkiraMinnie;
        }
        else if (Leon && William)
        {
            return Day_02_Alive_WilliamLeon;
        }
        else if (Zahra && Jodee)
        {
            return Day_02_Alive_ZahraJodee;
        }
        Debug.LogError("How did this even fail");
        return null;
    }

    private Texture[] findSet_Day_02_Dead()
    {
        if (Minnie && Akira)
        {
            return Day_02_Dead_AkiraMinnie;
        }
        else if (William && Akira)
        {
            return Day_02_Dead_WilliamAkira;
        }
        else if (Zahra && Jodee)
        {
            return Day_02_Dead_ZahraJodee;
        }
        Debug.LogError("How did this even fail");
        return null;
    }

    private Texture[] findSet_Day_03_Alive()
    {
        if (Leon && Akira)
        {
            return Day_03_Alive_AkiraLeon;
        }
        else if (William && Jodee)
        {
            return Day_03_Alive_WilliamJodee;
        }
        else if (Zahra && Minnie)
        {
            return Day_03_Alive_ZahraMinnie;
        }
        Debug.LogError("How did this even fail");
        return null;
    }

    private Texture[] findSet_Day_03_Dead()
    {
        if (Leon && Jodee)
        {
            return Day_03_Dead_LeonJodee;
        }
        else if (William && Jodee)
        {
            return Day_03_Dead_WilliamJodee;
        }
        else if (Zahra && Minnie)
        {
            return Day_03_Dead_ZahraMinnie;
        }
        Debug.LogError("How did this even fail");
        return null;
    }

    private Texture[] findSet_Day_04_Alive()
    {
        if (Leon && Minnie)
        {
            return Day_04_Alive_LeonMinnie;
        }
        else if (William && Jodee)
        {
            return Day_04_Alive_WilliamJodee;
        }
        else if (Zahra && Akira)
        {
            return Day_04_Alive_ZahraAkira;
        }
        Debug.LogError("How did this even fail");
        return null;
    }

    private Texture[] findSet_Day_04_Dead()
    {
        if (Leon && Akira)
        {
            return Day_04_Dead_LeonAkira;
        }
        else if (William && Jodee)
        {
            return Day_04_Dead_WilliamJodee;
        }
        Debug.LogError("How did this even fail");
        return null;
    }



    /*-------------------------------AUDIO DESCRIPTION SELECT----------------------------------------*/
    public AudioClip[] SelectAudio(int day, int char1, int char2)
    {
        voidChars();
        learnCharNames(char1, char2);
        AudioClip[] ConvFinal = null;
        if (day == 1)
        {
            ConvFinal = A_findSet_Day_01();
        }
        else if (day == 2 && !Gmanager.Chars[3].isAlive)
        {
            ConvFinal = A_findSet_Day_02_Dead();
        }
        else if (day == 2)
        {
            ConvFinal = A_findSet_Day_02_Alive();
        }
        else if (day == 3 && !Gmanager.Chars[1].isAlive)
        {
            ConvFinal = A_findSet_Day_03_Dead();
        }
        else if (day == 3)
        {
            ConvFinal = A_findSet_Day_03_Alive();
        }
        else if (day == 4 && !Gmanager.Chars[6].isAlive)
        {
            ConvFinal = A_findSet_Day_04_Dead();
        }
        else if (day == 4)
        {
            ConvFinal = A_findSet_Day_04_Alive();
        }
        return ConvFinal;
    }

    private AudioClip[] A_findSet_Day_01()
    {
        if (Leon && Akira)
        {
            return A_Day_01_LeonAkira;
        }
        else if (Minnie && Jodee)
        {
            return A_Day_01_MinnieJodee;
        }
        else if (William && Jodee)
        {
            return A_Day_01_WilliamJodee;
        }
        else if (Zahra && Akira)
        {
            return A_Day_01_ZahraAkira;
        }
        Debug.LogError("How did this even fail");
        return null;
    }

    private AudioClip[] A_findSet_Day_02_Alive()
    {
        if (Minnie && Akira)
        {
            return A_Day_02_Alive_AkiraMinnie;
        }
        else if (Leon && William)
        {
            return A_Day_02_Alive_WilliamLeon;
        }
        else if (Zahra && Jodee)
        {
            return A_Day_02_Alive_ZahraJodee;
        }
        Debug.LogError("How did this even fail");
        return null;
    }

    private AudioClip[] A_findSet_Day_02_Dead()
    {
        if (Minnie && Akira)
        {
            return A_Day_02_Dead_AkiraMinnie;
        }
        else if (William && Akira)
        {
            return A_Day_02_Dead_WilliamAkira;
        }
        else if (Zahra && Jodee)
        {
            return A_Day_02_Dead_ZahraJodee;
        }
        Debug.LogError("How did this even fail");
        return null;
    }

    private AudioClip[] A_findSet_Day_03_Alive()
    {
        if (Leon && Akira)
        {
            return A_Day_03_Alive_AkiraLeon;
        }
        else if (William && Jodee)
        {
            return A_Day_03_Alive_WilliamJodee;
        }
        else if (Zahra && Minnie)
        {
            return A_Day_03_Alive_ZahraMinnie;
        }
        Debug.LogError("How did this even fail");
        return null;
    }

    private AudioClip[] A_findSet_Day_03_Dead()
    {
        if (Leon && Jodee)
        {
            return A_Day_03_Dead_LeonJodee;
        }
        else if (William && Jodee)
        {
            return A_Day_03_Dead_WilliamJodee;
        }
        else if (Zahra && Minnie)
        {
            return A_Day_03_Dead_ZahraMinnie;
        }
        Debug.LogError("How did this even fail");
        return null;
    }

    private AudioClip[] A_findSet_Day_04_Alive()
    {
        if (Leon && Minnie)
        {
            return A_Day_04_Alive_LeonMinnie;
        }
        else if (William && Jodee)
        {
            return A_Day_04_Alive_WilliamJodee;
        }
        else if (Zahra && Akira)
        {
            return A_Day_04_Alive_ZahraAkira;
        }
        Debug.LogError("How did this even fail");
        return null;
    }

    private AudioClip[] A_findSet_Day_04_Dead()
    {
        if (Leon && Akira)
        {
            return A_Day_04_Dead_LeonAkira;
        }
        else if (William && Jodee)
        {
            return A_Day_04_Dead_WilliamJodee;
        }
        Debug.LogError("How did this even fail");
        return null;
    }

}
