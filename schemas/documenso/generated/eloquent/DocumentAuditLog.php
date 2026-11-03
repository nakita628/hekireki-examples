<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class DocumentAuditLog extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'DocumentAuditLog';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'envelopeId',
        'type',
        'data',
        'name',
        'email',
        'userId',
        'userAgent',
        'ipAddress',
    ];

    protected $casts = [
        'data' => 'array',
        'userId' => 'integer',
    ];

    public function envelope(): BelongsTo
    {
        return $this->belongsTo(Envelope::class, 'envelopeId');
    }
}