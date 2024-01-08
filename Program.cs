using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading;

using Blackjack;
using static System.Collections.Specialized.BitVector32;

namespace Blackjack
{
    public class Program
    {
        private static Deck deck = new Deck();
        private static Player player = new Player();
        static bool  havingfileinput = false;
        static string[] fileinputs=new string[1000];
        private static int lineno = 0;
        private static int totallines = -1;


        private enum RoundResult
        {
            PLAYER_WIN,
            PLAYER_BUST,
            DEALERBUSTS ,
            DEALER_WIN,
            SURRENDER,
            DRAW 
        }

        static void InitializeHands()
        {
            deck.Initialize();

            player.Hand = deck.DealHand();
            Dealer.DealerCards = deck.DealHand();

            player.WriteHand();
            Dealer.WriteHand();
        }


        static void StartRound()
        {
            if(!havingfileinput ){
                Console.WriteLine("Press any key to play.");
                Console.ReadKey();
                Console.WriteLine("  STARTING NEW GAME .............");
                Console.WriteLine();
            }
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            
            

            Console.ForegroundColor = ConsoleColor.White;

            InitializeHands();
            TakeActions();

            
            
                while (Dealer.GetHandValue() < 17)
                {
                    Dealer.DealerCards.Add(deck.DrawCard());
                }
                


                if (Dealer.GetHandValue() > 21) { Terminate(RoundResult.DEALERBUSTS); }
                if (player.GetHandValue() > Dealer.GetHandValue()) { Terminate(RoundResult.PLAYER_WIN); }
                else if (player.GetHandValue() < Dealer.GetHandValue()) { Terminate(RoundResult.DEALER_WIN); }
                else if (player.GetHandValue() == Dealer.GetHandValue())
                {
                    Terminate(RoundResult.DRAW);
                }
            

            
            
        }
        // Learned taking inputrs from following documentation 
        //https://learn.microsoft.com/en-us/troubleshoot/developer/visualstudio/csharp/language-compilers/file-io-operation 
        static void TakeActions()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Valid Moves:");

            Console.WriteLine("Hit, Stand");
            Console.ForegroundColor = ConsoleColor.White;
            

            while (true)
            {
                bool wronginput = false;
                string action = null;
                if( !havingfileinput )
                Console.WriteLine("Enter action ..");
                
                while ( action == null || ( action.ToUpper() != "HIT" && action.ToUpper() != "STAND"))
                {
                        if(havingfileinput){
                            if(lineno >= totallines){
                                Console.WriteLine(" total number of lines exceeded ");
                                Environment.Exit(0);
                            }
                            action = fileinputs[lineno++];
                        }
                        else action = Console.ReadLine();
                        if (wronginput)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(" Enter Valid Action ::");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        wronginput = true;
                } 

                if (action.ToUpper() == "STAND") break;
                if(action.ToUpper() == "HIT")
                {
                    player.Hand.Add(deck.DrawCard());
                    if(player.GetHandValue()> 21){
                        Terminate(RoundResult.PLAYER_BUST);
                    }
                    player.WriteHand();
                }
            }
           

        }


        

        static void Terminate(RoundResult r)
        {
            player.WriteHand();
            Dealer.WriteHand(0);
            Console.WriteLine();
            Console.WriteLine();
            if (r == RoundResult.PLAYER_BUST) { Console.WriteLine(" Dealer wins ,player busts "); }
            else if(r == RoundResult.PLAYER_WIN) { Console.WriteLine(" player wins "); }
            else if( r == RoundResult.DEALER_WIN) { Console.WriteLine("dealer wins"); }
            else if( r== RoundResult.DEALERBUSTS) {  Console.WriteLine("Player wins Dealer Busts "); }
            else { Console.WriteLine("draw"); }
            Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Do you want to play again??");
            Console.ForegroundColor= ConsoleColor.White;
            string response;
            if (havingfileinput )
            {
                response = fileinputs[lineno++];
            } else response = Console.ReadLine();
            
            if(response.ToUpper() == "YES")
            StartRound();
            else Environment.Exit(0);
            return;
        }




        

        static void Main(string[] args)
        {
            // Console cannot render unicode characters without this line
            Console.OutputEncoding = Encoding.UTF8;

            Console.Title = "♠♥♣♦ Blackjack";

            
            string response;
            do
            {
                Console.WriteLine("  Do you want to have file inputs ???? .............");
                response = Console.ReadLine();
                if (response.ToUpper() == "YES")
                {
                    havingfileinput = true;
                    
                }
            } while (response.ToUpper() != "YES" &&  response.ToUpper() != "NO");
            if(havingfileinput)
            {
                while(true)
                {
                    bool ok = false;
                    string filename = "C:\\New folder\\File.txt";
                    StreamReader reader=new StreamReader(filename);
                    try
                    {
                        int now = 0;
                        
                        do
                        {
                            fileinputs[now++] = reader.ReadLine();
                            totallines = now;
                        }
                        while(reader.Peek()!= -1);
                        ok = true;
                    }
                    catch
                    {
                        Console.WriteLine("File is empty or no file exixts");
                    }
                    finally
                    {
                        Console.WriteLine(totallines);
                        reader.Close();
                    }
                    if(ok)break;
                }
            }
            
            StartRound();
        }
    }
}