using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace BaseNet.Libs.Data.SDK.Base
{
    public static class ModelBuilderExtensions
    {
        public static void NamesToSnakeCase(this ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName().ToSnakeCase());
            }
        }

        private static string? ToSnakeCase(this string? input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            var startUnderscores = Regex.Match(input, @"^_+");
            return startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
        }
    }
}