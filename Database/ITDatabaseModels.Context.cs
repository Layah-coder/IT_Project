﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IT_Project.Database
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Core.Objects.DataClasses;
    using System.Linq;

    public partial class IT_DatabaseEntities : DbContext
    {
        public IT_DatabaseEntities()
            : base("name=IT_DatabaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Service_Request> Service_Request { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public virtual int sp_UpdateServiceRequest(Nullable<int> Id, Nullable<DateTime> date_created, Nullable<System.DateTime> date_completed, 
            string technician_name, string description, string message, string ticket_status )
        {
            var IdParameter = Id.HasValue ?
                new ObjectParameter("Id", Id) :
                new ObjectParameter("Id", typeof(int));

            var date_createdParameter = date_created.HasValue ?
                new ObjectParameter("date_created", date_created) :
                new ObjectParameter("date_created", typeof(DateTime));

            var date_completedParameter = date_created.HasValue ?
                new ObjectParameter("date_completed", date_completed) :
                new ObjectParameter("date_completed", typeof(DateTime));

            var technician_nameParameter = technician_name != null ?
                new ObjectParameter("technician_name", technician_name) :
                new ObjectParameter("technician_name", typeof(string));

            var descriptionParameter = description != null ?
                new ObjectParameter("description", description) :
                new ObjectParameter("description", typeof(string));

            var messageParameter = message != null ?
                new ObjectParameter("message", message) :
                new ObjectParameter("message", typeof(string));

            var ticket_statusParameter = ticket_status != null ?
                new ObjectParameter("ticket_status", ticket_status) :
                new ObjectParameter("ticket_status", typeof(string));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_UpdateServiceRequest", IdParameter, date_createdParameter, date_completedParameter, 
                technician_nameParameter, descriptionParameter, messageParameter, ticket_statusParameter);

        }
    }
}