﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Entities;

public partial class Net8duendeContext : DbContext
{
    public Net8duendeContext()
    {
    }

    public Net8duendeContext(DbContextOptions<Net8duendeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ApiResource> ApiResources { get; set; }

    public virtual DbSet<ApiResourceClaim> ApiResourceClaims { get; set; }

    public virtual DbSet<ApiResourceProperty> ApiResourceProperties { get; set; }

    public virtual DbSet<ApiResourceScope> ApiResourceScopes { get; set; }

    public virtual DbSet<ApiResourceSecret> ApiResourceSecrets { get; set; }

    public virtual DbSet<ApiScope> ApiScopes { get; set; }

    public virtual DbSet<ApiScopeClaim> ApiScopeClaims { get; set; }

    public virtual DbSet<ApiScopeProperty> ApiScopeProperties { get; set; }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientClaim> ClientClaims { get; set; }

    public virtual DbSet<ClientCorsOrigin> ClientCorsOrigins { get; set; }

    public virtual DbSet<ClientGrantType> ClientGrantTypes { get; set; }

    public virtual DbSet<ClientIdPrestriction> ClientIdPrestrictions { get; set; }

    public virtual DbSet<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { get; set; }

    public virtual DbSet<ClientProperty> ClientProperties { get; set; }

    public virtual DbSet<ClientRedirectUri> ClientRedirectUris { get; set; }

    public virtual DbSet<ClientScope> ClientScopes { get; set; }

    public virtual DbSet<ClientSecret> ClientSecrets { get; set; }

    public virtual DbSet<DeviceCode> DeviceCodes { get; set; }

    public virtual DbSet<IdentityProvider> IdentityProviders { get; set; }

    public virtual DbSet<IdentityResource> IdentityResources { get; set; }

    public virtual DbSet<IdentityResourceClaim> IdentityResourceClaims { get; set; }

    public virtual DbSet<IdentityResourceProperty> IdentityResourceProperties { get; set; }

    public virtual DbSet<Key> Keys { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<PersistedGrant> PersistedGrants { get; set; }

    public virtual DbSet<PushedAuthorizationRequest> PushedAuthorizationRequests { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleClaim> RoleClaims { get; set; }

    public virtual DbSet<ServerSideSession> ServerSideSessions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserClaim> UserClaims { get; set; }

    public virtual DbSet<UserLogin> UserLogins { get; set; }

    public virtual DbSet<UserToken> UserTokens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Database=NET8Duende;User Id=Admin;Password=tLocalSql;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApiResource>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_ApiResources_Name").IsUnique();

            entity.Property(e => e.AllowedAccessTokenSigningAlgorithms).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.DisplayName).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<ApiResourceClaim>(entity =>
        {
            entity.HasIndex(e => new { e.ApiResourceId, e.Type }, "IX_ApiResourceClaims_ApiResourceId_Type").IsUnique();

            entity.Property(e => e.Type).HasMaxLength(200);

            entity.HasOne(d => d.ApiResource).WithMany(p => p.ApiResourceClaims).HasForeignKey(d => d.ApiResourceId);
        });

        modelBuilder.Entity<ApiResourceProperty>(entity =>
        {
            entity.HasIndex(e => new { e.ApiResourceId, e.Key }, "IX_ApiResourceProperties_ApiResourceId_Key").IsUnique();

            entity.Property(e => e.Key).HasMaxLength(250);
            entity.Property(e => e.Value).HasMaxLength(2000);

            entity.HasOne(d => d.ApiResource).WithMany(p => p.ApiResourceProperties).HasForeignKey(d => d.ApiResourceId);
        });

        modelBuilder.Entity<ApiResourceScope>(entity =>
        {
            entity.HasIndex(e => new { e.ApiResourceId, e.Scope }, "IX_ApiResourceScopes_ApiResourceId_Scope").IsUnique();

            entity.Property(e => e.Scope).HasMaxLength(200);

            entity.HasOne(d => d.ApiResource).WithMany(p => p.ApiResourceScopes).HasForeignKey(d => d.ApiResourceId);
        });

        modelBuilder.Entity<ApiResourceSecret>(entity =>
        {
            entity.HasIndex(e => e.ApiResourceId, "IX_ApiResourceSecrets_ApiResourceId");

            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Type).HasMaxLength(250);
            entity.Property(e => e.Value).HasMaxLength(4000);

            entity.HasOne(d => d.ApiResource).WithMany(p => p.ApiResourceSecrets).HasForeignKey(d => d.ApiResourceId);
        });

        modelBuilder.Entity<ApiScope>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_ApiScopes_Name").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.DisplayName).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<ApiScopeClaim>(entity =>
        {
            entity.HasIndex(e => new { e.ScopeId, e.Type }, "IX_ApiScopeClaims_ScopeId_Type").IsUnique();

            entity.Property(e => e.Type).HasMaxLength(200);

            entity.HasOne(d => d.Scope).WithMany(p => p.ApiScopeClaims).HasForeignKey(d => d.ScopeId);
        });

