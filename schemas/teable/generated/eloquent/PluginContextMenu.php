<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class PluginContextMenu extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'last_modified_time';

    protected $table = 'plugin_context_menu';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'table_id',
        'plugin_install_id',
        'order',
        'created_time',
        'created_by',
        'last_modified_by',
    ];

    protected $casts = [
        'order' => 'float',
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

    public function table(): BelongsTo
    {
        return $this->belongsTo(TableMeta::class, 'table_id');
    }
}