<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class VerificationToken extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'VerificationToken';

    protected $fillable = [
        'identifier',
        'token',
        'expires',
        'expiresInDays',
        'teamId',
        'secondaryEmailId',
    ];

    protected $casts = [
        'expires' => 'datetime',
        'expiresInDays' => 'integer',
        'teamId' => 'integer',
        'secondaryEmailId' => 'integer',
    ];

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function secondaryEmail(): BelongsTo
    {
        return $this->belongsTo(SecondaryEmail::class, 'secondaryEmailId');
    }
}