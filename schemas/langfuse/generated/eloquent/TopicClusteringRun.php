<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class TopicClusteringRun extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    const UPDATED_AT = null;
    const ZONED_DATES = ['created_at', 'started_at', 'finished_at'];

    protected $table = 'topic_clustering_runs';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'project_id',
        'facet_id',
        'facet_version',
        'status',
        'config',
        'error',
        'started_at',
        'finished_at',
        'topic_version_ids',
        'rule_id',
        'batch_action_id',
    ];

    protected $attributes = [
        'status' => 'pending',
        'config' => '{}',
    ];

    protected $casts = [
        'facet_version' => 'integer',
        'config' => 'array',
        'started_at' => 'datetime',
        'finished_at' => 'datetime',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }
}