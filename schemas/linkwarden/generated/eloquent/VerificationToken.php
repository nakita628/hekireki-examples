<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class VerificationToken extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'VerificationToken';

    protected $primaryKey = 'token';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'token',
        'identifier',
        'expires',
    ];

    protected $casts = [
        'expires' => 'datetime',
    ];
}