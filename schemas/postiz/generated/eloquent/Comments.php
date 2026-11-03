<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Comments extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Comments';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'content',
        'organizationId',
        'postId',
        'userId',
        'displayName',
        'parentId',
        'anchorStart',
        'anchorEnd',
        'anchorQuote',
        'resolvedAt',
        'deletedAt',
    ];

    protected $casts = [
        'anchorStart' => 'integer',
        'anchorEnd' => 'integer',
        'resolvedAt' => 'datetime',
        'deletedAt' => 'datetime',
    ];

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organizationId');
    }

    public function post(): BelongsTo
    {
        return $this->belongsTo(Post::class, 'postId');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
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