using System;
using System.Collections.Generic;
using System.Linq;
using Hospital.Common.Models;

namespace Hospital.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<string> ToShortInfo(this IEnumerable<Patient> patients)
        {
            if (patients == null) yield break;
            foreach (var p in patients)
            {
                yield return $"{p.FullName()} | MRN: {p.MedicalRecordNumber} | Admitted: {p.IsAdmitted}";
            }
        }
        public static IEnumerable<Patient> FilterByInsurance(this IEnumerable<Patient> patients, string insurance)
        {
            return patients?.Where(p => string.Equals(p.InsuranceCompany, insurance, StringComparison.OrdinalIgnoreCase)) ?? Enumerable.Empty<Patient>();
        }
    }
}