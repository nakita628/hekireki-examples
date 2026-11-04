<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class TemporaryAuthTokens extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'temporary_auth_tokens';

    protected $fillable = [
        'token',
        'userId',
        'expiresAt',
    ];

    protected $casts = [
        'userId' => 'integer',
        'expiresAt' => 'datetime',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(Users::class, 'userId');
    }
}