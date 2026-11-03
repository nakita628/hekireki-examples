<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class TaskRunWaitpoint extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'TaskRunWaitpoint';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'taskRunId',
        'waitpointId',
        'projectId',
        'spanIdToComplete',
        'batchId',
        'batchIndex',
    ];

    protected $casts = [
        'batchIndex' => 'integer',
    ];

    public function taskRun(): BelongsTo
    {
        return $this->belongsTo(TaskRun::class, 'taskRunId');
    }

    public function waitpoint(): BelongsTo
    {
        return $this->belongsTo(Waitpoint::class, 'waitpointId');
    }

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function batch(): BelongsTo
    {
        return $this->belongsTo(BatchTaskRun::class, 'batchId');
    }
}