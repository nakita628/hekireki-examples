<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class OAuthClient extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'OAuthClient';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'clientId',
        'clientName',
        'clientUri',
        'logoUri',
        'redirectUris',
        'grantTypes',
        'scopes',
        'tokenAuthMethod',
        'hashedSecret',
        'registrationType',
        'metadataUrl',
    ];

    public function tokens(): HasMany
    {
        return $this->hasMany(RestrictedToken::class, 'clientId');
    }
}