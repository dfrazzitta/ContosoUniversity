using k8s;
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


        public IEnumerable<string> Index(   )
        {
            

            var podList = this._kubernetesClient.CoreV1.ListNamespacedPod("default");

            // Return names of pods
            return podList.Items.Select(pod => pod.Metadata.Name);


           // return View();
        }

  
    }
}
