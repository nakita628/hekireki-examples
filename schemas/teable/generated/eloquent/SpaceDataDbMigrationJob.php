<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class SpaceDataDbMigrationJob extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'last_modified_time';

    protected $table = 'space_data_db_migration_job';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'space_id',
        'source_connection_id',
        'target_connection_id',
        'target_mode',
        'switch_on_completion',
        'state',
        'target_url_fingerprint',
        'target_internal_schema',
        'inventory',
        'copy_stats',
        'validation_stats',
        'last_error',
        'started_at',
        'completed_at',
        'created_by',
        'created_time',
    ];

    protected $attributes = [
        'target_mode' => 'migrate-space',
        'switch_on_completion' => false,
        'state' => 'pending',
    ];

    protected $casts = [
        'switch_on_completion' => 'boolean',
        'state' => SpaceDataDbMigrationJobState::class,
        'inventory' => 'array',
        'copy_stats' => 'array',
        'validation_stats' => 'array',
        'started_at' => 'datetime',
        'completed_at' => 'datetime',
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

    public function sourceConnection(): BelongsTo
    {
        return $this->belongsTo(DataDbConnection::class, 'source_connection_id');
    }

    public function targetConnection(): BelongsTo
    {
        return $this->belongsTo(DataDbConnection::class, 'target_connection_id');
    }
}