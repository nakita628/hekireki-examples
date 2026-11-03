<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class BaseShare extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'last_modified_time';

    protected $table = 'base_share';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'base_id',
        'share_id',
        'password',
        'node_id',
        'allow_save',
        'allow_copy',
        'allow_edit',
        'enabled',
        'created_time',
        'created_by',
    ];

    protected $attributes = [
        'enabled' => true,
    ];

    protected $casts = [
        'allow_save' => 'boolean',
        'allow_copy' => 'boolean',
        'allow_edit' => 'boolean',
        'enabled' => 'boolean',
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
}