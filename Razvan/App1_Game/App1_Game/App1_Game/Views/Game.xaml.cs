using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1_Game.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Game : ContentPage
    {

        public string Entry { get; set; }

        public string rez { get; set; }

        public string randomNumber { get; set; }

        public long score { get; set; }

        public Game()
        {
            InitializeComponent();
            RandomNumber();

            BindingContext = this;
        }

        private String  Check(string name)
        {
            int num = int.Parse(name);
            int reverse1 = 0;
            int reverse2 = 0;
            int rem, rem2;
            long len = num.ToString().Length;

            while (num != 0 )
            {              
                rem = num % 10;
                if (rem == 9)
                {
                    rem = 0;
                }
                else
                {
                    rem++;
                }
                reverse1 = reverse1 * 10 + rem;
                num /= 10;
            }

            for(;len>0;len--)
            {
                rem2 = reverse1 % 10;              
                reverse2 = reverse2 * 10 + rem2;
                reverse1 /= 10;           
            }

            rez = (reverse2).ToString();

            OnPropertyChanged(nameof(rez));

            return (reverse2).ToString();

        }
          
        private void RandomNumber()
        {
            Random generaror = new Random();

            randomNumber = generaror.Next(1000,9999).ToString();

            OnPropertyChanged(nameof(randomNumber));
        }

        private void Button_Clicked(object Sender, EventArgs e)
        {
            if (string.Compare(Check(randomNumber), Entry) == 0)
            {
                score++;

                OnPropertyChanged(nameof(score));           
            }
            RandomNumber();
        }
    }
}