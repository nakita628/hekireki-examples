<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class TeamCollection extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'updatedOn';
    const ZONED_DATES = ['createdOn', 'updatedOn'];

    protected $table = 'TeamCollection';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'parentID',
        'teamID',
        'title',
        'orderIndex',
        'data',
        'createdOn',
    ];

    protected $casts = [
        'orderIndex' => 'integer',
        'data' => 'array',
        'createdOn' => 'datetime',
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

    public function parent(): BelongsTo
    {
        return $this->belongsTo(TeamCollection::class, 'parentID');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamID');
    }

    public function children(): HasMany
    {
        return $this->hasMany(TeamCollection::class, 'parentID');
    }

    public function requests(): HasMany
    {
        return $this->hasMany(TeamRequest::class, 'collectionID');
    }
}