<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class PlaygroundConversation extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'PlaygroundConversation';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'chatId',
        'title',
        'agentSlug',
        'runId',
        'clientData',
        'messages',
        'lastEventId',
        'projectId',
        'runtimeEnvironmentId',
        'userId',
    ];

    protected $attributes = [
        'title' => 'New conversation',
    ];

    protected $casts = [
        'clientData' => 'array',
        'messages' => 'array',
    ];

    public function run(): BelongsTo
    {
        return $this->belongsTo(TaskRun::class, 'runId');
    }

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function runtimeEnvironment(): BelongsTo
    {
        return $this->belongsTo(RuntimeEnvironment::class, 'runtimeEnvironmentId');
    }
}