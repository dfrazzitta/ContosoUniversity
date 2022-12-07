using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace ContosoUniversity.Pages.TestMe
{
    public class DetailsModel : PageModel
    {
        public List<string> lines1 = new List<string>();
        public string _content = null;
        public string[] blow = new string[30];
        public async Task OnGet(string id, string NameSpace)
        {
            var lines = new List<string>();
            var started = new ManualResetEvent(false);


            //  var config = KubernetesClientConfiguration.BuildConfigFromConfigFile("/config");
            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile(@"G:\k8slatest\csharp\examples\labels\config");
            IKubernetes client = new Kubernetes(config);
            Console.WriteLine("Starting Request!");

             


            var list = client.CoreV1.ListNamespacedPod(NameSpace);
            if (list.Items.Count == 0)
            {
                Console.WriteLine("No pods!");
               // return;
            }

            var pod = list.Items[1];

            string vb = null;

            foreach (var t in list)
            {
                Console.WriteLine(t.Metadata.Name);
              
                if (id.CompareTo(t.Metadata.Name) == 0)
                {
                    int ct = t.Spec.Containers.Count;
                    /*
                    if (ct == 1)
                    {
                        var response = await client.CoreV1.ReadNamespacedPodLogWithHttpMessagesAsync(
                            id,
                            NameSpace, container: pod.Spec.Containers[0].Name, follow: false).ConfigureAwait(true);
                        var stream = response.Body;

                        var buff = new byte[8192];
                        stream.Read(buff, 0, 8192);
                        _content = System.Text.Encoding.Default.GetString(buff);
                        blow = _content.Split("\n", 30, StringSplitOptions.RemoveEmptyEntries);
                        return;
                    }
                    else
                    {  */
                        int ctr = t.Spec.Containers.Count; //
                        for(int j=0; j< ctr; j++)
                        {
                            if (id.Contains(t.Spec.Containers[j].Name))
                            {
                                var response = await client.CoreV1.ReadNamespacedPodLogWithHttpMessagesAsync(
                                   id,
                                   NameSpace, container: t.Spec.Containers[j].Name, follow: false).ConfigureAwait(true);
                                var stream = response.Body;

                                var buff = new byte[8192];
                                stream.Read(buff, 0, 8192);
                                _content = System.Text.Encoding.Default.GetString(buff);
                                blow = _content.Split("\n", 30, StringSplitOptions.RemoveEmptyEntries);

                             
                                return;
                            }
                        }


                   // }
 
                }

                /*
                               string str1 = t.Name();
                               if (t.Name().CompareTo("sqlserver-fff5db8cd-459hs") == 0)
                               {
                                   pod = t;

                               }
                */
            }


            /*
            var response = await client.CoreV1.ReadNamespacedPodLogWithHttpMessagesAsync(
                id,
                NameSpace, container: pod.Spec.Containers[1].Name, follow: true).ConfigureAwait(true);
            var stream = response.Body;

            var buff = new byte[8192];
            stream.Read(buff, 0, 8192);
            _content = System.Text.Encoding.Default.GetString(buff);
            blow = _content.Split("\n", 30, StringSplitOptions.RemoveEmptyEntries);

            */
            int jj = 0;
        }
    }
}



/*
  // var lines = new List<string>();
  var started = new ManualResetEvent(false);

  var config = KubernetesClientConfiguration.BuildConfigFromConfigFile(@"G:\k8slatest\csharp\examples\labels\config");

  IKubernetes client = new Kubernetes(config);
  Console.WriteLine("Starting Request!");

  var list = client.CoreV1.ListNamespacedPod("default");
  if (list.Items.Count == 0)
  {
      Console.WriteLine("No pods!");
    //  return;
  }

  var pod = id;

  var response = await client.CoreV1.ReadNamespacedPodLogWithHttpMessagesAsync(
        pod.Metadata.Name,
        pod.Metadata.NamespaceProperty, container: pod.Spec.Containers[1].Name, follow: true).ConfigureAwait(false);
              var stream = response.Body;

  var buff = new byte[8192];
  stream.Read(buff, 0, 8192);
  var str = System.Text.Encoding.Default.GetString(buff);
  */

/*      // id, container: id, follow: true).ConfigureAwait(false);
       var stream1 = response;
      var stream11 = client.CoreV1.ReadNamespacedPodLog(id, NameSpace, follow: true);
      var reader1 = new StreamReader(stream11);
      while (!reader1.ReadLine().IsNullOrEmpty())
      {
          try
          {
              lines.Add(reader1.Rea                }
dLine());
          catch(Exception ex)
          {
              Console.WriteLine(ex.Message);
          }


      }
*/

// stream.CopyTo(Console.OpenStandardOutput());