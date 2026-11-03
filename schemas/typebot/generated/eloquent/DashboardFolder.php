<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class DashboardFolder extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'DashboardFolder';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'parentFolderId',
        'workspaceId',
    ];

    public function parentFolder(): BelongsTo
    {
        return $this->belongsTo(DashboardFolder::class, 'parentFolderId');
    }

    public function workspace(): BelongsTo
    {
        return $this->belongsTo(Workspace::class, 'workspaceId');
    }

    public function childrenFolder(): HasMany
    {
        return $this->hasMany(DashboardFolder::class, 'parentFolderId');
    }

    public function typebots(): HasMany
    {
        return $this->hasMany(Typebot::class, 'folderId');
    }
}