<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class AssignmentReason extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'AssignmentReason';

    protected $fillable = [
        'bookingId',
        'reasonEnum',
        'reasonString',
    ];

    protected $casts = [
        'bookingId' => 'integer',
        'reasonEnum' => AssignmentReasonEnum::class,
    ];

    public function booking(): BelongsTo
    {
        return $this->belongsTo(Booking::class, 'bookingId');
    }
}