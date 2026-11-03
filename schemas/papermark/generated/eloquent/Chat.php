<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Chat extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Chat';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'title',
        'teamId',
        'documentId',
        'dataroomId',
        'linkId',
        'viewId',
        'userId',
        'viewerId',
        'vectorStoreId',
        'lastMessageAt',
    ];

    protected $casts = [
        'lastMessageAt' => 'datetime',
    ];

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function document(): BelongsTo
    {
        return $this->belongsTo(Document::class, 'documentId');
    }

    public function dataroom(): BelongsTo
    {
        return $this->belongsTo(Dataroom::class, 'dataroomId');
    }

    public function link(): BelongsTo
    {
        return $this->belongsTo(Link::class, 'linkId');
    }

    public function view(): BelongsTo
    {
        return $this->belongsTo(View::class, 'viewId');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function viewer(): BelongsTo
    {
        return $this->belongsTo(Viewer::class, 'viewerId');
    }

    public function messages(): HasMany
    {
        return $this->hasMany(ChatMessage::class, 'chatId');
    }
}