<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Base extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'last_modified_time';

    protected $table = 'base';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'space_id',
        'name',
        'order',
        'icon',
        'schema_pass',
        'v2_enabled',
        'provision_state',
        'deleted_time',
        'created_time',
        'created_by',
        'last_modified_by',
    ];

    protected $attributes = [
        'v2_enabled' => false,
        'provision_state' => 'ready',
    ];

    protected $casts = [
        'order' => 'float',
        'v2_enabled' => 'boolean',
        'provision_state' => ProvisionState::class,
        'deleted_time' => 'datetime',
        'created_time' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('created_time', $this->attributes)) {
                $this->setAttribute('created_time', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function space(): BelongsTo
    {
        return $this->belongsTo(Space::class, 'space_id');
    }

    public function tables(): HasMany
    {
        return $this->hasMany(TableMeta::class, 'base_id');
    }

    public function dataDbMoveJobs(): HasMany
    {
        return $this->hasMany(BaseDataDbMoveJob::class, 'base_id');
    }

    public function userOrders(): HasMany
    {
        return $this->hasMany(UserBaseOrder::class, 'base_id');
    }
}