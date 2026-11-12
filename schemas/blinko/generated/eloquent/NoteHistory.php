<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class NoteHistory extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;
    const ZONED_DATES = ['createdAt'];

    protected $table = 'noteHistory';

    protected $fillable = [
        'noteId',
        'content',
        'metadata',
        'version',
        'accountId',
    ];

    protected $casts = [
        'noteId' => 'integer',
        'metadata' => 'array',
        'version' => 'integer',
        'accountId' => 'integer',
    ];

    public function note(): BelongsTo
    {
        return $this->belongsTo(Notes::class, 'noteId');
    }
}