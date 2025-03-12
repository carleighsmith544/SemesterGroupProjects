using System.Collections.Specialized;
using System.Reflection.Metadata;

namespace TheatreSeating
{

    public class SeatingUnit
    {
        public string Name { get; set; }
        public bool Reserved { get; set; }

        public SeatingUnit(string name, bool reserved = false)
        {
            Name = name;
            Reserved = reserved;
        }

    }
    public partial class MainPage : ContentPage
    {
        int count = 0;

        SeatingUnit[,] seatingChart = new SeatingUnit[5, 10];

        public MainPage()
        {
            InitializeComponent();
            GenerateSeatingNames();
            RefreshSeating();
        }

        private async void ButtonReserveSeat(object sender, EventArgs e)
        {
            var seat = await DisplayPromptAsync("Enter Seat Number", "Enter seat number: ");

            if (seat != null)
            {
                for (int i = 0; i < seatingChart.GetLength(0); i++)
                {
                    for (int j = 0; j < seatingChart.GetLength(1); j++)
                    {
                        if (seatingChart[i, j].Name == seat)
                        {
                            seatingChart[i, j].Reserved = true;
                            await DisplayAlert("Successfully Reserverd", "Your seat was reserverd successfully!", "Ok");
                            RefreshSeating();
                            return;
                        }
                    }
                }

                await DisplayAlert("Error", "Seat was not found.", "Ok");
            }
        }

        private void GenerateSeatingNames()
        {
            List<string> letters = new List<string>();
            for (char c = 'A'; c <= 'Z'; c++)
            {
                letters.Add(c.ToString());
            }

            int letterIndex = 0;

            for (int row = 0; row < seatingChart.GetLength(0); row++)
            {
                for (int column = 0; column < seatingChart.GetLength(1); column++)
                {
                    seatingChart[row, column] = new SeatingUnit(letters[letterIndex] + (column + 1).ToString());
                }

                letterIndex++;
            }
        }

        private void RefreshSeating()
        {
            grdSeatingView.RowDefinitions.Clear();
            grdSeatingView.ColumnDefinitions.Clear();
            grdSeatingView.Children.Clear();

            for (int row = 0; row < seatingChart.GetLength(0); row++)
            {
                var grdRow = new RowDefinition();
                grdRow.Height = 50;

                grdSeatingView.RowDefinitions.Add(grdRow);

                for (int column = 0; column < seatingChart.GetLength(1); column++)
                {
                    var grdColumn = new ColumnDefinition();
                    grdColumn.Width = 50;

                    grdSeatingView.ColumnDefinitions.Add(grdColumn);

                    var text = seatingChart[row, column].Name;

                    var seatLabel = new Label();
                    seatLabel.Text = text;
                    seatLabel.HorizontalOptions = LayoutOptions.Center;
                    seatLabel.VerticalOptions = LayoutOptions.Center;
                    seatLabel.BackgroundColor = Color.Parse("#333388");
                    seatLabel.Padding = 10;

                    if (seatingChart[row, column].Reserved == true)
                    {
                        //Change the color of this seat to represent its reserved.
                        seatLabel.BackgroundColor = Color.Parse("#883333");

                    }

                    Grid.SetRow(seatLabel, row);
                    Grid.SetColumn(seatLabel, column);
                    grdSeatingView.Children.Add(seatLabel);

                }
            }
        }

        //Carleigh Smith (W10111544) Made Repository
        //Kayla Pickering (W10105865) 
        //Assign to Team 1 Member
        private void ButtonReserveRange(object sender, EventArgs e)
        {
            
        }

        //Railyn Wingfield w10103698
        //Assign to Team 2 Member
        private void ButtonCancelReservation(object sender, EventArgs e)
        {
var seat = await DisplayPromptAsync("Enter Seat Number", "Enter seat number to cancel reservation: ");

    if (seat != null)
    {
        for (int i = 0; i < seatingChart.GetLength(0); i++)
        {
            for (int j = 0; j < seatingChart.GetLength(1); j++)
            {
                if (seatingChart[i, j].Name == seat)
                {
                    if (seatingChart[i, j].Reserved)
                    {
                        seatingChart[i, j].Reserved = false;
                        await DisplayAlert("Successfully Canceled", "Your reservation was canceled successfully!", "Ok");
                    }
                    else
                    {
                        await DisplayAlert("Error", "This seat is not reserved.", "Ok");
                    }
                    RefreshSeating();
                    return;
                }
            }
        }

        await DisplayAlert("Error", "Seat was not found.", "Ok");
    }
        }
        // Joseph White w10195902
        //Assign to Team 3 Member
       private async void ButtonCancelReservationRange(object sender, EventArgs e)
{
    
    var startSeat = await DisplayPromptAsync("Enter Start Seat", "Enter the starting seat number: ");
    var endSeat = await DisplayPromptAsync("Enter End Seat", "Enter the ending seat number: ");
    
    
    if (startSeat != null && endSeat != null)
    {
        List<SeatingUnit> seatsToCancel = new List<SeatingUnit>();

        for (int i = 0; i < seatingChart.GetLength(0); i++)
        {
            for (int j = 0; j < seatingChart.GetLength(1); j++)
            {
                
                if (string.Compare(seatingChart[i, j].Name, startSeat) >= 0 &&
                    string.Compare(seatingChart[i, j].Name, endSeat) <= 0)
                {
                    seatsToCancel.Add(seatingChart[i, j]);
                }
            }
        }

        
        if (seatsToCancel.Count > 0)
        {
            foreach (var seat in seatsToCancel)
            {
                if (seat.Reserved)
                {
                    seat.Reserved = false;
                }
            }

            await DisplayAlert("Successfully Canceled", "Reservations in the specified range are canceled.", "Ok");
            RefreshSeating();
        }
        else
        {
            await DisplayAlert("Error", "No seats found in the specified range.", "Ok");
        }
    }
}


            
        //Emily Lane w10185062
        //Assign to Team 4 Member
        private void ButtonResetSeatingChart(object sender, EventArgs e)
        {

        }
    }

}
