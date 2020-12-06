using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Auer.Models;
using Auer.Api;
using System.Threading.Tasks;

namespace Auer.API
{
    public static class NHTSAClient
    {
        public static async Task<NHTSAVehicleFullWrapper> GetVehicleData(string VIN)
        {
            string json = await ApiClient.CallWebService(new Request
            {
                Method = Method.GET,
                Url = $"https://vpic.nhtsa.dot.gov/api/vehicles/decodevinvalues/{VIN}?format=json"
            });
            return JsonConvert.DeserializeObject<NHTSAVehicleFullWrapper>(json);
        }


        
    }
}
