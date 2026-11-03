<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class RssSubscription extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'RssSubscription';

    protected $fillable = [
        'url',
        'name',
        'lastBuildDate',
        'collectionId',
        'ownerId',
    ];

    protected $casts = [
        'lastBuildDate' => 'datetime',
        'collectionId' => 'integer',
        'ownerId' => 'integer',
    ];

    public function collection(): BelongsTo
    {
        return $this->belongsTo(Collection::class, 'collectionId');
    }
}