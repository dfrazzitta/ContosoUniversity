using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoUniversity.Pages.evt
{
    public class IndexModel : PageModel
    {
        public  List<evt1> ev = new List<evt1>();

        public void OnGet()
        {
#if _docker
            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile(@"/app/config"); //@"G:\k8slatest\csharp\examples\labels\config");
#else        
        var config = KubernetesClientConfiguration.BuildConfigFromConfigFile(@"G:\k8slatest\csharp\examples\labels\config");
#endif

       //     var config = KubernetesClientConfiguration.BuildConfigFromConfigFile(@"G:\k8slatest\csharp\examples\labels\config");
            IKubernetes client = new Kubernetes(config);
            Console.WriteLine("Starting Request!");
            var evt = client.CoreV1.ListEventForAllNamespaces();
           
            if (evt.Items.Count == 0)
            {
                Console.WriteLine("No pods!");
                return;
            }

        


            foreach (var t in evt)
            {
                var e = new evt1();
                e.Message = t.Message;
                e.Kind= t.Kind;
                e.NameSpace = t.InvolvedObject.NamespaceProperty; //.Namespace;
                e.ObjectName = t.InvolvedObject.Kind;
                e.TimeStamp = t.EventTime.ToString();
                ev.Add(e);
            }
        }
    }
}
