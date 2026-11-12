<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Comments extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';
    const ZONED_DATES = ['createdAt', 'updatedAt'];

    protected $table = 'comments';

    protected $fillable = [
        'content',
        'accountId',
        'guestName',
        'guestIP',
        'guestUA',
        'noteId',
        'parentId',
    ];

    protected $casts = [
        'accountId' => 'integer',
        'noteId' => 'integer',
        'parentId' => 'integer',
    ];

    public function note(): BelongsTo
    {
        return $this->belongsTo(Notes::class, 'noteId');
    }

    public function account(): BelongsTo
    {
        return $this->belongsTo(Accounts::class, 'accountId');
    }

    public function parent(): BelongsTo
    {
        return $this->belongsTo(Comments::class, 'parentId');
    }

    public function replies(): HasMany
    {
        return $this->hasMany(Comments::class, 'parentId');
    }
}