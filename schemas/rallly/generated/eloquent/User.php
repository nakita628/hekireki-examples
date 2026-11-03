<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class User extends Model
{
    use PrismaDates;

    protected $table = 'users';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'email',
        'email_verified',
        'image',
        'time_zone',
        'week_start',
        'time_format',
        'locale',
        'customer_id',
        'banned',
        'banned_at',
        'ban_reason',
        'ban_expires',
        'role',
        'last_login_method',
        'anonymous',
        'last_seen_at',
        'deleted_at',
        'job_title',
        'review_requested_at',
        'default_destination_calendar_id',
    ];

    protected $attributes = [
        'banned' => false,
        'role' => 'user',
        'anonymous' => false,
    ];

    protected $casts = [
        'email_verified' => 'boolean',
        'week_start' => 'integer',
        'time_format' => TimeFormat::class,
        'banned' => 'boolean',
        'banned_at' => 'datetime',
        'ban_expires' => 'datetime',
        'role' => UserRole::class,
        'anonymous' => 'boolean',
        'last_seen_at' => 'datetime',
        'deleted_at' => 'datetime',
        'review_requested_at' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('last_seen_at', $this->attributes)) {
                $this->setAttribute('last_seen_at', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function defaultDestinationCalendar(): BelongsTo
    {
        return $this->belongsTo(ProviderCalendar::class, 'default_destination_calendar_id');
    }

    public function notificationPreferences(): HasOne
    {
        return $this->hasOne(UserNotificationPreferences::class, 'user_id');
    }

    public function comments(): HasMany
    {
        return $this->hasMany(Comment::class, 'user_id');
    }

    public function polls(): HasMany
    {
        return $this->hasMany(Poll::class, 'user_id');
    }

    public function queuedEmails(): HasMany
    {
        return $this->hasMany(QueuedEmail::class, 'user_id');
    }

    public function accounts(): HasMany
    {
        return $this->hasMany(Account::class, 'user_id');
    }

    public function participants(): HasMany
    {
        return $this->hasMany(Participant::class, 'user_id');
    }

    public function paymentMethods(): HasMany
    {
        return $this->hasMany(PaymentMethod::class, 'user_id');
    }

    public function spaceMemberInvites(): HasMany
    {
        return $this->hasMany(SpaceMemberInvite::class, 'inviter_id');
    }

    public function nonprofitApplications(): HasMany
    {
        return $this->hasMany(NonprofitApplication::class, 'user_id');
    }

    public function subscriptions(): HasMany
    {
        return $this->hasMany(Subscription::class, 'user_id');
    }

    public function spaces(): HasMany
    {
        return $this->hasMany(Space::class, 'owner_id');
    }

    public function memberOf(): HasMany
    {
        return $this->hasMany(SpaceMember::class, 'user_id');
    }

    public function scheduledEvents(): HasMany
    {
        return $this->hasMany(ScheduledEvent::class, 'user_id');
    }

    public function scheduledEventInvites(): HasMany
    {
        return $this->hasMany(ScheduledEventInvite::class, 'invitee_id');
    }

    public function hostedEventTypes(): HasMany
    {
        return $this->hasMany(EventType::class, 'host_id');
    }

    public function hostedSheets(): HasMany
    {
        return $this->hasMany(Sheet::class, 'host_id');
    }

    public function calendarConnections(): HasMany
    {
        return $this->hasMany(CalendarConnection::class, 'user_id');
    }

    public function conferencingConnections(): HasMany
    {
        return $this->hasMany(ConferencingConnection::class, 'user_id');
    }

    public function credentials(): HasMany
    {
        return $this->hasMany(Credential::class, 'user_id');
    }

    public function sessions(): HasMany
    {
        return $this->hasMany(Session::class, 'user_id');
    }

    public function impersonatedSessions(): HasMany
    {
        return $this->hasMany(Session::class, 'impersonated_by');
    }
}