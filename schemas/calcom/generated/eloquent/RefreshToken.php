<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class RefreshToken extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'RefreshToken';

    protected $fillable = [
        'secret',
        'expiresAt',
        'platformOAuthClientId',
        'userId',
    ];

    protected $casts = [
        'expiresAt' => 'datetime',
        'userId' => 'integer',
    ];

    public function owner(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function client(): BelongsTo
    {
        return $this->belongsTo(PlatformOAuthClient::class, 'platformOAuthClientId');
    }
}