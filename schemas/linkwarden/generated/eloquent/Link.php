<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Link extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Link';

    protected $fillable = [
        'name',
        'type',
        'description',
        'createdById',
        'collectionId',
        'collectionOrder',
        'icon',
        'iconWeight',
        'color',
        'url',
        'textContent',
        'preview',
        'image',
        'pdf',
        'readable',
        'monolith',
        'clientSide',
        'aiTagged',
        'metaDescription',
        'indexVersion',
        'lastPreserved',
        'importDate',
    ];

    protected $attributes = [
        'name' => '',
        'type' => 'url',
        'description' => '',
        'clientSide' => false,
        'aiTagged' => false,
    ];

    protected $casts = [
        'createdById' => 'integer',
        'collectionId' => 'integer',
        'collectionOrder' => 'float',
        'clientSide' => 'boolean',
        'aiTagged' => 'boolean',
        'indexVersion' => 'integer',
        'lastPreserved' => 'datetime',
        'importDate' => 'datetime',
    ];

    public function createdBy(): BelongsTo
    {
        return $this->belongsTo(User::class, 'createdById');
    }

    public function collection(): BelongsTo
    {
        return $this->belongsTo(Collection::class, 'collectionId');
    }

    public function highlight(): HasMany
    {
        return $this->hasMany(Highlight::class, 'linkId');
    }

    public function pinnedBy(): BelongsToMany
    {
        return $this->belongsToMany(User::class, '_PinnedLinks', 'A', 'B');
    }

    public function tags(): BelongsToMany
    {
        return $this->belongsToMany(Tag::class, '_LinkToTag', 'A', 'B');
    }
}