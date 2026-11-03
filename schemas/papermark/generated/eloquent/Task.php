<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Task extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Task';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'title',
        'description',
        'type',
        'status',
        'category',
        'dueDate',
        'orderIndex',
        'taskListId',
        'dataroomId',
        'teamId',
        'uploadFolderId',
        'createdByUserId',
        'lastActivityNotifiedAt',
    ];

    protected $attributes = [
        'type' => 'TODO',
        'status' => 'OPEN',
    ];

    protected $casts = [
        'dueDate' => 'datetime',
        'orderIndex' => 'integer',
        'lastActivityNotifiedAt' => 'datetime',
    ];

    public function taskList(): BelongsTo
    {
        return $this->belongsTo(TaskList::class, 'taskListId');
    }

    public function dataroom(): BelongsTo
    {
        return $this->belongsTo(Dataroom::class, 'dataroomId');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function uploadFolder(): BelongsTo
    {
        return $this->belongsTo(DataroomFolder::class, 'uploadFolderId');
    }

    public function createdByUser(): BelongsTo
    {
        return $this->belongsTo(User::class, 'createdByUserId');
    }

    public function assignments(): HasMany
    {
        return $this->hasMany(TaskAssignment::class, 'taskId');
    }

    public function activities(): HasMany
    {
        return $this->hasMany(TaskActivity::class, 'taskId');
    }

    public function uploads(): HasMany
    {
        return $this->hasMany(DocumentUpload::class, 'taskId');
    }
}