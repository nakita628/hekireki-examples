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
        'expires',
    ];

    protected $casts = [
        'expires' => 'datetime',
    ];
}