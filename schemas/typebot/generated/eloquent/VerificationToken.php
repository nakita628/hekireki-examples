<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class VerificationToken extends Model
{
    use PrismaDates;

    protected $table = 'VerificationToken';

    protected $primaryKey = 'token';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'token',
        'identifier',
        'value',
        'failedAttempts',
        'expires',
    ];

    protected $attributes = [
        'failedAttempts' => 0,
    ];

    protected $casts = [
        'failedAttempts' => 'integer',
        'expires' => 'datetime',
    ];
}