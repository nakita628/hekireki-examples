<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class Folder extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Folder';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'description',
        'projectId',
        'type',
        'accessLevel',
    ];

    protected $attributes = [
        'type' => 'default',
    ];

    protected $casts = [
        'type' => FolderType::class,
        'accessLevel' => FolderAccessLevel::class,
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function dashboard(): HasOne
    {
        return $this->hasOne(Dashboard::class, 'folderId');
    }

    public function links(): HasMany
    {
        return $this->hasMany(Link::class, 'folderId');
    }

    public function users(): HasMany
    {
        return $this->hasMany(FolderUser::class, 'folderId');
    }

    public function accessRequests(): HasMany
    {
        return $this->hasMany(FolderAccessRequest::class, 'folderId');
    }

    public function webhooks(): HasMany
    {
        return $this->hasMany(FolderWebhook::class, 'folderId');
    }
}