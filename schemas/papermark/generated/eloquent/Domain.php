<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Domain extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Domain';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'slug',
        'userId',
        'teamId',
        'verified',
        'isDefault',
        'redirectUrl',
        'lastChecked',
    ];

    protected $attributes = [
        'verified' => false,
        'isDefault' => false,
    ];

    protected $casts = [
        'verified' => 'boolean',
        'isDefault' => 'boolean',
        'lastChecked' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('lastChecked', $this->attributes)) {
                $this->setAttribute('lastChecked', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function Team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function links(): HasMany
    {
        return $this->hasMany(Link::class, 'domainId');
    }
}