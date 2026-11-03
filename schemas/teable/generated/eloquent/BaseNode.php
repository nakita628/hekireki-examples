<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class BaseNode extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'last_modified_time';

    protected $table = 'base_node';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'parent_id',
        'base_id',
        'resource_type',
        'resource_id',
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

    public function parent(): BelongsTo
    {
        return $this->belongsTo(BaseNode::class, 'parent_id');
    }

    public function children(): HasMany
    {
        return $this->hasMany(BaseNode::class, 'parent_id');
    }
}