﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Industrial.Data.Domain
{
    public class MainDataContext : DbContext
    {


        public MainDataContext()
            : base("name=MainContext")
        {

        }
        public virtual DbSet<ItemProduct> ItemProducts { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<ItemBOM> ItemBoms { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }


        public override int SaveChanges()
        {

            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException entityException)
            {
                var errors = entityException.EntityValidationErrors;
                var result = new StringBuilder();
                var allErrors = new List<ValidationResult>();
                foreach (var error in errors)
                {
                    foreach (var validationError in error.ValidationErrors)
                    {
                        result.AppendFormat(
                            "\r\n Entity of type {0} has validation error \"{1}\" for property {2}. \r\n",
                            error.Entry.Entity.GetType(), validationError.ErrorMessage, validationError.PropertyName);
                        allErrors.Add(new ValidationResult(validationError.ErrorMessage, new[] { validationError.PropertyName }));

                    }

                }
                throw new ModelValidationException(result.ToString(), entityException, allErrors);

            }
        }


    }
}
