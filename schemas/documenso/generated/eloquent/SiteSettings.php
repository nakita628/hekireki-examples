<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class SiteSettings extends Model
{
    use PrismaDates;

    protected $table = 'SiteSettings';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'enabled',
        'data',
        'lastModifiedByUserId',
        'lastModifiedAt',
    ];

    protected $attributes = [
        'enabled' => false,
    ];

    protected $casts = [
        'enabled' => 'boolean',
        'data' => 'array',
        'lastModifiedByUserId' => 'integer',
        'lastModifiedAt' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('lastModifiedAt', $this->attributes)) {
                $this->setAttribute('lastModifiedAt', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function lastModifiedByUser(): BelongsTo
    {
        return $this->belongsTo(User::class, 'lastModifiedByUserId');
    }
}