<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Feedbacks extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'feedbacks';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'review',
        'message',
        'email',
        'userId',
    ];

    protected $casts = [
        'review' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }
}