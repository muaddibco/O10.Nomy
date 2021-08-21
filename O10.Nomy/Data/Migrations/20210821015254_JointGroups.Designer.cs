﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using O10.Nomy.Data;

namespace O10.Nomy.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210821015254_JointGroups")]
    partial class JointGroups
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("O10.Nomy.Models.ExpertProfile", b =>
                {
                    b.Property<long>("ExpertProfileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Fee")
                        .HasColumnType("decimal(20,0)");

                    b.Property<long?>("NomyUserId")
                        .HasColumnType("bigint");

                    b.HasKey("ExpertProfileId");

                    b.HasIndex("NomyUserId");

                    b.ToTable("ExpertProfiles");
                });

            modelBuilder.Entity("O10.Nomy.Models.ExpertiseArea", b =>
                {
                    b.Property<long>("ExpertiseAreaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExpertiseAreaId");

                    b.ToTable("ExpertiseAreas");
                });

            modelBuilder.Entity("O10.Nomy.Models.ExpertiseSubArea", b =>
                {
                    b.Property<long>("ExpertiseSubAreaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ExpertProfileId")
                        .HasColumnType("bigint");

                    b.Property<long>("ExpertiseAreaId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExpertiseSubAreaId");

                    b.HasIndex("ExpertProfileId");

                    b.HasIndex("ExpertiseAreaId");

                    b.ToTable("ExpertiseSubAreas");
                });

            modelBuilder.Entity("O10.Nomy.Models.InvoiceRecord", b =>
                {
                    b.Property<long>("InvoiceRecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Commitment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateOfProcessing")
                        .HasColumnType("datetime2");

                    b.Property<long?>("PayoutRecordId")
                        .HasColumnType("bigint");

                    b.Property<string>("RangeProof")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("UserNomyUserId")
                        .HasColumnType("bigint");

                    b.HasKey("InvoiceRecordId");

                    b.HasIndex("PayoutRecordId");

                    b.HasIndex("UserNomyUserId");

                    b.ToTable("InvoiceRecords");
                });

            modelBuilder.Entity("O10.Nomy.Models.JointGroup", b =>
                {
                    b.Property<long>("JointGroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("O10RegistrationId")
                        .HasColumnType("bigint");

                    b.HasKey("JointGroupId");

                    b.HasIndex("O10RegistrationId");

                    b.ToTable("JointGroups");
                });

            modelBuilder.Entity("O10.Nomy.Models.JointGroupMember", b =>
                {
                    b.Property<long>("JointGroupMemberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("GroupJointGroupId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsRegistered")
                        .HasColumnType("bit");

                    b.HasKey("JointGroupMemberId");

                    b.HasIndex("GroupJointGroupId");

                    b.ToTable("JointGroupMembers");
                });

            modelBuilder.Entity("O10.Nomy.Models.NomyAccount", b =>
                {
                    b.Property<long>("NomyAccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("O10Id")
                        .HasColumnType("bigint");

                    b.HasKey("NomyAccountId");

                    b.HasIndex("O10Id");

                    b.ToTable("NomyAccount");
                });

            modelBuilder.Entity("O10.Nomy.Models.NomyServiceProvider", b =>
                {
                    b.Property<long>("NomyServiceProviderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("AccountNomyAccountId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NomyServiceProviderId");

                    b.HasIndex("AccountNomyAccountId");

                    b.ToTable("NomyServiceProvider");
                });

            modelBuilder.Entity("O10.Nomy.Models.NomyUser", b =>
                {
                    b.Property<long>("NomyUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("AccountNomyAccountId")
                        .HasColumnType("bigint");

                    b.Property<string>("BeneficiaryId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SenderId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WalletId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NomyUserId");

                    b.HasIndex("AccountNomyAccountId");

                    b.ToTable("NomyUsers");
                });

            modelBuilder.Entity("O10.Nomy.Models.PaymentRecord", b =>
                {
                    b.Property<long>("PaymentRecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Commitment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateOfProcessing")
                        .HasColumnType("datetime2");

                    b.Property<long?>("InvoiceRecordId")
                        .HasColumnType("bigint");

                    b.Property<long?>("PayoutRecordId")
                        .HasColumnType("bigint");

                    b.Property<string>("RangeProof")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Signature")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("UserNomyUserId")
                        .HasColumnType("bigint");

                    b.HasKey("PaymentRecordId");

                    b.HasIndex("InvoiceRecordId");

                    b.HasIndex("PayoutRecordId");

                    b.HasIndex("UserNomyUserId");

                    b.ToTable("PaymentRecords");
                });

            modelBuilder.Entity("O10.Nomy.Models.PayoutRecord", b =>
                {
                    b.Property<long>("PayoutRecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("From")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("To")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(20,0)");

                    b.HasKey("PayoutRecordId");

                    b.ToTable("PayoutRecords");
                });

            modelBuilder.Entity("O10.Nomy.Models.SecretInvoiceRecord", b =>
                {
                    b.Property<long>("SecretInvoiceRecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(20,0)");

                    b.Property<string>("BlindingFactor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("InvoiceRecordId")
                        .HasColumnType("bigint");

                    b.Property<long?>("UserNomyUserId")
                        .HasColumnType("bigint");

                    b.HasKey("SecretInvoiceRecordId");

                    b.HasIndex("InvoiceRecordId");

                    b.HasIndex("UserNomyUserId");

                    b.ToTable("SecretInvoiceRecords");
                });

            modelBuilder.Entity("O10.Nomy.Models.SecretPaymentRecord", b =>
                {
                    b.Property<long>("SecretPaymentRecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(20,0)");

                    b.Property<string>("BlindingFactor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("PaymentRecordId")
                        .HasColumnType("bigint");

                    b.Property<long?>("UserNomyUserId")
                        .HasColumnType("bigint");

                    b.HasKey("SecretPaymentRecordId");

                    b.HasIndex("PaymentRecordId");

                    b.HasIndex("UserNomyUserId");

                    b.ToTable("SecretPaymentRecords");
                });

            modelBuilder.Entity("O10.Nomy.Models.SystemParameter", b =>
                {
                    b.Property<long>("SystemParameterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SystemParameterId");

                    b.ToTable("SystemParameters");
                });

            modelBuilder.Entity("O10.Nomy.Models.ExpertProfile", b =>
                {
                    b.HasOne("O10.Nomy.Models.NomyUser", "NomyUser")
                        .WithMany()
                        .HasForeignKey("NomyUserId");

                    b.Navigation("NomyUser");
                });

            modelBuilder.Entity("O10.Nomy.Models.ExpertiseSubArea", b =>
                {
                    b.HasOne("O10.Nomy.Models.ExpertProfile", null)
                        .WithMany("ExpertiseSubAreas")
                        .HasForeignKey("ExpertProfileId");

                    b.HasOne("O10.Nomy.Models.ExpertiseArea", "ExpertiseArea")
                        .WithMany("ExpertiseSubAreas")
                        .HasForeignKey("ExpertiseAreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExpertiseArea");
                });

            modelBuilder.Entity("O10.Nomy.Models.InvoiceRecord", b =>
                {
                    b.HasOne("O10.Nomy.Models.PayoutRecord", null)
                        .WithMany("Invoices")
                        .HasForeignKey("PayoutRecordId");

                    b.HasOne("O10.Nomy.Models.NomyUser", "User")
                        .WithMany()
                        .HasForeignKey("UserNomyUserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("O10.Nomy.Models.JointGroupMember", b =>
                {
                    b.HasOne("O10.Nomy.Models.JointGroup", "Group")
                        .WithMany("Members")
                        .HasForeignKey("GroupJointGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("O10.Nomy.Models.NomyServiceProvider", b =>
                {
                    b.HasOne("O10.Nomy.Models.NomyAccount", "Account")
                        .WithMany()
                        .HasForeignKey("AccountNomyAccountId");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("O10.Nomy.Models.NomyUser", b =>
                {
                    b.HasOne("O10.Nomy.Models.NomyAccount", "Account")
                        .WithMany()
                        .HasForeignKey("AccountNomyAccountId");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("O10.Nomy.Models.PaymentRecord", b =>
                {
                    b.HasOne("O10.Nomy.Models.InvoiceRecord", "Invoice")
                        .WithMany()
                        .HasForeignKey("InvoiceRecordId");

                    b.HasOne("O10.Nomy.Models.PayoutRecord", null)
                        .WithMany("Payments")
                        .HasForeignKey("PayoutRecordId");

                    b.HasOne("O10.Nomy.Models.NomyUser", "User")
                        .WithMany()
                        .HasForeignKey("UserNomyUserId");

                    b.Navigation("Invoice");

                    b.Navigation("User");
                });

            modelBuilder.Entity("O10.Nomy.Models.SecretInvoiceRecord", b =>
                {
                    b.HasOne("O10.Nomy.Models.InvoiceRecord", "InvoiceRecord")
                        .WithMany()
                        .HasForeignKey("InvoiceRecordId");

                    b.HasOne("O10.Nomy.Models.NomyUser", "User")
                        .WithMany()
                        .HasForeignKey("UserNomyUserId");

                    b.Navigation("InvoiceRecord");

                    b.Navigation("User");
                });

            modelBuilder.Entity("O10.Nomy.Models.SecretPaymentRecord", b =>
                {
                    b.HasOne("O10.Nomy.Models.PaymentRecord", "PaymentRecord")
                        .WithMany()
                        .HasForeignKey("PaymentRecordId");

                    b.HasOne("O10.Nomy.Models.NomyUser", "User")
                        .WithMany()
                        .HasForeignKey("UserNomyUserId");

                    b.Navigation("PaymentRecord");

                    b.Navigation("User");
                });

            modelBuilder.Entity("O10.Nomy.Models.ExpertProfile", b =>
                {
                    b.Navigation("ExpertiseSubAreas");
                });

            modelBuilder.Entity("O10.Nomy.Models.ExpertiseArea", b =>
                {
                    b.Navigation("ExpertiseSubAreas");
                });

            modelBuilder.Entity("O10.Nomy.Models.JointGroup", b =>
                {
                    b.Navigation("Members");
                });

            modelBuilder.Entity("O10.Nomy.Models.PayoutRecord", b =>
                {
                    b.Navigation("Invoices");

                    b.Navigation("Payments");
                });
#pragma warning restore 612, 618
        }
    }
}
