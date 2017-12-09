using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTracker : MonoBehaviour {

    public GameObject cardimagetest;
    GameObject cardImage;
    GameObject prefab;
    int ImageNumber;
    string pathway = "deck_of_pixel_cards_by_poptarts_at_2am_";

    public enum Suit
    {
        Spades,
        Clubs,
        Hearts,
        Diamonds
    }

    public enum Number
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    public struct Card
    {
        public Number value;
        public Suit suit;

        public Card(Number val, Suit s)
        {
            value = val;
            suit = s;
        }
    }

    List<Card> deck = new List<Card>();

    //set of community cards
    List<Card> Board = new List<Card>();
    List<Card> Player1 = new List<Card>();
    List<Card> Player1BestHand = new List<Card>();

    ////create all cards for deck
    ////spades
    //Card S2 = new Card(Number.Two, Suit.Spades);
    //Card S3 = new Card(Number.Three, Suit.Spades);
    //Card S4 = new Card(Number.Four, Suit.Spades);
    //Card S5 = new Card(Number.Five, Suit.Spades);
    //Card S6 = new Card(Number.Six, Suit.Spades);
    //Card S7 = new Card(Number.Seven, Suit.Spades);
    //Card S8 = new Card(Number.Eight, Suit.Spades);
    //Card S9 = new Card(Number.Nine, Suit.Spades);
    //Card S10 = new Card(Number.Ten, Suit.Spades);
    //Card Sj = new Card(Number.Jack, Suit.Spades);
    //Card Sq = new Card(Number.Queen, Suit.Spades);
    //Card Sk = new Card(Number.King, Suit.Spades);
    //Card Sa = new Card(Number.Ace, Suit.Spades);

    ////clubs
    //Card C2 = new Card(Number.Two, Suit.Clubs);
    //Card C3 = new Card(Number.Three, Suit.Clubs);
    //Card C4 = new Card(Number.Four, Suit.Clubs);
    //Card C5 = new Card(Number.Five, Suit.Clubs);
    //Card C6 = new Card(Number.Six, Suit.Clubs);
    //Card C7 = new Card(Number.Seven, Suit.Clubs);
    //Card C8 = new Card(Number.Eight, Suit.Clubs);
    //Card C9 = new Card(Number.Nine, Suit.Clubs);
    //Card C10 = new Card(Number.Ten, Suit.Clubs);
    //Card Cj = new Card(Number.Jack, Suit.Clubs);
    //Card Cq = new Card(Number.Queen, Suit.Clubs);
    //Card Ck = new Card(Number.King, Suit.Clubs);
    //Card Ca = new Card(Number.Ace, Suit.Clubs);

    ////HEARTS
    //Card H2 = new Card(Number.Two, Suit.Hearts);
    //Card H3 = new Card(Number.Three, Suit.Hearts);
    //Card H4 = new Card(Number.Four, Suit.Hearts);
    //Card H5 = new Card(Number.Five, Suit.Hearts);
    //Card H6 = new Card(Number.Six, Suit.Hearts);
    //Card H7 = new Card(Number.Seven, Suit.Hearts);
    //Card H8 = new Card(Number.Eight, Suit.Hearts);
    //Card H9 = new Card(Number.Nine, Suit.Hearts);
    //Card H10 = new Card(Number.Ten, Suit.Hearts);
    //Card Hj = new Card(Number.Jack, Suit.Hearts);
    //Card Hq = new Card(Number.Queen, Suit.Hearts);
    //Card Hk = new Card(Number.King, Suit.Hearts);
    //Card Ha = new Card(Number.Ace, Suit.Hearts);

    ////Diamons
    //Card D2 = new Card(Number.Two, Suit.Diamonds);
    //Card D3 = new Card(Number.Three, Suit.Diamonds);
    //Card D4 = new Card(Number.Four, Suit.Diamonds);
    //Card D5 = new Card(Number.Five, Suit.Diamonds);
    //Card D6 = new Card(Number.Six, Suit.Diamonds);
    //Card D7 = new Card(Number.Seven, Suit.Diamonds);
    //Card D8 = new Card(Number.Eight, Suit.Diamonds);
    //Card D9 = new Card(Number.Nine, Suit.Diamonds);
    //Card D10 = new Card(Number.Ten, Suit.Diamonds);
    //Card Dj = new Card(Number.Jack, Suit.Diamonds);
    //Card Dq = new Card(Number.Queen, Suit.Diamonds);
    //Card Dk = new Card(Number.King, Suit.Diamonds);
    //Card Da = new Card(Number.Ace, Suit.Diamonds);

    // Use this for initialization
    void Start ()
    {
        //create the deck of cards
        foreach (Suit suit in Suit.GetValues(typeof(Suit)))
        {
            foreach (Number num in Number.GetValues(typeof(Number)))
            {
                deck.Add(new Card(num, suit));
            }
        }

        ////ADD SPADES    
        //deck.Add(S2);
        //deck.Add(S3);
        //deck.Add(S4);
        //deck.Add(S5);
        //deck.Add(S6);
        //deck.Add(S7);
        //deck.Add(S8);
        //deck.Add(S9);
        //deck.Add(S10);
        //deck.Add(Sj);
        //deck.Add(Sq);
        //deck.Add(Sk);
        //deck.Add(Sa);

        ////ADD Clubs    
        //deck.Add(C2);
        //deck.Add(C3);
        //deck.Add(C4);
        //deck.Add(C5);
        //deck.Add(C6);
        //deck.Add(C7);
        //deck.Add(C8);
        //deck.Add(C9);
        //deck.Add(C10);
        //deck.Add(Cj);
        //deck.Add(Cq);
        //deck.Add(Ck);
        //deck.Add(Ca);

        ////ADD HEARTS
        //deck.Add(H2);
        //deck.Add(H3);
        //deck.Add(H4);
        //deck.Add(H5);
        //deck.Add(H6);
        //deck.Add(H7);
        //deck.Add(H8);
        //deck.Add(H9);
        //deck.Add(H10);
        //deck.Add(Hj);
        //deck.Add(Hq);
        //deck.Add(Hk);
        //deck.Add(Ha);

        ////ADD DIAMONDS
        //deck.Add(D2);
        //deck.Add(D3);
        //deck.Add(D4);
        //deck.Add(D5);
        //deck.Add(D6);
        //deck.Add(D7);
        //deck.Add(D8);
        //deck.Add(D9);
        //deck.Add(D10);
        //deck.Add(Dj);
        //deck.Add(Dq);
        //deck.Add(Dk);
        //deck.Add(Da);

        //player cards
        //card1
        Card player1card1 = deck[Random.Range(0, deck.Count)];
        deck.Remove(player1card1);
        //load image 1
        ImageNumber = (int)player1card1.value + (int)player1card1.suit * 13;
        cardImage = Resources.Load(pathway + ImageNumber.ToString()) as GameObject;
        prefab = Instantiate(cardimagetest);
        prefab.GetComponent<SpriteRenderer>().sprite = cardImage.GetComponent<SpriteRenderer>().sprite;
        prefab.transform.position = new Vector3(0, -3, 0);

        //card2
        Card player1card2 = deck[Random.Range(0, deck.Count)];
        deck.Remove(player1card2);
        //load image 2
        ImageNumber = (int)player1card2.value + (int)player1card2.suit * 13;
        cardImage = Resources.Load(pathway + ImageNumber.ToString()) as GameObject;
        prefab = Instantiate(cardimagetest);
        prefab.GetComponent<SpriteRenderer>().sprite = cardImage.GetComponent<SpriteRenderer>().sprite;
        prefab.transform.position = new Vector3(1, -3, 0);

        Debug.Log(player1card1.value + " " + player1card1.suit);
        Debug.Log(player1card2.value + " " + player1card2.suit);

        Player1.Add(player1card1);
        Player1.Add(player1card2);
        


        //rivers cards
        Card river1 = deck[Random.Range(0, deck.Count)];
        deck.Remove(river1);
        ImageNumber = (int)river1.value + (int)river1.suit * 13;
        cardImage = Resources.Load(pathway + ImageNumber.ToString()) as GameObject;
        prefab = Instantiate(cardimagetest);
        prefab.GetComponent<SpriteRenderer>().sprite = cardImage.GetComponent<SpriteRenderer>().sprite;
        prefab.transform.position = new Vector3(0, 0, 0);

        Card river2 = deck[Random.Range(0, deck.Count)];
        deck.Remove(river2);
        ImageNumber = (int)river2.value + (int)river2.suit * 13;
        cardImage = Resources.Load(pathway + ImageNumber.ToString()) as GameObject;
        prefab = Instantiate(cardimagetest);
        prefab.GetComponent<SpriteRenderer>().sprite = cardImage.GetComponent<SpriteRenderer>().sprite;
        prefab.transform.position = new Vector3(1, 0, 0);

        Card river3 = deck[Random.Range(0, deck.Count)];
        deck.Remove(river3);
        ImageNumber = (int)river3.value + (int)river3.suit * 13;
        cardImage = Resources.Load(pathway + ImageNumber.ToString()) as GameObject;
        prefab = Instantiate(cardimagetest);
        prefab.GetComponent<SpriteRenderer>().sprite = cardImage.GetComponent<SpriteRenderer>().sprite;
        prefab.transform.position = new Vector3(2, 0, 0);

        //add to board
        Board.Add(river1);
        Board.Add(river2);
        Board.Add(river3);

        Debug.Log("Flop");
        Debug.Log(river1.value + " " + river1.suit);
        Debug.Log(river2.value + " " + river2.suit);
        Debug.Log(river3.value + " " + river3.suit);

        //turn card
        Card turn = deck[Random.Range(0, deck.Count)];
        deck.Remove(turn);
        Board.Add(turn);
        ImageNumber = (int)turn.value + (int)turn.suit * 13;
        cardImage = Resources.Load(pathway + ImageNumber.ToString()) as GameObject;
        prefab = Instantiate(cardimagetest);
        prefab.GetComponent<SpriteRenderer>().sprite = cardImage.GetComponent<SpriteRenderer>().sprite;
        prefab.transform.position = new Vector3(3, 0, 0);

        Debug.Log("Turn");
        Debug.Log(turn.value + " " + turn.suit);

        //river card
        Card river = deck[Random.Range(0, deck.Count)];
        deck.Remove(river);
        Board.Add(river);
        ImageNumber = (int)river.value + (int)river.suit * 13;
        cardImage = Resources.Load(pathway + ImageNumber.ToString()) as GameObject;
        prefab = Instantiate(cardimagetest);
        prefab.GetComponent<SpriteRenderer>().sprite = cardImage.GetComponent<SpriteRenderer>().sprite;
        prefab.transform.position = new Vector3(4, 0, 0);

        Debug.Log("River");
        Debug.Log(river.value + " " + river.suit);
        //Debug.Log(river.value + " " + (int)river.suit);

        //check who won
        WhatIsHand();

    }


	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void FindFlush()
    {
        List<List<Card>> flushes = new List<List<Card>>();
        flushes.Add(new List<Card>());
        flushes.Add(new List<Card>());
        flushes.Add(new List<Card>());
        flushes.Add(new List<Card>());
        
        for (int x = 0; x < Board.Count; x++)
        {
            if (Board[x].suit == Suit.Spades)
            {
                flushes[0].Add(Board[x]);
            }
            else if (Board[x].suit == Suit.Clubs)
            {
                flushes[1].Add(Board[x]);
            }
            else if (Board[x].suit == Suit.Hearts)
            {
                flushes[2].Add(Board[x]);
            }
            //diamonds
            else
            {
                flushes[3].Add(Board[x]);
            }
        }

        for (int x = 0; x < flushes.Count; x++)
        {
            Debug.Log("there are " + flushes[x].Count + " " + (Suit)x);
        }

        ////////////////////

        ////check for flush
        //int FlushCounter;

        //foreach (Suit suit in Suit.GetValues(typeof(Suit)))
        //{
        //    FlushCounter = 0;
        //    foreach (Card c in Board)
        //    {
        //        //Debug.Log(c.value + " " + c.suit);
        //        if (suit == c.suit)
        //        {
        //            if (Player1BestHand.Count > 0)
        //            {
        //                if ((int)c.value < (int)Player1BestHand[0].value)
        //                {
        //                    Player1BestHand.Insert(0, c);
        //                }
        //                else
        //                {
        //                    Player1BestHand.Add(c);
        //                }
        //            }
        //            else
        //            {
        //                Player1BestHand.Add(c);
        //            }
        //            FlushCounter += 1;
        //        }
        //    }

        //    Debug.Log("Number of " + suit + " = " + FlushCounter);
        //    if (FlushCounter >= 5)
        //    {
        //        Debug.Log("have flush");

        //        break;
        //    }
        //    else if (FlushCounter >= 3)
        //    {
        //        Player1BestHand.Clear();
        //        break;
        //    }
        //    else
        //    {
        //        Player1BestHand.Clear();
        //    }
        //}
    }

    public List<Card> FindStraight( List<Card> straight)
    {
        int straightCounter = 1;

        straight.Add(Board[0]);

        for (int x = 1; x < Board.Count; x++)
        {
            //new cards for the straight
            if((int)Board[x].value == (int)Board[x - 1].value + 1)
            {
                straightCounter += 1;
                straight.Add(Board[x]);
            }
            //same numbers are other card
            if ((int)Board[x].value == (int)Board[x - 1].value)
            {
                straight.Add(Board[x]);
            }
            //no straight yet
            else
            {
                if(straightCounter >= 5)
                {
                    break;
                }
                straightCounter = 0;
                straight.Clear();
            }
        }

        if(straightCounter >= 5)
        {
            Debug.Log("have straight");
        }
        return straight;
    }

    public void findDuplicates()
    {
        List<int> NumberOfCards = new List<int>();
        List<Card> TypesOfCards = new List<Card>();

        TypesOfCards.Add(Board[0]);

        int currentCount = 1;

        for(int x = 1; x < Board.Count; x++)
        {
            if((int)Board[x].value == (int)Board[x - 1].value)
            {
                currentCount += 1;
            }
            else
            {
                NumberOfCards.Add(currentCount);
                TypesOfCards.Add(Board[x]);
                currentCount = 1;
            }
        }
        NumberOfCards.Add(currentCount);

        for( int x = 0; x < NumberOfCards.Count; x++)
        {
            Debug.Log("there are " + NumberOfCards[x] + " " + TypesOfCards[x].value);
        }

    }

    public void bubblesort(List<Card> a)
    {
        Card temp;
        // foreach(int i in a)
        for (int i = 1; i <= a.Count; i++)
        {
            for (int j = 0; j < a.Count - i; j++)
            {
                if ((int)a[j].value > (int)a[j + 1].value)
                {
                    temp = a[j];
                    a[j] = a[j + 1];
                    a[j + 1] = temp;
                }
            }
        }
    }

    public void WhatIsHand()
    {
        List<Card> flush = new List<Card>();
        List<Card> straight = new List<Card>();

        Board.Add(Player1[0]);
        Board.Add(Player1[1]);

        bubblesort(Board);


        FindFlush();
        straight = FindStraight(straight);
        findDuplicates();

    }
}
