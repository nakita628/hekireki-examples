#nullable enable

using System;
using System.Collections.Generic;

namespace SuperProductivity.Models;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string? PasswordHash { get; set; }

    public int IsVerified { get; set; } = 0;

    public string? VerificationToken { get; set; }

    public long? VerificationTokenExpiresAt { get; set; }

    public int VerificationResendCount { get; set; } = 0;

    public string? ResetPasswordToken { get; set; }

    public long? ResetPasswordTokenExpiresAt { get; set; }

    public string? PasskeyRecoveryToken { get; set; }

    public long? PasskeyRecoveryTokenExpiresAt { get; set; }

    public string? LoginToken { get; set; }

    public long? LoginTokenExpiresAt { get; set; }

    public int FailedLoginAttempts { get; set; } = 0;

    public long? LockedUntil { get; set; }

    public int TokenVersion { get; set; } = 0;

    public long? TermsAcceptedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public long StorageQuotaBytes { get; set; } = 104857600L;

    public long StorageUsedBytes { get; set; } = 0L;

    public virtual ICollection<Operation> Operations { get; set; } = new List<Operation>();

    public virtual UserSyncState? SyncState { get; set; }

    public virtual ICollection<SyncDevice> Devices { get; set; } = new List<SyncDevice>();

    public virtual ICollection<Passkey> Passkeys { get; set; } = new List<Passkey>();

    public virtual ICollection<PendingPasskeyRegistration> PendingPasskeyRegistrations { get; set; } = new List<PendingPasskeyRegistration>();

    public virtual CaptureKey? CaptureKey { get; set; }

    public virtual ICollection<CaptureGrant> CaptureGrants { get; set; } = new List<CaptureGrant>();

    public virtual ICollection<CaptureItem> CaptureItems { get; set; } = new List<CaptureItem>();

    public virtual CaptureConnectCode? CaptureConnectCode { get; set; }

    public virtual ICollection<CaptureAuth> CaptureAuthorizations { get; set; } = new List<CaptureAuth>();

    public virtual ICollection<ConnectRequest> ConnectRequests { get; set; } = new List<ConnectRequest>();
}
