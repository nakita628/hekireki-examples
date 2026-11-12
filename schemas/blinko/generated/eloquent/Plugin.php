<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Plugin extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';
    const ZONED_DATES = ['createdAt', 'updatedAt'];

    protected $table = 'plugin';

    protected $fillable = [
        'metadata',
        'path',
        'isUse',
        'isDev',
    ];

    protected $attributes = [
        'isUse' => true,
        'isDev' => false,
    ];

    protected $casts = [
        'metadata' => 'array',
        'isUse' => 'boolean',
        'isDev' => 'boolean',
    ];
}