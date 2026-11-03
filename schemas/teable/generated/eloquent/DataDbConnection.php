<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class DataDbConnection extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'last_modified_time';

    protected $table = 'data_db_connection';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'provider',
        'encrypted_url',
        'url_fingerprint',
        'display_host',
        'display_database',
        'internal_schema',
        'status',
        'schema_version',
        'capabilities',
        'last_validated_at',
        'last_error',
        'health_state',
        'health_reason',
        'health_changed_at',
        'last_health_check_at',
        'consecutive_health_failures',
        'created_by',
        'created_time',
    ];

    protected $attributes = [
        'provider' => 'postgres',
        'status' => 'pending',
        'health_state' => 'healthy',
        'consecutive_health_failures' => 0,
    ];

    protected $casts = [
        'provider' => DataDbProvider::class,
        'status' => DataDbConnectionStatus::class,
        'capabilities' => 'array',
        'last_validated_at' => 'datetime',
        'health_state' => DataDbConnectionHealthState::class,
        'health_changed_at' => 'datetime',
        'last_health_check_at' => 'datetime',
        'consecutive_health_failures' => 'integer',
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

    public function spaceBindings(): HasMany
    {
        return $this->hasMany(SpaceDataDbBinding::class, 'data_db_connection_id');
    }

    public function sourceMigrationJobs(): HasMany
    {
        return $this->hasMany(SpaceDataDbMigrationJob::class, 'source_connection_id');
    }

    public function targetMigrationJobs(): HasMany
    {
        return $this->hasMany(SpaceDataDbMigrationJob::class, 'target_connection_id');
    }
}