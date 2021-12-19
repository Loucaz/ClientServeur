using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.Model;

namespace WpfApp1.ViewModel
{
    public class BoardGame_ViewModel
    {
        public List<Cards> Board { get; set; }

        Connexion connexion = new Connexion();

        public string text { get; set; }

        public BoardGame_ViewModel()
        {
            GenereBoard();
            text = "";
            _ = connexion.MainConnexion();
        }
        public async Task ClickCommandAsync()
        {
            _ = connexion.setMessageAsync("DATE");
            text = await connexion.getMessageAsync();
        }

        private void GenereBoard()
        {
            Board = new List<Cards>();

            for (int y = 0; y < 4; y++)
            {
                Cards cards = new Cards();

                for (int x = 0; x < 5; x++)
                {
                    Card c = new Card();
                    c.Num = new Random().Next(104);
                    cards.Line.Add(c);
                }

                Board.Add(cards);
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
    }

}
