<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class VerificationToken extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'VerificationToken';

    protected $fillable = [
        'secondaryId',
        'identifier',
        'token',
        'completed',
        'expires',
        'metadata',
        'userId',
    ];

    protected $attributes = [
        'completed' => false,
    ];

    protected $casts = [
        'completed' => 'boolean',
        'expires' => 'datetime',
        'metadata' => 'array',
        'userId' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }
}