<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Session extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';
    const ZONED_DATES = ['createdAt', 'updatedAt'];

    protected $table = 'session';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'sid',
        'data',
        'expiresAt',
    ];

    protected $casts = [
        'expiresAt' => 'datetime',
    ];
}