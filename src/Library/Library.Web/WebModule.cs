using Autofac;
using Library.Web.Areas.Admin.Models;

public class WebModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<BookCreateModel>().AsSelf();
        builder.RegisterType<BookListModel>().AsSelf();
        builder.RegisterType<BookUpdateModel>().AsSelf();
    }
}