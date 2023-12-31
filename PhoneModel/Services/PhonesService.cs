﻿using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneModel.Models;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace PhoneModel.Services
{
    public class PhonesService
    {
        private CountryData[] Countries { get; set; }
        private bool IsEmpty = true;
        public PhonesService() 
        {
            Countries = JsonConvert.DeserializeObject<CountryData[]>(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "CountryCodes.js")));
            IsEmpty = false;
        }

        public Country[] CountriesList(string language)
        {
            if (!IsEmpty)
            {
                var result = (
                    from 
                        country in Countries
                    select 
                        new Country
                        {
                            Code = country.Code,
                            DisplayText = $"{country.CountryNames.FirstOrDefault(item => item.Language == language).Text} ({country.Code})"
                        }
                        ).ToArray();  
                return result;
            }

            return null;
        }

        public string GetValidationNumber(string number)
        {
            string resultText = Regex.Replace(number, "[^0-9]", string.Empty);
            if (resultText.Length < 8 || resultText.Length > 15)
                throw new Exception("The string is not a phone number.");
            resultText = resultText[0] == '0' ? resultText.Substring(1) : resultText;
            return resultText;
        }
    }

    public static class PhonesServiceExtension
    {
        public static void AddPhonesService(this IServiceCollection services, IConfiguration configuration = null)
        {
            services.AddSingleton<PhonesService>();
        }
    }
}
