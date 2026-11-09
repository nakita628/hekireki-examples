<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ConfigurationObjectLink extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'ConfigurationObjectLink';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'deleted',
        'type',
        'data',
        'workspaceId',
        'fromId',
        'toId',
    ];

    protected $attributes = [
        'deleted' => false,
        'type' => 'push',
    ];

    protected $casts = [
        'deleted' => 'boolean',
        'data' => 'array',
    ];

    public function workspace(): BelongsTo
    {
        return $this->belongsTo(Workspace::class, 'workspaceId');
    }

    public function from(): BelongsTo
    {
        return $this->belongsTo(ConfigurationObject::class, 'fromId');
    }

    public function to(): BelongsTo
    {
        return $this->belongsTo(ConfigurationObject::class, 'toId');
    }
}