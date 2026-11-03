<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class InternalNotePreset extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'InternalNotePreset';

    protected $fillable = [
        'name',
        'cancellationReason',
        'teamId',
    ];

    protected $casts = [
        'teamId' => 'integer',
    ];

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function BookingInternalNote(): HasMany
    {
        return $this->hasMany(BookingInternalNote::class, 'notePresetId');
    }
}