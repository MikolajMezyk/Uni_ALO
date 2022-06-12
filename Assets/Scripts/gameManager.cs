using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;

public class gameManager : MonoBehaviour
{

    public Sprite[] cardFace;
    public Sprite cardBack;
    public GameObject[] cards;
    public GameObject gameTime;

    private bool _init = false;
    private int _matches = 0;

    // Update is called once per frame
    void Update()
    {
        if (!_init)
            initializeCards();

        if (Input.GetMouseButtonUp(0))
            checkCards();

    }

    void initializeCards()
    {
        _matches = cards.Length / 2;
        for (int id = 0; id < 2; id++)
        {
            for (int i = 1; i < (cards.Length / 2); i++)
            {

                bool test = false;
                int choice = 0;
                while (!test)
                {
                    choice = UnityEngine.Random.Range(0, cards.Length);
                    test = !(cards[choice].GetComponent<cardScript>().initialized);
                }
                if (i % 2 == 1)
                {
                    temp = -i;
                }
                else
                {
                    temp = i
                }
                cards[choice].GetComponent<cardScript>().cardValue = temp;
                cards[choice].GetComponent<cardScript>().initialized = true;
            }
        }

        foreach (GameObject c in cards)
            c.GetComponent<cardScript>().setupGraphics();

        if (!_init)

            _init = true;
    }

    public Sprite getCardBack()
    {
        return cardBack;
    }

    public Sprite getCardFace(int i)
    {
        return cardFace[i - 1];
    }

    void checkCards()
    {
        List<int> c = new List<int>();

        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i].GetComponent<cardScript>().state == 1)
                c.Add(i);
        }

        if (c.Count == 2)
            cardComparison(c);
    }

    void cardComparison(List<int> c)
    {
        cardScript.DO_NOT = true;

        int x = 0;

        if (Math.Abs(cards[c[0]].GetComponent<cardScript>().cardValue) == Math.Abs(cards[c[1]].GetComponent<cardScript>().cardValue))
        {
            x = 2;
            _matches--;
            if (_matches == 0)
                gameTime.GetComponent<timeScript>().endGame();
        }


        for (int i = 0; i < c.Count; i++)
        {
            cards[c[i]].GetComponent<cardScript>().state = x;
            cards[c[i]].GetComponent<cardScript>().falseCheck();
        }

    }

    public void reGame()
    {
        SceneManager.LoadScene("gameScene");
    }

    public void reMenu()
    {
        SceneManager.LoadScene("menuScene");
    }
}