<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class OAuthRecord extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'OAuthRecord';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'kind',
        'payload',
        'grantId',
        'userCode',
        'uid',
        'expiresAt',
        'consumedAt',
    ];

    protected $casts = [
        'payload' => 'array',
        'expiresAt' => 'datetime',
        'consumedAt' => 'datetime',
    ];
}