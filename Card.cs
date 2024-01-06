using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Blackjack.Suit;
using static Blackjack.Face;

namespace Blackjack
{
    public enum Suit
    {
        Clubs,
        Spades,
        Diamonds,
        Hearts
    }
    public enum Face
    {
        Ace,
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
        King
    }
    //The card class was adapted from this article
    //https://codereview.stackexchange.com/questions/202483/c-singleplayer-blackjack-game

    public class Card
    {
        public Suit Suit { get; }
        public Face Face { get; }
        public int Value { get; set; }
        public char Symbol { get; }
 
        public Card(Suit suit, Face face)
        {
            Suit = suit;
            Face = face;

            switch (Suit)
            {
                case Clubs:
                    Symbol = '♣';
                    break;
                case Spades:
                    Symbol = '♠';
                    break;
                case Diamonds:
                    Symbol = '♦';
                    break;
                case Hearts:
                    Symbol = '♥';
                    break;
            }
            switch (Face)
            {
                case Ten:
                case Jack:
                case Queen:
                case King:
                    Value = 10;
                    break;
                case Ace:
                    Value = 11;
                    break;
                default:
                    Value = (int)Face + 1;
                    break;
            }
        }

        public void WriteDescription()
        {
            if (Suit == Suit.Diamonds || Suit == Suit.Hearts)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }

            if (Face == Ace)
            {
                Console.WriteLine(Symbol + " Soft " + Face + " of " + Suit);
            }
            else
            {
                Console.WriteLine(Symbol + " " + Face + " of " + Suit);
            }
        }
    }
}