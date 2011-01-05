using System.Linq;

namespace WinPure.ContactManagement.Client.Data.Model.Extensions
{
    public static class CompanyExtension
    {
        public static bool IsCompanyNameUnique(this Company company, EntitiesDataContext context)
        {
            return
                context.Companies.Where(c => c.Name == company.Name && c.CompanyId != company.CompanyId).FirstOrDefault() ==
                null;
        }
    }
}