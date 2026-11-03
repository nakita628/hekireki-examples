<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Comment extends Model
{
    use PrismaDates;

    protected $table = 'comments';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'content',
        'poll_id',
        'author_name',
        'user_id',
    ];

    public function poll(): BelongsTo
    {
        return $this->belongsTo(Poll::class, 'poll_id');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }
}