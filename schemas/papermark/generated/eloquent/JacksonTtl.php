<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class JacksonTtl extends Model
{
    protected $table = 'jackson_ttl';

    protected $primaryKey = 'key';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'key',
        'expiresAt',
    ];

    protected $casts = [
        'expiresAt' => 'integer',
    ];
}