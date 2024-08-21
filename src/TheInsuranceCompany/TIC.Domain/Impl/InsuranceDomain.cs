﻿using TIC.DomainModel;
using TIC.DomainModel.Request;
using TIC.ServiceAdapter;

namespace TIC.DomainAPI.Impl
{
    public class InsuranceDomain : IInsuranceDomain
    {
        private readonly IInsuranceProvider _insuranceProvider;

        public InsuranceDomain(IInsuranceProvider insuranceProvider)
        {
            _insuranceProvider = insuranceProvider;
        }

        public IEnumerable<Insurance> GetInsurances(GetInsurancesRequest getInsurancesRequest)
        {
            return _insuranceProvider.GetInsurances();
        }

        public void AddInsurance(Insurance insurance)
        {
            _insuranceProvider.AddInsurance(insurance);
        }

        public IEnumerable<TravelInsurance> GetDutchTravelInsurances(GetDutchTravelInsurancesRequest getDutchTravelInsuranceRequest)
        {
            var travelInsurance = _insuranceProvider.GetInsurances().OfType<TravelInsurance>().Where(x => x.Coverage is not null && x.Coverage.Any(y => y.Code == "NL"))
                .Select(t => new DomainModel.TravelInsurance
                {
                    Name = t.Name,
                    Description = t.Description,
                    InsurancePremium = t.InsurancePremium,
                    InsuredAmount = t.InsuredAmount,
                    Coverage = t.Coverage

                });



            return travelInsurance;
        }
    }
}