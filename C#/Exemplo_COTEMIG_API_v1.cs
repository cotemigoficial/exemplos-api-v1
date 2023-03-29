// Documentação disponível em: https://api.cotemig.com.br/v1/doc
using Newtonsoft.Json; // Json.NET => baixe pelo gerenciador de pacotes NuGet
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Exemplo_COTEMIG_API {
    public class Perfil {
        public string Id { get; set; }
        public string Nome { get; set; }        
    }
    class Exemplo_COTEMIG_API_v1 {
        static HttpClient client = new HttpClient();

        static async Task<Perfil> GetPerfilAsync(string path) {
            Perfil perfil = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode) {
                string strresponse = await response.Content.ReadAsStringAsync();
                perfil = JsonConvert.DeserializeObject<Perfil>(strresponse);
            }
            else {
                throw new Exception(response.StatusCode.ToString());
            }
            return perfil;
        }
        static void Main(string[] args) {
            RunAsync().Wait();
        }
        static async Task RunAsync() {
            client.BaseAddress = new Uri("https://api.cotemig.com.br/v1/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", 
                Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", "USUARIO", "SENHA")))); // substitua pelos dados do usuário

            try {
                Perfil perfil = await GetPerfilAsync("perfil");
                Console.WriteLine("Código: " + perfil.Id);
                Console.WriteLine("Nome: " + perfil.Nome);
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
