<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasOne;

class Attendee extends Model
{
    protected $table = 'Attendee';

    public $timestamps = false;

    protected $fillable = [
        'email',
        'name',
        'timeZone',
        'phoneNumber',
        'locale',
        'bookingId',
        'noShow',
    ];

    protected $attributes = [
        'locale' => 'en',
        'noShow' => false,
    ];

    protected $casts = [
        'bookingId' => 'integer',
        'noShow' => 'boolean',
    ];

    public function booking(): BelongsTo
    {
        return $this->belongsTo(Booking::class, 'bookingId');
    }

    public function bookingSeat(): HasOne
    {
        return $this->hasOne(BookingSeat::class, 'attendeeId');
    }
}