<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Errors extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Errors';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'message',
        'platform',
        'organizationId',
        'postId',
        'body',
    ];

    protected $attributes = [
        'body' => '{}',
    ];

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organizationId');
    }

    public function post(): BelongsTo
    {
        return $this->belongsTo(Post::class, 'postId');
    }
}