        modelBuilder.Entity<ApiScopeProperty>(entity =>
        {
            entity.HasIndex(e => new { e.ScopeId, e.Key }, "IX_ApiScopeProperties_ScopeId_Key").IsUnique();

            entity.Property(e => e.Key).HasMaxLength(250);
            entity.Property(e => e.Value).HasMaxLength(2000);

            entity.HasOne(d => d.Scope).WithMany(p => p.ApiScopeProperties).HasForeignKey(d => d.ScopeId);
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.ToTable("AuditLog");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasIndex(e => e.ClientId, "IX_Clients_ClientId").IsUnique();

            entity.Property(e => e.AllowedIdentityTokenSigningAlgorithms).HasMaxLength(100);
            entity.Property(e => e.BackChannelLogoutUri).HasMaxLength(2000);
            entity.Property(e => e.ClientClaimsPrefix).HasMaxLength(200);
            entity.Property(e => e.ClientId).HasMaxLength(200);
            entity.Property(e => e.ClientName).HasMaxLength(200);
            entity.Property(e => e.ClientUri).HasMaxLength(2000);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.DpoPclockSkew).HasColumnName("DPoPClockSkew");
            entity.Property(e => e.DpoPvalidationMode).HasColumnName("DPoPValidationMode");
            entity.Property(e => e.FrontChannelLogoutUri).HasMaxLength(2000);
            entity.Property(e => e.InitiateLoginUri).HasMaxLength(2000);
            entity.Property(e => e.LogoUri).HasMaxLength(2000);
            entity.Property(e => e.PairWiseSubjectSalt).HasMaxLength(200);
            entity.Property(e => e.ProtocolType).HasMaxLength(200);
            entity.Property(e => e.RequireDpoP).HasColumnName("RequireDPoP");
            entity.Property(e => e.UserCodeType).HasMaxLength(100);
        });

        modelBuilder.Entity<ClientClaim>(entity =>
        {
            entity.HasIndex(e => new { e.ClientId, e.Type, e.Value }, "IX_ClientClaims_ClientId_Type_Value").IsUnique();

            entity.Property(e => e.Type).HasMaxLength(250);
            entity.Property(e => e.Value).HasMaxLength(250);

            entity.HasOne(d => d.Client).WithMany(p => p.ClientClaims).HasForeignKey(d => d.ClientId);
        });

        modelBuilder.Entity<ClientCorsOrigin>(entity =>
        {
            entity.HasIndex(e => new { e.ClientId, e.Origin }, "IX_ClientCorsOrigins_ClientId_Origin").IsUnique();

            entity.Property(e => e.Origin).HasMaxLength(150);

            entity.HasOne(d => d.Client).WithMany(p => p.ClientCorsOrigins).HasForeignKey(d => d.ClientId);
        });

        modelBuilder.Entity<ClientGrantType>(entity =>
        {
            entity.HasIndex(e => new { e.ClientId, e.GrantType }, "IX_ClientGrantTypes_ClientId_GrantType").IsUnique();

            entity.Property(e => e.GrantType).HasMaxLength(250);

            entity.HasOne(d => d.Client).WithMany(p => p.ClientGrantTypes).HasForeignKey(d => d.ClientId);
        });

        modelBuilder.Entity<ClientIdPrestriction>(entity =>
        {
            entity.ToTable("ClientIdPRestrictions");

            entity.HasIndex(e => new { e.ClientId, e.Provider }, "IX_ClientIdPRestrictions_ClientId_Provider").IsUnique();

            entity.Property(e => e.Provider).HasMaxLength(200);

            entity.HasOne(d => d.Client).WithMany(p => p.ClientIdPrestrictions).HasForeignKey(d => d.ClientId);
        });

        modelBuilder.Entity<ClientPostLogoutRedirectUri>(entity =>
        {
            entity.HasIndex(e => new { e.ClientId, e.PostLogoutRedirectUri }, "IX_ClientPostLogoutRedirectUris_ClientId_PostLogoutRedirectUri").IsUnique();

            entity.Property(e => e.PostLogoutRedirectUri).HasMaxLength(400);

            entity.HasOne(d => d.Client).WithMany(p => p.ClientPostLogoutRedirectUris).HasForeignKey(d => d.ClientId);
        });

        modelBuilder.Entity<ClientProperty>(entity =>
        {
            entity.HasIndex(e => new { e.ClientId, e.Key }, "IX_ClientProperties_ClientId_Key").IsUnique();

            entity.Property(e => e.Key).HasMaxLength(250);
            entity.Property(e => e.Value).HasMaxLength(2000);

            entity.HasOne(d => d.Client).WithMany(p => p.ClientProperties).HasForeignKey(d => d.ClientId);
        });

        modelBuilder.Entity<ClientRedirectUri>(entity =>
        {
            entity.HasIndex(e => new { e.ClientId, e.RedirectUri }, "IX_ClientRedirectUris_ClientId_RedirectUri").IsUnique();

            entity.Property(e => e.RedirectUri).HasMaxLength(400);

            entity.HasOne(d => d.Client).WithMany(p => p.ClientRedirectUris).HasForeignKey(d => d.ClientId);
        });

        modelBuilder.Entity<ClientScope>(entity =>
        {
            entity.HasIndex(e => new { e.ClientId, e.Scope }, "IX_ClientScopes_ClientId_Scope").IsUnique();

            entity.Property(e => e.Scope).HasMaxLength(200);

            entity.HasOne(d => d.Client).WithMany(p => p.ClientScopes).HasForeignKey(d => d.ClientId);
        });

        modelBuilder.Entity<ClientSecret>(entity =>
        {
            entity.HasIndex(e => e.ClientId, "IX_ClientSecrets_ClientId");

            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.Type).HasMaxLength(250);
            entity.Property(e => e.Value).HasMaxLength(4000);

            entity.HasOne(d => d.Client).WithMany(p => p.ClientSecrets).HasForeignKey(d => d.ClientId);
        });

        modelBuilder.Entity<DeviceCode>(entity =>
        {
            entity.HasKey(e => e.UserCode);

            entity.HasIndex(e => e.DeviceCode1, "IX_DeviceCodes_DeviceCode").IsUnique();

            entity.HasIndex(e => e.Expiration, "IX_DeviceCodes_Expiration");

            entity.Property(e => e.UserCode).HasMaxLength(200);
            entity.Property(e => e.ClientId).HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.DeviceCode1)
                .HasMaxLength(200)
                .HasColumnName("DeviceCode");
            entity.Property(e => e.SessionId).HasMaxLength(100);
            entity.Property(e => e.SubjectId).HasMaxLength(200);
        });

        modelBuilder.Entity<IdentityProvider>(entity =>
        {
            entity.HasIndex(e => e.Scheme, "IX_IdentityProviders_Scheme").IsUnique();

            entity.Property(e => e.DisplayName).HasMaxLength(200);
            entity.Property(e => e.Scheme).HasMaxLength(200);
            entity.Property(e => e.Type).HasMaxLength(20);
        });

        modelBuilder.Entity<IdentityResource>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_IdentityResources_Name").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.DisplayName).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<IdentityResourceClaim>(entity =>
        {
            entity.HasIndex(e => new { e.IdentityResourceId, e.Type }, "IX_IdentityResourceClaims_IdentityResourceId_Type").IsUnique();

            entity.Property(e => e.Type).HasMaxLength(200);

            entity.HasOne(d => d.IdentityResource).WithMany(p => p.IdentityResourceClaims).HasForeignKey(d => d.IdentityResourceId);
        });

        modelBuilder.Entity<IdentityResourceProperty>(entity =>
        {
            entity.HasIndex(e => new { e.IdentityResourceId, e.Key }, "IX_IdentityResourceProperties_IdentityResourceId_Key").IsUnique();

            entity.Property(e => e.Key).HasMaxLength(250);
            entity.Property(e => e.Value).HasMaxLength(2000);

            entity.HasOne(d => d.IdentityResource).WithMany(p => p.IdentityResourceProperties).HasForeignKey(d => d.IdentityResourceId);
        });

        modelBuilder.Entity<Key>(entity =>
        {
            entity.HasIndex(e => e.Use, "IX_Keys_Use");

            entity.Property(e => e.Algorithm).HasMaxLength(100);
            entity.Property(e => e.IsX509certificate).HasColumnName("IsX509Certificate");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.ToTable("Log");

            entity.Property(e => e.Level).HasMaxLength(128);
        });

        modelBuilder.Entity<PersistedGrant>(entity =>
        {
            entity.HasIndex(e => e.ConsumedTime, "IX_PersistedGrants_ConsumedTime");

            entity.HasIndex(e => e.Expiration, "IX_PersistedGrants_Expiration");

            entity.HasIndex(e => e.Key, "IX_PersistedGrants_Key")
                .IsUnique()
                .HasFilter("([Key] IS NOT NULL)");

            entity.HasIndex(e => new { e.SubjectId, e.ClientId, e.Type }, "IX_PersistedGrants_SubjectId_ClientId_Type");

            entity.HasIndex(e => new { e.SubjectId, e.SessionId, e.Type }, "IX_PersistedGrants_SubjectId_SessionId_Type");

            entity.Property(e => e.ClientId).HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.Key).HasMaxLength(200);
            entity.Property(e => e.SessionId).HasMaxLength(100);
            entity.Property(e => e.SubjectId).HasMaxLength(200);
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<PushedAuthorizationRequest>(entity =>
        {
            entity.HasIndex(e => e.ExpiresAtUtc, "IX_PushedAuthorizationRequests_ExpiresAtUtc");

            entity.HasIndex(e => e.ReferenceValueHash, "IX_PushedAuthorizationRequests_ReferenceValueHash").IsUnique();

            entity.Property(e => e.ReferenceValueHash).HasMaxLength(64);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<RoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_RoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<ServerSideSession>(entity =>
        {
            entity.HasIndex(e => e.DisplayName, "IX_ServerSideSessions_DisplayName");

            entity.HasIndex(e => e.Expires, "IX_ServerSideSessions_Expires");

            entity.HasIndex(e => e.Key, "IX_ServerSideSessions_Key").IsUnique();

            entity.HasIndex(e => e.SessionId, "IX_ServerSideSessions_SessionId");

            entity.HasIndex(e => e.SubjectId, "IX_ServerSideSessions_SubjectId");

            entity.Property(e => e.DisplayName).HasMaxLength(100);
            entity.Property(e => e.Key).HasMaxLength(100);
            entity.Property(e => e.Scheme).HasMaxLength(100);
            entity.Property(e => e.SessionId).HasMaxLength(100);
            entity.Property(e => e.SubjectId).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<User>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("UserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_UserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<UserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_UserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.UserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<UserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_UserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.UserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<UserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.UserTokens).HasForeignKey(d => d.UserId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
