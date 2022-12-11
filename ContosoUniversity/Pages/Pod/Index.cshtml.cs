using k8s.Models;
using k8s;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoUniversity.Pages.Pod
{
    public class IndexModel : PageModel
    {
        public IEnumerable<V1Pod> pd { get; set; }
        public  V1PodList pd1;
        public void OnGet()
        {
            //  var config = KubernetesClientConfiguration.BuildConfigFromConfigFile(@"G:\k8slatest\csharp\examples\labels\config");
#if _docker
            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile(@"/app/config"); //@"G:\k8slatest\csharp\examples\labels\config");
#else        
            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile(@"G:\k8slatest\csharp\examples\labels\config");
#endif
            IKubernetes client = new Kubernetes(config);
            Console.WriteLine("Starting Request!");

            pd1 = client.CoreV1.ListPodForAllNamespaces(); //.ListNamespacedPod("default"); 
            pd = pd1.Items.ToList();
           // ViewData["pods"] = pd;
 
        }


        public void log()
        {
#if _docker
            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile(@"/app/config"); //@"G:\k8slatest\csharp\examples\labels\config");
#else        
            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile(@"G:\k8slatest\csharp\examples\labels\config");
#endif//  
            //var config = KubernetesClientConfiguration.BuildConfigFromConfigFile(@"G:\k8slatest\csharp\examples\labels\config");
            IKubernetes client = new Kubernetes(config);
            Console.WriteLine("Starting Request!");

            pd1 = client.CoreV1.ListPodForAllNamespaces(); //.ListNamespacedPod("default"); 
            pd = pd1.Items.ToList();
            // ViewData["pods"] = pd;
            
        }
    }
}
