using ContosoUniversity.Models;
using ContosoUniversity.Pages.Models;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoUniversity.Pages.TestMe
{
    
    public class IndexModel : PageModel
    {
        List<PodInfo> pinfo = new List<PodInfo>();
        public string LogItem { get; set; } 
        public IEnumerable<V1Pod> pd { get; set; }
        public V1PodList pd1;
        public PaginatedListPod<PodInfo> podInfoPage { get; set; }
        private readonly IConfiguration Configuration;
        public string CurrentFilter { get; set; }

        public IndexModel(IConfiguration configuration)
        {
            
            Configuration = configuration;
        }


        public void OnGet(int? pageIndex, string CurrentFilter)
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

            foreach(var obj in pd1)
            {
                PodInfo pf = new PodInfo();
                pf.ControllerName = obj.Metadata.Name;
                pf.NameSpace = obj.Metadata.NamespaceProperty;
                pinfo.Add(pf);

                string ty = obj.Metadata.Name;
            }
            var pageSize = pinfo.Count; // Configuration.GetValue("PageSize", 4);
            podInfoPage =  PaginatedListPod<PodInfo>.CreateAsync(pinfo, pageIndex ?? 1, pageSize);
           // return podInfoPage;

        }
    }
}
