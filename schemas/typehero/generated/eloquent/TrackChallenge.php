<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class TrackChallenge extends Model
{
    protected $table = 'TrackChallenge';

    public $timestamps = false;

    protected $fillable = [
        'orderId',
        'challengeId',
        'trackId',
    ];

    protected $casts = [
        'orderId' => 'integer',
        'challengeId' => 'integer',
        'trackId' => 'integer',
    ];

    public function challenge(): BelongsTo
    {
        return $this->belongsTo(Challenge::class, 'challengeId');
    }

    public function track(): BelongsTo
    {
        return $this->belongsTo(Track::class, 'trackId');
    }
}