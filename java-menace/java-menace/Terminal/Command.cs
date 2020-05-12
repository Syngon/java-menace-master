using java_menace.Economy;
using java_menace.Economy.Shop;
using java_menace.Itens;
using java_menace.Itens.Hardwares;
using java_menace.Itens.Potions;
using java_menace.Physical_Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace java_menace.Terminal
{
    enum Mode
    {
        Explorer,
        Hacker,
        Root
    }
    class Command
    {
        public string Activator { get; set; }
        public Mode Mode { get; set; }
        public string Description { get; set; }
        public Action<Bash, object, string[]> Function { get; set; }
        public Dictionary<string, string> ArgsDescription { get; set; }

        public Command(string Activator, Mode Mode, string Description, Action<Bash, object, string[]> Function, Dictionary<string, string> ArgsDescription = null)
        {
            this.Activator = Activator;
            this.Description = Description;
            this.Function = Function;
            this.ArgsDescription = ArgsDescription;
            this.Mode = Mode;
        }
    }

    static class CommandDAO
    {
        static List<List<Command>> CommandsByCategory = new List<List<Command>>()
        {
                new List<Command>()
                {
                    new Command("Activator", Mode.Explorer, "Description", (UserBash, ListPhysicalObject, Args) => {

                    }),

                    new Command("openchest", Mode.Explorer, "open a chest in front of the player", (Bash, Lista, args) =>
                    {
                        var User = Bash.UserNotebook.User;
                        var Chest = Lista as List<PhysicalObject>;
                        var Chest2 = Chest.Where(x => x is Chest && ((Vector2.Distance(x.GetLocation(), User.GetLocation() )) < 80)).ToList();

                        if(Chest2.Count != 0)
                        {
                            var RealChest = Chest2[0] as Chest;
                            RealChest.ChestOpened();
                            foreach(Item Item in RealChest.ChestItems)
                            {
                                if(RealChest.Times == 1)
                                {
                                    if (User.Inventory.AddItem(Item))
                                    {

                                    }
                                    else
                                    {
                                        Bash.SendMessage("Inventory is already full!\n", true);
                                        break;
                                    }
                                }
                                else continue;

                                Bash.SendMessage("Item: " + Item.Name + " | Type: " + Item.Type +  " | Price:" + Item.Price + " | Rarity: " + Item.Rarity + "\n", true);
                            }
                        }
                        else
                        {
                            Bash.SendMessage("Nenhum baivis foi encontrada!\n", true);
                        }

                    }),

                    new Command("openshop", Mode.Explorer, "open a shop in front of the player", (Bash, Shops, Args) =>
                    {
                        var User = Bash.UserNotebook.User;
                        var PhysicalObjects = Shops as List<PhysicalObject>;
                        var Jolis = PhysicalObjects.Where(x => (x is Jolishop || x is JoliFox) && (Vector2.Distance(x.GetLocation(), User.GetLocation() ) < 80)).ToList();
                        Jolishop JoliShop;

                        if (Jolis.Count != 0)
                        {
                            if(Jolis[0] is Jolishop)
                            {
                                JoliShop = Jolis[0] as Jolishop;
                            }
                            else
                            {
                                JoliShop = (Jolis[0] as JoliFox).Jolishop;
                            }

                            JoliShop.ShopOpened();

                            foreach (Item Item in JoliShop.Estoque)
                            {
                                Bash.SendMessage("Item: " + Item.Name + " | Type: " + Item.Type +  " | Price:" + Item.Price + " | Rarity: " + Item.Rarity + "\n", true);
                            }
                        }

                        else
                        {
                            Bash.SendMessage("There is no shop around here\n", true);
                        }
                    }),

                    new Command("sell", Mode.Explorer, "sell an item", (Bash, Shop, args) =>
                    {

                        var User = Bash.UserNotebook.User;
                        var PhysicalObjects = Shop as List<PhysicalObject>;
                        var Jolis = PhysicalObjects.Where(x => (x is Jolishop || x is JoliFox) && (Vector2.Distance(x.GetLocation(), User.GetLocation() ) < 80)).ToList();
                        int Count = 0;
                        Jolishop JoliShop;

                        if(Jolis[0] is Jolishop)
                        {
                            JoliShop = Jolis[0] as Jolishop;
                        }
                        else
                        {
                            JoliShop = (Jolis[0] as JoliFox).Jolishop;
                        }

                        if(Jolis.Count != 0)
                        {
                            if(args != null)
                            {
                                foreach(var arg in args)
                                {
                                    if(arg.Contains("Life") || arg.Contains("Mana") )
                                    {
                                        Bash.SendMessage("you cant sell a potion\n", true);
                                        break;
                                    }
                                    int index = 0;
                                    int count = 0;
                                    var Bag = Bash.UserNotebook.User.Inventory;
                                    foreach(Hardware Hardware in Bag.HardwareInventory)
                                    {
                                        if(Hardware.Name.Contains(arg))
                                        {
                                             index = Bag.HardwareInventory.IndexOf(Hardware);
                                             User.WalletJLC += Hardware.Price;
                                             count++;
                                        }
                                        else
                                        {
                                            Count++;
                                        }
                                    }
                                    if(count != 0)
                                    {
                                        Bag.HardwareInventory.RemoveAt(index);
                                        Bash.SendMessage("Item sold!\n", true);
                                    }
                                    else
                                    {
                                        Bash.SendMessage("your bag has no hardware like that!\n", true);
                                    }
                                }
                            }

                            else
                            {
                                Bash.SendMessage("Item must be specified\n", true);
                            }
                        }
                        else
                        {
                            Bash.SendMessage("Shop not found!\n", true);
                        }
                    }),

                    new Command("buy", Mode.Explorer, "buy an item", (Bash, Shop, args) =>
                    {
                        var User = Bash.UserNotebook.User;
                        var PhysicalObjects = Shop as List<PhysicalObject>;
                        var Jolis = PhysicalObjects.Where(x => (x is Jolishop || x is JoliFox) && (Vector2.Distance(x.GetLocation(), User.GetLocation() ) < 80)).ToList();
                        int Count = 0;
                        Jolishop JoliShop;

                        if(Jolis[0] is Jolishop)
                        {
                            JoliShop = Jolis[0] as Jolishop;
                        }
                        else
                        {
                            JoliShop = (Jolis[0] as JoliFox).Jolishop;
                        }

                        if(Jolis.Count != 0)
                        {
                            if(args != null)
                            {
                                foreach(Item Item in JoliShop.Estoque)
                                {
                                    if(Item.Name.Contains(args[0]))
                                    {
                                        if(User.WalletJLC >= Item.Price)
                                        {
                                            if (User.Inventory.AddItem(Item))
                                            {
                                                User.WalletJLC -= Item.Price;
                                                Bash.SendMessage("Item bought!\n", true);
                                                JoliShop.Estoque.Remove(Item);
                                                break;
                                            }
                                            else
                                            {
                                                Bash.SendMessage("Inventory is already full!\n", true);
                                            }

                                        }
                                        else
                                        {
                                            Bash.SendMessage("Not enough Jolicoin to buy\n", true);
                                        }
                                    }
                                    else
                                    {
                                        Count++;
                                    }
                                }
                                if (Count >= JoliShop.Estoque.Count)
                                {
                                    Bash.SendMessage("This shop doesn't have this item\n", true);
                                }
                            }
                            else
                            {
                                Bash.SendMessage("Item must be specified\n", true);
                            }
                        }
                        else
                        {
                            Bash.SendMessage("Shop not found!\n", true);
                        }
                    }),

                    new Command("mine", Mode.Explorer, "chance to get BTC", (Bash, yemoja, args) =>
                    {
                        var User = Bash.UserNotebook.User;
                        var PhysicalObjects = yemoja as List<PhysicalObject>;
                        var Yemoj = PhysicalObjects.Where(x => (x is Yemoja) && (Vector2.Distance(x.GetLocation(), User.GetLocation() ) < 80)).ToList();

                        if(Yemoj.Count != 0)
                        {
                            Random NumRandom = new Random();
                            int Percentage = NumRandom.Next(100);
                            Bitcoin BTC = new Bitcoin();
                            double BonusBTC = double.Parse(BTC.Quotation);

                            if (Percentage <= 50)
                            {
                                Bash.SendMessage("you didt it wrong, no BTC found\n", true);
                            }
                            else if(Percentage > 50 && Percentage <= 80)
                            {
                                User.WalletBTC += BonusBTC;
                                Bash.SendMessage("you got " + BonusBTC + " BTC\n", true);
                            }
                            else
                            {
                                User.WalletBTC += BonusBTC * 2;
                                Bash.SendMessage("well done! we added " + BonusBTC + " BTC, \n", true);
                            }
                        }
                        else
                        {
                            Bash.SendMessage("Yemoja not found!\n", true);
                        }
                    }),

                    new Command("exchange", Mode.Explorer, "exchanges BTC to JLC, param is the quantity of BTC to exchange", (Bash, Value, args) =>
                    {
                        Jolicoin JLC = new Jolicoin();
                        var User = Bash.UserNotebook.User;
                        double BTC_to_JLC;
                        double ValueJLC = JLC.ValueJLC;
                        double ValueBTC = JLC.ValueBTC;
                        int Quantity = 0;
                        if(args != null)
                        {
                            if(double.TryParse(args[0], out BTC_to_JLC))
                            {
                                if(BTC_to_JLC < User.WalletBTC)
                                {
                                    if(BTC_to_JLC > ValueBTC)
                                    {
                                        while((BTC_to_JLC / ValueJLC) >= 1)
                                        {
                                            User.WalletJLC += 1;
                                            User.WalletBTC -= BTC_to_JLC;
                                            BTC_to_JLC -= ValueBTC;
                                            Quantity++;
                                        }
                                        Bash.SendMessage(Quantity + " Jolicoins adicionados com sucesso!\n", true);
                                        Bash.SendMessage("Voce tem " + User.WalletBTC+ " BTC  e " + User.WalletJLC  + " JLC\n" , true);
                                    }
                                    else
                                    {
                                        Bash.SendMessage("Quantidade insuficiente para comprar um JLC\n", true);
                                    }
                                }
                                else
                                {
                                    Bash.SendMessage("Voce nao tem BTC suficientes\n", true);
                                }
                            }
                            else
                            {
                                Bash.SendMessage("Valor inserido nao corresponde a um numero!\n", true);
                            }
                        }
                        else
                        {
                             Bash.SendMessage("Nada foi inserido apos o comando Exchange!\n", true);
                        }
                    }),

                    new Command("quotation", Mode.Explorer, "shows money's quotation", (Bash, Whatever, args) =>
                    {
                        Jolicoin JLC = new Jolicoin();
                        double ValueJLC = JLC.ValueJLC;
                        double ValueBTC = JLC.ValueBTC;
                        Bash.SendMessage("BTC value : " + ValueBTC + "\n", true);
                        Bash.SendMessage("JLC value : " + ValueJLC + "\n", true);
                    }),

                    new Command("stats", Mode.Explorer, "shows user stats", (Bash, ListPhysicalObject, Args) =>
                    {
                        var Notebook = Bash.UserNotebook;
                        var StatsArguments = new Dictionary<string, string>()
                        {
                            {"-pwr", "Attack Power: " + Notebook.AttackPower + "\n"},
                            {"-def", "Defense Power: " + Notebook.DefensePower + "\n"},
                            {"-mana", "Mana: " + Notebook.CurrentMana + "/" + Notebook.MaxMana + "\n"},
                            {"-life", "Life: " + Notebook.CurrentLife + "/" + Notebook.MaxLife + "\n"},
                            {"-xp", "XP: " + Notebook.CurrentXP + "/" + Notebook.MaxXP + "\n"},
                            {"-lvl", "Level: " + Notebook.Level + "\n"}
                        };

                        if(Args == null || Args[0] == "-a")
                        {
                            foreach(var Stats in StatsArguments.Values)
                            {
                                Bash.SendMessage(Stats, true);
                            }
                        }

                        else
                        {
                            foreach(var Arg in Args)
                            {
                                if(StatsArguments.ContainsKey(Arg))
                                {
                                    Bash.SendMessage(StatsArguments[Arg], true);
                                }
                            }
                        }

                    }, new Dictionary<string, string>(){
                            {"-pwr", "Shows attack power\n"},
                            {"-def", "Shows defense power\n"},
                            {"-mana", "Shows mana\n"},
                            {"-life", "Shows life\n"},
                            {"-xp", "Shows xp\n"},
                            {"-lvl", "Shows lvl\n"}

                    }),
                    new Command("money", Mode.Explorer, "shows user's money", (Bash, ListPhysicalObject, Args) => {
                        var User = Bash.UserNotebook.User;
                        Bash.SendMessage("Carteira BTC: " + User.WalletBTC + "\nCarteira JLC: " + User.WalletJLC + "\n", true);
                    })
                },

                new List<Command>()
                {
                    new Command("Activator", Mode.Hacker, "Description", (UserBash, InvaderBash, Args) => {

                    })
                },

                new List<Command>()
                {
                    new Command("Activator", Mode.Root, "Description", (UserBash, NullObject, Args) => {

                    }),

                    new Command("lifepot", Mode.Root, "Use life potion", (Bash, NullObject, args) =>
                    {
                        var User = Bash.UserNotebook.User;
                        if(User.Inventory.LifePotionInventory.Count > 0)
                        {
                            User.Inventory.UseLifePotion(User);
                            Bash.SendMessage("Potion Used \n", true);
                        }
                        else
                        {
                            Bash.SendMessage("No potion in inventory \n", true);
                        }
                    }),

                    new Command("manapot", Mode.Root, "Use mana potion", (Bash, NullObject, args) =>
                    {
                        var User = Bash.UserNotebook.User;
                        if(User.Inventory.ManaPotionInventory.Count > 0)
                        {
                            User.Inventory.UseManaPotion(User);
                            Bash.SendMessage("Potion Used \n", true);
                        }
                        else
                        {
                            Bash.SendMessage("No potion in inventory \n", true);
                        }
                    }),

                    new Command("bag", Mode.Root, "Shows bag's content", (Bash, NullObject, args) =>
                    {
                        var Bag = Bash.UserNotebook.User.Inventory;
                        if(Bag.HardwareInventory.Count != 0)
                        {
                            foreach(Item Item in Bag.HardwareInventory)
                            {
                                Bash.SendMessage("Item: " + Item.Name + " | Type: " + Item.Type +  " | Price:" + Item.Price + " | Rarity: " + Item.Rarity + "\n", true);
                            }
                        }

                        else Bash.SendMessage("Your Bag has no Hardware\n", true);

                        if(Bag.LifePotionInventory.Count != 0 )
                        {
                            Bash.SendMessage("LifePots: " + Bag.LifePotionInventory.Count + "\n", true);
                        }

                        else
                        {
                            Bash.SendMessage("LifePots: 0\n", true);
                        }

                        if(Bag.ManaPotionInventory.Count != 0 )
                        {
                            Bash.SendMessage("ManaPots: " + Bag.ManaPotionInventory.Count + "\n", true);
                        }

                        else
                        {
                            Bash.SendMessage("ManaPots: 0\n", true);
                        }

                    }),

                    new Command("ls", Mode.Root, "Show pieces on user's notebook", (Bash, NullObject, Args) => {

                        var Note = Bash.UserNotebook;

                        var StatsArguments = new Dictionary<string, string>()
                        {
                            {"-p", "Processor: "+ Note.Processor.Name + " | Rarity: " + Note.Processor.Rarity + "\n"},
                            {"-g", "GPU: " + Note.GPU.Name + " | Rarity: " + Note.GPU.Rarity + "\n"},
                            {"-r", "RAM: " + Note.RAM.Name + " | Rarity: " + Note.RAM.Rarity + "\n"},
                            {"-m", "MotherBoard: " + Note.MotherBoard.Name + " | Rarity: " + Note.MotherBoard.Rarity + "\n"},
                            {"-h", "HardDisk: " + Note.HardDisk.Name + " | Rarity: " + Note.HardDisk.Rarity +"\n"}
                        };

                        if(Args == null || Args[0] == "-a")
                        {
                            foreach(var Stats in StatsArguments.Values)
                            {
                                Bash.SendMessage(Stats, true);
                            }
                        }

                        else
                        {
                            foreach(var Arg in Args)
                            {
                                if(StatsArguments.ContainsKey(Arg))
                                {
                                    Bash.SendMessage(StatsArguments[Arg], true);
                                }
                            }
                        }

                    }, new Dictionary<string, string>(){
                            {"-p", "Shows processor's info\n"},
                            {"-def", "Shows gpu's info\n"},
                            {"-mana", "Shows ram's info\n"},
                            {"-life", "Shows motherboard's info\n"},
                            {"-xp", "Shows harddisk's info\n"}
                    }),

                    new Command("clear", Mode.Root, "Clears the terminal", (Bash, NullObject, Args) =>
                    {
                        Bash.BufferCache = string.Empty;
                        Bash.GUI.Document.SetText(Windows.UI.Text.TextSetOptions.None, string.Empty);
                    }),

                    new Command("help", Mode.Root, "Manual of commands", (Bash, NullObject, Args) =>
                    {
                        Bash.SendMessage(Bash.Interpreter.Commands.Values.Count + " commands found on interpreter\n\n", true);

                        foreach(var Command in Bash.Interpreter.Commands.Values)
                        {
                            Bash.SendMessage("Activator: " + Command.Activator + "\n", true);

                            switch (Command.Mode)
                            {
                                case Mode.Explorer:
                                    Bash.SendMessage("Mode: Explorer\n", true);
                                    break;

                                case Mode.Hacker:
                                    Bash.SendMessage("Mode: Hacker\n", true);
                                    break;

                                case Mode.Root:
                                    Bash.SendMessage("Mode: Root\n", true);
                                    break;
                            }

                            Bash.SendMessage("Description: " + Command.Description + "\n", true);

                            if(Command.ArgsDescription != null)
                            {
                                Bash.SendMessage("Args:\n", true);
                                foreach(var Pair in Command.ArgsDescription)
                                {
                                    Bash.SendMessage(Pair.Key + ": " + Pair.Value, true);
                                }
                            }

                            else
                            {
                                Bash.SendMessage("No args!\n", true);
                            }

                            Bash.SendMessage("\n\n", true);
                        }

                        Bash.SendMessage("\n", true);
                    }),
                    new Command("cd", Mode.Root, "change notebook's piece, 1st param is the part, 2nd is the piece", (Bash, NullObject, Args) => {
                        var User = Bash.UserNotebook.User;
                        var Note = Bash.UserNotebook;
                        int Count = 0;

                        var StatsArguments = new Dictionary<string, string>()
                        {
                            {"-p", "Processor: "+ Note.Processor.Name + " | Rarity: " + Note.Processor.Rarity + "\n"},
                            {"-g", "GPU: " + Note.GPU.Name + " | Rarity: " + Note.GPU.Rarity + "\n"},
                            {"-r", "RAM: " + Note.RAM.Name + " | Rarity: " + Note.RAM.Rarity + "\n"},
                            {"-m", "MotherBoard: " + Note.MotherBoard.Name + " | Rarity: " + Note.MotherBoard.Rarity + "\n"},
                            {"-h", "HardDisk: " + Note.HardDisk.Name + " | Rarity: " + Note.HardDisk.Rarity +"\n"}
                        };

                        if(Args != null)
                        {
                            if(Args.Length == 2)
                            {
                                if (StatsArguments.ContainsKey(Args[0]))
                                {
                                    foreach(Hardware Hardware in User.Inventory.HardwareInventory)
                                    {
                                        if (Hardware.Name.Contains(Args[1]))
                                        {
                                            if (Note.ChangePience(Hardware, Bash.UserNotebook.User.Inventory))
                                            {
                                                Bash.SendMessage("piece has been changed\n", true);
                                            }
                                            break;
                                        }
                                    }
                                    if(Count != 0)
                                    {
                                        Bash.SendMessage("your bag has no piece like that\n", true);
                                    }
                                }
                                else
                                {
                                    Bash.SendMessage("piece doesnt exist\n", true);
                                }
                            }
                            else
                            {
                                Bash.SendMessage("more or less than 2 args\n", true);
                            }
                        }
                        else
                        {
                            Bash.SendMessage("param must be specified\n", true);
                        }
                    })
                }
        };

        public static List<List<Command>> GetCommands()
        {
            return CommandsByCategory;
        }
    }
}
