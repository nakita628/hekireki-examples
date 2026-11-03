<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class AnonymousVerificationToken extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'AnonymousVerificationToken';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'token',
        'expiresAt',
    ];

    protected $casts = [
        'expiresAt' => 'datetime',
    ];
}