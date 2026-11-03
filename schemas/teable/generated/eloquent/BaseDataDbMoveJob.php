<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class BaseDataDbMoveJob extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'last_modified_time';

    protected $table = 'base_data_db_move_job';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'base_id',
        'source_space_id',
        'target_space_id',
        'source_connection_id',
        'target_connection_id',
        'state',
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
        'state' => 'pending',
    ];

    protected $casts = [
        'state' => BaseDataDbMoveJobState::class,
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

    public function base(): BelongsTo
    {
        return $this->belongsTo(Base::class, 'base_id');
    }

    public function sourceSpace(): BelongsTo
    {
        return $this->belongsTo(Space::class, 'source_space_id');
    }

    public function targetSpace(): BelongsTo
    {
        return $this->belongsTo(Space::class, 'target_space_id');
    }
}