<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class ApiKey extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'ApiKey';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'userId',
        'teamId',
        'note',
        'expiresAt',
        'lastUsedAt',
        'hashedKey',
        'appId',
    ];

    protected $casts = [
        'userId' => 'integer',
        'teamId' => 'integer',
        'expiresAt' => 'datetime',
        'lastUsedAt' => 'datetime',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function app(): BelongsTo
    {
        return $this->belongsTo(App::class, 'appId');
    }

    public function rateLimits(): HasMany
    {
        return $this->hasMany(RateLimit::class, 'apiKeyId');
    }
}