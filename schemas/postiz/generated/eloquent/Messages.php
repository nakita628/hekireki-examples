<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Messages extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Messages';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'from',
        'content',
        'groupId',
        'special',
        'deletedAt',
    ];

    protected $casts = [
        'from' => From::class,
        'deletedAt' => 'datetime',
    ];

    public function group(): BelongsTo
    {
        return $this->belongsTo(MessagesGroup::class, 'groupId');
    }

    public function posts(): HasMany
    {
        return $this->hasMany(Post::class, 'lastMessageId');
    }
}