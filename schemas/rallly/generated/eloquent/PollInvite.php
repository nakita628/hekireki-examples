<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class PollInvite extends Model
{
    use PrismaDates;

    protected $table = 'poll_invites';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'poll_id',
        'email',
        'token',
        'opened_at',
        'reminded_at',
        'revoked_at',
        'participant_id',
    ];

    protected $casts = [
        'opened_at' => 'datetime',
        'reminded_at' => 'datetime',
        'revoked_at' => 'datetime',
    ];

    public function poll(): BelongsTo
    {
        return $this->belongsTo(Poll::class, 'poll_id');
    }

    public function participant(): BelongsTo
    {
        return $this->belongsTo(Participant::class, 'participant_id');
    }
}