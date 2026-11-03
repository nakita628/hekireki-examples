<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class View extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'last_modified_time';

    protected $table = 'view';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'description',
        'table_id',
        'type',
        'sort',
        'filter',
        'group',
        'options',
        'order',
        'version',
        'column_meta',
        'is_locked',
        'enable_share',
        'share_id',
        'share_meta',
        'created_time',
        'deleted_time',
        'created_by',
        'last_modified_by',
    ];

    protected $casts = [
        'order' => 'float',
        'version' => 'integer',
        'is_locked' => 'boolean',
        'enable_share' => 'boolean',
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