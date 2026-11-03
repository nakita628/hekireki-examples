<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Attribute extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Attribute';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'teamId',
        'type',
        'name',
        'slug',
        'enabled',
        'usersCanEditRelation',
        'isWeightsEnabled',
        'isLocked',
    ];

    protected $attributes = [
        'enabled' => true,
        'usersCanEditRelation' => false,
        'isWeightsEnabled' => false,
        'isLocked' => false,
    ];

    protected $casts = [
        'teamId' => 'integer',
        'type' => AttributeType::class,
        'enabled' => 'boolean',
        'usersCanEditRelation' => 'boolean',
        'isWeightsEnabled' => 'boolean',
        'isLocked' => 'boolean',
    ];

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function options(): HasMany
    {
        return $this->hasMany(AttributeOption::class, 'attributeId');
    }

    public function attributeSyncFieldMappings(): HasMany
    {
        return $this->hasMany(AttributeSyncFieldMapping::class, 'attributeId');
    }
}