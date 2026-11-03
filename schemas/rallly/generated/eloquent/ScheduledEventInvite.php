<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ScheduledEventInvite extends Model
{
    use PrismaDates;

    protected $table = 'scheduled_event_invites';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'uid',
        'scheduled_event_id',
        'invitee_name',
        'invitee_email',
        'invitee_id',
        'invitee_time_zone',
        'invitee_locale',
        'status',
    ];

    protected $attributes = [
        'status' => 'pending',
    ];

    protected $casts = [
        'status' => ScheduledEventInviteStatus::class,
    ];

    public function scheduledEvent(): BelongsTo
    {
        return $this->belongsTo(ScheduledEvent::class, 'scheduled_event_id');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'invitee_id');
    }
}