using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HandTracker : MonoBehaviour
{

    //know bugs
    // will pick A-5 staight over A-6 straight if A-5 hand comes first

    //game prefabs
    public GameObject CardPrefab;
    public GameObject playerPrefab;

    //canvas holders
    public GameObject CommunityCards;
    public GameObject PlayerCards;

    public List<Card> deck = new List<Card>();
    bool firstRun = true;
    int numPlayers = 5;

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

    // Use this for initialization
    void Start()
    {
        for (int x = 0; x < 4; x++)
        {
            for(int y = 0; y < 13; y++)
            {
                deck.Add(new Card((Number)y, (Suit)x));

                //places all 52 cards
                //ImageNumber = y + x * 13;
                //cardImage = Resources.Load(pathway + ImageNumber.ToString()) as GameObject;
                //prefab = Instantiate(cardimagetest);
                //prefab.GetComponent<SpriteRenderer>().sprite = cardImage.GetComponent<SpriteRenderer>().sprite;
                //prefab.transform.position = new Vector3(y, 5.2f-(x*1.1f), 0);
                //prefab.name = (Number)y + " of " + (Suit)x;
            }
        }

        List<Card> communityCards = new List<Card>();

        //player cards
        List<List<Card>> playerCards = new List<List<Card>>();

        communityCards = MakeBoard(communityCards);

        //enter number of players
        playerCards = NumberPlayers(playerCards, numPlayers);

        //check who won
        WhatIsHand(communityCards, playerCards);
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (Transform holder in CommunityCards.transform)
            {
                Destroy(holder.gameObject);
            }
            //foreach (Transform holder in PlayerCards.transform)
            //{
            //    Destroy(holder.gameObject);
            //}

            deck = new List<Card>();

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 13; y++)
                {
                    deck.Add(new Card((Number)y, (Suit)x));

                    //places all 52 cards
                    //ImageNumber = y + x * 13;
                    //cardImage = Resources.Load(pathway + ImageNumber.ToString()) as GameObject;
                    //prefab = Instantiate(cardimagetest);
                    //prefab.GetComponent<SpriteRenderer>().sprite = cardImage.GetComponent<SpriteRenderer>().sprite;
                    //prefab.transform.position = new Vector3(y, 5.2f-(x*1.1f), 0);
                    //prefab.name = (Number)y + " of " + (Suit)x;
                }
            }

            List<Card> Board = new List<Card>();
            //player cards
            List<List<Card>> playerCards = new List<List<Card>>();
            Board = MakeBoard(Board);

            //enter number of players
            playerCards = NumberPlayers(playerCards, numPlayers);

            //check who won
            WhatIsHand(Board, playerCards);
        }
    }

    List<Card> MakeBoard(List<Card> Board)
    {
        //4 of spades
        Card newCard = deck[0];
        deck.Remove(newCard);
        Board.Add(newCard);
        int ImageNumber = (int)newCard.value + (int)newCard.suit * 13;
        GameObject cardImage = Resources.Load(pathway + ImageNumber.ToString()) as GameObject;
        GameObject prefab = Instantiate(CardPrefab, CommunityCards.transform);
        prefab.GetComponent<Image>().sprite = cardImage.GetComponent<SpriteRenderer>().sprite;
        prefab.name = newCard.value + " of " + newCard.suit;

        //Debug.Log("Card " + 0 + " is " + newCard.value + " " + newCard.suit);

        //5 of spades
        newCard = deck[0];
        deck.Remove(newCard);
        Board.Add(newCard);
        ImageNumber = (int)newCard.value + (int)newCard.suit * 13;
        cardImage = Resources.Load(pathway + ImageNumber.ToString()) as GameObject;
        prefab = Instantiate(CardPrefab, CommunityCards.transform);
        prefab.GetComponent<Image>().sprite = cardImage.GetComponent<SpriteRenderer>().sprite;
        prefab.name = newCard.value + " of " + newCard.suit;

        //Debug.Log("Card " + 1 + " is " + newCard.value + " " + newCard.suit);

        //7 of spades
        newCard = deck[0];
        deck.Remove(newCard);
        Board.Add(newCard);
        ImageNumber = (int)newCard.value + (int)newCard.suit * 13;
        cardImage = Resources.Load(pathway + ImageNumber.ToString()) as GameObject;
        prefab = Instantiate(CardPrefab, CommunityCards.transform);
        prefab.GetComponent<Image>().sprite = cardImage.GetComponent<SpriteRenderer>().sprite;
        prefab.name = newCard.value + " of " + newCard.suit;

        //Debug.Log("Card " + 2 + " is " + newCard.value + " " + newCard.suit);

        //Ace of Clubs
        newCard = deck[13];
        deck.Remove(newCard);
        Board.Add(newCard);
        ImageNumber = (int)newCard.value + (int)newCard.suit * 13;
        cardImage = Resources.Load(pathway + ImageNumber.ToString()) as GameObject;
        prefab = Instantiate(CardPrefab, CommunityCards.transform);
        prefab.GetComponent<Image>().sprite = cardImage.GetComponent<SpriteRenderer>().sprite;
        prefab.name = newCard.value + " of " + newCard.suit;

        //Debug.Log("Card " + 3 + " is " + newCard.value + " " + newCard.suit);

        //Jack of Hearts
        newCard = deck[20];
        deck.Remove(newCard);
        Board.Add(newCard);
        ImageNumber = (int)newCard.value + (int)newCard.suit * 13;
        cardImage = Resources.Load(pathway + ImageNumber.ToString()) as GameObject;
        prefab = Instantiate(CardPrefab, CommunityCards.transform);
        prefab.GetComponent<Image>().sprite = cardImage.GetComponent<SpriteRenderer>().sprite;
        prefab.name = newCard.value + " of " + newCard.suit;

        //Debug.Log("Card " + 4 + " is " + newCard.value + " " + newCard.suit);


        //for (int x = 0; x < 5; x++)
        //{
        //    //new card
        //    Card newCard = deck[Random.Range(0, deck.Count)];
        //    deck.Remove(newCard);
        //    Board.Add(newCard);
        //    int ImageNumber = (int)newCard.value + (int)newCard.suit * 13;
        //    GameObject cardImage = Resources.Load(pathway + ImageNumber.ToString()) as GameObject;
        //    GameObject prefab = Instantiate(CardPrefab, CommunityCards.transform);
        //    prefab.GetComponent<Image>().sprite = cardImage.GetComponent<SpriteRenderer>().sprite;
        //    prefab.name = newCard.value + " of " + newCard.suit;
        //}
        return Board;
    }

    public List<List<Card>> NumberPlayers(List<List<Card>> playerCards, int num)
    {
        for (int x = 0; x < num; x++)
        {
            //create holder for cards

            GameObject playerCardHolder;
            if (firstRun)
            {
                playerCardHolder = Instantiate(playerPrefab, PlayerCards.transform);
                playerCardHolder.name = "player " + x;
            }
            else
            {
                playerCardHolder = GameObject.Find("player " + x);
            }
            

            var CardImages = playerCardHolder.GetComponentsInChildren<Image>();

            //create text box
            GameObject text = playerCardHolder.GetComponentInChildren<Text>().gameObject;
            text.transform.localPosition = new Vector3(.5f, -1, 0);
            text.name = "PlayerText" + x;

            List<Card> currentPlayer = new List<Card>();

            //card 1
            Card player1card1 = deck[Random.Range(0, deck.Count)];
            deck.Remove(player1card1);
            //load image 1
            int ImageNumber1 = (int)player1card1.value + (int)player1card1.suit * 13;
            GameObject cardImage = Resources.Load(pathway + ImageNumber1.ToString()) as GameObject;
            CardImages[1].GetComponent<Image>().sprite = cardImage.GetComponent<SpriteRenderer>().sprite;
            CardImages[1].name = player1card1.value + " of " + player1card1.suit;

            //card2
            Card player1card2 = deck[Random.Range(0, deck.Count)];
            deck.Remove(player1card2);
            //load image 2
            int ImageNumber2 = (int)player1card2.value + (int)player1card2.suit * 13;
            cardImage = Resources.Load(pathway + ImageNumber2.ToString()) as GameObject;
            CardImages[2].GetComponent<Image>().sprite = cardImage.GetComponent<SpriteRenderer>().sprite;
            CardImages[2].name = player1card2.value + " of " + player1card2.suit;

            //    Debug.Log(player1card1.value + " " + player1card1.suit);
            //    Debug.Log(player1card2.value + " " + player1card2.suit);

            //add to list of players
            currentPlayer.Add(player1card1);
            currentPlayer.Add(player1card2);

            playerCards.Add(currentPlayer);
        }

        firstRun = false;
        return playerCards;
    }

    public List<Card> FindFlush(List<Card> Board)
    {
        List<List<Card>> flushes = new List<List<Card>>();
        flushes.Add(new List<Card>());
        flushes.Add(new List<Card>());
        flushes.Add(new List<Card>());
        flushes.Add(new List<Card>());

        for (int x = 0; x < Board.Count; x++)
        {
            if (Board[Board.Count - 1 - x].suit == Suit.Spades)
            {
                flushes[0].Add(Board[Board.Count - 1 - x]);
            }
            else if (Board[Board.Count - 1 - x].suit == Suit.Clubs)
            {
                flushes[1].Add(Board[Board.Count - 1 - x]);
            }
            else if (Board[Board.Count - 1 - x].suit == Suit.Hearts)
            {
                flushes[2].Add(Board[Board.Count - 1 - x]);
            }
            //diamonds
            else
            {
                flushes[3].Add(Board[Board.Count - 1 - x]);
            }
        }

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
    public List<Card> FindStraight(List<Card> Board)
    {
        int straightCounter = 1;
        List<Card> straight = new List<Card>();
        straight.Add(Board[Board.Count - 1]);

        for (int x = 0; x < Board.Count - 1; x++)
        {
            //minus 1 for current card an minus two for next card
            // next card.value minus one to check for card being one less
            if ((int)Board[Board.Count - 1 - x].value - 1 == (int)Board[Board.Count - 2 - x].value)
            {
                straightCounter++;
                straight.Add(Board[Board.Count - 2 - x]);
            }
            // next card.value same to check for another card that is same value as
            else if ((int)Board[Board.Count - 1 - x].value == (int)Board[Board.Count - 2 - x].value)
            {
                //do nothing if card value is the same
            }
            else if((int)Board[Board.Count - 1 - x].value == 0 && (int)Board[Board.Count - 1].value == 12)
            {
                straightCounter++;
                straight.Add(Board[Board.Count - 1]);
            }
            else
            {
                straightCounter = 1;
                straight.Clear();
                straight.Add(Board[Board.Count - 2 - x]);
            }

            if(straightCounter >= 5)
            {
                return straight;
            }
        }

        if ((int)Board[0].value == 0 && (int)Board[Board.Count - 1].value == 12)
        {
            straightCounter++;
            straight.Add(Board[Board.Count - 1]);
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

    public List<List<Card>> findDuplicates(List<Card> Board)
    {
        List<Card> TypesOfCards = new List<Card>();
        List<List<Card>> Duplicates = new List<List<Card>>();

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

    List<Card> bubblesort(List<Card> a)
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
        return a;
    }

    //public List<Card> RemoveExtraCards(List<Card> Hand)
    //{
    //    while(Hand.Count > 5)
    //    {
    //        Hand.RemoveAt(0);
    //    }
    //    return Hand;
    //}

    public List<Card> FillInHighCards(List<Card> Board, List<Card> PartialHand)
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
    Hand BestHand(List<Card> Board, List<Card> flush, List<Card> straight, List<List<Card>> Duplicates, int textIncrement)
    {
        Hand bestHand;
        //print("player " + textIncrement.ToString());
        Text text = GameObject.Find("PlayerText" + textIncrement.ToString()).GetComponent<Text>();
        //checks for a straight flush
        if (flush.Count >= 5)
        {
            List<Card> straightFlush = new List<Card>();
            flush = bubblesort(flush);
            straightFlush = FindStraight(flush);
            if (straightFlush.Count >= 5)
            {
                //straightFlush = RemoveExtraCards(straightFlush);

                text.text = "Straight Flush";

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
            text.text = "4 of a Kind";

            Cards4 = FillInHighCards(Board, Cards4);
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
            text.text = "Full House";
            //Add pair1 to cards3 so the ThreeKind is at the start of the list
            if (cards3.Count == 6)
            {
                cards3.RemoveAt(5);
            }
            cards3.AddRange(pair1);
            bestHand = new Hand(cards3, HandType.FullHouse);
            return bestHand;
        }

        //checks for flush and then straight
        if (flush.Count >= 5)
        {
            text.text = "Flush";

            //flush = RemoveExtraCards(flush);
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

            text.text = "Straight";

            //sortedStraight = RemoveExtraCards(sortedStraight);
            bestHand = new Hand(sortedStraight, HandType.Straight);
            return bestHand;
        }
        else
        {
            //rest of hand types
            if (cards3.Count == 3)
            {
                text.text = "3 of a Kind";

                cards3 = FillInHighCards(Board, cards3);
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
                text.text = "2 Pair";

                //add pair two to list pair 1
                pair1.AddRange(pair2);
                pair1 = FillInHighCards(Board, pair1);
                bestHand = new Hand(pair1, HandType.TwoPair);
                return bestHand;
            }

            if (pair1.Count == 2)
            {
                text.text = "Pair";

                pair1 = FillInHighCards(Board, pair1);
                bestHand = new Hand(pair1, HandType.Pair);
                return bestHand;
            }

            text.text = "High Card";

            List<Card> highCard = new List<Card>();

            for(int x = 0; x < 5; x++)
            {
                highCard.Add(Board[Board.Count - 1 - x]);
            }
            //highCard.Add(Board[Board.Count - 1]);
            //highCard.Add(Board[Board.Count - 2]);
            //highCard.Add(Board[Board.Count - 3]);
            //highCard.Add(Board[Board.Count - 4]);
            //highCard.Add(Board[Board.Count - 5]);
            bestHand = new Hand(highCard, HandType.HighCard);
            return bestHand;
        }
    }

    void FindWinningHandType(List<Hand> playerHands, out HandType winningHandType)
    {
        winningHandType = HandType.HighCard;
        for (int x = 0; x < playerHands.Count; x++)
        {
            if ((int)winningHandType < (int)playerHands[x].type)
            {
                winningHandType = playerHands[x].type;
            }
        }
    }

    List<int> FindNumberWinningPlayers(HandType winningHandType, List<Hand> playerHands)
    {
        List<int> winningTypePlayerIndex = new List<int>();

        //find hands that have the same winning type
        for (int x = 0; x < playerHands.Count; x++)
        {
            if ((int)winningHandType == (int)playerHands[x].type)
            {
                winningTypePlayerIndex.Add(x);
            }
        }

        return winningTypePlayerIndex;
    }

    void CompareWinningHands()
    {

    }

    public void CompareHands(List<Hand> playerHands)
    {
        //foreach (Hand h in playerHands)
        //{
        //    Debug.Log("Players hand is of type = " + h.type);
        //    foreach (Card c in h.hand)
        //    {
        //        Debug.Log("cards are = " + c.value);
        //    }
        //}

        List<Hand> winningHand = new List<Hand>();
        //List<int> winningTypePlayerIndex = new List<int>();
        //HandType winningHandType;

        //FindWinningHandType(playerHands, out winningHandType);
        //winningTypePlayerIndex = FindNumberWinningPlayers(winningHandType, playerHands);

        //int currentValue = 0;
        //for (int x = 0; x < playerHands[winningTypePlayerIndex[0]].hand.Count; x++)
        //{
        //    currentValue = 0;
        //    for (int y = 0; y < winningTypePlayerIndex.Count; y++)
        //    {
        //        if(currentValue >= (int)playerHands[winningTypePlayerIndex[y]].hand[x].value)
        //        {
        //            currentValue = (int)playerHands[winningTypePlayerIndex[y]].hand[x].value;
        //        }

        //    }
        //    if (winningTypePlayerIndex.Count == 1)
        //    {

        //    }
        //}

        //compare winning hands


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
                //Debug.Log("winningHand[y].hand[x].value = " + winningHand[y].hand[x].value + " prevCardNumber = " + prevCardNumber);
                if ((int)winningHand[y].hand[x].value > (int)prevCardNumber)
                {
                    //Debug.Log("greater");
                    prevCardNumber = winningHand[y].hand[x].value;
                    winningHandNumber = y;
                    winningHandCardIndex = x;

                }
                else if ((int)winningHand[y].hand[x].value == (int)prevCardNumber)
                {
                    equal = true;
                   // Debug.Log("equal");
                }
                else if((int)winningHand[y].hand[x].value < (int)prevCardNumber)
                {
                   // Debug.Log("less");
                    equal = false;
                }
            }
            
            if (equal)
            {
                //Debug.Log("next card");
                if (x + 1 < winningHand[0].hand.Count)
                {
                    prevCardNumber = winningHand[0].hand[x + 1].value;
                }
                else
                {
                   // Debug.Log("no more cards");
                }
            }
            else
            {
                //Debug.Log("break");
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
            temp = 0;
            Debug.Log("Winning hand is a " +
                winningHand[winningHandNumber].type);
            foreach (Card c in winningHand[winningHandNumber].hand)
            {
                print(c.value);
            }
        }
        else if (winningHand[winningHandNumber].type == HandType.FullHouse)
        {
            temp = 3;
            winningHandCardIndex = 0;
            Debug.Log("Winning hand is a " + 
                winningHand[winningHandNumber].type + " with " + 
                winningHand[winningHandNumber].hand[winningHandCardIndex].value + "s full of " + 
                winningHand[winningHandNumber].hand[temp].value + "s");
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

        
        //foreach(Card c in winningHand[winningHandNumber].hand)
        //{
        //    //Debug.Log(c.value + " of " + c.suit);
        //}
    }

    public void WhatIsHand(List<Card> Board, List<List<Card>> playerCards)
    {
        //Player hands
        List<Hand> playerHands = new List<Hand>();

        int textIncrement = 0;
        foreach (List<Card> player in playerCards)
        {
            List<Card> BoardAndPlayer = new List<Card>(Board);
            List<Card> flush = new List<Card>();
            List<Card> straight = new List<Card>();
            List<List<Card>> Duplicates = new List<List<Card>>();

            BoardAndPlayer.Add(player[0]);
            BoardAndPlayer.Add(player[1]);

            BoardAndPlayer = bubblesort(BoardAndPlayer);

            flush = FindFlush(BoardAndPlayer);
            straight = FindStraight(BoardAndPlayer);

            Duplicates = findDuplicates(BoardAndPlayer);

            playerHands.Add(BestHand(BoardAndPlayer, flush, straight, Duplicates, textIncrement));
            textIncrement++;
        }

        //foreach (Hand h in playerHands)
        //{
        //    Debug.Log("Players hand is of type = " + h.type);
        //}
        CompareHands(playerHands);
    }
}
