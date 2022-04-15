using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship
{
    public class Board
    {        
        public int[,] ShipsBoard { get; set; }
        public int[,] FiresBoard { get; set; }
        private int[,] _occupancyBoard { get; set; }

        public Board(int boardSize)
        {
            ShipsBoard = new int[boardSize, boardSize];
            _occupancyBoard = new int[boardSize, boardSize];
            FiresBoard = new int[boardSize, boardSize];            
        }

        private bool CheckOccupancy(int startRow, int startColumn, bool direction, int shipSize)
        {
            //check occupancy
            if (direction)
            {
                for (int i = 0; i < shipSize; i++)
                {
                    if (_occupancyBoard[startRow, startColumn + i] > 0)
                    {
                        return true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < shipSize; i++)
                {
                    if (_occupancyBoard[startRow + i, startColumn] > 0)
                    {                       
                        return true;
                    }
                }
            }
            return false;
        }

        public void PlaceShipsRandomOnBoard(List<Ship> ShipsToPlace)
        {
            Random rnd = new();
            int startColumn, startRow;
            int endColumn, endRow;
            bool direction; // 0 - horizontal, 1 - vertical

            foreach (var ship in ShipsToPlace)
            {
                int shipSize = ship.Size;
                bool isPlaced = false;
                while (!isPlaced)
                {
                    //draw starting coordinates
                    startColumn = rnd.Next(0, 10);
                    startRow = rnd.Next(0, 10);
                    direction = Convert.ToBoolean(rnd.Next(0, 2));
                    endRow = startRow + shipSize;
                    endColumn = startColumn + shipSize;

                    //check boundaries
                    if (direction && (endColumn) > 10)
                    {                       
                        continue;
                    }
                    else if (!direction && endRow > 10)
                    {
                        continue;
                    }

                    if (CheckOccupancy(startRow, startColumn, direction, shipSize))
                        continue;

                    //place ship and occupy fields around it
                    isPlaced = true;
                    if (direction)
                    {
                        for (int i = 0; i < shipSize; i++)
                        {
                            ShipsBoard[startRow, startColumn + i] = ShipsToPlace.IndexOf(ship)+1;
                            _occupancyBoard[startRow, startColumn + i] = 1;

                            //make occupied border around ship - horizontal ships
                            if (i == 0)
                            {
                                if (startRow > 0)
                                {
                                    _occupancyBoard[startRow - 1, startColumn] = 2;
                                }
                                if (startColumn > 0)
                                {
                                    _occupancyBoard[startRow, startColumn - 1] = 2;
                                }
                                if (startColumn > 0 && startRow > 0)
                                {
                                    _occupancyBoard[startRow - 1, startColumn - 1] = 2;
                                }
                                if (startRow < 9 && startColumn > 0)
                                {
                                    _occupancyBoard[startRow + 1, startColumn - 1] = 2;
                                }
                                if (startRow < 9)
                                {
                                    _occupancyBoard[startRow + 1, startColumn] = 2;
                                }
                            }
                            else if (i == shipSize - 1)
                            {
                                if (endColumn < 10)
                                {
                                    _occupancyBoard[startRow, endColumn] = 2;
                                }
                                if (endColumn < 10 && startRow > 0)
                                {
                                    _occupancyBoard[startRow - 1, endColumn] = 2;
                                }
                                if (endColumn < 10 && startRow < 9)
                                {
                                    _occupancyBoard[startRow + 1, endColumn] = 2;
                                }

                                if (startRow > 0 && endColumn - 1 < 10)
                                {
                                    _occupancyBoard[startRow - 1, endColumn - 1] = 2;
                                }
                                if (startRow < 9 && endColumn - 1 < 10)
                                {
                                    _occupancyBoard[startRow + 1, endColumn - 1] = 2;
                                }

                                if (startRow < 9 && endColumn - 1 < 10)
                                {
                                    _occupancyBoard[startRow + 1, endColumn - 1] = 2;
                                }
                                if (startRow > 0 && endColumn - 1 < 10)
                                {
                                    _occupancyBoard[startRow - 1, endColumn - 1] = 2;
                                }
                            }
                            else
                            {
                                if (startRow > 0)
                                {
                                    _occupancyBoard[startRow - 1, startColumn + i] = 2;
                                }
                                if (startRow < 9)
                                {
                                    _occupancyBoard[startRow + 1, startColumn + i] = 2;
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < shipSize; i++)
                        {
                            ShipsBoard[startRow + i, startColumn] = ShipsToPlace.IndexOf(ship)+1;
                            _occupancyBoard[startRow + i, startColumn] = 1;

                            //make occupied border around ship - vertical ships
                            if (i == 0)
                            {
                                if (startColumn > 0)
                                {
                                    _occupancyBoard[startRow, startColumn - 1] = 2;
                                }
                                if (startRow > 0)
                                {
                                    _occupancyBoard[startRow - 1, startColumn] = 2;
                                }
                                if (startColumn > 0 && startRow > 0)
                                {
                                    _occupancyBoard[startRow - 1, startColumn - 1] = 2;
                                }
                                if (startColumn < 9 && startRow > 0)
                                {
                                    _occupancyBoard[startRow - 1, startColumn + 1] = 2;
                                }
                                if (startColumn < 9)
                                {
                                    _occupancyBoard[startRow, startColumn + 1] = 2;
                                }
                            }
                            else if (i == shipSize - 1)
                            {
                                if (endRow < 10)
                                {
                                    _occupancyBoard[endRow, startColumn] = 2;
                                }
                                if (startColumn > 0 && endRow < 10)
                                {
                                    _occupancyBoard[endRow, startColumn - 1] = 2;
                                }
                                if (startColumn < 9 && endRow < 10)
                                {
                                    _occupancyBoard[endRow, startColumn + 1] = 2;
                                }

                                if (endRow - 1 < 10 && startColumn > 0)
                                {
                                    _occupancyBoard[endRow - 1, startColumn - 1] = 2;
                                }
                                if (endRow - 1 < 10 && startColumn < 9)
                                {
                                    _occupancyBoard[endRow - 1, startColumn + 1] = 2;
                                }

                                if (endRow - 1 < 10 && startColumn < 9)
                                {
                                    _occupancyBoard[endRow - 1, startColumn + 1] = 2;
                                }
                                if (endRow - 1 < 10 && startColumn > 0)
                                {
                                    _occupancyBoard[endRow - 1, startColumn - 1] = 2;
                                }
                            }
                            else
                            {
                                if (startColumn > 0)
                                {
                                    _occupancyBoard[startRow + i, startColumn - 1] = 2;
                                }

                                if (startColumn < 9)
                                {
                                    _occupancyBoard[startRow + i, startColumn + 1] = 2;
                                }
                            }
                        }
                    }
                }
            }
            _occupancyBoard = null;            
        }

        public int CheckFire(Coordinates shot)
        {
            if (ShipsBoard[shot.Row, shot.Column] > 0)
            {                
                return ShipsBoard[shot.Row, shot.Column];
            }                
            else
                return 0;
        }

        public void SaveShot(Coordinates shot)
        {
            FiresBoard[shot.Row, shot.Column] = 1;
        }

        public void PrintBoard()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(FiresBoard[i, j] + " ");
                }
                Console.WriteLine("");
            }
        }
    }
}
