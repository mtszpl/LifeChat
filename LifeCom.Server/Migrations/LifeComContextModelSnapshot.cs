﻿// <auto-generated />
using System;
using LifeCom.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LifeCom.Server.Migrations
{
    [DbContext(typeof(LifeComContext))]
    partial class LifeComContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ChatUser", b =>
                {
                    b.Property<int>("chatsId")
                        .HasColumnType("int");

                    b.Property<int>("membersId")
                        .HasColumnType("int");

                    b.HasKey("chatsId", "membersId");

                    b.HasIndex("membersId");

                    b.ToTable("ChatUser");
                });

            modelBuilder.Entity("LifeCom.Server.Chats.Channels.Channel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("chatId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("chatId");

                    b.ToTable("Channel");
                });

            modelBuilder.Entity("LifeCom.Server.Chats.Channels.UserChannel", b =>
                {
                    b.Property<int>("channelId")
                        .HasColumnType("int");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.Property<bool>("connected")
                        .HasColumnType("bit");

                    b.HasKey("channelId", "userId");

                    b.HasIndex("userId");

                    b.ToTable("UserChannel");
                });

            modelBuilder.Entity("LifeCom.Server.Chats.Chat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Chat");
                });

            modelBuilder.Entity("LifeCom.Server.Chats.Messages.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("authorId")
                        .HasColumnType("int");

                    b.Property<int>("channelId")
                        .HasColumnType("int");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("authorId");

                    b.HasIndex("channelId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("LifeCom.Server.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("passwordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("refreshExpirationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("refreshToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ChatUser", b =>
                {
                    b.HasOne("LifeCom.Server.Chats.Chat", null)
                        .WithMany()
                        .HasForeignKey("chatsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LifeCom.Server.Users.User", null)
                        .WithMany()
                        .HasForeignKey("membersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LifeCom.Server.Chats.Channels.Channel", b =>
                {
                    b.HasOne("LifeCom.Server.Chats.Chat", "chat")
                        .WithMany("channels")
                        .HasForeignKey("chatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("chat");
                });

            modelBuilder.Entity("LifeCom.Server.Chats.Channels.UserChannel", b =>
                {
                    b.HasOne("LifeCom.Server.Chats.Channels.Channel", null)
                        .WithMany()
                        .HasForeignKey("channelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LifeCom.Server.Users.User", null)
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LifeCom.Server.Chats.Messages.Message", b =>
                {
                    b.HasOne("LifeCom.Server.Users.User", "author")
                        .WithMany()
                        .HasForeignKey("authorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LifeCom.Server.Chats.Channels.Channel", "channel")
                        .WithMany("messages")
                        .HasForeignKey("channelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("author");

                    b.Navigation("channel");
                });

            modelBuilder.Entity("LifeCom.Server.Chats.Channels.Channel", b =>
                {
                    b.Navigation("messages");
                });

            modelBuilder.Entity("LifeCom.Server.Chats.Chat", b =>
                {
                    b.Navigation("channels");
                });
#pragma warning restore 612, 618
        }
    }
}
