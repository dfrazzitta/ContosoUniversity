using IdentityModel.OidcClient;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContosoUniversity.controllers
{
    [Route("TestX")]
    public class TestX : Controller
    {
        private  IKubernetes _kubernetesClient;

        public TestX(IKubernetes kubernetesClient)
        {
            _kubernetesClient = kubernetesClient;
        }


        public async Task<string> Index(   )
        {
 
            using (var client1 = new System.Net.Http.HttpClient())
            {
          
                var request = new System.Net.Http.HttpRequestMessage();
               
                string uriApi = "http://webweb/api/values/1";
                request.RequestUri = new Uri(uriApi); // ASP.NET 3 (VS 2019 only)

        
                var response = await client1.SendAsync(request);
                var resp = await response.Content.ReadAsStringAsync();
                return resp;
            }

           // var podList = this._kubernetesClient.CoreV1.ListNamespacedPod("default");

            // Return names of pods
           // return podList.Items.Select(pod => pod.Metadata.Name);


           // return View();
        }

        /*
        public async Task<string> GetCust()
        {
            using (var client1 = new System.Net.Http.HttpClient())
            {

                return "";
            }
 
        }
        */

    }
}
