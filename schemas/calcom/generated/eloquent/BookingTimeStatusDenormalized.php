<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class BookingTimeStatusDenormalized extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'BookingTimeStatusDenormalized';

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
        'eventParentId',
        'userEmail',
        'userName',
        'userUsername',
        'ratingFeedback',
        'rating',
        'noShowHost',
        'isTeamBooking',
        'timeStatus',
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