using Autofac;
using RemoteNotes.Service.Authentication;
using RemoteNotes.Service.Note;
using RemoteNotes.Service.Storage;
using RemoteNotes.Service.User;

namespace RemoteNotes.Service
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>();
            builder.RegisterType<NoteService>().As<INoteService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<StorageService>().As<IStorageService>();
        }
    }
}
