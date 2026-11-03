<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class BookingInternalNote extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'BookingInternalNote';

    protected $fillable = [
        'notePresetId',
        'text',
        'bookingId',
        'createdById',
    ];

    protected $casts = [
        'notePresetId' => 'integer',
        'bookingId' => 'integer',
        'createdById' => 'integer',
    ];

    public function notePreset(): BelongsTo
    {
        return $this->belongsTo(InternalNotePreset::class, 'notePresetId');
    }

    public function booking(): BelongsTo
    {
        return $this->belongsTo(Booking::class, 'bookingId');
    }

    public function createdBy(): BelongsTo
    {
        return $this->belongsTo(User::class, 'createdById');
    }
}