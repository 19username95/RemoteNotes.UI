using Autofac;
using RemoteNotes.Service.Client.Contract;

namespace RemoteNotes.Service.Client.Stub
{
    public class HubModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<FrontServiceClient>().As<IFrontServiceClient>();
        }
    }
}
