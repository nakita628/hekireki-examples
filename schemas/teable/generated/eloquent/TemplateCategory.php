<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class TemplateCategory extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'last_modified_time';

    protected $table = 'template_category';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'created_time',
        'created_by',
        'last_modified_by',
        'order',
    ];

    protected $casts = [
        'created_time' => 'datetime',
        'order' => 'float',
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