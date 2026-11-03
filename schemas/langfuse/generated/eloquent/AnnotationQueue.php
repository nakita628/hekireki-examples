<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class AnnotationQueue extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'annotation_queues';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'description',
        'score_config_ids',
        'project_id',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }

    public function annotationQueueItem(): HasMany
    {
        return $this->hasMany(AnnotationQueueItem::class, 'queue_id');
    }

    public function annotationQueueAssignment(): HasMany
    {
        return $this->hasMany(AnnotationQueueAssignment::class, 'queue_id');
    }
}