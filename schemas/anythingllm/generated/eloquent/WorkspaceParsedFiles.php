<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class WorkspaceParsedFiles extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'workspace_parsed_files';

    protected $fillable = [
        'filename',
        'workspaceId',
        'userId',
        'threadId',
        'metadata',
        'tokenCountEstimate',
    ];

    protected $attributes = [
        'tokenCountEstimate' => 0,
    ];

    protected $casts = [
        'workspaceId' => 'integer',
        'userId' => 'integer',
        'threadId' => 'integer',
        'tokenCountEstimate' => 'integer',
    ];

    public function workspace(): BelongsTo
    {
        return $this->belongsTo(Workspaces::class, 'workspaceId');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(Users::class, 'userId');
    }

    public function thread(): BelongsTo
    {
        return $this->belongsTo(WorkspaceThreads::class, 'threadId');
    }
}