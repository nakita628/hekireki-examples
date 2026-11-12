<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class NoteReference extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;
    const ZONED_DATES = ['createdAt'];

    protected $table = 'noteReference';

    protected $fillable = [
        'fromNoteId',
        'toNoteId',
    ];

    protected $casts = [
        'fromNoteId' => 'integer',
        'toNoteId' => 'integer',
    ];

    public function fromNote(): BelongsTo
    {
        return $this->belongsTo(Notes::class, 'fromNoteId');
    }

    public function toNote(): BelongsTo
    {
        return $this->belongsTo(Notes::class, 'toNoteId');
    }
}