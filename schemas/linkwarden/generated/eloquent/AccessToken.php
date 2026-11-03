<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class AccessToken extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'AccessToken';

    protected $fillable = [
        'name',
        'userId',
        'token',
        'revoked',
        'isSession',
        'expires',
        'lastUsedAt',
    ];

    protected $attributes = [
        'revoked' => false,
        'isSession' => false,
    ];

    protected $casts = [
        'userId' => 'integer',
        'revoked' => 'boolean',
        'isSession' => 'boolean',
        'expires' => 'datetime',
        'lastUsedAt' => 'datetime',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }
}