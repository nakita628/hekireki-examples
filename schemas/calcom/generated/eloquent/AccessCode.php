<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class AccessCode extends Model
{
    use PrismaDates;

    protected $table = 'AccessCode';

    public $timestamps = false;

    protected $fillable = [
        'code',
        'clientId',
        'expiresAt',
        'scopes',
        'userId',
        'teamId',
        'codeChallenge',
        'codeChallengeMethod',
    ];

    protected $attributes = [
        'codeChallengeMethod' => 'S256',
    ];

    protected $casts = [
        'expiresAt' => 'datetime',
        'userId' => 'integer',
        'teamId' => 'integer',
    ];

    public function client(): BelongsTo
    {
        return $this->belongsTo(OAuthClient::class, 'clientId');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }
}