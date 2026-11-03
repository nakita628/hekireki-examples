<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class PayoutProblems extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'PayoutProblems';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'status',
        'orderId',
        'userId',
        'postId',
        'amount',
    ];

    protected $casts = [
        'amount' => 'integer',
    ];

    public function order(): BelongsTo
    {
        return $this->belongsTo(Orders::class, 'orderId');
    }

    public function post(): BelongsTo
    {
        return $this->belongsTo(Post::class, 'postId');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }
}