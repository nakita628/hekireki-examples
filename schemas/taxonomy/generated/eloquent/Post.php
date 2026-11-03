<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Post extends Model
{
    use PrismaDates;

    protected $table = 'posts';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'title',
        'content',
        'published',
        'authorId',
    ];

    protected $attributes = [
        'published' => false,
    ];

    protected $casts = [
        'content' => 'array',
        'published' => 'boolean',
    ];

    public function author(): BelongsTo
    {
        return $this->belongsTo(User::class, 'authorId');
    }
}