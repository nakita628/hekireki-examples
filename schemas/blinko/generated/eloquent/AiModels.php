<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class AiModels extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';
    const ZONED_DATES = ['createdAt', 'updatedAt'];

    protected $table = 'aiModels';

    protected $fillable = [
        'providerId',
        'title',
        'modelKey',
        'capabilities',
        'config',
        'sortOrder',
    ];

    protected $attributes = [
        'sortOrder' => 0,
    ];

    protected $casts = [
        'providerId' => 'integer',
        'capabilities' => 'array',
        'config' => 'array',
        'sortOrder' => 'integer',
    ];

    public function provider(): BelongsTo
    {
        return $this->belongsTo(AiProviders::class, 'providerId');
    }
}