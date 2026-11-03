<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class TeamRequest extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'updatedOn';
    const ZONED_DATES = ['createdOn', 'updatedOn'];

    protected $table = 'TeamRequest';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'collectionID',
        'teamID',
        'title',
        'request',
        'mockExamples',
        'orderIndex',
        'createdOn',
    ];

    protected $casts = [
        'request' => 'array',
        'mockExamples' => 'array',
        'orderIndex' => 'integer',
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

    public function collection(): BelongsTo
    {
        return $this->belongsTo(TeamCollection::class, 'collectionID');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamID');
    }
}