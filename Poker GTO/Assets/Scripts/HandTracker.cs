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

    //player cards
    List<List<Card>> playerCards = new List<List<Card>>();

    //List<Card> Player1 = new List<Card>();


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

        MakeBoard();

        //enter number of players
        NumberPlayers(8);

        //check who won
        WhatIsHand();

    }


	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void MakeBoard()
    {
        for(int x = 0; x < 5; x++)
        {
            //turn card
            Card newCard = deck[Random.Range(0, deck.Count)];
            deck.Remove(newCard);
            Board.Add(newCard);
            ImageNumber = (int)newCard.value + (int)newCard.suit * 13;
            cardImage = Resources.Load(pathway + ImageNumber.ToString()) as GameObject;
            prefab = Instantiate(cardimagetest);
            prefab.GetComponent<SpriteRenderer>().sprite = cardImage.GetComponent<SpriteRenderer>().sprite;
            prefab.transform.position = new Vector3(x, 0, 0);

            Debug.Log("Card " + x + " is " + newCard.value + " " + newCard.suit);
        }
    }

    public List<Card> FindFlush()
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

        ////print cards in flushes
        //for (int x = 0; x < flushes.Count; x++)
        //{
        //    Debug.Log("there are " + flushes[x].Count + " " + (Suit)x);
        //}

        foreach(List<Card> listCards in flushes)
        {
            if(listCards.Count >= 5)
            {
                return listCards;
            }
        }

        return new List<Card>();

    }

    //needs to be reworked to be list the flush counters// all strighter need to go in their own lists
    public List<Card> FindStraight( List<Card> straight)
    {
        bool ace1 = true;
        bool ace2 = true;
        bool ace3 = true;
        bool ace4 = true;
        int straightCounter = 1;
        straight.Add(Board[0]);

        for (int x = 0; x < Board.Count - 1; x++)
        {
            //new cards for the straight
            //Debug.Log("            //Debug.Log("Board[x].value = " + Board[x].value + " " + Board[x].suit);
            //Debug.Log("Board[x + 1].value = " + Board[x + 1].value + " Board[x + 1].suit = " + Board[x + 1].suit);x = " + x);

            //Debug.Log("staightcounter = " + straightCounter);

            if((int)Board[x].value == 0)
            {
                if((int)Board[Board.Count - 1].value == 12 && ace1)
                {
                    ace1 = false;
                    //Debug.Log("ace 1 ");
                    straight.Insert(0, Board[Board.Count - 1]);
                    straightCounter += 1;
                }
                if ((int)Board[Board.Count - 2].value == 12 && ace2)
                {
                    ace2 = false;
                    //Debug.Log("ace 2 ");
                    straight.Insert(0, Board[Board.Count - 2]);
                    straightCounter += 1;
                }
                if ((int)Board[Board.Count - 3].value == 12 && ace3)
                {
                    ace3 = false;
                    //Debug.Log("ace 3 ");
                    straight.Insert(0, Board[Board.Count - 3]);
                    straightCounter += 1;
                }
                if ((int)Board[Board.Count - 4].value == 12 && ace4)
                {
                    ace4 = false;
                    //Debug.Log("ace 4 ");
                    straight.Insert(0, Board[Board.Count - 4]);
                    straightCounter += 1;
                }
            }

            if ((int)Board[x].value + 1 == (int)Board[x + 1].value)
            {
                //Debug.Log("found next card");
                straightCounter += 1;
                straight.Add(Board[x + 1]);
            }
            //same numbers are other card
            else if ((int)Board[x].value == (int)Board[x + 1].value)
            {
                //Debug.Log("found same card");
                straight.Add(Board[x + 1]);
            }
            //no straight yet
            else
            {
                //Debug.Log("else");
                if (straightCounter >= 5)
                {
                    //can't be anymore straights so just stop looking
                    break;
                    //Debug.Log("straigh counter at 5");
                }
                else
                {
                    //Debug.Log("clear straight counter");
                    straightCounter = 0;
                    straight.Clear();
                    straight.Add(Board[x + 1]);
                }
            }
        }

        //straight.Remove(new Card(Number.Ace, Suit.Spades));
        //straight.Remove(new Card(Number.Ace, Suit.Spades));

        //Debug.Log("straightCounter = " + straightCounter + " With " + straight.Count + " Cards");



        //foreach(Card c in straight)
        //{
        //    Debug.Log(c.value + " " + c.suit);
        //}

        if (straight.Count >= 5)
        {
            Debug.Log("have straight");
            foreach (Card c in straight)
            {
                Debug.Log(c.value + " " + c.suit);
            }
        }
        return straight;
    }

    public List<List<Card>> findDuplicates(List<List<Card>> Duplicates)
    {
        List<Card> TypesOfCards = new List<Card>();

        //always add first card
        TypesOfCards.Add(Board[0]);

        for(int x = 0; x < Board.Count - 1; x++)
        {
            if ((int)Board[x].value == (int)Board[x + 1].value)
            {
                TypesOfCards.Add(Board[x + 1]);
            }
            else
            {
                Duplicates.Add(TypesOfCards);
                TypesOfCards = new List<Card>();
                TypesOfCards.Add(Board[x + 1]);
            }
        }

        //always add last list to list
        Duplicates.Add(TypesOfCards);

        return Duplicates;
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

    public void NumberPlayers(int num)
    {
        //player cards
        //card1
        for (int x = 0; x < num; x++)
        {
            List<Card> currentPlayer = new List<Card>();

            Card player1card1 = deck[Random.Range(0, deck.Count)];
            deck.Remove(player1card1);
            //load image 1
            ImageNumber = (int)player1card1.value + (int)player1card1.suit * 13;
            cardImage = Resources.Load(pathway + ImageNumber.ToString()) as GameObject;
            prefab = Instantiate(cardimagetest);
            prefab.GetComponent<SpriteRenderer>().sprite = cardImage.GetComponent<SpriteRenderer>().sprite;
            prefab.transform.position = new Vector3(-12 + 3*x, -3, 0);
             
            //card2
            Card player1card2 = deck[Random.Range(0, deck.Count)];
            deck.Remove(player1card2);
            //load image 2
            ImageNumber = (int)player1card2.value + (int)player1card2.suit * 13;
            cardImage = Resources.Load(pathway + ImageNumber.ToString()) as GameObject;
            prefab = Instantiate(cardimagetest);
            prefab.GetComponent<SpriteRenderer>().sprite = cardImage.GetComponent<SpriteRenderer>().sprite;
            prefab.transform.position = new Vector3(-11 + 3*x, -3 , 0);

            Debug.Log(player1card1.value + " " + player1card1.suit);
            Debug.Log(player1card2.value + " " + player1card2.suit);

            currentPlayer.Add(player1card1);
            currentPlayer.Add(player1card2);

            playerCards.Add(currentPlayer);
        }
    }

    List<Card> BestHand(List<Card> flush, List<Card> straight, List<List<Card>> Duplicates)
    {
        //checks for flush and straight flush
        if(flush.Count >= 5)
        {
            if(straight.Count >= 5)
            {
                List<Card> StraightFlush = new List<Card>();
                foreach (Card c in flush)
                {
                    if (straight.Contains(c))
                    {
                        StraightFlush.Add(c);
                    }
                }
                if(StraightFlush.Count >= 5)
                {
                    return StraightFlush;
                }
            }
            return flush;
        }
        else if(straight.Count >= 5)
        {
            List<Card> sortedStraight = new List<Card>();
            sortedStraight.Add(straight[0]);
            for (int x = 0; x  < straight.Count - 1; x++)
            {
                if(straight[x].value == straight[x + 1].value)
                {
                    //nothing
                }
                else
                {
                    sortedStraight.Add(straight[x + 1]);
                }
            }
            return sortedStraight;
        }
        else
        {
            List<Card> Cards4 = new List<Card>();

            //for(int x = Duplicates.Count - 1; x >= 0; x--)
            //{
            //    foreach(Card c in Duplicates[x])
            //    {
            //        bestDups.Add(c);
            //    }
            //}

            //check for 4 of a king
            foreach(List<Card> listCards in Duplicates)
            {
                if (listCards.Count == 4)
                {
                    Cards4 = new List<Card>(listCards);
                }
            }

            if(Cards4.Count == 4)
            {
                return Cards4;
            }

            List<Card> pair1 = new List<Card>();
            List<Card> pair2 = new List<Card>();
            List<Card> cards3 = new List<Card>();

            //check for full house
            foreach (List<Card> listCards in Duplicates)
            {
                if (listCards.Count == 3)
                {
                    cards3 = new List<Card>(listCards);
                }
            }

            foreach (List<Card> listCards in Duplicates)
            {
                if (listCards.Count == 2)
                {
                    pair1 = new List<Card>(listCards);
                }
            }

            if(cards3.Count == 3 && pair1.Count == 2)
            {
                cards3.AddRange(pair1);
                return cards3;
            }

            if(cards3.Count == 3)
            {
                return cards3;
            }

            if (pair1.Count > 0)
            {
                foreach (List<Card> listCards in Duplicates)
                {
                    if (listCards.Count == 2 && listCards[0].value != pair1[0].value)
                    {
                        pair2 = new List<Card>(listCards);
                    }
                }
            }

            if (pair2.Count == 2)
            {
                pair1.AddRange(pair2);
                return pair1;
            }

            if(pair1.Count == 2)
            {
                return pair1;
            }

            List<Card> highCard = new List<Card>();
            highCard.Add(Duplicates[Duplicates.Count - 1][0]);
            return highCard;
        }
    }

    public void WhatIsHand()
    {
        List<Card> bestHand = new List<Card>();

        foreach (List<Card> player in playerCards)
        {
            List<Card> flush = new List<Card>();
            List<Card> straight = new List<Card>();
            List<List<Card>> Duplicates = new List<List<Card>>();

            Board.Add(player[0]);
            Board.Add(player[1]);


            bubblesort(Board);
            //foreach(Card c in Board)
            //{
            //    Debug.Log(c.value + " " + c.suit);
            //}

            flush = FindFlush();
            straight = FindStraight(straight);

            //Debug.Log("flushes:");
            //foreach ( Card c in flush)
            //{
            //    Debug.Log(c.value + " " + c.suit);
            //}
            //Debug.Log("straights:");
            //foreach (Card c in straight)
            //{
            //    Debug.Log(c.value + " " + c.suit);
            //}

            Duplicates = findDuplicates(Duplicates);

            //foreach (List<Card> dups in Duplicates)
            //{
            //    Debug.Log("cards number = " + dups.Count);
            //    foreach (Card c in dups)
            //    {
            //        Debug.Log(c.value + " " + c.suit);
            //    }
            //}

            bestHand = BestHand(flush, straight, Duplicates);

            Debug.Log("Best Hand is =");
            foreach (Card c in bestHand)
            {
                Debug.Log(c.value + " " + c.suit);
            }

            Board.Remove(player[0]);
            Board.Remove(player[1]);

        }

    }
}
