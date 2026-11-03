<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Verification extends Model
{
    use PrismaDates;

    protected $table = 'verifications';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'identifier',
        'value',
        'expires_at',
    ];

    protected $casts = [
        'expires_at' => 'datetime',
    ];
}