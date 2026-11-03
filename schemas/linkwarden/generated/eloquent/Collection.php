<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Collection extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Collection';

    protected $fillable = [
        'name',
        'description',
        'icon',
        'iconWeight',
        'color',
        'parentId',
        'isPublic',
        'ownerId',
        'createdById',
    ];

    protected $attributes = [
        'description' => '',
        'color' => '#0ea5e9',
        'isPublic' => false,
    ];

    protected $casts = [
        'parentId' => 'integer',
        'isPublic' => 'boolean',
        'ownerId' => 'integer',
        'createdById' => 'integer',
    ];

    public function parent(): BelongsTo
    {
        return $this->belongsTo(Collection::class, 'parentId');
    }

    public function owner(): BelongsTo
    {
        return $this->belongsTo(User::class, 'ownerId');
    }

    public function createdBy(): BelongsTo
    {
        return $this->belongsTo(User::class, 'createdById');
    }

    public function subCollections(): HasMany
    {
        return $this->hasMany(Collection::class, 'parentId');
    }

    public function members(): HasMany
    {
        return $this->hasMany(UsersAndCollections::class, 'collectionId');
    }

    public function links(): HasMany
    {
        return $this->hasMany(Link::class, 'collectionId');
    }

    public function rssSubscriptions(): HasMany
    {
        return $this->hasMany(RssSubscription::class, 'collectionId');
    }

    public function DashboardSection(): HasMany
    {
        return $this->hasMany(DashboardSection::class, 'collectionId');
    }
}