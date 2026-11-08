<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class License extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'licenses';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'key',
        'userId',
        'validFrom',
        'validUntil',
        'maxUsers',
        'features',
        'activatedAt',
        'lastCheckedAt',
    ];

    protected $attributes = [
        'maxUsers' => 1,
    ];

    protected $casts = [
        'validFrom' => 'datetime',
        'validUntil' => 'datetime',
        'maxUsers' => 'integer',
        'features' => 'array',
        'activatedAt' => 'datetime',
        'lastCheckedAt' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('validFrom', $this->attributes)) {
                $this->setAttribute('validFrom', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }
}