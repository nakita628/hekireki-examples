<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Links extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'links';

    protected $fillable = [
        'uuid',
        'userId',
        'albumId',
        'identifier',
        'views',
        'enabled',
        'enableDownload',
        'expiresAt',
        'editedAt',
    ];

    protected $attributes = [
        'views' => 0,
        'enabled' => true,
        'enableDownload' => false,
    ];

    protected $casts = [
        'userId' => 'integer',
        'albumId' => 'integer',
        'views' => 'integer',
        'enabled' => 'boolean',
        'enableDownload' => 'boolean',
        'expiresAt' => 'datetime',
        'editedAt' => 'datetime',
    ];
}