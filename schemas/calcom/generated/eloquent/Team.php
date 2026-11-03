<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class Team extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'Team';

    protected $fillable = [
        'name',
        'slug',
        'logoUrl',
        'calVideoLogo',
        'appLogo',
        'appIconLogo',
        'bio',
        'hideBranding',
        'hideTeamProfileLink',
        'isPrivate',
        'hideBookATeamMember',
        'metadata',
        'theme',
        'rrResetInterval',
        'rrTimestampBasis',
        'brandColor',
        'darkBrandColor',
        'bannerUrl',
        'parentId',
        'timeFormat',
        'timeZone',
        'weekStart',
        'isOrganization',
        'pendingPayment',
        'isPlatform',
        'createdByOAuthClientId',
        'smsLockState',
        'smsLockReviewedByAdmin',
        'bookingLimits',
        'includeManagedEventsInLimits',
        'autoOptInFeatures',
    ];

    protected $attributes = [
        'hideBranding' => false,
        'hideTeamProfileLink' => false,
        'isPrivate' => false,
        'hideBookATeamMember' => false,
        'rrResetInterval' => 'MONTH',
        'rrTimestampBasis' => 'CREATED_AT',
        'timeZone' => 'Europe/London',
        'weekStart' => 'Sunday',
        'isOrganization' => false,
        'pendingPayment' => false,
        'isPlatform' => false,
        'smsLockState' => 'UNLOCKED',
        'smsLockReviewedByAdmin' => false,
        'includeManagedEventsInLimits' => false,
        'autoOptInFeatures' => false,
    ];

    protected $casts = [
        'hideBranding' => 'boolean',
        'hideTeamProfileLink' => 'boolean',
        'isPrivate' => 'boolean',
        'hideBookATeamMember' => 'boolean',
        'metadata' => 'array',
        'rrResetInterval' => RRResetInterval::class,
        'rrTimestampBasis' => RRTimestampBasis::class,
        'parentId' => 'integer',
        'timeFormat' => 'integer',
        'isOrganization' => 'boolean',
        'pendingPayment' => 'boolean',
        'isPlatform' => 'boolean',
        'smsLockState' => SMSLockState::class,
        'smsLockReviewedByAdmin' => 'boolean',
        'bookingLimits' => 'array',
        'includeManagedEventsInLimits' => 'boolean',
        'autoOptInFeatures' => 'boolean',
    ];

    public function parent(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'parentId');
    }

    public function createdByOAuthClient(): BelongsTo
    {
        return $this->belongsTo(PlatformOAuthClient::class, 'createdByOAuthClientId');
    }

    public function organizationSettings(): HasOne
    {
        return $this->hasOne(OrganizationSettings::class, 'organizationId');
    }

    public function platformBilling(): HasOne
    {
        return $this->hasOne(PlatformBilling::class, 'id');
    }

    public function creditBalance(): HasOne
    {
        return $this->hasOne(CreditBalance::class, 'teamId');
    }

    public function organizationOnboarding(): HasOne
    {
        return $this->hasOne(OrganizationOnboarding::class, 'organizationId');
    }

    public function managedOrganization(): HasOne
    {
        return $this->hasOne(ManagedOrganization::class, 'managedOrganizationId');
    }

    public function teamBilling(): HasOne
    {
        return $this->hasOne(TeamBilling::class, 'teamId');
    }

    public function organizationBilling(): HasOne
    {
        return $this->hasOne(OrganizationBilling::class, 'teamId');
    }

    public function members(): HasMany
    {
        return $this->hasMany(Membership::class, 'teamId');
    }

    public function eventTypes(): HasMany
    {
        return $this->hasMany(EventType::class, 'teamId');
    }

    public function verifiedNumbers(): HasMany
    {
        return $this->hasMany(VerifiedNumber::class, 'teamId');
    }

    public function verifiedEmails(): HasMany
    {
        return $this->hasMany(VerifiedEmail::class, 'teamId');
    }

    public function children(): HasMany
    {
        return $this->hasMany(Team::class, 'parentId');
    }

    public function orgUsers(): HasMany
    {
        return $this->hasMany(User::class, 'organizationId');
    }

    public function inviteTokens(): HasMany
    {
        return $this->hasMany(VerificationToken::class, 'teamId');
    }

    public function webhooks(): HasMany
    {
        return $this->hasMany(Webhook::class, 'teamId');
    }

    public function apiKeys(): HasMany
    {
        return $this->hasMany(ApiKey::class, 'teamId');
    }

    public function credentials(): HasMany
    {
        return $this->hasMany(Credential::class, 'teamId');
    }

    public function accessCodes(): HasMany
    {
        return $this->hasMany(AccessCode::class, 'teamId');
    }

    public function instantMeetingTokens(): HasMany
    {
        return $this->hasMany(InstantMeetingToken::class, 'teamId');
    }

    public function orgProfiles(): HasMany
    {
        return $this->hasMany(Profile::class, 'organizationId');
    }

    public function dsyncTeamGroupMapping(): HasMany
    {
        return $this->hasMany(DSyncTeamGroupMapping::class, 'teamId');
    }

    public function platformOAuthClient(): HasMany
    {
        return $this->hasMany(PlatformOAuthClient::class, 'organizationId');
    }

    public function attributes(): HasMany
    {
        return $this->hasMany(Attribute::class, 'teamId');
    }

    public function delegationCredentials(): HasMany
    {
        return $this->hasMany(DelegationCredential::class, 'organizationId');
    }

    public function roles(): HasMany
    {
        return $this->hasMany(Role::class, 'teamId');
    }

    public function calAiPhoneNumbers(): HasMany
    {
        return $this->hasMany(CalAiPhoneNumber::class, 'teamId');
    }

    public function agents(): HasMany
    {
        return $this->hasMany(Agent::class, 'teamId');
    }

    public function bookingReports(): HasMany
    {
        return $this->hasMany(BookingReport::class, 'organizationId');
    }

    public function wrongAssignmentReports(): HasMany
    {
        return $this->hasMany(WrongAssignmentReport::class, 'teamId');
    }

    public function features(): HasMany
    {
        return $this->hasMany(TeamFeatures::class, 'teamId');
    }

    public function internalNotePresets(): HasMany
    {
        return $this->hasMany(InternalNotePreset::class, 'teamId');
    }

    public function managedOrganizations(): HasMany
    {
        return $this->hasMany(ManagedOrganization::class, 'managerOrganizationId');
    }

    public function filterSegments(): HasMany
    {
        return $this->hasMany(FilterSegment::class, 'teamId');
    }

    public function integrationAttributeSyncs(): HasMany
    {
        return $this->hasMany(IntegrationAttributeSync::class, 'organizationId');
    }

    public function seatChangeLogs(): HasMany
    {
        return $this->hasMany(SeatChangeLog::class, 'teamId');
    }

    public function monthlyProrations(): HasMany
    {
        return $this->hasMany(MonthlyProration::class, 'teamId');
    }
}