<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Verification extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'verification';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'identifier',
        'value',
        'expiresAt',
    ];

    protected $casts = [
        'expiresAt' => 'datetime',
    ];
}