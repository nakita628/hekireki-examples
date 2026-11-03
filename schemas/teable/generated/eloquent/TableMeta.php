<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class TableMeta extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'last_modified_time';

    protected $table = 'table_meta';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'base_id',
        'name',
        'description',
        'icon',
        'db_table_name',
        'db_view_name',
        'search_index',
        'search_index_policy',
        'query_index_policy',
        'provision_state',
        'version',
        'order',
        'created_time',
        'deleted_time',
        'created_by',
        'last_modified_by',
    ];

    protected $attributes = [
        'provision_state' => 'ready',
    ];

    protected $casts = [
        'search_index' => 'array',
        'search_index_policy' => 'array',
        'query_index_policy' => 'array',
        'provision_state' => ProvisionState::class,
        'version' => 'integer',
        'order' => 'float',
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

    public function base(): BelongsTo
    {
        return $this->belongsTo(Base::class, 'base_id');
    }

    public function fields(): HasMany
    {
        return $this->hasMany(Field::class, 'table_id');
    }

    public function views(): HasMany
    {
        return $this->hasMany(View::class, 'table_id');
    }

    public function pluginPanel(): HasMany
    {
        return $this->hasMany(PluginPanel::class, 'table_id');
    }

    public function pluginContextMenu(): HasMany
    {
        return $this->hasMany(PluginContextMenu::class, 'table_id');
    }
}