using Binance.Net.Clients;
using Binance.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net.Sockets;
using Microsoft.VisualBasic;
using System;
using System.IO;
using System.IO.Compression;


namespace BinanceBot
{
    class Listener
    {
        private string _lastDate = "";
        private decimal _lastPrice = 0;
        const int StringShorter = 5;
        private string _lastPath = "";
        private void Handle(DataEvent<BinanceStreamBookPrice> update)
        {

            string dataNow = DateTime.UtcNow.ToString("dd-M-yyyy-HH");
            string dataFolder = "results//" + dataNow;
            string resultPath = $"{dataFolder}//{dataNow}.txt";
            decimal currentPrice = GetCurrentPrice(update);
            string currentDate = DateTime.UtcNow.ToString("dd-M-yyyy-HH-mm-ss");
            if (string.IsNullOrEmpty(_lastPath))
            {
                _lastPath = resultPath; 
            }
            if (_lastPath != resultPath)
            {
                var archivator = new Archivator();
                archivator.FilesToZip(_lastPath, _lastPath);
                _lastPath = resultPath;
            }
            if (!Directory.Exists(dataFolder))
            {
                Directory.CreateDirectory(dataFolder);
            }

            GetData(currentDate, currentPrice, resultPath, update);

        }


        public void GetData(string currentDate, decimal currentPrice, string resultPath, DataEvent<BinanceStreamBookPrice> update)
        {
            if ((currentDate != _lastDate) || (currentPrice != _lastPrice))
            {
                string dataString = GetOnceData(update, currentDate, _lastDate).Remove((GetOnceData(update, currentDate, _lastDate).Length - StringShorter)) + "\n";

                File.AppendAllText(resultPath, dataString);
                _lastDate = currentDate;
                _lastPrice = currentPrice;
            }
        }

        public decimal GetCurrentPrice(DataEvent<BinanceStreamBookPrice> update)
        {
            decimal currentPrice = (update.Data.BestBidPrice + update.Data.BestAskPrice) / 2;
            return currentPrice;
        }

        public string GetOnceData(DataEvent<BinanceStreamBookPrice> update, string currentDate, string lastDate)
        {
            if (currentDate == lastDate)
            {
                string currentData = "0\t\t    " + (update.Data.BestBidPrice + update.Data.BestAskPrice) / 2;
                return currentData;
            }
            else
            {
                string currentData = DateTime.UtcNow + " " + (update.Data.BestBidPrice + update.Data.BestAskPrice) / 2;
                return currentData;
            }

        }

        public string GetOnlyPrice(DataEvent<BinanceStreamBookPrice> update)
        {
            string onlyPrice = $"{(update.Data.BestBidPrice + update.Data.BestAskPrice) / 2}";
            return onlyPrice;
        }


        public async Task StartRecieving()
        {
            string symbol = "BTCUSDT";
            int precision = 0;
            var client = new BinanceSocketClient();

            var response = await client
                .SpotStreams
                .SubscribeToBookTickerUpdatesAsync(symbol, (update) => Handle(update));

            if (!response.Success)
            {
                Console.WriteLine($"could not request: {response.Error?.Message}");

            }
            while (true)
            {

            }
        }

    }
}

