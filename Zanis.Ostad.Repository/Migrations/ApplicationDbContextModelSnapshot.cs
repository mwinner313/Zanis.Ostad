﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Zanis.Ostad.Repository;

namespace Zanis.Ostad.Repository.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<long>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<long>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<long>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<long>", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<long>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<long>", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.AndroidApp.AppVersion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsRequired");

                    b.Property<string>("Version");

                    b.HasKey("Id");

                    b.ToTable("AppVersions");
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.AndroidApp.AppVersionFeature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title");

                    b.Property<int>("VersionId");

                    b.HasKey("Id");

                    b.HasIndex("VersionId");

                    b.ToTable("AppVersionFeatures");
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.Cart.CartItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CourseId");

                    b.Property<int>("ItemType");

                    b.Property<long?>("LessonExamId");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("LessonExamId");

                    b.HasIndex("UserId");

                    b.ToTable("CartItem");
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.College", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<string>("CoverPath");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Colleges");
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.Contents.Course", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdminMessageForTeacher");

                    b.Property<int>("ApprovalStatus");

                    b.Property<int>("CourseTitleId");

                    b.Property<string>("Description");

                    b.Property<bool>("HasPendingItemToApprove");

                    b.Property<int>("Price");

                    b.Property<long>("TeacherLessonMappingId");

                    b.Property<string>("TeacherMessageForAdmin");

                    b.Property<string>("ZipFilesPath");

                    b.HasKey("Id");

                    b.HasIndex("CourseTitleId");

                    b.HasIndex("TeacherLessonMappingId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.Contents.CourseItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdminMessageForTeacher");

                    b.Property<int>("ContentType");

                    b.Property<long>("CourseId");

                    b.Property<string>("FilePath");

                    b.Property<bool>("IsPreview");

                    b.Property<int?>("LatestEditStatus");

                    b.Property<int>("Order");

                    b.Property<int>("State");

                    b.Property<string>("TeacherMessageForAdmin");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Contents");
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.Contents.CourseTitle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("CourseTitles");
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.Contents.TeacherLessonMapping", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("LessonId");

                    b.Property<long>("TeacherId");

                    b.HasKey("Id");

                    b.HasIndex("LessonId");

                    b.HasIndex("TeacherId");

                    b.ToTable("TeacherLessonMappings");
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.Edits.EditAssignment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CourseItemId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<long>("EditorId");

                    b.Property<string>("FilePath");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("CourseItemId");

                    b.HasIndex("EditorId");

                    b.ToTable("EditAssignment");
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.ExamSampleFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FilePath");

                    b.Property<int>("SemesterId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("SemesterId");

                    b.ToTable("ExamSampleFiles");
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.Field", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<int>("CollegeId");

                    b.Property<int>("GradeId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CollegeId");

                    b.HasIndex("GradeId");

                    b.ToTable("Fields");
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.Grade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.Lesson", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ExamSamplesPrice");

                    b.Property<string>("LessonCode");

                    b.Property<string>("LessonName");

                    b.HasKey("Id");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.LessonExamMapping", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ExamSampleFileId");

                    b.Property<long>("LessonId");

                    b.HasKey("Id");

                    b.HasIndex("ExamSampleFileId");

                    b.HasIndex("LessonId");

                    b.ToTable("LessonExamMappings");
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.LessonFieldMapping", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FieldId");

                    b.Property<int>("GradeId");

                    b.Property<long>("LessonId");

                    b.HasKey("Id");

                    b.HasIndex("FieldId");

                    b.HasIndex("GradeId");

                    b.HasIndex("LessonId");

                    b.ToTable("LessonFieldMappings");
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.Notifications.Notification", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsSeen");

                    b.Property<string>("JsonExtraData");

                    b.Property<long>("ReceiverId");

                    b.Property<int>("RelatedItemType");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.Orders.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ExtraData");

                    b.Property<int>("PaymentStatus");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.Orders.OrderItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("OrderId");

                    b.Property<int>("Price");

                    b.Property<long>("ProductId");

                    b.Property<int>("ProductType");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.Semester", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Semesters");
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.StudentCourseMapping", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CourseId");

                    b.Property<long>("StudentId");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentCourseMappings");
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.StudentExamSampleMapping", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("LessonId");

                    b.Property<long>("StudentId");

                    b.HasKey("Id");

                    b.HasIndex("LessonId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentExamSampleMappings");
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.Tickets.Ticket", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId");

                    b.Property<long?>("CourseId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("LastMessageText");

                    b.Property<int>("OperatorUnReadedMessagesCount");

                    b.Property<int>("State");

                    b.Property<int>("TicketOwnerUnReadedMessagesCount");

                    b.Property<string>("TicketReason");

                    b.Property<DateTime>("UpdatedOn");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CourseId");

                    b.HasIndex("UserId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.Tickets.TicketCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryType");

                    b.Property<bool>("IsDeletible");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("TicketCategory");
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.Tickets.TicketItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<bool>("IsEdited");

                    b.Property<bool>("IsSeen");

                    b.Property<string>("Message");

                    b.Property<long>("TicketId");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("TicketId");

                    b.HasIndex("UserId");

                    b.ToTable("TicketItems");
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("AvatarPath");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FullName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("TeacherCode");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<long>", b =>
                {
                    b.HasOne("Zanis.Ostad.Core.Entities.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<long>", b =>
                {
                    b.HasOne("Zanis.Ostad.Core.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<long>", b =>
                {
                    b.HasOne("Zanis.Ostad.Core.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<long>", b =>
                {
                    b.HasOne("Zanis.Ostad.Core.Entities.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Zanis.Ostad.Core.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<long>", b =>
                {
                    b.HasOne("Zanis.Ostad.Core.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.AndroidApp.AppVersionFeature", b =>
                {
                    b.HasOne("Zanis.Ostad.Core.Entities.AndroidApp.AppVersion", "Version")
                        .WithMany("Features")
                        .HasForeignKey("VersionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.Cart.CartItem", b =>
                {
                    b.HasOne("Zanis.Ostad.Core.Entities.Contents.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Zanis.Ostad.Core.Entities.Lesson", "LessonExam")
                        .WithMany()
                        .HasForeignKey("LessonExamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Zanis.Ostad.Core.Entities.User", "User")
                        .WithMany("Cart")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.Contents.Course", b =>
                {
                    b.HasOne("Zanis.Ostad.Core.Entities.Contents.CourseTitle", "CourseTitle")
                        .WithMany("Courses")
                        .HasForeignKey("CourseTitleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Zanis.Ostad.Core.Entities.Contents.TeacherLessonMapping", "TeacherLessonMapping")
                        .WithMany("Courses")
                        .HasForeignKey("TeacherLessonMappingId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.Contents.CourseItem", b =>
                {
                    b.HasOne("Zanis.Ostad.Core.Entities.Contents.Course", "Course")
                        .WithMany("Contents")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.Contents.TeacherLessonMapping", b =>
                {
                    b.HasOne("Zanis.Ostad.Core.Entities.LessonFieldMapping", "LessonFieldMapping")
                        .WithMany("TeacherLessonMappings")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Zanis.Ostad.Core.Entities.User", "Teacher")
                        .WithMany("ProducedContents")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.Edits.EditAssignment", b =>
                {
                    b.HasOne("Zanis.Ostad.Core.Entities.Contents.CourseItem", "CourseItem")
                        .WithMany("Edits")
                        .HasForeignKey("CourseItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Zanis.Ostad.Core.Entities.User", "Editor")
                        .WithMany("EditAssignments")
                        .HasForeignKey("EditorId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.ExamSampleFile", b =>
                {
                    b.HasOne("Zanis.Ostad.Core.Entities.Semester", "Semester")
                        .WithMany("ExamSampleFiles")
                        .HasForeignKey("SemesterId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.Field", b =>
                {
                    b.HasOne("Zanis.Ostad.Core.Entities.College", "College")
                        .WithMany("Fields")
                        .HasForeignKey("CollegeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Zanis.Ostad.Core.Entities.Grade", "Grade")
                        .WithMany("Fields")
                        .HasForeignKey("GradeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.LessonExamMapping", b =>
                {
                    b.HasOne("Zanis.Ostad.Core.Entities.ExamSampleFile", "ExamSampleFile")
                        .WithMany("Lessons")
                        .HasForeignKey("ExamSampleFileId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Zanis.Ostad.Core.Entities.Lesson", "Lesson")
                        .WithMany("ExamSamples")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.LessonFieldMapping", b =>
                {
                    b.HasOne("Zanis.Ostad.Core.Entities.Field", "Field")
                        .WithMany("FieldLessons")
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Zanis.Ostad.Core.Entities.Grade", "Grade")
                        .WithMany()
                        .HasForeignKey("GradeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Zanis.Ostad.Core.Entities.Lesson", "Lesson")
                        .WithMany("Fields")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.Notifications.Notification", b =>
                {
                    b.HasOne("Zanis.Ostad.Core.Entities.User", "Receiver")
                        .WithMany()
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.Orders.Order", b =>
                {
                    b.HasOne("Zanis.Ostad.Core.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.Orders.OrderItem", b =>
                {
                    b.HasOne("Zanis.Ostad.Core.Entities.Orders.Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.StudentCourseMapping", b =>
                {
                    b.HasOne("Zanis.Ostad.Core.Entities.Contents.Course", "Course")
                        .WithMany("Students")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Zanis.Ostad.Core.Entities.User", "Student")
                        .WithMany("BoughtCourses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.StudentExamSampleMapping", b =>
                {
                    b.HasOne("Zanis.Ostad.Core.Entities.Lesson", "Lesson")
                        .WithMany()
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Zanis.Ostad.Core.Entities.User", "Student")
                        .WithMany("BoughtExamSamples")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.Tickets.Ticket", b =>
                {
                    b.HasOne("Zanis.Ostad.Core.Entities.Tickets.TicketCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Zanis.Ostad.Core.Entities.Contents.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Zanis.Ostad.Core.Entities.User", "User")
                        .WithMany("Tickets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Zanis.Ostad.Core.Entities.Tickets.TicketItem", b =>
                {
                    b.HasOne("Zanis.Ostad.Core.Entities.Tickets.Ticket", "Ticket")
                        .WithMany("TicketItems")
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Zanis.Ostad.Core.Entities.User", "User")
                        .WithMany("TicketItems")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
