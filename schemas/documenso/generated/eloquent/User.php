<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class User extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'User';

    protected $fillable = [
        'name',
        'email',
        'emailVerified',
        'password',
        'source',
        'signature',
        'lastSignedIn',
        'roles',
        'identityProvider',
        'avatarImageId',
        'disabled',
        'twoFactorSecret',
        'twoFactorEnabled',
        'twoFactorBackupCodes',
    ];

    protected $attributes = [
        'identityProvider' => 'DOCUMENSO',
        'disabled' => false,
        'twoFactorEnabled' => false,
    ];

    protected $casts = [
        'emailVerified' => 'datetime',
        'lastSignedIn' => 'datetime',
        'identityProvider' => IdentityProvider::class,
        'disabled' => 'boolean',
        'twoFactorEnabled' => 'boolean',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('lastSignedIn', $this->attributes)) {
                $this->setAttribute('lastSignedIn', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function avatarImage(): BelongsTo
    {
        return $this->belongsTo(AvatarImage::class, 'avatarImageId');
    }

    public function accounts(): HasMany
    {
        return $this->hasMany(Account::class, 'userId');
    }

    public function sessions(): HasMany
    {
        return $this->hasMany(Session::class, 'userId');
    }

    public function passwordResetTokens(): HasMany
    {
        return $this->hasMany(PasswordResetToken::class, 'userId');
    }

    public function ownedOrganisations(): HasMany
    {
        return $this->hasMany(Organisation::class, 'ownerUserId');
    }

    public function organisationMember(): HasMany
    {
        return $this->hasMany(OrganisationMember::class, 'userId');
    }

    public function folders(): HasMany
    {
        return $this->hasMany(Folder::class, 'userId');
    }

    public function envelopes(): HasMany
    {
        return $this->hasMany(Envelope::class, 'userId');
    }

    public function verificationTokens(): HasMany
    {
        return $this->hasMany(VerificationToken::class, 'userId');
    }

    public function apiTokens(): HasMany
    {
        return $this->hasMany(ApiToken::class, 'userId');
    }

    public function securityAuditLogs(): HasMany
    {
        return $this->hasMany(UserSecurityAuditLog::class, 'userId');
    }

    public function webhooks(): HasMany
    {
        return $this->hasMany(Webhook::class, 'userId');
    }

    public function siteSettings(): HasMany
    {
        return $this->hasMany(SiteSettings::class, 'lastModifiedByUserId');
    }

    public function passkeys(): HasMany
    {
        return $this->hasMany(Passkey::class, 'userId');
    }
}