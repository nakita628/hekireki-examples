<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class ConfigurationObject extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'ConfigurationObject';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'deleted',
        'type',
        'workspaceId',
        'config',
    ];

    protected $attributes = [
        'deleted' => false,
    ];

    protected $casts = [
        'deleted' => 'boolean',
        'config' => 'array',
    ];

    public function workspace(): BelongsTo
    {
        return $this->belongsTo(Workspace::class, 'workspaceId');
    }

    public function fromLinks(): HasMany
    {
        return $this->hasMany(ConfigurationObjectLink::class, 'toId');
    }

    public function toLinks(): HasMany
    {
        return $this->hasMany(ConfigurationObjectLink::class, 'fromId');
    }

    public function ProfileBuilder(): HasMany
    {
        return $this->hasMany(ProfileBuilder::class, 'destinationId');
    }

    public function ProfileBuilderFunction(): HasMany
    {
        return $this->hasMany(ProfileBuilderFunction::class, 'functionId');
    }
}