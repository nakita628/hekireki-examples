<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Comment extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'comments';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'project_id',
        'object_type',
        'object_id',
        'content',
        'author_user_id',
        'data_field',
        'path',
        'range_start',
        'range_end',
    ];

    protected $casts = [
        'object_type' => CommentObjectType::class,
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }

    public function reactions(): HasMany
    {
        return $this->hasMany(CommentReaction::class, 'comment_id');
    }
}