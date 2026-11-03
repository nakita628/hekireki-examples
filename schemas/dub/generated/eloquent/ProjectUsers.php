<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasOne;

class ProjectUsers extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'ProjectUsers';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'role',
        'userId',
        'projectId',
        'workspacePreferences',
        'defaultFolderId',
    ];

    protected $attributes = [
        'role' => 'member',
    ];

    protected $casts = [
        'role' => WorkspaceRole::class,
        'workspacePreferences' => 'array',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function notificationPreference(): HasOne
    {
        return $this->hasOne(NotificationPreference::class, 'projectUserId');
    }
}