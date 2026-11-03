<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class UserSecurityAuditLog extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'UserSecurityAuditLog';

    protected $fillable = [
        'userId',
        'type',
        'userAgent',
        'ipAddress',
    ];

    protected $casts = [
        'userId' => 'integer',
        'type' => UserSecurityAuditLogType::class,
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }
}