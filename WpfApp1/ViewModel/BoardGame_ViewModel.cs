using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.Model;

namespace WpfApp1.ViewModel
{
    public class BoardGame_ViewModel : INotifyPropertyChanged
    {
        private List<Cards> board;

        public List<Cards> Board
        {
            get { return board; }
            set
            {
                board = value;
                OnPropertyChanged();
            }
        }
        private List<Card> hand;

        public List<Card> Hand
        {
            get { return hand; }
            set
            {
                hand = value;
                OnPropertyChanged();
            }
        }

        Connexion connexion = new Connexion();

        private List<string> messageServ;

        public List<string> MessageServ
        {
            get { return messageServ; }
            set
            {
                messageServ = value;
                OnPropertyChanged();
            }
        }
        public string namePlayer
        {
            get;
            set;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private string Text;
        public string text
        {
            get { return Text; }
            set
            {
                MessageServ = new List<string>
                {
                    MessageServ[1],
                    MessageServ[2],
                    MessageServ[3],
                    MessageServ[4],
                    value
                };
                Text = value;
                OnPropertyChanged();
                Lire(value);

            }
        }


        public BoardGame_ViewModel(string nameP)
        {
            GenereBoard();
            MessageServ = new List<string>
            {
                "...",
                "...",
                "En attente du serv",
                "Il arrive promis",
                "..."
            };
            namePlayer = nameP;
            text = "JOIN:" + namePlayer;
            _ = connexion.MainConnexion();

            _ = ClickCommandAsync();

            _ = Boucle();
        }
        public async Task ClickCommandAsync()
        {
            _ = await connexion.setMessageAsync(text);
        }

        private void GenereBoard()
        {
            List<Cards> newBoard = new List<Cards>();
            List<Card> newHand = new List<Card>();

            for (int y = 0; y < 4; y++)
            {
                Cards cards = new Cards();

                for (int x = 0; x < 5; x++)
                {
                    Card c = new()
                    {
                        Num = new Random().Next(104)
                    };
                    cards.Line.Add(c);
                    if (x < 2)
                    {
                        newHand.Add(c);
                    }
                }

                newBoard.Add(cards);

                Board = newBoard;
                Hand = newHand;
            }

        }



        private ICommand _clickCommand;
        public ICommand ClickCommand
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new CommandHandler(() => MyAction(), () => CanExecute));
            }
        }
        public bool CanExecute
        {
            get
            {
                // check if executing is allowed, i.e., validate, check if a process is running, etc. 
                return true;
            }
        }

        public void MyAction()
        {
            _ = ClickCommandAsync();
        }


        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        private async Task Boucle()
        {
            while (true)
            {
                text = await Task.Run(() => connexion.getMessageAsync());

                _ = connexion.setMessageAsync("OK");
            }
        }
        private void Lire(string message)
        {
            string[] code = message.Split(':');
            switch (code[0])
            {
                case "CARDS":
                    UpdateHand(code[1]);
                    break;
                case "BOARD":
                    UpdateBoard(code[1]);
                    break;
                default:
                    break;
            }
        }

        private void UpdateHand(string message)
        {

            string[] stringHand = message.Split(',');

            List<Card> newHand = new();

            foreach (string num in stringHand)
            {
                if (string.IsNullOrEmpty(num))
                {
                    continue;
                }
                newHand.Add(new Card()
                {
                    Num = int.Parse(num)
                });
            }


            Hand = newHand;
        }
        private void UpdateBoard(string message)
        {

            string[] stringLine = message.Split(';');

            List<Cards> newBoard = new();

            foreach (string line in stringLine)
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                Cards cards = new();
                string[] stringLineCards = line.Split(',');
                foreach (string num in stringLineCards)
                {
                    if (string.IsNullOrEmpty(num))
                    {
                        continue;
                    }
                    cards.Line.Add(new Card()
                    {
                        Num = int.Parse(num)
                    });
                }

                newBoard.Add(cards);
            }


            Board = newBoard;
        }
    }

}
