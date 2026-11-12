<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class AiProviders extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';
    const ZONED_DATES = ['createdAt', 'updatedAt'];

    protected $table = 'aiProviders';

    protected $fillable = [
        'title',
        'provider',
        'baseURL',
        'apiKey',
        'config',
        'sortOrder',
    ];

    protected $attributes = [
        'sortOrder' => 0,
    ];

    protected $casts = [
        'config' => 'array',
        'sortOrder' => 'integer',
    ];

    public function models(): HasMany
    {
        return $this->hasMany(AiModels::class, 'providerId');
    }
}