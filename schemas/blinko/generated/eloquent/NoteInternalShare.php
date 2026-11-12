<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class NoteInternalShare extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';
    const ZONED_DATES = ['createdAt', 'updatedAt'];

    protected $table = 'noteInternalShare';

    protected $fillable = [
        'noteId',
        'accountId',
        'canEdit',
    ];

    protected $attributes = [
        'canEdit' => true,
    ];

    protected $casts = [
        'noteId' => 'integer',
        'accountId' => 'integer',
        'canEdit' => 'boolean',
    ];

    public function note(): BelongsTo
    {
        return $this->belongsTo(Notes::class, 'noteId');
    }

    public function account(): BelongsTo
    {
        return $this->belongsTo(Accounts::class, 'accountId');
    }
}