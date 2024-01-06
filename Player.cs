using Blackjack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Player
    {

        public List<Card> Hand { get; set; }

        public int GetHandValue()
        {
            int value = 0,acecount = 0;
            foreach (Card card in Hand)
            {
                value += card.Value;
                if(card.Face == Face.Ace ) { acecount++; }
            }
            while (value > 21){
                if(acecount>0){
                    acecount--;
                    value-=10;
                }
                else break;
            }
            return value;
        }

        public void WriteHand()
        {


            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Your Hand (" + GetHandValue() + "):");
            Console.ForegroundColor = ConsoleColor.White;
            foreach (Card card in Hand)
            {
                card.WriteDescription();
            }
            Console.WriteLine();
        }
    }
}
