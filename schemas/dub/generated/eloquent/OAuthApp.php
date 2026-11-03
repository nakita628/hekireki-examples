<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class OAuthApp extends Model
{
    protected $table = 'OAuthApp';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'integrationId',
        'clientId',
        'hashedClientSecret',
        'partialClientSecret',
        'redirectUris',
        'pkce',
    ];

    protected $attributes = [
        'pkce' => false,
    ];

    protected $casts = [
        'redirectUris' => 'array',
        'pkce' => 'boolean',
    ];

    public function integration(): BelongsTo
    {
        return $this->belongsTo(Integration::class, 'integrationId');
    }

    public function oAuthCodes(): HasMany
    {
        return $this->hasMany(OAuthCode::class, 'clientId', 'clientId');
    }
}