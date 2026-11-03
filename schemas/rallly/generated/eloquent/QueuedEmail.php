<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class QueuedEmail extends Model
{
    use PrismaDates;

    protected $table = 'queued_emails';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'kind',
        'subject_id',
        'batch_id',
        'user_id',
        'status',
        'send_after',
        'attempts',
        'claimed_at',
        'sent_at',
        'last_error',
    ];

    protected $attributes = [
        'status' => 'pending',
        'attempts' => 0,
    ];

    protected $casts = [
        'status' => QueuedEmailStatus::class,
        'send_after' => 'datetime',
        'attempts' => 'integer',
        'claimed_at' => 'datetime',
        'sent_at' => 'datetime',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }
}