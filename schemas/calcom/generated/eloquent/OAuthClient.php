<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class OAuthClient extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'OAuthClient';

    protected $primaryKey = 'clientId';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'clientId',
        'redirectUri',
        'clientSecret',
        'clientType',
        'name',
        'purpose',
        'logo',
        'websiteUrl',
        'rejectionReason',
        'isTrusted',
        'status',
        'userId',
    ];

    protected $attributes = [
        'clientType' => 'confidential',
        'isTrusted' => false,
        'status' => 'approved',
    ];

    protected $casts = [
        'clientType' => OAuthClientType::class,
        'isTrusted' => 'boolean',
        'status' => OAuthClientStatus::class,
        'userId' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function accessCodes(): HasMany
    {
        return $this->hasMany(AccessCode::class, 'clientId');
    }
}