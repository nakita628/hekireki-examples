<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class TaskAssignment extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'TaskAssignment';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'taskId',
        'viewerId',
        'groupId',
        'linkId',
        'email',
        'notifiedAt',
        'lastReminderAt',
    ];

    protected $casts = [
        'notifiedAt' => 'datetime',
        'lastReminderAt' => 'datetime',
    ];

    public function task(): BelongsTo
    {
        return $this->belongsTo(Task::class, 'taskId');
    }

    public function viewer(): BelongsTo
    {
        return $this->belongsTo(Viewer::class, 'viewerId');
    }

    public function group(): BelongsTo
    {
        return $this->belongsTo(ViewerGroup::class, 'groupId');
    }

    public function link(): BelongsTo
    {
        return $this->belongsTo(Link::class, 'linkId');
    }
}