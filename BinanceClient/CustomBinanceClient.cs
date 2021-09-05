using Binance.Net;
using Binance.Net.Interfaces;
using Binance.Net.Objects;
using CryptoExchange.Net.Authentication;

namespace ExternalLibrary
{
    public static class CustomBinanceClient
    {
        public static IBinanceClient GetInstance(string apiKey, string apiSecret) =>
            new BinanceClient(new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials(apiKey, apiSecret)
            });
    }
}
