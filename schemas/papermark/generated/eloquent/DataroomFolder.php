<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class DataroomFolder extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'DataroomFolder';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'path',
        'parentId',
        'icon',
        'color',
        'dataroomId',
        'orderIndex',
        'hierarchicalIndex',
    ];

    protected $casts = [
        'orderIndex' => 'integer',
    ];

    public function parentFolder(): BelongsTo
    {
        return $this->belongsTo(DataroomFolder::class, 'parentId');
    }

    public function dataroom(): BelongsTo
    {
        return $this->belongsTo(Dataroom::class, 'dataroomId');
    }

    public function documents(): HasMany
    {
        return $this->hasMany(DataroomDocument::class, 'folderId');
    }

    public function childFolders(): HasMany
    {
        return $this->hasMany(DataroomFolder::class, 'parentId');
    }

    public function uploadTasks(): HasMany
    {
        return $this->hasMany(Task::class, 'uploadFolderId');
    }
}