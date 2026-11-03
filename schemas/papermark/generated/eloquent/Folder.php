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
        'path',
        'parentId',
        'hiddenInAllDocuments',
        'icon',
        'color',
        'teamId',
    ];

    protected $attributes = [
        'hiddenInAllDocuments' => false,
    ];

    protected $casts = [
        'hiddenInAllDocuments' => 'boolean',
    ];

    public function parentFolder(): BelongsTo
    {
        return $this->belongsTo(Folder::class, 'parentId');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function documents(): HasMany
    {
        return $this->hasMany(Document::class, 'folderId');
    }

    public function childFolders(): HasMany
    {
        return $this->hasMany(Folder::class, 'parentId');
    }
}