using Microsoft.AspNetCore.WebUtilities;

namespace Cfo.Cats.Server.UI.Extensions;

public static class UriExtensions
{
    public static string AppendQuery(this NavigationManager nav, string path, string key, string value)
    {
        var uri = new Uri(nav.Uri);
        var query = QueryHelpers.ParseQuery(uri.Query)
            .ToDictionary(k => k.Key, v => v.Value.ToString());

        query[key] = value;

        return QueryHelpers.AddQueryString(path, query!);
    }

}