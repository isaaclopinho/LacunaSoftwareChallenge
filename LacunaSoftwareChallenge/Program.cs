using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LacunaSoftwareChallenge
{
    public class Result
    {
       public string token;
       public  string status;
    }
    class Program
    {

       

        static async Task Main(string[] args)
        {
            string xor(string a, string b)
            {
                string str = "";
                for (int i = 0; i < a.Length; i++)
                {
                    var h = (Convert.ToInt64(a[i].ToString(), 16) ^ Convert.ToInt64(b[i].ToString(), 16));
                    str += h.ToString("X");
                }
                return str;
            }

            string HexStringToString(string hexString)
            {
                return string.Join("", Regex.Split(hexString, "(?<=\\G..)(?!$)").Select(x => (char)Convert.ToByte(x, 16)));
            }

            string StringToHexString(string str)
            {
                return string.Join("", str.Select(c => ((int)c).ToString("X2")));
            }

            string ReplaceUsernameInToken(string token, string username)
            {
                return token.Substring(0, 12) + username + token.Substring(12 + username.Length);
            }

            var firstUsername = "00000000000000000000000000000000";
            var r = StringToHexString(firstUsername);

            var firstUsernameCrypted = "8AB433807445B548A7D07CCD4A54139DCEC628DB22DC4A0AD5E19FFA2BFECCAF";
            var key = xor(r, firstUsernameCrypted);

            Console.WriteLine(key);

            var secondUsername = "8AB433807445B548B4C36FDE5947008EDDD53BC831CF5919C6F28CE938EDDFBC";
            var secondUsernameDecrypted = xor(secondUsername, key);

            Console.WriteLine(HexStringToString(secondUsernameDecrypted));

            var masterUsername = "master##########################";
            var masterUsernameEncrypted = xor(StringToHexString(masterUsername), key);

            Console.WriteLine("MasterUsername: " + masterUsernameEncrypted);


            var data = new
            {
                username = "00000000",
                password = "godgodgod"
            };

            var client = new HttpClient();


            string json = JsonConvert.SerializeObject(data);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            client.BaseAddress = new Uri("https://weak-system-lab.lacunasoftware.com/");
            var response = await client.PostAsJsonAsync("api/users/login", data);
            var result = response.Content.ReadAsStringAsync().Result;
            var ob = JsonConvert.DeserializeObject<Result>(result);

            var token = ReplaceUsernameInToken(ob.token, masterUsernameEncrypted);
            
           client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var responseSecret = await client.GetAsync("api/secret");
            
            var resultSecret = responseSecret.Content.ReadAsStringAsync().Result;
            Console.WriteLine("Result: " + resultSecret);

            Console.ReadLine();
        }
    }
}
