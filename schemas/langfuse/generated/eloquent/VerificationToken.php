<?php

namespace App\Models;


class VerificationToken extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'verification_tokens';

    protected $primaryKey = 'token';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'token',
        'identifier',
        'expires',
    ];

    protected $casts = [
        'expires' => 'datetime',
    ];
}