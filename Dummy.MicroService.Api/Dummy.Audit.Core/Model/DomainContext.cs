using System;
using System.Collections.Generic;
using Dummy.Audit.Core.Model.ClassesDomain;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Dummy.Audit.Core.Model;

public partial class DomainContext : DbContext
{
    public DomainContext()
    {
    }

    public DomainContext(DbContextOptions<DomainContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AddressType> AddressTypes { get; set; }

    public virtual DbSet<AppointmentStateType> AppointmentStateTypes { get; set; }

    public virtual DbSet<AppointmentsCancellationReason> AppointmentsCancellationReasons { get; set; }

    public virtual DbSet<BudgetPaymentMethod> BudgetPaymentMethods { get; set; }

    public virtual DbSet<BusinessUnit> BusinessUnits { get; set; }

    public virtual DbSet<CardType> CardTypes { get; set; }

    public virtual DbSet<ChecklistAttributesInputType> ChecklistAttributesInputTypes { get; set; }

    public virtual DbSet<ChecklistInputType> ChecklistInputTypes { get; set; }

    public virtual DbSet<ChecklistVehicleType> ChecklistVehicleTypes { get; set; }

    public virtual DbSet<ContactType> ContactTypes { get; set; }

    public virtual DbSet<ContractStateType> ContractStateTypes { get; set; }

    public virtual DbSet<CounterpartType> CounterpartTypes { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<DiaryType> DiaryTypes { get; set; }

    public virtual DbSet<DocumentType> DocumentTypes { get; set; }

    public virtual DbSet<EmailType> EmailTypes { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeType> EmployeeTypes { get; set; }

    public virtual DbSet<ExtAppointmentConfigurationType> ExtAppointmentConfigurationTypes { get; set; }

    public virtual DbSet<ExtAppointmentDetailType> ExtAppointmentDetailTypes { get; set; }

    public virtual DbSet<ExtAppointmentStateType> ExtAppointmentStateTypes { get; set; }

    public virtual DbSet<ExtAppointmentType> ExtAppointmentTypes { get; set; }

    public virtual DbSet<FinancialInstitutionType> FinancialInstitutionTypes { get; set; }

    public virtual DbSet<FuelType> FuelTypes { get; set; }

    public virtual DbSet<IvaConditionType> IvaConditionTypes { get; set; }

    public virtual DbSet<MaritalStatus> MaritalStatuses { get; set; }

    public virtual DbSet<OrderStateType> OrderStateTypes { get; set; }

    public virtual DbSet<OrderType> OrderTypes { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<PeriodType> PeriodTypes { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<PersonType> PersonTypes { get; set; }

    public virtual DbSet<PhoneType> PhoneTypes { get; set; }

    public virtual DbSet<ProductRelationshipsType> ProductRelationshipsTypes { get; set; }

    public virtual DbSet<ProductRequestCancellationsReason> ProductRequestCancellationsReasons { get; set; }

    public virtual DbSet<ProductRequestChannel> ProductRequestChannels { get; set; }

    public virtual DbSet<ProductRequestStateType> ProductRequestStateTypes { get; set; }

    public virtual DbSet<ProductSpecType> ProductSpecTypes { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    public virtual DbSet<ProductsUnitOfMeasurementType> ProductsUnitOfMeasurementTypes { get; set; }

    public virtual DbSet<PropertyType> PropertyTypes { get; set; }

    public virtual DbSet<QaControlCalificationType> QaControlCalificationTypes { get; set; }

    public virtual DbSet<ReportTemplateContentType> ReportTemplateContentTypes { get; set; }

    public virtual DbSet<ReportTemplateType> ReportTemplateTypes { get; set; }

    public virtual DbSet<SocietyType> SocietyTypes { get; set; }

    public virtual DbSet<SupplierArea> SupplierAreas { get; set; }

    public virtual DbSet<UnitOfMeasurementMultiplier> UnitOfMeasurementMultipliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VehicleInspectionType> VehicleInspectionTypes { get; set; }

    public virtual DbSet<VehicleType> VehicleTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=aftrsls-testing-db.cjzjmkcngiws.us-east-1.rds.amazonaws.com;port=3306;database=timm30_aftersales_db;user=timm30_aftersales_usr;password=timm30-after-sales-password", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.35-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<AddressType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("address_types");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<AppointmentStateType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("appointment_state_type");

            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<AppointmentsCancellationReason>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("appointments_cancellation_reasons");

            entity.Property(e => e.Name).HasMaxLength(150);
        });

        modelBuilder.Entity<BudgetPaymentMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("budget_payment_methods");

            entity.HasIndex(e => e.BudgetId, "IX_budget_payment_methods_BudgetId");

            entity.HasIndex(e => e.CardId, "IX_budget_payment_methods_CardId");

            entity.HasIndex(e => e.FinancialInstitutionId, "IX_budget_payment_methods_FinancialInstitutionId");

            entity.HasIndex(e => e.PaymentMethodId, "IX_budget_payment_methods_PaymentMethodId");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.BudgetPaymentMethods).HasForeignKey(d => d.PaymentMethodId);
        });

        modelBuilder.Entity<BusinessUnit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("business_units");

            entity.HasIndex(e => e.AddressId, "IX_business_units_AddressId");

            entity.HasIndex(e => e.BusinessUnitTypeId, "IX_business_units_BusinessUnitTypeId");

            entity.HasIndex(e => e.CompanyId, "IX_business_units_CompanyId");

            entity.HasIndex(e => e.EmailId, "IX_business_units_EmailId");

            entity.HasIndex(e => e.FileId, "IX_business_units_FileId");

            entity.HasIndex(e => e.PhoneId, "IX_business_units_PhoneId");

            entity.Property(e => e.CreationDate).HasMaxLength(6);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasDefaultValueSql("''");
        });

        modelBuilder.Entity<CardType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("card_types");

            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<ChecklistAttributesInputType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("checklist_attributes_input_types");

            entity.Property(e => e.Name).HasMaxLength(500);
        });

        modelBuilder.Entity<ChecklistInputType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("checklist_input_types");

            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<ChecklistVehicleType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("checklist_vehicle_types");

            entity.HasIndex(e => e.ChecklistId, "IX_checklist_vehicle_types_ChecklistId");

            entity.HasIndex(e => e.VehicleTypeId, "IX_checklist_vehicle_types_VehicleTypeId");

            entity.Property(e => e.Enabled).HasDefaultValueSql("'1'");

            entity.HasOne(d => d.VehicleType).WithMany(p => p.ChecklistVehicleTypes).HasForeignKey(d => d.VehicleTypeId);
        });

        modelBuilder.Entity<ContactType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("contact_types");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<ContractStateType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("contract_state_types");

            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<CounterpartType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("counterpart_types");

            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("customers");

            entity.HasIndex(e => e.PersonId, "IX_Customers_PersonId");

            entity.HasOne(d => d.Person).WithMany(p => p.Customers)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK_Customers_People_PersonId");
        });

        modelBuilder.Entity<DiaryType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("diary_types");

            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<DocumentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("document_types");

            entity.Property(e => e.Abbreviation).HasMaxLength(15);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<EmailType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("email_types");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("employees");

            entity.HasIndex(e => e.CompanyId, "IX_Employees_CompanyId");

            entity.HasIndex(e => e.EmployeeTypeId, "IX_Employees_EmployeeTypeId");

            entity.HasIndex(e => e.PersonId, "IX_Employees_PersonId");

            entity.Property(e => e.DisableDate).HasMaxLength(6);

            entity.HasOne(d => d.EmployeeType).WithMany(p => p.Employees)
                .HasForeignKey(d => d.EmployeeTypeId)
                .HasConstraintName("FK_Employees_employee_types_EmployeeTypeId");

            entity.HasOne(d => d.Person).WithMany(p => p.Employees)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK_Employees_People_PersonId");
        });

        modelBuilder.Entity<EmployeeType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("employee_types");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<ExtAppointmentConfigurationType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ext_appointment_configuration_types");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<ExtAppointmentDetailType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ext_appointment_detail_types");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<ExtAppointmentStateType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ext_appointment_state_types");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<ExtAppointmentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ext_appointment_types");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<FinancialInstitutionType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("financial_institution_types");

            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<FuelType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("fuel_types");

            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<IvaConditionType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("iva_condition_types");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<MaritalStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("marital_statuses");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<OrderStateType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("order_state_types");

            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<OrderType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("order_types");

            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("payment_methods");

            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<PeriodType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("period_types");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("people");

            entity.HasIndex(e => e.GenderId, "IX_People_GenderId");

            entity.HasIndex(e => e.LegalStatusId, "IX_People_LegalStatusId");

            entity.HasIndex(e => e.MaritalStatusId, "IX_People_MaritalStatusId");

            entity.HasIndex(e => e.OriginCountryId, "IX_People_OriginCountryId");

            entity.HasIndex(e => e.OriginProvinceId, "IX_People_OriginProvinceId");

            entity.HasIndex(e => e.PersonTaxConditionId, "IX_People_PersonTaxConditionId");

            entity.HasIndex(e => e.PublicEntityId, "IX_People_PublicEntityId");

            entity.HasIndex(e => e.SocietyTypeId, "IX_People_SocietyTypeId");

            entity.Property(e => e.BusinessName).HasMaxLength(150);
            entity.Property(e => e.FantasyName).HasMaxLength(150);
            entity.Property(e => e.InscriptionNumber).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Profession).HasMaxLength(150);
            entity.Property(e => e.StartActivity).HasMaxLength(6);
            entity.Property(e => e.Surname).HasMaxLength(50);

            entity.HasOne(d => d.MaritalStatus).WithMany(p => p.People)
                .HasForeignKey(d => d.MaritalStatusId)
                .HasConstraintName("FK_People_marital_statuses_MaritalStatusId");

            entity.HasOne(d => d.SocietyType).WithMany(p => p.People)
                .HasForeignKey(d => d.SocietyTypeId)
                .HasConstraintName("FK_People_society_types_SocietyTypeId");
        });

        modelBuilder.Entity<PersonType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("person_types");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<PhoneType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("phone_types");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<ProductRelationshipsType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("product_relationships_types");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<ProductRequestCancellationsReason>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("product_request_cancellations_reasons");

            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<ProductRequestChannel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("product_request_channels");

            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<ProductRequestStateType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("product_request_state_types");

            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<ProductSpecType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("product_spec_types");

            entity.HasIndex(e => e.UnitOfMeasurementId, "IX_product_spec_types_UnitOfMeasurementId");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("product_types");

            entity.HasIndex(e => e.ProductTypeParentId, "IX_product_types_ProductTypeParentId");

            entity.Property(e => e.Abbreviation).HasMaxLength(5);
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.ProductTypeParent).WithMany(p => p.InverseProductTypeParent).HasForeignKey(d => d.ProductTypeParentId);
        });

        modelBuilder.Entity<ProductsUnitOfMeasurementType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("products_unit_of_measurement_types");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<PropertyType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("property_types");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<QaControlCalificationType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("qa_control_calification_types");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Value).HasPrecision(4, 3);
        });

        modelBuilder.Entity<ReportTemplateContentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("report_template_content_types");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<ReportTemplateType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("report_template_types");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<SocietyType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("society_types");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<SupplierArea>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("supplier_areas");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<UnitOfMeasurementMultiplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("unit_of_measurement_multipliers");

            entity.HasIndex(e => e.UnitOfMeasurementId, "IX_unit_of_measurement_multipliers_UnitOfMeasurementId");

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Symbol).HasMaxLength(5);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.PersonId, "IX_Users_PersonId");

            entity.HasOne(d => d.Person).WithMany(p => p.Users)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_People_PersonId");
        });

        modelBuilder.Entity<VehicleInspectionType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("vehicle_inspection_types");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<VehicleType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("vehicle_types");

            entity.Property(e => e.Name).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
