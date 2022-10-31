using Binance.Net.Clients;
using Binance.Net.Interfaces;
using Binance.Net.Objects;
using Binance.Net.Objects.Models.Spot.Socket;
using BinanceBot;
using CryptoExchange.Net.Sockets;
using Microsoft.VisualBasic;
using System.Data;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

var listener = new Listener();
await listener.StartRecieving();

//var archivator = new Archivator();
//archivator.FilesToZip("results//29-10-2022-14-53//", "results//1");