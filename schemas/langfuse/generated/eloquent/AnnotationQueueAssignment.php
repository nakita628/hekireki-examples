<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class AnnotationQueueAssignment extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'annotation_queue_assignments';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'project_id',
        'user_id',
        'queue_id',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }

    public function queue(): BelongsTo
    {
        return $this->belongsTo(AnnotationQueue::class, 'queue_id');
    }
}