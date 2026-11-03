<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class PluginInstall extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'last_modified_time';

    protected $table = 'plugin_install';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'plugin_id',
        'base_id',
        'name',
        'position_id',
        'position',
        'storage',
        'created_time',
        'created_by',
        'last_modified_by',
    ];

    protected $casts = [
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

    public function plugin(): BelongsTo
    {
        return $this->belongsTo(Plugin::class, 'plugin_id');
    }
}