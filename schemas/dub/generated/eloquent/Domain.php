<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

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
        'verified',
        'placeholder',
        'expiredUrl',
        'notFoundUrl',
        'primary',
        'archived',
        'lastChecked',
        'logo',
        'appleAppSiteAssociation',
        'assetLinks',
        'deepviewData',
        'linkRetentionDays',
        'projectId',
    ];

    protected $attributes = [
        'verified' => false,
        'primary' => false,
        'archived' => false,
        'deepviewData' => '{}',
    ];

    protected $casts = [
        'verified' => 'boolean',
        'primary' => 'boolean',
        'archived' => 'boolean',
        'lastChecked' => 'datetime',
        'appleAppSiteAssociation' => 'array',
        'assetLinks' => 'array',
        'deepviewData' => 'array',
        'linkRetentionDays' => 'integer',
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

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function registeredDomain(): HasOne
    {
        return $this->hasOne(RegisteredDomain::class, 'domainId');
    }

    public function links(): HasMany
    {
        return $this->hasMany(Link::class, 'domain', 'slug');
    }
}