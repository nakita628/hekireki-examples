<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class User extends Model
{
    use PrismaDates;

    const UPDATED_AT = null;

    protected $table = 'users';

    protected $fillable = [
        'email',
        'password_hash',
        'is_verified',
        'verification_token',
        'verification_token_expires_at',
        'verification_resend_count',
        'reset_password_token',
        'reset_password_token_expires_at',
        'passkey_recovery_token',
        'passkey_recovery_token_expires_at',
        'login_token',
        'login_token_expires_at',
        'failed_login_attempts',
        'locked_until',
        'token_version',
        'terms_accepted_at',
        'storage_quota_bytes',
        'storage_used_bytes',
    ];

    protected $attributes = [
        'is_verified' => 0,
        'verification_resend_count' => 0,
        'failed_login_attempts' => 0,
        'token_version' => 0,
        'storage_quota_bytes' => 104857600,
        'storage_used_bytes' => 0,
    ];

    protected $casts = [
        'is_verified' => 'integer',
        'verification_token_expires_at' => 'integer',
        'verification_resend_count' => 'integer',
        'reset_password_token_expires_at' => 'integer',
        'passkey_recovery_token_expires_at' => 'integer',
        'login_token_expires_at' => 'integer',
        'failed_login_attempts' => 'integer',
        'locked_until' => 'integer',
        'token_version' => 'integer',
        'terms_accepted_at' => 'integer',
        'storage_quota_bytes' => 'integer',
        'storage_used_bytes' => 'integer',
    ];

    public function syncState(): HasOne
    {
        return $this->hasOne(UserSyncState::class, 'user_id');
    }

    public function captureKey(): HasOne
    {
        return $this->hasOne(CaptureKey::class, 'user_id');
    }

    public function captureConnectCode(): HasOne
    {
        return $this->hasOne(CaptureConnectCode::class, 'user_id');
    }

    public function operations(): HasMany
    {
        return $this->hasMany(Operation::class, 'user_id');
    }

    public function devices(): HasMany
    {
        return $this->hasMany(SyncDevice::class, 'user_id');
    }

    public function passkeys(): HasMany
    {
        return $this->hasMany(Passkey::class, 'user_id');
    }

    public function pendingPasskeyRegistrations(): HasMany
    {
        return $this->hasMany(PendingPasskeyRegistration::class, 'user_id');
    }

    public function captureGrants(): HasMany
    {
        return $this->hasMany(CaptureGrant::class, 'user_id');
    }

    public function captureItems(): HasMany
    {
        return $this->hasMany(CaptureItem::class, 'user_id');
    }

    public function captureAuthorizations(): HasMany
    {
        return $this->hasMany(CaptureAuth::class, 'user_id');
    }

    public function connectRequests(): HasMany
    {
        return $this->hasMany(ConnectRequest::class, 'user_id');
    }
}