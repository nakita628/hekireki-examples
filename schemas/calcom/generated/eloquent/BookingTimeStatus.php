<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class BookingTimeStatus extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'BookingTimeStatus';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'uid',
        'eventTypeId',
        'title',
        'description',
        'startTime',
        'endTime',
        'location',
        'paid',
        'status',
        'rescheduled',
        'userId',
        'teamId',
        'eventLength',
        'timeStatus',
        'eventParentId',
        'userEmail',
        'username',
        'ratingFeedback',
        'rating',
        'noShowHost',
        'isTeamBooking',
    ];

    protected $casts = [
        'eventTypeId' => 'integer',
        'startTime' => 'datetime',
        'endTime' => 'datetime',
        'paid' => 'boolean',
        'status' => BookingStatus::class,
        'rescheduled' => 'boolean',
        'userId' => 'integer',
        'teamId' => 'integer',
        'eventLength' => 'integer',
        'eventParentId' => 'integer',
        'rating' => 'integer',
        'noShowHost' => 'boolean',
        'isTeamBooking' => 'boolean',
    ];
}