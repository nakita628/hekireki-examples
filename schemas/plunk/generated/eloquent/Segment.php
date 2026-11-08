<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Segment extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'segments';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'name',
        'description',
        'type',
        'condition',
        'trackMembership',
        'memberCount',
        'projectId',
    ];

    protected $attributes = [
        'type' => 'DYNAMIC',
        'trackMembership' => false,
        'memberCount' => 0,
    ];

    protected $casts = [
        'type' => SegmentType::class,
        'condition' => 'array',
        'trackMembership' => 'boolean',
        'memberCount' => 'integer',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function memberships(): HasMany
    {
        return $this->hasMany(SegmentMembership::class, 'segmentId');
    }

    public function campaigns(): HasMany
    {
        return $this->hasMany(Campaign::class, 'segmentId');
    }
}