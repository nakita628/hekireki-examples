<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;
use Illuminate\Support\Str;

class User extends Model
{
    use PrismaDates;

    protected $table = 'users';

    public $timestamps = false;

    protected $fillable = [
        'uuid',
        'username',
        'name',
        'email',
        'emailVerified',
        'bio',
        'avatarUrl',
        'timeZone',
        'weekStart',
        'bufferTime',
        'hideBranding',
        'theme',
        'appTheme',
        'created',
        'trialEndsAt',
        'lastActiveAt',
        'defaultScheduleId',
        'completedOnboarding',
        'locale',
        'timeFormat',
        'twoFactorSecret',
        'twoFactorEnabled',
        'backupCodes',
        'identityProvider',
        'identityProviderId',
        'invitedTo',
        'brandColor',
        'darkBrandColor',
        'allowDynamicBooking',
        'allowSEOIndexing',
        'receiveMonthlyDigestEmail',
        'requiresBookerEmailVerification',
        'metadata',
        'verified',
        'role',
        'organizationId',
        'locked',
        'movedToProfileId',
        'isPlatformManaged',
        'smsLockState',
        'smsLockReviewedByAdmin',
        'referralLinkId',
        'creationSource',
        'autoOptInFeatures',
    ];

    protected $attributes = [
        'timeZone' => 'Europe/London',
        'weekStart' => 'Sunday',
        'bufferTime' => 0,
        'hideBranding' => false,
        'completedOnboarding' => false,
        'timeFormat' => 12,
        'twoFactorEnabled' => false,
        'identityProvider' => 'CAL',
        'allowDynamicBooking' => true,
        'allowSEOIndexing' => true,
        'receiveMonthlyDigestEmail' => true,
        'requiresBookerEmailVerification' => false,
        'verified' => false,
        'role' => 'USER',
        'locked' => false,
        'isPlatformManaged' => false,
        'smsLockState' => 'UNLOCKED',
        'smsLockReviewedByAdmin' => false,
        'autoOptInFeatures' => false,
    ];

    protected $casts = [
        'emailVerified' => 'datetime',
        'bufferTime' => 'integer',
        'hideBranding' => 'boolean',
        'created' => 'datetime',
        'trialEndsAt' => 'datetime',
        'lastActiveAt' => 'datetime',
        'defaultScheduleId' => 'integer',
        'completedOnboarding' => 'boolean',
        'timeFormat' => 'integer',
        'twoFactorEnabled' => 'boolean',
        'identityProvider' => IdentityProvider::class,
        'invitedTo' => 'integer',
        'allowDynamicBooking' => 'boolean',
        'allowSEOIndexing' => 'boolean',
        'receiveMonthlyDigestEmail' => 'boolean',
        'requiresBookerEmailVerification' => 'boolean',
        'metadata' => 'array',
        'verified' => 'boolean',
        'role' => UserPermissionRole::class,
        'organizationId' => 'integer',
        'locked' => 'boolean',
        'movedToProfileId' => 'integer',
        'isPlatformManaged' => 'boolean',
        'smsLockState' => SMSLockState::class,
        'smsLockReviewedByAdmin' => 'boolean',
        'creationSource' => CreationSource::class,
        'autoOptInFeatures' => 'boolean',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('uuid', $this->attributes)) {
                $this->setAttribute('uuid', (string) Str::orderedUuid());
            }
            if (! array_key_exists('created', $this->attributes)) {
                $this->setAttribute('created', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'organizationId');
    }

    public function movedToProfile(): BelongsTo
    {
        return $this->belongsTo(Profile::class, 'movedToProfileId');
    }

    public function password(): HasOne
    {
        return $this->hasOne(UserPassword::class, 'userId');
    }

    public function destinationCalendar(): HasOne
    {
        return $this->hasOne(DestinationCalendar::class, 'userId');
    }

    public function creditBalance(): HasOne
    {
        return $this->hasOne(CreditBalance::class, 'userId');
    }

    public function holidaySettings(): HasOne
    {
        return $this->hasOne(UserHolidaySettings::class, 'userId');
    }

    public function travelSchedules(): HasMany
    {
        return $this->hasMany(TravelSchedule::class, 'userId');
    }

    public function credentials(): HasMany
    {
        return $this->hasMany(Credential::class, 'userId');
    }

    public function teams(): HasMany
    {
        return $this->hasMany(Membership::class, 'userId');
    }

    public function bookings(): HasMany
    {
        return $this->hasMany(Booking::class, 'userId');
    }

    public function schedules(): HasMany
    {
        return $this->hasMany(Schedule::class, 'userId');
    }

    public function selectedCalendars(): HasMany
    {
        return $this->hasMany(SelectedCalendar::class, 'userId');
    }

