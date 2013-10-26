using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace WordPress_Sharp.Web.Build
{
    class Program
    {
        static void Main(string[] args)
        {
            string wpRoot = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..", "WordPress-Sharp.Web");
            List<string> phpcArgs = new List<string>
            {
                "/target:web",
                string.Format("/recurse:{0}", wpRoot),
                string.Format("/root:{0}", wpRoot),
                "/static+",
                string.Format("/skip:{0}", Path.Combine(wpRoot, "wp-config-sample.php")),
#if DEBUG
                "/debug+",
#else
                "/debug-",
#endif
            };
            Process phpc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "phpc",
                    Arguments = phpcArgs.Concat(" ", "\""),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }
            };
            Console.WriteLine("Building with {0}", phpc.StartInfo.Arguments);
            phpc.Start();
            using (phpc.StandardOutput)
            {
                File.WriteAllText("build.log", phpc.StandardOutput.ReadToEnd());
            }
            using (phpc.StandardError)
            {
                File.WriteAllText("build.error.log", phpc.StandardError.ReadToEnd());
            }
            phpc.WaitForExit();
            int code = phpc.ExitCode;
            if (code > 0)
                Console.WriteLine("Exited with {0}", code);
            else
                Console.WriteLine("Success!");
        }
    }
}
