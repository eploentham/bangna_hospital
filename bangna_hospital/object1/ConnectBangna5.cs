using NgrokApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class ConnectBangna5
    {
        public ConnectBangna5()
        {

        }
        public async void TestConnect()
        {
            try
            {
                var ngrok = new Ngrok(Environment.GetEnvironmentVariable("NGROK_API_KEY"));
                var policy = await ngrok.IpPolicies.Create(new IpPolicyCreate());

                foreach (var cidr in new string[] { "24.0.0.0/8", "12.0.0.0/8" })
                {
                    await ngrok.IpPolicyRules.Create(new IpPolicyRuleCreate
                    {
                        Cidr = cidr,
                        IpPolicyId = policy.Id,
                        Action = "allow",
                    });
                }
            }
            catch (NgrokException e)
            {
                if (e.IsErrorCode("ERR_NGROK_402", "ERR_NGROK_403"))
                {
                    Console.Out.WriteLine("Ignoring invalid wildcard domain.");
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
