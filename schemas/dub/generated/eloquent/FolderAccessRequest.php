<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class FolderAccessRequest extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'FolderAccessRequest';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'folderId',
        'userId',
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