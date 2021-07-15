using O10.Core.Architecture;
using O10.Core.Configuration;
using O10.Core.Logging;
using O10.Nomy.Rapyd.DTOs;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using O10.Nomy.Utils;

namespace O10.Nomy.Rapyd
{
    [RegisterDefaultImplementation(typeof(IRapydApi), Lifetime = LifetimeManagement.Singleton)]
    public class RapydApi : IRapydApi
    {
        private readonly IRapydApiConfig _rapydApiConfig;
        private readonly ILogger _logger;
        private readonly JsonSerializerSettings _serializerSettings;

        public RapydApi(IConfigurationService configurationService, ILoggerService loggerService)
        {
            _rapydApiConfig = configurationService.Get<IRapydApiConfig>();
            _logger = loggerService.GetLogger(nameof(RapydApi));
            _serializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.None,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                Formatting = Formatting.None,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy
                    {
                        OverrideSpecifiedNames = false
                    }
                }
            };
            _serializerSettings.Converters.Add(new StringEnumConverter());

        }

        public async Task<WalletResponseDTO?> CreateWallet(WalletRequestDTO wallet)
        {
            var response = await PostToRapyd<WalletResponseDTO>(wallet, "user").ConfigureAwait(false);

            return response;
        }

        public async Task<WalletResponseDTO?> GetWallet(string id)
        {
            var response = await GetFromRapyd<WalletResponseDTO>("user", id).ConfigureAwait(false);

            return response;
        }

        public async Task<DepositFundsResponseDTO?> DepositFunds(string walletId, string currency, ulong amount)
        {
            var response = await PostToRapyd<DepositFundsResponseDTO>(new DepositFundsRequestDTO
            {
                WalletId = walletId,
                Currency = currency,
                Amount = amount
            }, "account", "deposit");

            return response;
        }

        public async Task<PutFundsOnHoldResponseDTO?> PutFundsOnHold(string walletId, string currency, ulong amount)
        {
            try
            {
                var response = await PostToRapyd<PutFundsOnHoldResponseDTO>(new PutFundsOnHoldDTO
                {
                    WalletId = walletId,
                    Currency = currency,
                    Amount = amount
                }, "account", "balance", "hold");

                return response;
            }
            catch(Exception ex)
            {
                _logger.Error("Failed to put funds on hold", ex);
                throw;
            }
        }

        private async Task<T?> GetFromRapyd<T>(params string[] segments)
        {
            try
            {
                var req = FlurlRequest("GET", null, segments);
                
                _logger.Debug(() => $"Sending Rapyd request: {req.Url}");

                var resp = await req.GetJsonAsync<RapydResponse<T>>().ConfigureAwait(false);

                return resp.Data;
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException is FlurlHttpException fex)
                {
                    var resp = await fex.Call.Response.GetJsonAsync<RapydResponse<object>>().ConfigureAwait(false);
                    _logger.Error($"The request failed with the status: {JsonConvert.SerializeObject(resp.Status)}", fex);
                    throw fex;
                }
                
                _logger.Error("Failed to send request", ex);
                throw;
            }
            catch (FlurlHttpException fex)
            {
                var resp = await fex.Call.Response.GetJsonAsync<RapydResponse<object>>().ConfigureAwait(false);
                _logger.Error($"The request failed with the status: {JsonConvert.SerializeObject(resp.Status)}", fex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error("Failed to send request", ex);
                throw;
            }
        }

        private async Task<T?> PostToRapyd<T>(object payload, params string[] segments)
        {
            try
            {
                var req = FlurlRequest("POST", payload, segments);

                _logger.Debug(() => $"Sending Rapyd request: {req.Url}\r\n{JsonConvert.SerializeObject(payload, Formatting.Indented)}");

                var resp = await req
                    .PostJsonAsync(payload)
                    .ReceiveJson<RapydResponse<T>>()
                    .ConfigureAwait(false);

                return resp.Data;
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException is FlurlHttpException fex)
                {
                    var resp = await fex.Call.Response.GetJsonAsync<RapydResponse<object>>().ConfigureAwait(false);
                    _logger.Error($"The request failed with the status: {JsonConvert.SerializeObject(resp.Status)}", fex);
                    throw fex;
                }

                _logger.Error("Failed to send request", ex);
                throw;
            }
            catch (FlurlHttpException fex)
            {
                if(fex.Call.Response != null)
                {
                    var resp = await fex.Call.Response.GetJsonAsync<RapydResponse<object>>().ConfigureAwait(false);
                    _logger.Error($"The request failed with the status: {JsonConvert.SerializeObject(resp.Status)}", fex);
                }
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error("Failed to send request", ex);
                throw;
            }
        }

        private IFlurlRequest FlurlRequest(string method, object? payload, params string[] segments)
        {
            var salt = GenerateRandomString(12);
            var timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            var body = payload != null ? JsonConvert.SerializeObject(payload, _serializerSettings) : string.Empty;
            var signature = Sign(method, $"/v1/{string.Join('/', segments)}", salt, timestamp, body);

            return _rapydApiConfig.BaseUri
                .AppendPathSegment("v1")
                .AppendPathSegments(segments)
                .WithHeader("Content-Type", "application/json")
                .WithHeader("salt", salt)
                .WithHeader("timestamp", timestamp)
                .WithHeader("signature", signature)
                .WithHeader("access_key", _rapydApiConfig.AccessKey);
        }

        private string Sign(string method, string urlPath, string salt, long timestamp, string body)
        {

            try
            {
                string bodyString = string.Empty;
                if (!string.IsNullOrWhiteSpace(body))
                {
                    bodyString = body == "{}" ? "" : body;
                }

                string toSign =$"{method.ToLower()}{urlPath}{salt}{timestamp}{_rapydApiConfig.AccessKey}{_rapydApiConfig.SecretKey}{bodyString}";
                _logger.Info("\ntoSign: " + toSign);

                UTF8Encoding encoding = new();
                byte[] secretKeyBytes = encoding.GetBytes(_rapydApiConfig.SecretKey);
                byte[] signatureBytes = encoding.GetBytes(toSign);
                string signature = string.Empty;
                using (HMACSHA256 hmac = new(secretKeyBytes))
                {
                    byte[] signatureHash = hmac.ComputeHash(signatureBytes);
                    string signatureHex = string.Concat(Array.ConvertAll(signatureHash, x => x.ToString("x2")));
                    signature = Convert.ToBase64String(encoding.GetBytes(signatureHex));
                }

                _logger.Info("signature: " + signature);

                return signature;
            }
            catch (Exception ex)
            {
                _logger.Error("Error generating signature", ex);
                throw;
            }

        }

        private string GenerateRandomString(int size)
        {
            try
            {
                using RandomNumberGenerator rng = new RNGCryptoServiceProvider();
                byte[] randomBytes = new byte[size];
                rng.GetBytes(randomBytes);
                return string.Concat(Array.ConvertAll(randomBytes, x => x.ToString("x2")));
            }
            catch (Exception ex)
            {
                _logger.Error("Error generating salt", ex);
                throw;
            }
        }
    }
}
