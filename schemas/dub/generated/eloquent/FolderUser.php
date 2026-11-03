<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class FolderUser extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'FolderUser';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'folderId',
        'userId',
        'role',
    ];

    protected $casts = [
        'role' => FolderUserRole::class,
    ];

    public function folder(): BelongsTo
    {
        return $this->belongsTo(Folder::class, 'folderId');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }
}