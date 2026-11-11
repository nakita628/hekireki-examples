<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class EmailVerificationCode extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'EmailVerificationCode';

    protected $primaryKey = 'identifier';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'identifier',
        'code',
        'attempts',
        'expires',
    ];

    protected $attributes = [
        'attempts' => 0,
    ];

    protected $casts = [
        'attempts' => 'integer',
        'expires' => 'datetime',
    ];
}