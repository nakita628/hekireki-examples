<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Fonts extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';
    const ZONED_DATES = ['createdAt', 'updatedAt'];

    protected $table = 'fonts';

    protected $fillable = [
        'name',
        'displayName',
        'url',
        'fileData',
        'isLocal',
        'isSystem',
        'weights',
        'category',
        'sortOrder',
    ];

    protected $attributes = [
        'isLocal' => false,
        'isSystem' => false,
        'category' => 'sans-serif',
        'sortOrder' => 0,
    ];

    protected $casts = [
        'fileData' => AsBytes::class,
        'isLocal' => 'boolean',
        'isSystem' => 'boolean',
        'weights' => 'array',
        'sortOrder' => 'integer',
    ];
}