<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Field extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'last_modified_time';

    protected $table = 'field';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'description',
        'options',
        'meta',
        'ai_config',
        'type',
        'cell_value_type',
        'is_multiple_cell_value',
        'db_field_type',
        'db_field_name',
        'provision_state',
        'not_null',
        'unique',
        'is_primary',
        'is_computed',
        'is_lookup',
        'is_conditional_lookup',
        'is_pending',
        'has_error',
        'lookup_linked_field_id',
        'lookup_options',
        'table_id',
        'order',
        'version',
        'created_time',
        'deleted_time',
        'created_by',
        'last_modified_by',
    ];

    protected $attributes = [
        'provision_state' => 'ready',
    ];

    protected $casts = [
        'is_multiple_cell_value' => 'boolean',
        'provision_state' => ProvisionState::class,
        'not_null' => 'boolean',
        'unique' => 'boolean',
        'is_primary' => 'boolean',
        'is_computed' => 'boolean',
        'is_lookup' => 'boolean',
        'is_conditional_lookup' => 'boolean',
        'is_pending' => 'boolean',
        'has_error' => 'boolean',
        'order' => 'float',
        'version' => 'integer',
        'created_time' => 'datetime',
        'deleted_time' => 'datetime',
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

    public function table(): BelongsTo
    {
        return $this->belongsTo(TableMeta::class, 'table_id');
    }
}