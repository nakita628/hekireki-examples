<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class SpaceDataDbBinding extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'last_modified_time';

    protected $table = 'space_data_db_binding';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'space_id',
        'data_db_connection_id',
        'mode',
        'state',
        'created_by',
        'created_time',
        'last_history_flushed_at',
        'last_removal_flushed_at',
    ];

    protected $attributes = [
        'mode' => 'default',
        'state' => 'ready',
    ];

    protected $casts = [
        'mode' => SpaceDataDbBindingMode::class,
        'state' => SpaceDataDbBindingState::class,
        'created_time' => 'datetime',
        'last_history_flushed_at' => 'datetime',
        'last_removal_flushed_at' => 'datetime',
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

    public function dataDbConnection(): BelongsTo
    {
        return $this->belongsTo(DataDbConnection::class, 'data_db_connection_id');
    }
}