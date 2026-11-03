<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Poll extends Model
{
    use PrismaDates;

    protected $table = 'polls';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'deadline',
        'title',
        'description',
        'location',
        'conferencing',
        'user_id',
        'time_zone',
        'status',
        'closed_reason',
        'kind',
        'deleted',
        'deleted_at',
        'event_id',
        'scheduled_event_id',
        'hide_participants',
        'hide_scores',
        'disable_comments',
        'require_participant_email',
        'allow_tentative_votes',
        'muted',
        'space_id',
    ];

    protected $attributes = [
        'status' => 'open',
        'kind' => 'date',
        'deleted' => false,
        'hide_participants' => false,
        'hide_scores' => false,
        'disable_comments' => true,
        'require_participant_email' => false,
        'allow_tentative_votes' => true,
        'muted' => false,
    ];

    protected $casts = [
        'deadline' => 'datetime',
        'conferencing' => 'array',
        'status' => PollStatus::class,
        'closed_reason' => PollClosedReason::class,
        'kind' => PollKind::class,
        'deleted' => 'boolean',
        'deleted_at' => 'datetime',
        'hide_participants' => 'boolean',
        'hide_scores' => 'boolean',
        'disable_comments' => 'boolean',
        'require_participant_email' => 'boolean',
        'allow_tentative_votes' => 'boolean',
        'muted' => 'boolean',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }

    public function scheduledEvent(): BelongsTo
    {
        return $this->belongsTo(ScheduledEvent::class, 'scheduled_event_id');
    }

    public function space(): BelongsTo
    {
        return $this->belongsTo(Space::class, 'space_id');
    }

    public function options(): HasMany
    {
        return $this->hasMany(Option::class, 'poll_id');
    }

    public function participants(): HasMany
    {
        return $this->hasMany(Participant::class, 'poll_id');
    }

    public function comments(): HasMany
    {
        return $this->hasMany(Comment::class, 'poll_id');
    }

    public function votes(): HasMany
    {
        return $this->hasMany(Vote::class, 'poll_id');
    }

    public function invites(): HasMany
    {
        return $this->hasMany(PollInvite::class, 'poll_id');
    }

    public function activities(): HasMany
    {
        return $this->hasMany(PollActivity::class, 'poll_id');
    }
}