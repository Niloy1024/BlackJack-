using Blackjack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack 
{
    public class Dealer
    {
        public static List<Card> DealerCards { get; set; } = new List<Card>();
        

        public static int GetHandValue()
        {
            int value = 0,acecount = 0;
            foreach (Card card in DealerCards)
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

        public static void WriteHand(int  ongoinggame = 1 )
        {
            Console.ForegroundColor = ConsoleColor.Green;
            
            if (ongoinggame == 0)
            {
                Console.WriteLine("Dealers Hand (" + GetHandValue() + "):");
            }
            else Console.WriteLine("Dealers  Hand ");
            Console.ForegroundColor = ConsoleColor.White;

            for (int i = 0; i < DealerCards.Count - (ongoinggame&1 ); i++)
            {
                DealerCards[i].WriteDescription();
            }
            if(ongoinggame == 1 )Console.WriteLine("<hidden>");

        }
    }
}
