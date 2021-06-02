using CryptocurrencyApp;
using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CryptocurrencyApplication
{
    public partial class Form1 : Form
    {
        private dynamic[] coins;
        private string selectedCoin = "";
        API api = new API();
        PDF pdf = new PDF();
        Email email = new Email();
        public Form1()
        {
            InitializeComponent();
            MoveSidePanel(dashboardButton);
            this.coins = this.api.getNeededCoins();
        }

        private void reset()
        {
            // This resets the buttons
            bitcoinButton.BackColor = System.Drawing.Color.White;
            cardanoButton.BackColor = System.Drawing.Color.White;
            dogecoinButton.BackColor = System.Drawing.Color.White;
            ethereumButton.BackColor = System.Drawing.Color.White;
            iotaButton.BackColor = System.Drawing.Color.White;
            litecoinButton.BackColor = System.Drawing.Color.White;
            moneroButton.BackColor = System.Drawing.Color.White;
            neoButton.BackColor = System.Drawing.Color.White;
            stellarButton.BackColor = System.Drawing.Color.White;

            //This resets the labels and textboxes
            purchaseLabelCoin.Text = "Purchase Coins : ";
            calulcatedFullPriceTextBox.Text = "";
            dollarTextBox.Text = "";
            coinPriceLabel.Text = "";

        }

        public void MoveSidePanel(Control control)
        {
            sidePanel.Height = control.Height;
            sidePanel.Top = control.Top;
        }
        private void dashboardButton_Click(object sender, EventArgs e)
        {
            MoveSidePanel(dashboardButton);
            emailLabel.Visible = false;
            emailTextBox.Visible = false;
            sendEmailButton.Visible = false;
            purchasePanel.Visible = true;
            cryptocurrencyPanel.Visible = true;
            payPanel.Visible = true;
            cryptoCurrencyChart.Visible = false;
        }

        private void dataButton_Click(object sender, EventArgs e)
        {
            if (this.selectedCoin.Equals("")) {
                MessageBox.Show("Select a coin first!", "Error");
                return;
            }
            MoveSidePanel(dataButton);
            emailLabel.Visible = false;
            emailTextBox.Visible = false;
            sendEmailButton.Visible = false;
            purchasePanel.Visible = false;
            cryptocurrencyPanel.Visible = false;
            payPanel.Visible = false;

            cryptoCurrencyChart.Visible = true;
            dynamic coin = this.coins[0]["name"];
            for (int j = 0; j < this.coins.Length; j++)
            {
                string name = this.coins[j]["name"];

                if (String.Equals(name, this.selectedCoin))
                {
                    coin = this.coins[j];
                }
            }

            double currentPrice = coin["quote"]["USD"]["price"];
            double price1H = coin["quote"]["USD"]["percent_change_1h"];
            price1H = Math.Abs((currentPrice * (100 - price1H)) / 100);
            double price24H = coin["quote"]["USD"]["percent_change_24h"];
            price24H = Math.Abs((currentPrice * (100 - price24H)) / 100);
            double price7D = coin["quote"]["USD"]["percent_change_7d"];
            price7D = Math.Abs((currentPrice * (100 - price7D)) / 100);
            double price30D = coin["quote"]["USD"]["percent_change_30d"];
            price30D = Math.Abs((currentPrice * (100 - price30D)) / 100);
            double price90D = coin["quote"]["USD"]["percent_change_90d"];
            price90D = Math.Abs((currentPrice * (100 - price90D)) / 100);

            cryptoCurrencyChart.Series.Clear();
            cryptoCurrencyChart.Titles.Clear();
            var series1 = new Series
            {
                Name = this.selectedCoin,
                Color = System.Drawing.Color.Red,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Spline,
                IsValueShownAsLabel = true,
            };

            cryptoCurrencyChart.Titles.Add(this.selectedCoin);
            cryptoCurrencyChart.Titles["Title1"].Name = "MyTitle";

            this.cryptoCurrencyChart.Series.Add(series1);

            cryptoCurrencyChart.Series[this.selectedCoin].Points.AddXY("CurrentPrice", currentPrice);
            cryptoCurrencyChart.Series[this.selectedCoin].Points.AddXY("1h", price1H);
            cryptoCurrencyChart.Series[this.selectedCoin].Points.AddXY("24h", price24H);
            cryptoCurrencyChart.Series[this.selectedCoin].Points.AddXY("7D", price7D);
            cryptoCurrencyChart.Series[this.selectedCoin].Points.AddXY("30D", price30D);
            cryptoCurrencyChart.Series[this.selectedCoin].Points.AddXY("90D", price90D);

            cryptoCurrencyChart.ResetAutoValues();


        }


        private void calculatePriceButton_Click(object sender, EventArgs e)
        {
            if (String.Equals(dollarTextBox.Text.Trim(), ""))
            {
                MessageBox.Show("Input a value", "Error");
                return;
            }

            if (String.Equals(coinPriceLabel.Text, "Coin Price : "))
            {
                MessageBox.Show("Please select a coin first!", "Error");
                return;
            }

            double price = Double.Parse(coinPriceLabel.Text.Substring(14));
            double inputValue = Double.Parse(dollarTextBox.Text);
            string data = " $" + Math.Round((price * inputValue), 2);
            calulcatedFullPriceTextBox.Text = data;
            this.priceToPayLabel.Text = data;
        }

        private void bitcoinButton_Click(object sender, EventArgs e)
        {
            reset();
            this.selectedCoin = coins[0]["name"];
            bitcoinButton.BackColor = System.Drawing.Color.Blue;
            purchaseLabelCoin.Text = "Purchase " + coins[0]["name"] + " : ";
            double price = coins[0]["quote"]["USD"]["price"];
            coinPriceLabel.Text = "Coin Price : $ " + Math.Round(price, 2);
        }

        private void ethereumButton_Click(object sender, EventArgs e)
        {
            reset();
            this.selectedCoin = coins[1]["name"];
            ethereumButton.BackColor = System.Drawing.Color.Blue;
            purchaseLabelCoin.Text = "Purchase " + coins[1]["name"] + " : ";
            double price = coins[1]["quote"]["USD"]["price"];
            coinPriceLabel.Text = "Coin Price : $ " + Math.Round(price, 2);
        }

        private void stellarButton_Click(object sender, EventArgs e)
        {
            reset();
            stellarButton.BackColor = System.Drawing.Color.Blue;
            this.selectedCoin = coins[6]["name"];
            purchaseLabelCoin.Text = "Purchase " + coins[6]["name"] + " : ";
            double price = coins[6]["quote"]["USD"]["price"];
            coinPriceLabel.Text = "Coin Price : $ " + Math.Round(price, 2);
        }

        private void litecoinButton_Click(object sender, EventArgs e)
        {
            reset();
            this.selectedCoin = coins[2]["name"];
            litecoinButton.BackColor = System.Drawing.Color.Blue;
            purchaseLabelCoin.Text = "Purchase " + coins[2]["name"] + " : ";
            double price = coins[2]["quote"]["USD"]["price"];
            coinPriceLabel.Text = "Coin Price : $ " + Math.Round(price, 2);
        }

        private void cardanoButton_Click(object sender, EventArgs e)
        {
            reset();
            this.selectedCoin = coins[5]["name"];
            cardanoButton.BackColor = System.Drawing.Color.Blue;
            purchaseLabelCoin.Text = "Purchase " + coins[5]["name"] + " : ";
            double price = coins[5]["quote"]["USD"]["price"];
            coinPriceLabel.Text = "Coin Price : $ " + Math.Round(price, 2);
        }

        private void iotaButton_Click(object sender, EventArgs e)
        {
            reset();
            this.selectedCoin = coins[3]["name"];
            iotaButton.BackColor = System.Drawing.Color.Blue;
            purchaseLabelCoin.Text = "Purchase " + coins[3]["name"] + " : ";
            double price = coins[3]["quote"]["USD"]["price"];
            coinPriceLabel.Text = "Coin Price : $ " + Math.Round(price, 2);
        }

        private void moneroButton_Click(object sender, EventArgs e)
        {
            reset();
            this.selectedCoin = coins[7]["name"];
            moneroButton.BackColor = System.Drawing.Color.Blue;
            purchaseLabelCoin.Text = "Purchase " + coins[7]["name"] + " : ";
            double price = coins[7]["quote"]["USD"]["price"];
            coinPriceLabel.Text = "Coin Price : $ " + Math.Round(price, 2);
        }

        private void neoButton_Click(object sender, EventArgs e)
        {
            reset();
            this.selectedCoin = coins[4]["name"];
            neoButton.BackColor = System.Drawing.Color.Blue;
            purchaseLabelCoin.Text = "Purchase " + coins[4]["name"] + " : ";
            double price = coins[4]["quote"]["USD"]["price"];
            coinPriceLabel.Text = "Coin Price : $ " + Math.Round(price, 2);
        }

        private void dogecoinButton_Click(object sender, EventArgs e)
        {
            reset();
            this.selectedCoin = coins[8]["name"];
            dogecoinButton.BackColor = System.Drawing.Color.Blue;
            purchaseLabelCoin.Text = "Purchase " + coins[8]["name"] + " : ";
            double price = coins[8]["quote"]["USD"]["price"];
            coinPriceLabel.Text = "Coin Price : $ " + Math.Round(price, 2);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void updateDataButton_Click(object sender, EventArgs e)
        {
            MoveSidePanel(updateDataButton);
            this.api.updateData();
            this.coins = this.api.getNeededCoins();
            Console.WriteLine(this.coins[0]);
        }

        private void payButton_Click(object sender, EventArgs e)
        {
            double priceToPay = Double.Parse(this.priceToPayLabel.Text.Substring(2));
            double balance = Double.Parse(this.walletBalance.Text.Substring(1));

            if (priceToPay > balance)
            {
                MessageBox.Show("Not enough money!", "Warning");
                return;
            }

            balance = balance - priceToPay;
            this.walletBalance.Text = "$" + Math.Round(balance, 2);
        }

        private void savePDF_Click(object sender, EventArgs e)
        {
            MoveSidePanel(savePDF);
            pdf.createDocument(this.coins);
        }

        private void sendEmail_Click(object sender, EventArgs e)
        {
            purchasePanel.Visible = false;
            cryptocurrencyPanel.Visible = false;
            payPanel.Visible = false;
            cryptoCurrencyChart.Visible = false;
            emailLabel.Visible = true;
            emailTextBox.Visible = true;
            sendEmailButton.Visible = true;
            MoveSidePanel(sendEmail);
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private void sendEmailButton_Click(object sender, EventArgs e)
        {
            string senderEmail = emailTextBox.Text;
            if (IsValidEmail(senderEmail)) {
                email.SendEmail(senderEmail);
            } else
            {
                MessageBox.Show("Email not valid!", "Warning");
                return;
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string[] namedCoins = new string[] { "Bitcoin", "Ethereum", "Litecoin", "IOTA", "Neo", "Cardano", "Stellar", "Monero", "Dogecoin" };
            string searchedCoin = searchTextBox.Text.ToLower();
            bool found = false;
            foreach (string coin in namedCoins)
            {
                if (coin.ToLower().Equals(searchedCoin))
                {
                    found = true;
                    this.selectedCoin = coin;
                    dataButton_Click(null, null);

                    switch(searchedCoin)
                    {
                        case "bitcoin":
                            bitcoinButton_Click(null, null);
                            break;

                        case "ethereum":
                            ethereumButton_Click(null, null);
                            break;

                        case "litecoin":
                            litecoinButton_Click(null, null);
                            break;

                        case "iota":
                            iotaButton_Click(null, null);
                            break;

                        case "neo":
                            neoButton_Click(null, null);
                            break;

                        case "cardano":
                            cardanoButton_Click(null, null);
                            break;

                        case "stellar":
                            stellarButton_Click(null, null);
                            break;

                        case "monero":
                            moneroButton_Click(null, null);
                            break;

                        case "dogecoin":
                            dogecoinButton_Click(null, null);
                            break;

                        default:
                            bitcoinButton_Click(null, null);
                            break;
                    }
                }
            }

            if (found == false)
            {
                MessageBox.Show("Coint not found, search again, maybe you mispelled it!", "Warning");
                return;
            }
        }
    }
}
