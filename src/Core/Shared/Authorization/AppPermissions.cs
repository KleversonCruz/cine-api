using System.Collections.ObjectModel;

namespace Shared.Authorization
{
    public static class AppAction
    {
        public const string View = nameof(View);
        public const string List = nameof(List);
        public const string Create = nameof(Create);
        public const string Update = nameof(Update);
        public const string Delete = nameof(Delete);
    }

    public static class AppResource
    {
        public const string Users = nameof(Users);
        public const string UserRoles = nameof(UserRoles);
        public const string Roles = nameof(Roles);
        public const string RoleClaims = nameof(RoleClaims);
        public const string Cinemas = nameof(Cinemas);
        public const string Filmes = nameof(Filmes);
        public const string Gerentes = nameof(Gerentes);
        public const string Sessoes = nameof(Sessoes);
    }

    public static class AppPermissions
    {
        private static readonly AppPermission[] _all = new AppPermission[]
        {
        new("View Users", AppAction.View, AppResource.Users),
        new("List Users", AppAction.List, AppResource.Users),
        new("Create Users", AppAction.Create, AppResource.Users),
        new("Update Users", AppAction.Update, AppResource.Users),
        new("Delete Users", AppAction.Delete, AppResource.Users),
        new("View UserRoles", AppAction.View, AppResource.UserRoles),
        new("Update UserRoles", AppAction.Update, AppResource.UserRoles),
        new("View Roles", AppAction.View, AppResource.Roles),
        new("Create Roles", AppAction.Create, AppResource.Roles),
        new("Update Roles", AppAction.Update, AppResource.Roles),
        new("Delete Roles", AppAction.Delete, AppResource.Roles),
        new("View RoleClaims", AppAction.View, AppResource.RoleClaims),
        new("Update RoleClaims", AppAction.Update, AppResource.RoleClaims),

        new("View Cinemas", AppAction.View, AppResource.Cinemas, IsBasic: true),
        new("List Cinemas", AppAction.List, AppResource.Cinemas, IsBasic: true),
        new("Create Cinemas", AppAction.Create, AppResource.Cinemas),
        new("Update Cinemas", AppAction.Update, AppResource.Cinemas),
        new("Delete Cinemas", AppAction.Delete, AppResource.Cinemas),

        new("View Filmes", AppAction.View, AppResource.Filmes, IsBasic: true),
        new("List Filmes", AppAction.List, AppResource.Filmes, IsBasic: true),
        new("Create Filmes", AppAction.Create, AppResource.Filmes),
        new("Update Filmes", AppAction.Update, AppResource.Filmes),
        new("Delete Filmes", AppAction.Delete, AppResource.Filmes),

        new("View Gerentes", AppAction.View, AppResource.Gerentes, IsBasic: true),
        new("List Gerentes", AppAction.List, AppResource.Gerentes, IsBasic: true),
        new("Create Gerentes", AppAction.Create, AppResource.Gerentes),
        new("Update Gerentes", AppAction.Update, AppResource.Gerentes),
        new("Delete Gerentes", AppAction.Delete, AppResource.Gerentes),

        new("View Sessoes", AppAction.View, AppResource.Sessoes, IsBasic: true),
        new("List Sessoes", AppAction.List, AppResource.Sessoes, IsBasic: true),
        new("Create Sessoes", AppAction.Create, AppResource.Sessoes),
        new("Update Sessoes", AppAction.Update, AppResource.Sessoes),
        new("Delete Sessoes", AppAction.Delete, AppResource.Sessoes),
        };

        public static IReadOnlyList<AppPermission> All { get; } = new ReadOnlyCollection<AppPermission>(_all);
        public static IReadOnlyList<AppPermission> Root { get; } = new ReadOnlyCollection<AppPermission>(_all.Where(p => p.IsRoot).ToArray());
        public static IReadOnlyList<AppPermission> Admin { get; } = new ReadOnlyCollection<AppPermission>(_all.Where(p => !p.IsRoot).ToArray());
        public static IReadOnlyList<AppPermission> Basic { get; } = new ReadOnlyCollection<AppPermission>(_all.Where(p => p.IsBasic).ToArray());
    }

    public record AppPermission(string Description, string Action, string Resource, bool IsBasic = false, bool IsRoot = false)
    {
        public string Name => NameFor(Action, Resource);
        public static string NameFor(string action, string resource) => $"Permissions.{resource}.{action}";
    }
}
