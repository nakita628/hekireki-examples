<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class InstantMeetingToken extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'InstantMeetingToken';

    protected $fillable = [
        'token',
        'expires',
        'teamId',
        'bookingId',
    ];

    protected $casts = [
        'expires' => 'datetime',
        'teamId' => 'integer',
        'bookingId' => 'integer',
    ];

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function booking(): BelongsTo
    {
        return $this->belongsTo(Booking::class, 'bookingId');
    }
}