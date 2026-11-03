<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class OAuthApp extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'OAuthApp';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'organizationId',
        'name',
        'description',
        'pictureId',
        'redirectUrl',
        'redirectUris',
        'clientId',
        'clientSecret',
        'dynamic',
        'tokenEndpointAuthMethod',
        'deletedAt',
    ];

    protected $attributes = [
        'dynamic' => false,
    ];

    protected $casts = [
        'dynamic' => 'boolean',
        'deletedAt' => 'datetime',
    ];

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organizationId');
    }

    public function picture(): BelongsTo
    {
        return $this->belongsTo(Media::class, 'pictureId');
    }

    public function authorizations(): HasMany
    {
        return $this->hasMany(OAuthAuthorization::class, 'oauthAppId');
    }

    public function selfHostedAuthorizations(): HasMany
    {
        return $this->hasMany(OAuthSelfHostedAuthorization::class, 'oauthAppId');
    }
}