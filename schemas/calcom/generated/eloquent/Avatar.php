<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Avatar extends Model
{
    protected $table = 'avatars';

    protected $primaryKey = 'objectKey';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'objectKey',
        'teamId',
        'userId',
        'data',
        'isBanner',
    ];

    protected $attributes = [
        'teamId' => 0,
        'userId' => 0,
        'isBanner' => false,
    ];

    protected $casts = [
        'teamId' => 'integer',
        'userId' => 'integer',
        'isBanner' => 'boolean',
    ];
}