<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class PasswordResetToken extends Model
{
    use PrismaDates;

    protected $table = 'PasswordResetToken';

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