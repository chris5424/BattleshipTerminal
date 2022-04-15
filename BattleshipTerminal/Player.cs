using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship
{
    public class Player
    {
        public Coordinates ShotCoordinates { get; set; }
        public List<Ship> PlayerShips { get; set; }
        public List<Coordinates> _boardFields { get; set; }
        public Board PlayerBoards;
        public bool Lost { get; set; }
        public Player()
        {
            PlayerBoards = new(10);
            PlayerShips = new();

            PlayerShips.Add(new Ship { Size = 5, Name = "Carrier" });
            PlayerShips.Add(new Ship { Size = 4, Name = "Battleship" });
            PlayerShips.Add(new Ship { Size = 3, Name = "Cruiser" });
            PlayerShips.Add(new Ship { Size = 3, Name = "Submarine" });
            PlayerShips.Add(new Ship { Size = 2, Name = "Destroyer" });

            PlayerBoards.PlaceShipsRandomOnBoard(PlayerShips);
            _boardFields = new();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    _boardFields.Add(new Coordinates { Row = i, Column = j });
                }
            }
        }
        public Coordinates GetShot()
        {
            Coordinates shotCoords;
            Random rnd = new();

            int shotIndex = rnd.Next(0, _boardFields.Count);
            shotCoords = _boardFields[shotIndex];
            this._boardFields.RemoveAt(shotIndex);
            return shotCoords;
        }

        public void CheckLost()
        {
            Lost = true;
            foreach (var item in PlayerShips)
            {
                if (!item.IsSunk)
                {
                    Lost = false;
                }
            }
        }
    }
}
