<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class Organisation extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Organisation';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'type',
        'name',
        'url',
        'avatarImageId',
        'customerId',
        'organisationClaimId',
        'ownerUserId',
        'organisationGlobalSettingsId',
        'organisationAuthenticationPortalId',
    ];

    protected $casts = [
        'type' => OrganisationType::class,
        'ownerUserId' => 'integer',
    ];

    public function organisationClaim(): BelongsTo
    {
        return $this->belongsTo(OrganisationClaim::class, 'organisationClaimId');
    }

    public function avatarImage(): BelongsTo
    {
        return $this->belongsTo(AvatarImage::class, 'avatarImageId');
    }

    public function owner(): BelongsTo
    {
        return $this->belongsTo(User::class, 'ownerUserId');
    }

    public function organisationGlobalSettings(): BelongsTo
    {
        return $this->belongsTo(OrganisationGlobalSettings::class, 'organisationGlobalSettingsId');
    }

    public function organisationAuthenticationPortal(): BelongsTo
    {
        return $this->belongsTo(OrganisationAuthenticationPortal::class, 'organisationAuthenticationPortalId');
    }

    public function subscription(): HasOne
    {
        return $this->hasOne(Subscription::class, 'organisationId');
    }

    public function members(): HasMany
    {
        return $this->hasMany(OrganisationMember::class, 'organisationId');
    }

    public function invites(): HasMany
    {
        return $this->hasMany(OrganisationMemberInvite::class, 'organisationId');
    }

    public function groups(): HasMany
    {
        return $this->hasMany(OrganisationGroup::class, 'organisationId');
    }

    public function teams(): HasMany
    {
        return $this->hasMany(Team::class, 'organisationId');
    }

    public function emailDomains(): HasMany
    {
        return $this->hasMany(EmailDomain::class, 'organisationId');
    }

    public function organisationEmails(): HasMany
    {
        return $this->hasMany(OrganisationEmail::class, 'organisationId');
    }

    public function monthlyStats(): HasMany
    {
        return $this->hasMany(OrganisationMonthlyStat::class, 'organisationId');
    }

    public function embedSignatures(): HasMany
    {
        return $this->hasMany(EmbedSignature::class, 'organisationId');
    }
}