    public function availability(): HasMany
    {
        return $this->hasMany(Availability::class, 'userId');
    }

    public function webhooks(): HasMany
    {
        return $this->hasMany(Webhook::class, 'userId');
    }

    public function apiKeys(): HasMany
    {
        return $this->hasMany(ApiKey::class, 'userId');
    }

    public function accounts(): HasMany
    {
        return $this->hasMany(Account::class, 'userId');
    }

    public function sessions(): HasMany
    {
        return $this->hasMany(Session::class, 'userId');
    }

    public function Feedback(): HasMany
    {
        return $this->hasMany(Feedback::class, 'userId');
    }

    public function ownedEventTypes(): HasMany
    {
        return $this->hasMany(EventType::class, 'userId');
    }

    public function verifiedNumbers(): HasMany
    {
        return $this->hasMany(VerifiedNumber::class, 'userId');
    }

    public function verifiedEmails(): HasMany
    {
        return $this->hasMany(VerifiedEmail::class, 'userId');
    }

    public function hosts(): HasMany
    {
        return $this->hasMany(Host::class, 'userId');
    }

    public function accessCodes(): HasMany
    {
        return $this->hasMany(AccessCode::class, 'userId');
    }

    public function bookingRedirects(): HasMany
    {
        return $this->hasMany(OutOfOfficeEntry::class, 'userId');
    }

    public function bookingRedirectsTo(): HasMany
    {
        return $this->hasMany(OutOfOfficeEntry::class, 'toUserId');
    }

    public function AccessToken(): HasMany
    {
        return $this->hasMany(AccessToken::class, 'userId');
    }

    public function RefreshToken(): HasMany
    {
        return $this->hasMany(RefreshToken::class, 'userId');
    }

    public function PlatformAuthorizationToken(): HasMany
    {
        return $this->hasMany(PlatformAuthorizationToken::class, 'userId');
    }

    public function profiles(): HasMany
    {
        return $this->hasMany(Profile::class, 'userId');
    }

    public function secondaryEmails(): HasMany
    {
        return $this->hasMany(SecondaryEmail::class, 'userId');
    }

    public function OutOfOfficeReasons(): HasMany
    {
        return $this->hasMany(OutOfOfficeReason::class, 'userId');
    }

    public function NotificationsSubscriptions(): HasMany
    {
        return $this->hasMany(NotificationsSubscriptions::class, 'userId');
    }

    public function features(): HasMany
    {
        return $this->hasMany(UserFeatures::class, 'userId');
    }

    public function reassignedBookings(): HasMany
    {
        return $this->hasMany(Booking::class, 'reassignById');
    }

    public function createdAttributeToUsers(): HasMany
    {
        return $this->hasMany(AttributeToUser::class, 'createdById');
    }

    public function updatedAttributeToUsers(): HasMany
    {
        return $this->hasMany(AttributeToUser::class, 'updatedById');
    }

    public function createdTranslations(): HasMany
    {
        return $this->hasMany(EventTypeTranslation::class, 'createdBy');
    }

    public function updatedTranslations(): HasMany
    {
        return $this->hasMany(EventTypeTranslation::class, 'updatedBy');
    }

    public function reportedBookings(): HasMany
    {
        return $this->hasMany(BookingReport::class, 'reportedById');
    }

    public function wrongAssignmentReports(): HasMany
    {
        return $this->hasMany(WrongAssignmentReport::class, 'reportedById');
    }

    public function reviewedWrongAssignmentReports(): HasMany
    {
        return $this->hasMany(WrongAssignmentReport::class, 'reviewedById');
    }

    public function BookingInternalNote(): HasMany
    {
        return $this->hasMany(BookingInternalNote::class, 'createdById');
    }

    public function createdOrganizationOnboardings(): HasMany
    {
        return $this->hasMany(OrganizationOnboarding::class, 'createdById');
    }

    public function filterSegments(): HasMany
    {
        return $this->hasMany(FilterSegment::class, 'userId');
    }

    public function filterSegmentPreferences(): HasMany
    {
        return $this->hasMany(UserFilterSegmentPreference::class, 'userId');
    }

    public function calAiPhoneNumbers(): HasMany
    {
        return $this->hasMany(CalAiPhoneNumber::class, 'userId');
    }

    public function agents(): HasMany
    {
        return $this->hasMany(Agent::class, 'userId');
    }

    public function oAuthClients(): HasMany
    {
        return $this->hasMany(OAuthClient::class, 'userId');
    }

    public function eventTypes(): BelongsToMany
    {
        return $this->belongsToMany(EventType::class, '_user_eventtype', 'B', 'A');
    }

    public function platformOAuthClients(): BelongsToMany
    {
        return $this->belongsToMany(PlatformOAuthClient::class, '_PlatformOAuthClientToUser', 'B', 'A');
    }
}