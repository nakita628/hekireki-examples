<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Invites extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'invites';

    protected $fillable = [
        'createdBy',
        'usedBy',
        'code',
        'used',
        'editedAt',
    ];

    protected $attributes = [
        'used' => false,
    ];

    protected $casts = [
        'used' => 'boolean',
        'editedAt' => 'datetime',
    ];
}