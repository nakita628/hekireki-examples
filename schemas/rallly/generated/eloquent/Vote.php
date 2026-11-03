<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Vote extends Model
{
    use PrismaDates;

    protected $table = 'votes';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'participant_id',
        'option_id',
        'poll_id',
        'type',
    ];

    protected $attributes = [
        'type' => 'yes',
    ];

    protected $casts = [
        'type' => VoteType::class,
    ];

    public function participant(): BelongsTo
    {
        return $this->belongsTo(Participant::class, 'participant_id');
    }

    public function option(): BelongsTo
    {
        return $this->belongsTo(Option::class, 'option_id');
    }

    public function poll(): BelongsTo
    {
        return $this->belongsTo(Poll::class, 'poll_id');
    }
}