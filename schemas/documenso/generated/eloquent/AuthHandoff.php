<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class AuthHandoff extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'AuthHandoff';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'type',
        'payload',
        'expiresAt',
    ];

    protected $casts = [
        'expiresAt' => 'datetime',
    ];
}