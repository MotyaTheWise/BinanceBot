using BinanceBot;
using Newtonsoft.Json;


Dictionary<string, int> DeserializeJson(string json)
{
    var json1 = File.ReadAllText(json);
    Dictionary<string, int>? config = JsonConvert.DeserializeObject<Dictionary<string, int>>(json1);
    return config;
}

Dictionary<string, int> configuration = DeserializeJson("config.json");
var list = new List<Listener>();


foreach (var item in configuration)
{
    
    string symbol = item.Key;
    int precision = item.Value;
    var listener = new Listener(symbol, precision);
    await listener.StartRecieving(symbol, precision);
    list.Add(listener);

}


while (true)
{

}