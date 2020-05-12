using java_menace.Characters;
using java_menace.Physical_Objects;
using java_menace.Terminal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace java_menace.Itens.Hardwares
{
    class Notebook
    {
        public int AttackPower { get; set; }
        public int DefensePower { get; set; }
        public int MaxLife { get; set; }
        public int MaxMana { get; set; }
        public int CurrentLife { get; set; }
        public int CurrentMana { get; set; }
        public int CurrentXP { get; set; }
        public int MaxXP { get; set; }
        public int Level { get; set; }

        //All notebooks must have this pieces
        public Hardware Processor { get; set; }
        public Hardware GPU { get; set; }
        public Hardware RAM { get; set; }
        public Hardware HardDisk { get; set; }
        public Hardware MotherBoard { get; set; }
        public Bash Bash { get; set; }

        public Character User { get; set; }

        public Notebook(Character User)
        {
            this.User = User;
            MaxXP = 100;
            Level = 1;
        }

        public void AddXP(Character User, int DropXP)
        {
            if (User is Linus)
            {
                if (MaxXP <= CurrentXP + DropXP)
                {
                    CurrentXP += DropXP;
                    CurrentXP -= MaxXP;
                    MaxXP += 10;
                    Level += 1;
                    MaxLife += 7;
                    MaxMana += 7;
                }
                else
                {
                    CurrentXP += DropXP;
                }
            }
            else if (User is Steve)
            {
                if (MaxXP <= CurrentXP + DropXP)
                {
                    CurrentXP += DropXP;
                    CurrentXP -= MaxXP;
                    MaxXP += 10;
                    Level += 1;
                    User.WalletJLC += 20;
                    MaxLife += 5;
                    MaxMana += 5;
                }
                else
                {
                    CurrentXP += DropXP;
                }
            }
            else
            {
                if (MaxXP <= CurrentXP + DropXP)
                {
                    CurrentXP += DropXP;
                    CurrentXP -= MaxXP;
                    MaxXP += 10;
                    Level += 1;
                    AttackPower += 2;
                    MaxLife += 5;
                    MaxMana += 5;
                }
                else
                {
                    CurrentXP += DropXP;
                }
            }


        }
        public void PutPiece(Hardware Piece)
        {
            if (Piece.Type == "Processor")
            {
                Processor = Piece;
                DefensePower += Piece.Bonus;
            }
            else if (Piece.Type == "GPU")
            {
                GPU = Piece;
                AttackPower += Piece.Bonus;
            }
            else if (Piece.Type == "RAM")
            {
                RAM = Piece;
                MaxLife += Piece.Bonus;
            }
            else if (Piece.Type == "HardDisk")
            {
                HardDisk = Piece;
                MaxMana += Piece.Bonus;
            }
            else
            {
                MotherBoard = Piece;
                MaxLife += Piece.Bonus;
            }
        }
        public bool ChangePience(Hardware Piece, Inventory Inventory)
        {
            var Bag = Inventory.HardwareInventory;
            int Max = 15;

            if (Piece.Type == "Processor")
            {
                if (Bag.Count >= Max)
                {
                    return false;
                }
                else
                {
                    DefensePower -= Processor.Bonus;
                    Bag.Add(Processor);
                    Processor = Piece;
                    DefensePower += Piece.Bonus;
                    Bag.Remove(Piece);
                }
            }
            else if (Piece.Type == "GPU")
            {
                if (Bag.Count >= Max)
                {
                    return false;
                }
                else
                {
                    AttackPower -= GPU.Bonus;
                    Bag.Add(GPU);
                    GPU = Piece;
                    AttackPower += Piece.Bonus;
                    Bag.Remove(Piece);
                }
            }
            else if (Piece.Type == "RAM")
            {
                if (Bag.Count >= Max)
                {
                    return false;
                }
                else
                {
                    MaxLife -= RAM.Bonus;
                    Bag.Add(RAM);
                    RAM = Piece;
                    MaxLife += Piece.Bonus;
                    Bag.Remove(Piece);
                }
            }
            else if (Piece.Type == "HardDisk")
            {

                if (Bag.Count >= Max)
                {
                    return false;
                }
                else
                {
                    MaxMana -= HardDisk.Bonus;
                    Bag.Add(HardDisk);
                    HardDisk = Piece;
                    MaxMana += Piece.Bonus;
                    Bag.Remove(Piece);
                }
            }
            else
            {
                if (Bag.Count >= Max)
                {
                    return false;
                }
                else
                {
                    MaxLife -= MotherBoard.Bonus;
                    Bag.Add(MotherBoard);
                    MotherBoard = Piece;
                    MaxLife += Piece.Bonus;
                    Bag.Remove(Piece);
                }
            }
            return true;
        }

        public bool IsDead()
        {
            if (CurrentLife <= 0) return true;
            else return false;
        }
    }
}
