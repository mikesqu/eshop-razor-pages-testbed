using MadStickWebAppTester.Data;
using MadStickWebAppTester.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace MadStickWebAppTester.Utilities
{
    public class SlugGenerator : ValueGenerator<string>
    {
        public override bool GeneratesTemporaryValues => false;

        public SlugGenerator()
        {
        }
        /// <summary>
        /// Get current account number from table and increment by 1
        /// </summary>
        /// <returns>Next account number or recycle back to first alpha number</returns>
        private string NextSlugNumber(EntityEntry entry)
        {
            string currentProductNameSlugged = entry.CurrentValues.Properties.Where(p => p.Name == "Name").First().ToString().ToSlug();
            if(currentProductNameSlugged != null)
            {
                return currentProductNameSlugged;
            }
            else
            {
                throw new NullReferenceException("SlugName can't be null");
            }
            
        }

        /// <summary>
        /// Template method to perform value generation for AccountNumber.
        /// </summary>
        /// <param name="entry">In this case Customer</param>
        /// <returns>Current account number</returns>
        public override string Next(EntityEntry entry) => NextSlugNumber(entry);
        /// <summary>
        /// Gets a value to be assigned to AccountNumber property
        /// </summary>
        /// <param name="entry">In this case Customer</param>
        /// <returns>Current account number</returns>
        protected override object NextValue(EntityEntry entry) => NextSlugNumber(entry);
    }
}
