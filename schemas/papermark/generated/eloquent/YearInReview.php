<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class YearInReview extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'YearInReview';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'teamId',
        'status',
        'attempts',
        'lastAttempted',
        'error',
        'stats',
    ];

    protected $attributes = [
        'status' => 'pending',
        'attempts' => 0,
    ];

    protected $casts = [
        'attempts' => 'integer',
        'lastAttempted' => 'datetime',
        'stats' => 'array',
    ];
}