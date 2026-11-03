<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class MockServer extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'updatedOn';
    const ZONED_DATES = ['lastHitAt', 'createdOn', 'updatedOn', 'deletedAt'];

    protected $table = 'MockServer';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'subdomain',
        'creatorUid',
        'collectionID',
        'workspaceType',
        'workspaceID',
        'delayInMs',
        'isPublic',
        'isActive',
        'hitCount',
        'lastHitAt',
        'createdOn',
        'deletedAt',
    ];

    protected $attributes = [
        'delayInMs' => 0,
        'isPublic' => true,
        'isActive' => true,
        'hitCount' => 0,
    ];

    protected $casts = [
        'workspaceType' => WorkspaceType::class,
        'delayInMs' => 'integer',
        'isPublic' => 'boolean',
        'isActive' => 'boolean',
        'hitCount' => 'integer',
        'lastHitAt' => 'datetime',
        'createdOn' => 'datetime',
        'deletedAt' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('createdOn', $this->attributes)) {
                $this->setAttribute('createdOn', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'creatorUid');
    }

    public function requestLogs(): HasMany
    {
        return $this->hasMany(MockServerLog::class, 'mockServerID');
    }

    public function activityHistory(): HasMany
    {
        return $this->hasMany(MockServerActivity::class, 'mockServerID');
    }
}