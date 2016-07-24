using Autofac;
using Bitly.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitly.Business
{
    public class AutofacConfig
    {
        public static void Build(ContainerBuilder builder)
        {
            builder.RegisterType<BitlyContext>().As<BitlyContext>();
        }
    }
}
