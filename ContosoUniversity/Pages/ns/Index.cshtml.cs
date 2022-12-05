using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoUniversity.Pages.ns
{
    public class IndexModel : PageModel
    {
        public IEnumerable<V1Namespace> pd { get; set; }
        public V1NamespaceList pd1;

        public void OnGet()
        {
            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile(@"G:\k8slatest\csharp\examples\labels\config");
            IKubernetes client = new Kubernetes(config);
          

            pd1 = client.CoreV1.ListNamespace(); //.ListNamespacedPod("default"); 
            pd = pd1.Items.ToList();
          //  ViewData["pods"] = pd;



        }
    }
}
