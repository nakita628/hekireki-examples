<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class FeatureFlag extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'FeatureFlag';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'key',
        'value',
    ];

    protected $casts = [
        'value' => 'array',
    ];
}