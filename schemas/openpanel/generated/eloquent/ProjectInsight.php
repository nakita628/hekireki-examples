<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class ProjectInsight extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'lastUpdatedAt';

    protected $table = 'project_insights';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'projectId',
        'moduleKey',
        'dimensionKey',
        'windowKind',
        'state',
        'title',
        'summary',
        'displayName',
        'payload',
        'direction',
        'impactScore',
        'severityBand',
        'relevanceScore',
        'aiSummary',
        'aiCategory',
        'emailWorthy',
        'referenceWorthy',
        'enrichedAt',
        'enrichVersion',
        'version',
        'threadId',
        'windowStart',
        'windowEnd',
        'firstDetectedAt',
        'lastSeenAt',
    ];

    protected $attributes = [
        'state' => 'active',
        'displayName' => '',
        'payload' => '{}',
        'impactScore' => 0.0,
        'emailWorthy' => false,
        'referenceWorthy' => false,
        'version' => 1,
    ];

    protected $casts = [
        'state' => InsightState::class,
        'payload' => 'array',
        'impactScore' => 'float',
        'relevanceScore' => 'float',
        'emailWorthy' => 'boolean',
        'referenceWorthy' => 'boolean',
        'enrichedAt' => 'datetime',
        'enrichVersion' => 'integer',
        'version' => 'integer',
        'windowStart' => 'datetime',
        'windowEnd' => 'datetime',
        'firstDetectedAt' => 'datetime',
        'lastSeenAt' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('firstDetectedAt', $this->attributes)) {
                $this->setAttribute('firstDetectedAt', $this->freshTimestamp());
            }
            if (! array_key_exists('lastSeenAt', $this->attributes)) {
                $this->setAttribute('lastSeenAt', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function events(): HasMany
    {
        return $this->hasMany(InsightEvent::class, 'insightId');
    }
}