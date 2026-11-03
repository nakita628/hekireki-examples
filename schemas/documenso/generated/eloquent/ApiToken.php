<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ApiToken extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'ApiToken';

    protected $fillable = [
        'name',
        'token',
        'algorithm',
        'expires',
        'lastUsedAt',
        'userId',
        'teamId',
    ];

    protected $attributes = [
        'algorithm' => 'SHA512',
    ];

    protected $casts = [
        'algorithm' => ApiTokenAlgorithm::class,
        'expires' => 'datetime',
        'lastUsedAt' => 'datetime',
        'userId' => 'integer',
        'teamId' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }
}