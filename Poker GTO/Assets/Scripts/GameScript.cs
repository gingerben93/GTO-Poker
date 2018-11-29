
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour {

    public GameObject FreeCardPrefab;

    //canvas holders
    //public GameObject CommunityCardsHolder;
    public GameObject FreeCards;

    //text
    public GameObject TopText;
    public GameObject MiddleText;
    public GameObject BottomText;

    //variables
    public List<Card> deck = new List<Card>();

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
    void Start ()
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 13; y++)
            {
                deck.Add(new Card((Number)y, (Suit)x));
            }
        }

        List<Card> playerHandCards = new List<Card>();

        //7 for player hand size
        playerHandCards = MakeBoard(playerHandCards, 7);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SubmitCards();
        }

    }

    public void SubmitCards()
    {
        //disable buttons
        PlayerController.PlayerControllerSingle.SubmitButton.SetActive(false);
        PlayerController.PlayerControllerSingle.ResetButton.SetActive(false);

        List<Card> top = new List<Card>();
        List<Card> middle = new List<Card>();
        List<Card> bottom = new List<Card>();

        top = getCardsFromRows("Top");
        middle = getCardsFromRows("Middle");
        bottom = getCardsFromRows("Bottom");

        List<List<Card>> TopCards = new List<List<Card>>();
        List<List<Card>> MiddleCards = new List<List<Card>>();
        List<List<Card>> AllBotCombos = new List<List<Card>>();

        TopCards.Add(top);
        MiddleCards.Add(middle);

        List<Card> set1 = new List<Card>();
        List<Card> set2 = new List<Card>();
        List<Card> set3 = new List<Card>();
        List<Card> set4 = new List<Card>();
        List<Card> set5 = new List<Card>();
        List<Card> set6 = new List<Card>();

        set1.Add(bottom[0]);
        set1.Add(bottom[1]);

        set2.Add(bottom[0]);
        set2.Add(bottom[2]);

        set3.Add(bottom[0]);
        set3.Add(bottom[3]);

        set4.Add(bottom[1]);
        set4.Add(bottom[2]);

        set5.Add(bottom[1]);
        set5.Add(bottom[3]);

        set6.Add(bottom[2]);
        set6.Add(bottom[3]);

        AllBotCombos.Add(set1);
        AllBotCombos.Add(set2);
        AllBotCombos.Add(set3);
        AllBotCombos.Add(set4);
        AllBotCombos.Add(set5);
        AllBotCombos.Add(set6);

        List<Card> playerHandCards = new List<Card>();

        //5 for community cards
        playerHandCards = MakeBoard(playerHandCards, 5);

        TopText.GetComponent<Text>().text = WhatIsHand(playerHandCards, TopCards);
        MiddleText.GetComponent<Text>().text = WhatIsHand(playerHandCards, MiddleCards);
        BottomText.GetComponent<Text>().text = WhatIsHand(playerHandCards, AllBotCombos);
    }

    List<Card> getCardsFromRows(string rowName)
    {
        List<Card> temp = new List<Card>();

        foreach (Transform child in GameObject.Find(rowName).transform)
        {
            Number cardNumber = Number.Ace;
            Suit cardSuit = Suit.Spades;
            string PlayerCard = child.GetChild(0).name;
            string[] ssizes = PlayerCard.Split(' ');
            foreach (string s in ssizes)
            {
                try
                {
                    cardNumber = (Number)System.Enum.Parse(typeof(Number), s);
                }
                catch
                {

                }
                try
                {
                    cardSuit = (Suit)System.Enum.Parse(typeof(Suit), s);
                }
                catch
                {

                }
            }
            temp.Add(new Card(cardNumber, cardSuit));
        }
        return temp;
    }

    List<Card> MakeBoard(List<Card> Board, int boardSize)
    {
        for (int x = 0; x < boardSize; x++)
        {
            //new card
            Card newCard = deck[Random.Range(0, deck.Count)];
            deck.Remove(newCard);
            Board.Add(newCard);
            int ImageNumber = (int)newCard.value + (int)newCard.suit * 13;
            GameObject cardImage = Resources.Load(pathway + ImageNumber.ToString()) as GameObject;
            GameObject prefab = Instantiate(FreeCardPrefab, FreeCards.transform);
            prefab.GetComponent<Image>().sprite = cardImage.GetComponent<SpriteRenderer>().sprite;
            prefab.name = newCard.value + " of " + newCard.suit;
        }
        return Board;
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
            else if ((int)Board[Board.Count - 1 - x].value == 0 && (int)Board[Board.Count - 1].value == 12)
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

            if (straightCounter >= 5)
            {
                return straight;
            }
        }

        if ((int)Board[0].value == 0 && (int)Board[Board.Count - 1].value == 12)
        {
            straightCounter++;
            straight.Add(Board[Board.Count - 1]);
        }

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

    public List<Card> FillInHighCards(List<Card> Board, List<Card> PartialHand)
    {

        for (int x = Board.Count - 1; x >= 0; x--)
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
            if (PartialHand.Count == 5)
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
        //Text text = GameObject.Find("PlayerText" + textIncrement.ToString()).GetComponent<Text>();
        //checks for a straight flush
        if (flush.Count >= 5)
        {
            List<Card> straightFlush = new List<Card>();
            flush = bubblesort(flush);
            straightFlush = FindStraight(flush);
            if (straightFlush.Count >= 5)
            {
                //straightFlush = RemoveExtraCards(straightFlush);

                //text.text = "Straight Flush";

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
            //text.text = "4 of a Kind";
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
            //text.text = "Full House";
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
            //text.text = "Flush";

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

            //text.text = "Straight";

            //sortedStraight = RemoveExtraCards(sortedStraight);
            bestHand = new Hand(sortedStraight, HandType.Straight);
            return bestHand;
        }
        else
        {
            //rest of hand types
            if (cards3.Count == 3)
            {
                //text.text = "3 of a Kind";

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
                //text.text = "2 Pair";

                //add pair two to list pair 1
                pair1.AddRange(pair2);
                pair1 = FillInHighCards(Board, pair1);
                bestHand = new Hand(pair1, HandType.TwoPair);
                return bestHand;
            }

            if (pair1.Count == 2)
            {
                //text.text = "Pair";

                pair1 = FillInHighCards(Board, pair1);
                bestHand = new Hand(pair1, HandType.Pair);
                return bestHand;
            }

            //text.text = "High Card";

            List<Card> highCard = new List<Card>();

            for (int x = 0; x < 5; x++)
            {
                highCard.Add(Board[Board.Count - 1 - x]);
            }
            bestHand = new Hand(highCard, HandType.HighCard);
            return bestHand;
        }
    }

    public string CompareHands(List<Hand> playerHands)
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
                else if ((int)winningHand[y].hand[x].value < (int)prevCardNumber)
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
            return winningHand[winningHandNumber].type + " " + 
                winningHand[winningHandNumber].hand[0].value + " " + 
                winningHand[winningHandNumber].hand[1].value + " " + 
                winningHand[winningHandNumber].hand[2].value + " " + 
                winningHand[winningHandNumber].hand[3].value + " " + 
                winningHand[winningHandNumber].hand[4].value;
        }
        else if (winningHand[winningHandNumber].type == HandType.Pair)
        {
            temp = 0;
            winningHandCardIndex = 2;
            return winningHand[winningHandNumber].type + " of " + 
                winningHand[winningHandNumber].hand[temp].value + "s with kickers " +
                winningHand[winningHandNumber].hand[2].value + " " +
                winningHand[winningHandNumber].hand[3].value + " " + 
                winningHand[winningHandNumber].hand[4].value; ;
        }
        else if (winningHand[winningHandNumber].type == HandType.TwoPair)
        {
            temp = 0;
            winningHandCardIndex = 2;
            return winningHand[winningHandNumber].type + " " + 
                winningHand[winningHandNumber].hand[temp].value + "s over " + 
                winningHand[winningHandNumber].hand[winningHandCardIndex].value + "s" + " with kicker " + 
                winningHand[winningHandNumber].hand[4].value;
        }
        else if (winningHand[winningHandNumber].type == HandType.ThreeKind)
        {
            temp = 0;
            return winningHand[winningHandNumber].type + " with " + 
                winningHand[winningHandNumber].hand[temp].value + " with kickers " + 
                winningHand[winningHandNumber].hand[3].value + " " + 
                winningHand[winningHandNumber].hand[4].value;
        }
        else if (winningHand[winningHandNumber].type == HandType.Straight)
        {
            temp = 4;
            winningHandCardIndex = 0;
            return winningHand[winningHandNumber].type + " " + 
                winningHand[winningHandNumber].hand[temp].value + " through " + 
                winningHand[winningHandNumber].hand[winningHandCardIndex].value;
        }
        else if (winningHand[winningHandNumber].type == HandType.Flush)
        {
            temp = 0;
            return winningHand[winningHandNumber].hand[0].suit  + " " + 
                winningHand[winningHandNumber].type + " " + 
                winningHand[winningHandNumber].hand[0].value + " " + 
                winningHand[winningHandNumber].hand[1].value +" " + 
                winningHand[winningHandNumber].hand[2].value + " " + 
                winningHand[winningHandNumber].hand[3].value + " " + 
                winningHand[winningHandNumber].hand[4].value;
        }
        else if (winningHand[winningHandNumber].type == HandType.FullHouse)
        {
            temp = 3;
            winningHandCardIndex = 0;
            return winningHand[winningHandNumber].type + " " +
                winningHand[winningHandNumber].hand[winningHandCardIndex].value + "s full of " +
                winningHand[winningHandNumber].hand[temp].value + "s";
        }
        else if (winningHand[winningHandNumber].type == HandType.FourKind)
        {
            temp = 0;
            winningHandCardIndex = 4;
            return winningHand[winningHandNumber].type + " of " + 
                winningHand[winningHandNumber].hand[temp].value + "s with a kicker " + 
                winningHand[winningHandNumber].hand[winningHandCardIndex].value;
        }
        else if (winningHand[winningHandNumber].type == HandType.StraightFlush)
        {
            temp = 4;
            winningHandCardIndex = 0;
            return winningHand[winningHandNumber].type + " " + 
                winningHand[winningHandNumber].hand[temp].value + " through " + 
                winningHand[winningHandNumber].hand[winningHandCardIndex].value;
        }

        return winningHand[winningHandNumber].type + " " + 
            winningHand[winningHandNumber].hand[0].value + " with kickers " + 
            winningHand[winningHandNumber].hand[1].value + " " + 
            winningHand[winningHandNumber].hand[2].value + " " + 
            winningHand[winningHandNumber].hand[3].value + " " + 
            winningHand[winningHandNumber].hand[4].value;
    }

    public string WhatIsHand(List<Card> Board, List<List<Card>> playerCards)
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

            foreach(Card c in player)
            {
                BoardAndPlayer.Add(c);
            }
            //BoardAndPlayer.Add(player[0]);
            //BoardAndPlayer.Add(player[1]);

            BoardAndPlayer = bubblesort(BoardAndPlayer);

            flush = FindFlush(BoardAndPlayer);
            straight = FindStraight(BoardAndPlayer);

            Duplicates = findDuplicates(BoardAndPlayer);

            playerHands.Add(BestHand(BoardAndPlayer, flush, straight, Duplicates, textIncrement));
            textIncrement++;
        }
            return CompareHands(playerHands);
    }

}
