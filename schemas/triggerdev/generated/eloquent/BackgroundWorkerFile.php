<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;
use Illuminate\Database\Eloquent\Relations\HasMany;

class BackgroundWorkerFile extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'BackgroundWorkerFile';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'friendlyId',
        'filePath',
        'contentHash',
        'contents',
        'projectId',
    ];

    protected $casts = [
        'contents' => AsBytes::class,
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function tasks(): HasMany
    {
        return $this->hasMany(BackgroundWorkerTask::class, 'fileId');
    }

    public function backgroundWorkers(): BelongsToMany
    {
        return $this->belongsToMany(BackgroundWorker::class, '_BackgroundWorkerToBackgroundWorkerFile', 'B', 'A');
    }
}