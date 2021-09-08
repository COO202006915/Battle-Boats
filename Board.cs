using System;
using System.Collections.Generic;

namespace BattleBoats
{

    class Board
    {
        private Random _random = new Random();
        private string _letters = "ABCDEFGH";
        private string _numbers = "12345678";

        private List<string> _comBoats = new List<string>();
        private List<string> _playerBoats = new List<string>();
        private List<string> _comHitCoords = new List<string>();
        private List<string> _hitCoords = new List<string>();

        public Board()
        {
            int comBoats = 1;
            while (comBoats != 6)
            {
                string coords = GetRandomCoords();

                // Check if coords are already used
                bool used = false;
                foreach (string comBoat in _comBoats)
                {
                    if (comBoat == coords)
                        used = true;
                }

                if (used)
                {
                    continue;
                }
                else
                {
                    _comBoats.Add(coords);
                    comBoats++;
                }
            }
        }

        public bool ValidateCoords(string coords)
        {
            bool valid = false;

            if (_letters.Contains(coords[0]) && _numbers.Contains(coords[1]))
                valid = true;

            return valid;
        }

        public bool SetPlayerBoat(string coords)
        {
            foreach (string coord in _playerBoats)
            {
                if (coords == coord)
                    return false;
            }

            _playerBoats.Add(coords);

            return true;
        }

        public bool AddHitCoordinate(string coords)
        {
            foreach (string coord in _hitCoords)
            {
                if (coords == coord)
                    return false;
            }

            _hitCoords.Add(coords);

            return true;
        }

        public bool IsHit(string coords)
        {
            foreach (string coord in _comBoats)
            {
                if (coords == coord)
                    return true;
            }

            return false;
        }

        public bool IsComHit(string coords)
        {
            foreach (string coord in _playerBoats)
            {
                if (coords == coord)
                    return true;
            }

            return false;
        }

        public string CheckWinConditions() {
            int comBoatsHit = 0;
            foreach (string coord in _hitCoords) {
                if (_comBoats.Contains(coord))
                    comBoatsHit++;
            }

            if (comBoatsHit == _comBoats.Count) return "Player";

            int boatsHit = 0;
            foreach (string coord in _comHitCoords) {
                if (_playerBoats.Contains(coord))
                    boatsHit++;
            }

            if (boatsHit == _playerBoats.Count) return "Computer";

            return "";
        }

        public string ComputerAttack()
        {
            string coords = GetRandomCoords();
            while (_comHitCoords.Contains(coords))
            {
                coords = GetRandomCoords();
            }

            _comHitCoords.Add(coords);

            return coords;
        }

        public void DisplayFleetGrid()
        {
            BoardUtils.PrintLine();
            BoardUtils.PrintRow("F", "A", "B", "C", "D", "E", "F", "G", "H");

            for (int i = 0; i < _numbers.Length; i++)
            {
                BoardUtils.PrintRow(GetFleetGridRow(_numbers[i].ToString()));
            }

            BoardUtils.PrintLine();
        }

        public void DisplayTargetTracker()
        {
            BoardUtils.PrintLine();
            BoardUtils.PrintRow("T", "A", "B", "C", "D", "E", "F", "G", "H");

            for (int i = 0; i < _numbers.Length; i++)
            {
                BoardUtils.PrintRow(GetTargetTrackerRow(_numbers[i].ToString()));
            }

            BoardUtils.PrintLine();
        }

        private string GetRandomCoords()
        {
            return "" + _letters[_random.Next(_letters.Length)] + _numbers[_random.Next(_numbers.Length)];
        }

        private string[] GetFleetGridRow(string rowNumber)
        {
            List<string> row = new List<string>();

            row.Add(rowNumber);

            for (int i = 0; i < _letters.Length; i++)
            {
                string coords = _letters[i] + rowNumber;
                if (_playerBoats.Contains(coords) && !_comHitCoords.Contains(coords))
                    row.Add("B");
                else if (_playerBoats.Contains(coords) && _comHitCoords.Contains(coords))
                    row.Add("H");
                else
                    row.Add(" ");
            }

            return row.ToArray();
        }

        private string[] GetTargetTrackerRow(string rowNumber)
        {
            List<string> row = new List<string>();
            row.Add(rowNumber);

            for (int i = 0; i < _letters.Length; i++)
            {
                string coords = _letters[i] + rowNumber;
                if (_hitCoords.Contains(coords) && _comBoats.Contains(coords))
                    row.Add("H");
                else if (_hitCoords.Contains(coords) && !_comBoats.Contains(coords))
                    row.Add("M");
                else
                    row.Add(" ");
            }

            return row.ToArray();
        }

    }

}