using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AddOneGame.ViewModels;
using System.Timers;
using AddOneGame.Services;
using Xamarin.Forms.Internals;
//using System.Windows.Forms;

namespace AddOneGame.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePage : ContentPage
    {
        GameViewModel viewModel;

        public long GameNumber { get; set; } = 123;
        public long _Score;

        //private System.Windows.Forms.Timer aTkimer;

        public long Score
        {
            get
            {
                return _Score;
            }
            set
            {
                _Score = value;
                OnPropertyChanged();
            }
        }
        public TimerPlus GameTimer { get; set; }


        public string _GameTimeLeft;

        public string GameTimeLeft
        {
            get
            {
                return _GameTimeLeft;
            }
            set 
            {
                _GameTimeLeft = value;
                OnPropertyChanged();
            }
        }


        //{
        //    get
        //    {
        //        return countdown.RemainTime;
        //    }
        //    set
        //    {
        //        _GameTimeLeft = countdown.RemainTime;
        //        OnPropertyChanged(nameof(countdown.RemainTime));
        //        OnPropertyChanged();
        //    }
        //}

        Countdown countdown = new Countdown();

        public String _InputNumber;
        public String InputNumber
        {
            get
            {
                return _InputNumber;
            }
            set
            {
                _InputNumber = value;
                OnPropertyChanged();

                if (Check_Increase_Generate_Reset())
                {
                    Reset_Input();
                }
            }
        }

        public GamePage()
        {
            InitializeComponent();

            Generate_Number();
            Score = 0;          

            countdown = new Countdown();
            StartUpdating(20);

            BindingContext = this;
        }

        void Check_Number(object sender, EventArgs e)
        {
            if (this.RemainTime > 0)
            {
                Check_Increase_Generate_Reset();
            } else
            {
                DisplayAlert("Time is OUT", "Your score is: " + Score, "OK");
            }
        }

        Boolean Check_Increase_Generate_Reset()
        {
            if (InputNumber != null && InputNumber != "" && Valid_Number(GameNumber) == Int64.Parse(InputNumber))
            {
                Score++;
                Generate_Number();
                Reset_Input();

                return true;
            }

            return false;
        }

        void Generate_Number()
        {
            Random rnd = new Random();
            GameNumber = rnd.Next(100, 10000);
            OnPropertyChanged("GameNumber");
        }

        void Reset_Input()
        {
            InputNumber = "";
            OnPropertyChanged("InputNumber");
        }

        long Valid_Number_Razvan(long Number)
        {
            long Len = Number.ToString().Length;
            long Reverse1 = 0, Reverse2 = 0, Rest;

            while (Number != 0)
            {
                Rest = Number % 10;

                Console.WriteLine("Rest: " + Rest);

                if (Rest == 9)
                {
                    Rest = 0;
                } else
                {
                    Rest++;
                }
                Reverse1 = Reverse1 * 10 + Rest;
                Number /= 10;
            }

            while (Len > 0)
            {
                if (Reverse1 != 0)
                {
                    Rest = Reverse1 % 10;
                    Reverse2 = Reverse2 * 10 + Rest;
                    Reverse1 /= 10;
                } else
                {
                    Reverse2 *= 10;
                }
                Len--;
            }
            Console.WriteLine(Reverse2);

            return Reverse2;
        }


        long Valid_Number(long Number)
        {

            Console.WriteLine("GameNumer " + Number.ToString().Length);
            long Correct_Number = 0;
            long Next = 1;

            while (Number > 0)
            {
                switch (Number % 10)
                {
                    case 9:
                        break;
                    default:
                        Correct_Number += (Next * (Number % 10 + 1));
                        break;
                }
                Number /= 10;
                Next *= 10;
            }

            return Correct_Number;
        }
        async void Back_To_Menu(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        public DateTime StartDateTime { get; private set; }

        /// <summary>
        /// Gets the remain time in seconds.
        /// </summary>
        public double RemainTime
        {
            get { return remainTime; }

            private set
            {
                remainTime = value;
                this.GameTimeLeft = CountdownConverter.Convert(value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Occurs when completed.
        /// </summary>
        public event Action Completed;

        /// <summary>
        /// Occurs when ticked.
        /// </summary>
        public event Action Ticked;

        /// <summary>
        /// The timer.
        /// </summary>
        Timer timer;

        /// <summary>
        /// The remain time.
        /// </summary>
        double remainTime;

        /// <summary>
        /// The remain time total.
        /// </summary>
        double remainTimeTotal;

        /// <summary>
        /// Starts the updating with specified period, total time and period are specified in seconds.
        /// </summary>
        public void StartUpdating(double total, double period = 1.0)
        {
            if (timer != null)
            {
                StopUpdating();
            }

            remainTimeTotal = total;
            RemainTime = total;

            StartDateTime = DateTime.Now;

            timer = new Timer(period * 1000);
            timer = new Timer();
            timer.Elapsed += (sender, e) => Tick();
            timer.Enabled = true;
        }

        /// <summary>
        /// Stops the updating.
        /// </summary>
        public void StopUpdating()
        {
            RemainTime = 0;
            remainTimeTotal = 0;

            if (timer != null)
            {
                timer.Enabled = false;
                timer = null;
            }
        }

        /// <summary>
        /// Updates the time remain.
        /// </summary>
        public void Tick()
        {
            var delta = (DateTime.Now - StartDateTime).TotalSeconds;

            if (delta < remainTimeTotal)
            {
                RemainTime = remainTimeTotal - delta;

                var ticked = Ticked;
                if (ticked != null)
                {
                    ticked();
                }
            }
            else
            {
                RemainTime = 0;

                var completed = Completed;
                if (completed != null)
                {
                    completed();
                }
            }
        }

    }
}