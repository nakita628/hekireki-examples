<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class RestrictedToken extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'RestrictedToken';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'hashedKey',
        'partialKey',
        'scopes',
        'mode',
        'source',
        'subjectType',
        'clientId',
        'expires',
        'lastUsed',
        'rateLimit',
        'userId',
        'teamId',
    ];

    protected $attributes = [
        'mode' => 'live',
        'source' => 'dashboard',
        'subjectType' => 'user',
        'rateLimit' => 60,
    ];

    protected $casts = [
        'expires' => 'datetime',
        'lastUsed' => 'datetime',
        'rateLimit' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function oauthClient(): BelongsTo
    {
        return $this->belongsTo(OAuthClient::class, 'clientId');
    }
}