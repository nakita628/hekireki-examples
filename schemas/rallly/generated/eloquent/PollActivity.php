<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class PollActivity extends Model
{
    use PrismaDates;

    const UPDATED_AT = null;

    protected $table = 'poll_activities';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'poll_id',
        'type',
        'user_id',
        'participant_id',
        'invite_id',
        'option_id',
        'payload',
    ];

    protected $attributes = [
        'payload' => '{}',
    ];

    protected $casts = [
        'payload' => 'array',
    ];

    public function poll(): BelongsTo
    {
        return $this->belongsTo(Poll::class, 'poll_id');
    }
}