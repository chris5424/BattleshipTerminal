using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship
{
    public class Game
    {
        public void InitGame()
        {
            Console.SetWindowSize(50, 35);
            Player playerOne = new();
            Player playerTwo = new();       

            Random rnd = new();
            int whoseTurn = rnd.Next(1, 3);
            Coordinates shot;
            int shipIndex;

            //play till one player win
            while (!(playerOne.Lost || playerTwo.Lost))
            {
                Console.WriteLine("Plansza gracza 1");
                PrintCurrentGame(playerTwo.PlayerBoards, playerOne.PlayerBoards);
                Console.WriteLine("Plansza gracza 2");
                PrintCurrentGame(playerOne.PlayerBoards, playerTwo.PlayerBoards);
                if (whoseTurn == 1)
                {
                    shot = playerOne.GetShot();
                    playerOne.PlayerBoards.SaveShot(shot);
                    shipIndex = playerTwo.PlayerBoards.CheckFire(shot);
                    if (shipIndex>0)
                    {
                        playerTwo.PlayerShips[shipIndex - 1].ReceivedHits++;
                        Console.WriteLine("Trafiony");
                        if (playerTwo.PlayerShips[shipIndex - 1].ReceivedHits == playerTwo.PlayerShips[shipIndex - 1].Size)
                        {
                            Console.WriteLine("Zatopiony\n");
                            playerTwo.PlayerShips[shipIndex - 1].IsSunk = true; ;
                            Console.WriteLine("Tura gracza 2");
                            whoseTurn = 2;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Pudło");
                        Console.WriteLine("\nTura gracza 2");
                        whoseTurn = 2;
                    }                                           
                    playerTwo.CheckLost();
                }
                else
                {
                    shot = playerTwo.GetShot();
                    playerTwo.PlayerBoards.SaveShot(shot);
                    shipIndex = playerOne.PlayerBoards.CheckFire(shot);
                    if (shipIndex > 0)
                    {
                        playerOne.PlayerShips[shipIndex - 1].ReceivedHits++;
                        Console.WriteLine("Trafiony");
                        if (playerOne.PlayerShips[shipIndex - 1].ReceivedHits == playerOne.PlayerShips[shipIndex - 1].Size)
                        {
                            Console.WriteLine("Zatopiony\n");
                            playerOne.PlayerShips[shipIndex -1].IsSunk = true;
                            Console.WriteLine("Tura gracza 1");
                            whoseTurn = 1;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Pudło");
                        Console.WriteLine("\nTura gracza 1");
                        whoseTurn = 1;
                    }
                    playerOne.CheckLost();
                }
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
            }
            Console.WriteLine("Plansza gracza 1");
            PrintCurrentGame(playerTwo.PlayerBoards, playerOne.PlayerBoards);
            Console.WriteLine("Plansza gracza 2");
            PrintCurrentGame(playerOne.PlayerBoards, playerTwo.PlayerBoards);

            if (playerOne.Lost)
            {
                Console.WriteLine("Gra zakończona. Wygrał gracz drugi!");
            }
            else
            {
                Console.WriteLine("Gra zakończona. Wygrał gracz pierwszy!");
            }            
        }

        private void PrintCurrentGame(Board playerOneBoards, Board playerTwoBoards)
        {
            for (int i = 0; i < 45; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("");
            Console.WriteLine("|   | A | B | C | D | E | F | G | H | I | J |");
            for (int i = 1; i < 11; i++)
            {
                if (i < 10)
                {
                    Console.Write("| " + i + " | ");
                }
                else
                {
                    Console.Write("| 10| ");
                }

                for (int j = 0; j < 10; j++)
                {
                    char charToPrint = ' ';
                    if(playerOneBoards.FiresBoard[i-1, j] == 1 && playerTwoBoards.ShipsBoard[i-1, j] > 0)
                    {
                        charToPrint = 'X';
                    }
                    else if(playerTwoBoards.ShipsBoard[i-1, j] > 0)
                    {
                        charToPrint = 'S';
                    }
                    else if(playerOneBoards.FiresBoard[i-1,j] == 1)
                    {
                        charToPrint = '*';
                    }
                    Console.Write( charToPrint + " | ");                    
                }
                Console.Write("\n");
            }

            for (int i = 0; i < 45; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("");
        }
    }
}
