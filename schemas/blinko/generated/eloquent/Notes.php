<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Notes extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';
    const ZONED_DATES = ['shareExpiryDate', 'createdAt', 'updatedAt'];

    protected $table = 'notes';

    protected $fillable = [
        'type',
        'content',
        'isArchived',
        'isRecycle',
        'isShare',
        'isTop',
        'isReviewed',
        'sharePassword',
        'shareEncryptedUrl',
        'shareExpiryDate',
        'shareMaxView',
        'shareViewCount',
        'metadata',
        'accountId',
        'sortOrder',
    ];

    protected $attributes = [
        'type' => 0,
        'content' => '',
        'isArchived' => false,
        'isRecycle' => false,
        'isShare' => false,
        'isTop' => false,
        'isReviewed' => false,
        'sharePassword' => '',
        'shareMaxView' => 0,
        'shareViewCount' => 0,
        'sortOrder' => 0,
    ];

    protected $casts = [
        'type' => 'integer',
        'isArchived' => 'boolean',
        'isRecycle' => 'boolean',
        'isShare' => 'boolean',
        'isTop' => 'boolean',
        'isReviewed' => 'boolean',
        'shareExpiryDate' => 'datetime',
        'shareMaxView' => 'integer',
        'shareViewCount' => 'integer',
        'metadata' => 'array',
        'accountId' => 'integer',
        'sortOrder' => 'integer',
    ];

    public function account(): BelongsTo
    {
        return $this->belongsTo(Accounts::class, 'accountId');
    }

    public function attachments(): HasMany
    {
        return $this->hasMany(Attachments::class, 'noteId');
    }

    public function tags(): HasMany
    {
        return $this->hasMany(TagsToNote::class, 'noteId');
    }

    public function referencedBy(): HasMany
    {
        return $this->hasMany(NoteReference::class, 'toNoteId');
    }

    public function references(): HasMany
    {
        return $this->hasMany(NoteReference::class, 'fromNoteId');
    }

    public function comments(): HasMany
    {
        return $this->hasMany(Comments::class, 'noteId');
    }

    public function histories(): HasMany
    {
        return $this->hasMany(NoteHistory::class, 'noteId');
    }

    public function internalShares(): HasMany
    {
        return $this->hasMany(NoteInternalShare::class, 'noteId');
    }
}