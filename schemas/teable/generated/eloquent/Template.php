<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Template extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'last_modified_time';

    protected $table = 'template';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'base_id',
        'cover',
        'name',
        'description',
        'markdown_description',
        'category_id',
        'created_time',
        'created_by',
        'last_modified_by',
        'is_system',
        'is_published',
        'featured',
        'snapshot',
        'order',
        'usage_count',
        'publish_info',
        'visit_count',
        'kind',
        'version',
        'manifest',
        'review',
        'history',
        'parent_template_id',
        'slug',
    ];

    protected $attributes = [
        'usage_count' => 0,
        'visit_count' => 0,
        'version' => 1,
    ];

    protected $casts = [
        'created_time' => 'datetime',
        'is_system' => 'boolean',
        'is_published' => 'boolean',
        'featured' => 'boolean',
        'order' => 'float',
        'usage_count' => 'integer',
        'publish_info' => 'array',
        'visit_count' => 'integer',
        'version' => 'integer',
        'manifest' => 'array',
        'review' => 'array',
        'history' => 'array',
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