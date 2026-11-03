<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class UserFilterSegmentPreference extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'UserFilterSegmentPreference';

    protected $fillable = [
        'userId',
        'tableIdentifier',
        'segmentId',
        'systemSegmentId',
    ];

    protected $casts = [
        'userId' => 'integer',
        'segmentId' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function segment(): BelongsTo
    {
        return $this->belongsTo(FilterSegment::class, 'segmentId');
    }
}