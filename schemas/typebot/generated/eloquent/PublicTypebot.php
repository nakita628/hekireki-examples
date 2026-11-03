<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class PublicTypebot extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'PublicTypebot';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'version',
        'lastActivityAt',
        'typebotId',
        'groups',
        'events',
        'variables',
        'edges',
        'theme',
        'settings',
    ];

    protected $casts = [
        'lastActivityAt' => 'datetime',
        'groups' => 'array',
        'events' => 'array',
        'variables' => 'array',
        'edges' => 'array',
        'theme' => 'array',
        'settings' => 'array',
    ];

    public function typebot(): BelongsTo
    {
        return $this->belongsTo(Typebot::class, 'typebotId');
    }

    public function mediaIdsCache(): HasMany
    {
        return $this->hasMany(RuntimeMediaIdCache::class, 'publicTypebotId');
    }
}