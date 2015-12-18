using System.Collections.Generic;

namespace ebIModels.Models
{
    public interface ICountryCodes
    {
        List<CountryCodeModel> GetCountryCodeList();
    }
}
