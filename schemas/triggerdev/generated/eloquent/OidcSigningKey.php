<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class OidcSigningKey extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'OidcSigningKey';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'kid',
        'purpose',
        'status',
        'publicJwk',
        'readyAt',
        'publicUntil',
        'generatedByUserId',
        'activatedAt',
        'activatedByUserId',
        'retiredAt',
        'retiredByUserId',
    ];

    protected $attributes = [
        'purpose' => 'EXTERNAL_OIDC',
    ];

    protected $casts = [
        'purpose' => OidcSigningKeyPurpose::class,
        'status' => OidcSigningKeyStatus::class,
        'publicJwk' => 'array',
        'readyAt' => 'datetime',
        'publicUntil' => 'datetime',
        'activatedAt' => 'datetime',
        'retiredAt' => 'datetime',
    ];
}