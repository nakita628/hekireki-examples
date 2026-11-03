<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class AnnotationQueueItem extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'annotation_queue_items';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'queue_id',
        'object_id',
        'object_type',
        'status',
        'locked_at',
        'locked_by_user_id',
        'annotator_user_id',
        'completed_at',
        'project_id',
    ];

    protected $attributes = [
        'status' => 'PENDING',
    ];

    protected $casts = [
        'object_type' => AnnotationQueueObjectType::class,
        'status' => AnnotationQueueStatus::class,
        'locked_at' => 'datetime',
        'completed_at' => 'datetime',
    ];

    public function queue(): BelongsTo
    {
        return $this->belongsTo(AnnotationQueue::class, 'queue_id');
    }

    public function lockedByUser(): BelongsTo
    {
        return $this->belongsTo(User::class, 'locked_by_user_id');
    }

    public function annotatorUser(): BelongsTo
    {
        return $this->belongsTo(User::class, 'annotator_user_id');
    }

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }
}