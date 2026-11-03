<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

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
        'userId',
        'teamId',
        'pinned',
        'parentId',
        'visibility',
        'type',
    ];

    protected $attributes = [
        'pinned' => false,
        'visibility' => 'EVERYONE',
    ];

    protected $casts = [
        'userId' => 'integer',
        'teamId' => 'integer',
        'pinned' => 'boolean',
        'visibility' => DocumentVisibility::class,
        'type' => FolderType::class,
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function parent(): BelongsTo
    {
        return $this->belongsTo(Folder::class, 'parentId');
    }

    public function envelopes(): HasMany
    {
        return $this->hasMany(Envelope::class, 'folderId');
    }

    public function subfolders(): HasMany
    {
        return $this->hasMany(Folder::class, 'parentId');
    }
}