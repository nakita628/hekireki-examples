<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class OutOfOfficeEntry extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'OutOfOfficeEntry';

    protected $fillable = [
        'uuid',
        'start',
        'end',
        'notes',
        'showNotePublicly',
        'userId',
        'toUserId',
        'reasonId',
    ];

    protected $attributes = [
        'showNotePublicly' => false,
    ];

    protected $casts = [
        'start' => 'datetime',
        'end' => 'datetime',
        'showNotePublicly' => 'boolean',
        'userId' => 'integer',
        'toUserId' => 'integer',
        'reasonId' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function toUser(): BelongsTo
    {
        return $this->belongsTo(User::class, 'toUserId');
    }

    public function reason(): BelongsTo
    {
        return $this->belongsTo(OutOfOfficeReason::class, 'reasonId');
    }
}