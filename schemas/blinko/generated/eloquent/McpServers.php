<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class McpServers extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';
    const ZONED_DATES = ['createdAt', 'updatedAt'];

    protected $table = 'mcpServers';

    protected $fillable = [
        'name',
        'description',
        'type',
        'command',
        'args',
        'url',
        'env',
        'headers',
        'isEnabled',
    ];

    protected $attributes = [
        'isEnabled' => true,
    ];

    protected $casts = [
        'args' => 'array',
        'env' => 'array',
        'headers' => 'array',
        'isEnabled' => 'boolean',
    ];
}