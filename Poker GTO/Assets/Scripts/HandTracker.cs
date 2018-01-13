using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HandTracker : MonoBehaviour
{

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

    public enum HandType
    {
        StraightFlush,
        FourKind,
        FullHouse,
        Flush,
        Straight,
        ThreeKind,
        TwoPair,
        Pair,
        HighCard
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

    public struct Hand
    {
        public List<Card> hand;
        public HandType type;

        public Hand(List<Card> h, HandType t)
        {
            hand = h;
            type = t;
        }

    }

    List<Card> deck = new List<Card>();

    //set of community cards
    List<Card> Board = new List<Card>();

    //player cards
    List<List<Card>> playerCards = new List<List<Card>>();

    //Player hands
    List<Hand> playerHands = new List<Hand>();

    // Use this for initialization
    void Start()
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
        NumberPlayers(7);

        //check who won
        WhatIsHand();
    }



    // Update is called once per frame
    void Update()
    {

    }

    public void MakeBoard()
    {
        ////4 of spades
        //Card newCard = deck[0];
        //deck.Remove(newCard);
        //Board.Add(newCard);
        //ImageNumber = (int)newCard.value + (int)newCard.suit * 13;
        //cardImage = Resources.Load(pathway + ImageNumber.ToString()) as GameObject;
        //prefab = Instantiate(cardimagetest);
        //prefab.GetComponent<SpriteRenderer>().sprite = cardImage.GetComponent<SpriteRenderer>().sprite;
        //prefab.transform.position = new Vector3(0, 0, 0);

        //Debug.Log("Card " + 0 + " is " + newCard.value + " " + newCard.suit);

        ////5 of spades
        //newCard = deck[4];
        //deck.Remove(newCard);
        //Board.Add(newCard);
        //ImageNumber = (int)newCard.value + (int)newCard.suit * 13;
        //cardImage = Resources.Load(pathway + ImageNumber.ToString()) as GameObject;
        //prefab = Instantiate(cardimagetest);
        //prefab.GetComponent<SpriteRenderer>().sprite = cardImage.GetComponent<SpriteRenderer>().sprite;
        //prefab.transform.position = new Vector3(1, 0, 0);

        //Debug.Log("Card " + 1 + " is " + newCard.value + " " + newCard.suit);

        ////7 of spades
        //newCard = deck[27];
        //deck.Remove(newCard);
        //Board.Add(newCard);
        //ImageNumber = (int)newCard.value + (int)newCard.suit * 13;
        //cardImage = Resources.Load(pathway + ImageNumber.ToString()) as GameObject;
        //prefab = Instantiate(cardimagetest);
        //prefab.GetComponent<SpriteRenderer>().sprite = cardImage.GetComponent<SpriteRenderer>().sprite;
        //prefab.transform.position = new Vector3(2, 0, 0);

        //Debug.Log("Card " + 2 + " is " + newCard.value + " " + newCard.suit);

        ////Ace of Clubs
        //newCard = deck[37];
        //deck.Remove(newCard);
        //Board.Add(newCard);
        //ImageNumber = (int)newCard.value + (int)newCard.suit * 13;
        //cardImage = Resources.Load(pathway + ImageNumber.ToString()) as GameObject;
        //prefab = Instantiate(cardimagetest);
        //prefab.GetComponent<SpriteRenderer>().sprite = cardImage.GetComponent<SpriteRenderer>().sprite;
        //prefab.transform.position = new Vector3(3, 0, 0);

        //Debug.Log("Card " + 3 + " is " + newCard.value + " " + newCard.suit);

        ////Jack of Hearts
        //newCard = deck[46];
        //deck.Remove(newCard);
        //Board.Add(newCard);
        //ImageNumber = (int)newCard.value + (int)newCard.suit * 13;
        //cardImage = Resources.Load(pathway + ImageNumber.ToString()) as GameObject;
        //prefab = Instantiate(cardimagetest);
        //prefab.GetComponent<SpriteRenderer>().sprite = cardImage.GetComponent<SpriteRenderer>().sprite;
        //prefab.transform.position = new Vector3(4, 0, 0);

        //Debug.Log("Card " + 4 + " is " + newCard.value + " " + newCard.suit);

        
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
            prefab.name = newCard.value + " of " + newCard.suit;

            GameObject text = GameObject.Find("Text");
            text.GetComponent<Text>().transform.position = prefab.transform.position;

            Debug.Log("Card " + x + " is " + newCard.value + " " + newCard.suit);
        }
        
    }

    public void NumberPlayers(int num)
    {
        //player cards
        //card1

        /*
        List<Card> currentPlayer = new List<Card>();

        Card player1card1 = deck[20];
        deck.Remove(player1card1);
        //load image 1
        ImageNumber = (int)player1card1.value + (int)player1card1.suit * 13;
        cardImage = Resources.Load(pathway + ImageNumber.ToString()) as GameObject;
        prefab = Instantiate(cardimagetest);
        prefab.GetComponent<SpriteRenderer>().sprite = cardImage.GetComponent<SpriteRenderer>().sprite;
        prefab.transform.position = new Vector3(-12 + 3 * 0, -3, 0);

        //card2
        Card player1card2 = deck[32];
        deck.Remove(player1card2);
        //load image 2
        ImageNumber = (int)player1card2.value + (int)player1card2.suit * 13;
        cardImage = Resources.Load(pathway + ImageNumber.ToString()) as GameObject;
        prefab = Instantiate(cardimagetest);
        prefab.GetComponent<SpriteRenderer>().sprite = cardImage.GetComponent<SpriteRenderer>().sprite;
        prefab.transform.position = new Vector3(-11 + 3 * 0, -3, 0);

        //    Debug.Log(player1card1.value + " " + player1card1.suit);
        //    Debug.Log(player1card2.value + " " + player1card2.suit);

        currentPlayer.Add(player1card1);
        currentPlayer.Add(player1card2);

        playerCards.Add(currentPlayer);
        */

        for (int x = 0; x < num; x++)
        {
            //create holder for cards
            GameObject playerCardHolder = new GameObject();
            playerCardHolder.name = "player " + x;
            playerCardHolder.transform.position = new Vector3(-12 + 3 * x, -3, 0);

            List<Card> currentPlayer = new List<Card>();

            Card player1card1 = deck[Random.Range(0, deck.Count)];
            //Card player1card1 = deck[16 + 10*x];
            deck.Remove(player1card1);
            //load image 1
            ImageNumber = (int)player1card1.value + (int)player1card1.suit * 13;
            cardImage = Resources.Load(pathway + ImageNumber.ToString()) as GameObject;
            prefab = Instantiate(cardimagetest, playerCardHolder.transform);
            prefab.GetComponent<SpriteRenderer>().sprite = cardImage.GetComponent<SpriteRenderer>().sprite;
            //prefab.transform.position = new Vector3(-12 + 3 * x, -3, 0);
            prefab.name = player1card1.value + " of " + player1card1.suit;

            //card2
            Card player1card2 = deck[Random.Range(0, deck.Count)];
            //Card player1card2 = deck[16 + x *3];
            deck.Remove(player1card2);
            //load image 2
            ImageNumber = (int)player1card2.value + (int)player1card2.suit * 13;
            cardImage = Resources.Load(pathway + ImageNumber.ToString()) as GameObject;
            prefab = Instantiate(cardimagetest, playerCardHolder.transform);
            prefab.GetComponent<SpriteRenderer>().sprite = cardImage.GetComponent<SpriteRenderer>().sprite;
            prefab.transform.localPosition = new Vector3(1, 0, 0);
            prefab.name = player1card2.value + " of " + player1card2.suit;

            //    Debug.Log(player1card1.value + " " + player1card1.suit);
            //    Debug.Log(player1card2.value + " " + player1card2.suit);

            currentPlayer.Add(player1card1);
            currentPlayer.Add(player1card2);

            playerCards.Add(currentPlayer);
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

        foreach (List<Card> listCards in flushes)
        {
            if (listCards.Count >= 5)
            {
                return listCards;
            }
        }

        return new List<Card>();

    }

    //needs to be reworked to be list the flush counters// all strighter need to go in their own lists
    public List<Card> FindStraight(List<Card> straight)
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

            if ((int)Board[x].value == 0)
            {
                if ((int)Board[Board.Count - 1].value == 12 && ace1)
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
                    //    straightCounter += 1;
                }
                if ((int)Board[Board.Count - 3].value == 12 && ace3)
                {
                    ace3 = false;
                    //Debug.Log("ace 3 ");
                    straight.Insert(0, Board[Board.Count - 3]);
                    //    straightCounter += 1;
                }
                if ((int)Board[Board.Count - 4].value == 12 && ace4)
                {
                    ace4 = false;
                    //Debug.Log("ace 4 ");
                    straight.Insert(0, Board[Board.Count - 4]);
                    //    straightCounter += 1;
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
                //straight.Add(Board[x + 1]);
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

        //if (straight.Count >= 5)
        //{
        //    Debug.Log("straight");
        //    foreach (Card c in straight)
        //    {
        //        Debug.Log(c.value + " " + c.suit);
        //    }
        //}
        return straight;
    }

    public List<List<Card>> findDuplicates(List<List<Card>> Duplicates)
    {
        List<Card> TypesOfCards = new List<Card>();

        //always add first card
        TypesOfCards.Add(Board[0]);

        for (int x = 0; x < Board.Count - 1; x++)
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

    public List<Card> RemoveExtraCards(List<Card> Hand)
    {
        while(Hand.Count > 5)
        {
            Hand.RemoveAt(0);
        }
        return Hand;
    }

    public List<Card> FillInHighCards(List<Card> PartialHand)
    {

        for(int x = Board.Count - 1; x >= 0; x--)
        {
            if (PartialHand.Contains(Board[x]))
            {
                //nothing
            }
            else
            {
                PartialHand.Add(Board[x]);
            }

            //break when hand count is 5
            if(PartialHand.Count == 5)
            {
                break;
            }
        }
        return PartialHand;
    }

    //find hand in order or strength
    Hand BestHand(List<Card> flush, List<Card> straight, List<List<Card>> Duplicates)
    {
        Hand bestHand;
        //checks for a straight flush
        if (flush.Count >= 5)
        {
            List<Card> straightFlush = new List<Card>();
            straightFlush = FindStraight(flush);
            if (straightFlush.Count >= 5)
            {
                straightFlush = RemoveExtraCards(straightFlush);
                Debug.Log("Straight Flush");
                bestHand = new Hand(straightFlush, HandType.StraightFlush);
                return bestHand;
            }
        }

        //checks for 4 of a kind
        List<Card> Cards4 = new List<Card>();

        foreach (List<Card> listCards in Duplicates)
        {
            if (listCards.Count == 4)
            {
                Cards4 = new List<Card>(listCards);
            }
        }

        if (Cards4.Count == 4)
        {
            Cards4 = FillInHighCards(Cards4);
            Debug.Log("4 of a Kind");
            bestHand = new Hand(Cards4, HandType.FourKind);
            return bestHand;
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
            /// two three of a kinds is jsut a full house
            if (listCards.Count >= 2 && !cards3.SequenceEqual(listCards))
            {
                pair1 = new List<Card>(listCards);
            }
        }

        if (cards3.Count == 3 && pair1.Count == 2)
        {
            //Add cards3 to pair1 so the ThreeKind is at the end of the list
            pair1.AddRange(cards3);
            pair1 = RemoveExtraCards(pair1);
            Debug.Log("Full House");
            bestHand = new Hand(pair1, HandType.FullHouse);
            return bestHand;
        }

        //checks for flush and then straight
        if (flush.Count >= 5)
        {
            flush = RemoveExtraCards(flush);
            Debug.Log("Flush");
            bestHand = new Hand(flush, HandType.Flush);
            return bestHand;
        }
        else if (straight.Count >= 5)
        {
            List<Card> sortedStraight = new List<Card>();
            sortedStraight.Add(straight[0]);
            for (int x = 0; x < straight.Count - 1; x++)
            {
                if (straight[x].value == straight[x + 1].value)
                {
                    //nothing
                }
                else
                {
                    sortedStraight.Add(straight[x + 1]);
                }
            }
            sortedStraight = RemoveExtraCards(sortedStraight);
            Debug.Log("Straight");
            bestHand = new Hand(sortedStraight, HandType.Straight);
            return bestHand;
        }
        else
        {
            //rest of hand types
            if (cards3.Count == 3)
            {
                cards3 = FillInHighCards(cards3);
                Debug.Log("3 of a Kind");
                bestHand = new Hand(cards3, HandType.ThreeKind);
                return bestHand;
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
                //add pair two to list pair 1
                pair1.AddRange(pair2);
                pair1 = FillInHighCards(pair1);
                Debug.Log("2 pair");
                bestHand = new Hand(pair1, HandType.TwoPair);
                return bestHand;
            }

            if (pair1.Count == 2)
            {
                pair1 = FillInHighCards(pair1);
                Debug.Log("Pair");
                bestHand = new Hand(pair1, HandType.Pair);
                return bestHand;
            }

            List<Card> highCard = new List<Card>();
            highCard.Add(Board[Board.Count - 1]);
            highCard.Add(Board[Board.Count - 2]);
            highCard.Add(Board[Board.Count - 3]);
            highCard.Add(Board[Board.Count - 4]);
            highCard.Add(Board[Board.Count - 5]);
            Debug.Log("High Card");
            bestHand = new Hand(highCard, HandType.HighCard);
            return bestHand;
        }
    }

    public void CompareHands()
    {
        List<Hand> winningHand = new List<Hand>();

        //foreach (Hand h in playerHands)
        //{
        //    Debug.Log("Players hand is of type = " + h.type);
        //    foreach( Card c in h.hand)
        //    {
        //        Debug.Log("cards are = " + c.value);
        //    }
        //}

        //Compare Hand Types
        winningHand.Add(playerHands[0]);

        for (int x = 1; x < playerHands.Count; x++)
        {
            if ((int)playerHands[x].type < (int)winningHand[0].type)
            {
                winningHand.Clear();
                winningHand.Add(playerHands[x]);
            }
            else if ((int)playerHands[x].type == (int)winningHand[0].type)
            {
                winningHand.Add(playerHands[x]);
            }
        }


        //foreach (Hand h in winningHand)
        //{
        //    Debug.Log("winning hand(s) is/are of type = " + h.type);
        //    foreach (Card c in h.hand)
        //    {
        //        Debug.Log("cards are = " + c.value);
        //    }
        //}

        //Compare Card Values if equivelant hand types

        List<Card> highCards = new List<Card>();
        highCards.Add(winningHand[0].hand[0]);

        Number prevCardNumber = winningHand[0].hand[0].value;
        int winningHandNumber = 0;
        int winningHandCardIndex = 0;

        bool equal = false;

        //Debug.Log("winningHand[0].hand[0] = " + winningHand[0].hand[0]);
        for (int x = 0; x < winningHand[0].hand.Count; x++)
        {
            for (int y = 1; y < winningHand.Count; y++)
            {
                Debug.Log("winningHand[y].hand[x].value = " + winningHand[y].hand[x].value + " prevCardNumber = " + prevCardNumber);
                if ((int)winningHand[y].hand[x].value > (int)prevCardNumber)
                {
                    Debug.Log("greater");
                    prevCardNumber = winningHand[y].hand[x].value;
                    winningHandNumber = y;
                    winningHandCardIndex = x;

                }
                else if ((int)winningHand[y].hand[x].value == (int)prevCardNumber)
                {
                    equal = true;
                    Debug.Log("equal");
                }
                else if((int)winningHand[y].hand[x].value < (int)prevCardNumber)
                {
                    Debug.Log("less");
                    equal = false;
                }
            }
            
            if (equal)
            {
                Debug.Log("next card");
                if (x + 1 < winningHand[0].hand.Count)
                {
                    prevCardNumber = winningHand[0].hand[x + 1].value;
                }
                else
                {
                    Debug.Log("no more cards");
                }
            }
            else
            {
                Debug.Log("break");
                break;
            }

        }

        int temp = 0;
        if (winningHand[winningHandNumber].type == HandType.HighCard)
        {
            temp = 0;
            winningHandCardIndex = 1;
            Debug.Log("Winning hand is " + winningHand[winningHandNumber].type + " " + winningHand[winningHandNumber].hand[temp].value + " with kicker " + winningHand[winningHandNumber].hand[winningHandCardIndex].value);
        }
        else if (winningHand[winningHandNumber].type == HandType.Pair)
        {
            temp = 0;
            winningHandCardIndex = 2;
            Debug.Log("Winning hand is a " + winningHand[winningHandNumber].type + " of " + winningHand[winningHandNumber].hand[temp].value + "s with kicker " + winningHand[winningHandNumber].hand[winningHandCardIndex].value);
        }
        else if (winningHand[winningHandNumber].type == HandType.TwoPair)
        {
            temp = 0;
            winningHandCardIndex = 2;
            Debug.Log("Winning hand is " + winningHand[winningHandNumber].type + " " + winningHand[winningHandNumber].hand[temp].value + "s over " + winningHand[winningHandNumber].hand[winningHandCardIndex].value + "s");
        }
        else if (winningHand[winningHandNumber].type == HandType.ThreeKind)
        {
            temp = 0;
            winningHandCardIndex = 3;
            Debug.Log("Winning hand is " + winningHand[winningHandNumber].type + " with " + winningHand[winningHandNumber].hand[temp].value + "s and a kicker " + winningHand[winningHandNumber].hand[winningHandCardIndex].value);
        }
        else if (winningHand[winningHandNumber].type == HandType.Straight)
        {
            temp = 4;
            winningHandCardIndex = 0;
            Debug.Log("Winning hand is a " + winningHand[winningHandNumber].type + " " + winningHand[winningHandNumber].hand[temp].value + " through " + winningHand[winningHandNumber].hand[winningHandCardIndex].value);
        }
        else if (winningHand[winningHandNumber].type == HandType.Flush)
        {
            temp = 4;
            Debug.Log("Winning hand is a " + winningHand[winningHandNumber].type + " HighCard " + winningHand[winningHandNumber].hand[temp].value);
        }
        else if (winningHand[winningHandNumber].type == HandType.FullHouse)
        {
            temp = 3;
            winningHandCardIndex = 0;
            Debug.Log("Winning hand is a " + winningHand[winningHandNumber].type + " with " + winningHand[winningHandNumber].hand[temp].value + "s full of " + winningHand[winningHandNumber].hand[winningHandCardIndex].value + "s");
        }
        else if (winningHand[winningHandNumber].type == HandType.FourKind)
        {
            temp = 0;
            winningHandCardIndex = 4;
            Debug.Log("Winning hand is a " + winningHand[winningHandNumber].type + " of " + winningHand[winningHandNumber].hand[temp].value + "s with a kicker " + winningHand[winningHandNumber].hand[winningHandCardIndex].value);
        }
        else if (winningHand[winningHandNumber].type == HandType.StraightFlush)
        {
            temp = 4;
            winningHandCardIndex = 0;
            Debug.Log("Winning hand is a " + winningHand[winningHandNumber].type + " " + winningHand[winningHandNumber].hand[temp].value + " through " + winningHand[winningHandNumber].hand[winningHandCardIndex].value);
        }

        
        foreach(Card c in winningHand[winningHandNumber].hand)
        {
            Debug.Log(c.value + " of " + c.suit);
        }
    }

    public void WhatIsHand()
    {

        foreach (List<Card> player in playerCards)
        {
            List<Card> flush = new List<Card>();
            List<Card> straight = new List<Card>();
            List<List<Card>> Duplicates = new List<List<Card>>();

            Board.Add(player[0]);
            Board.Add(player[1]);


            bubblesort(Board);

            flush = FindFlush();
            straight = FindStraight(straight);

            Duplicates = findDuplicates(Duplicates);

            playerHands.Add(BestHand(flush, straight, Duplicates));

            Board.Remove(player[0]);
            Board.Remove(player[1]);

        }

        //foreach (Hand h in playerHands)
        //{
        //    Debug.Log("Players hand is of type = " + h.type);
        //}
        CompareHands();
    }
}
