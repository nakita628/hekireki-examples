<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ContactFormMessage extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'ContactFormMessage';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'userId',
        'content',
        'isRead',
        'repliedAt',
    ];

    protected $attributes = [
        'isRead' => false,
    ];

    protected $casts = [
        'isRead' => 'boolean',
        'repliedAt' => 'datetime',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }
}