<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class Space extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'last_modified_time';

    protected $table = 'space';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'avatar',
        'credit',
        'deleted_time',
        'created_time',
        'created_by',
        'last_modified_by',
        'is_template',
    ];

    protected $casts = [
        'credit' => 'integer',
        'deleted_time' => 'datetime',
        'created_time' => 'datetime',
        'is_template' => 'boolean',
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

    public function dataDbBinding(): HasOne
    {
        return $this->hasOne(SpaceDataDbBinding::class, 'space_id');
    }

    public function baseGroup(): HasMany
    {
        return $this->hasMany(Base::class, 'space_id');
    }

    public function dataDbMigrationJobs(): HasMany
    {
        return $this->hasMany(SpaceDataDbMigrationJob::class, 'space_id');
    }

    public function baseDataDbMoveJobsAsSource(): HasMany
    {
        return $this->hasMany(BaseDataDbMoveJob::class, 'source_space_id');
    }

    public function baseDataDbMoveJobsAsTarget(): HasMany
    {
        return $this->hasMany(BaseDataDbMoveJob::class, 'target_space_id');
    }
}