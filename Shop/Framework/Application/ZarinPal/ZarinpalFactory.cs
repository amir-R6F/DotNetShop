using System;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace Shop.Application.ZarinPal
{
    public class ZarinPalFactory : IZarinPalFactory
    {
        private readonly string _baseUrl;
        private readonly IConfiguration _configuration;

        public ZarinPalFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            Prefix = _configuration.GetSection("payment")["method"];
            MerchantId = _configuration.GetSection("payment")["merchant"];
            _baseUrl = $"https://{Prefix}.zarinpal.com/pg/v4/payment/";


        }

        private string MerchantId { get; }

        public string Prefix { get; set; }

        public PaymentResponse CreatePaymentRequest(string amount, string mobile, string email, string description, long orderId)
        {
            amount = Regex.Replace(amount, @"\D", "");

            var finalAmount = int.Parse(amount);
            var siteUrl = _configuration.GetSection("payment")["siteUrl"];


            var body = new 
            {
                merchant_id  = MerchantId,
                amount = finalAmount,
                callback_url = $"{siteUrl}/Checkout?handler=CallBack&oId={orderId}",
                description = description,
                email = email,
                mobile = mobile
            };
            var json = JsonConvert.SerializeObject(body);

            var client = new RestClient(_baseUrl);
            var request = new RestRequest("request.json", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            
            var response = client.Execute(request);
            
            return JsonConvert.DeserializeObject<PaymentResponse>(response.Content);
        }

        public VerificationResponse CreateVerificationRequest(string authority, string amount)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("verify.json", Method.Post);
            request.AddHeader("Content-Type", "application/json");

            amount = amount.Replace(",", "");
            var finalAmount = int.Parse(amount);

            var body = new
            {
                merchant_id = MerchantId,
                amount = finalAmount,
                authority = authority
            };
            
            var json = JsonConvert.SerializeObject(body);
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            
            var response = client.Execute(request);
            
            Console.WriteLine("Raw Response:");
            Console.WriteLine(response.Content);
            Console.WriteLine("HTTP Status Code: " + response.StatusCode);

            return JsonConvert.DeserializeObject<VerificationResponse>(response.Content);
        }
    }
}