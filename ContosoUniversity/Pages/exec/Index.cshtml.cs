using k8s;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoUniversity.Pages.exec
{
    public class IndexModel : PageModel
    {
        public string[]? strings { get; set; } = default;
        public List<nslookup> nslookup = new List<nslookup>();
        public async Task OnGet()
        {
            //  var config = KubernetesClientConfiguration.BuildConfigFromConfigFile(@"G:\k8slatest\csharp\examples\labels\config");
#if _docker
            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile(@"/app/config"); //@"G:\k8slatest\csharp\examples\labels\config");
#else        
            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile(@"G:\k8slatest\csharp\examples\labels\config");
#endif
            IKubernetes client = new Kubernetes(config);
            Console.WriteLine("Starting Request!");

            var list = client.CoreV1.ListNamespacedPod("default");
            var pod = list.Items[0];


            var webSocket =
                await client.WebSocketNamespacedPodExecAsync(pod.Metadata.Name, "default",
                command: new string[] { "/bin/bash", "-c", $"nslookup 192.168.1.100" },
                    pod.Spec.Containers[0].Name).ConfigureAwait(true);

            var demux = new StreamDemuxer(webSocket);
            demux.Start();

 
            byte[] buff = new byte[500];
           
            var stream = demux.GetStream(1, 1);
            
            var read = stream.Read(buff, 0, 500);
            var str = System.Text.Encoding.Default.GetString(buff);
            int l = str.IndexOf('\0');


            var ct = str.Split("\r\n");
            foreach(string s in ct)
            {
                nslookup ns = new nslookup();
                ns.nslookupresult = s;
                nslookup.Add(ns);

            }
        //    int vt = strings.Length;

        }
    }
}
