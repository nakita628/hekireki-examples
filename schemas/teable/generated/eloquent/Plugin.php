<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Plugin extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'last_modified_time';

    protected $table = 'plugin';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'description',
        'detail_desc',
        'logo',
        'help_url',
        'status',
        'positions',
        'url',
        'secret',
        'masked_secret',
        'i18n',
        'config',
        'plugin_user',
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

    public function pluginInstall(): HasMany
    {
        return $this->hasMany(PluginInstall::class, 'plugin_id');
    }
}