<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ThrottleClaim extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'throttle_claims';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'projectId',
        'key',
        'recipientKey',
        'contactId',
        'emailId',
        'workflowId',
        'windowSeconds',
        'expiresAt',
    ];

    protected $casts = [
        'windowSeconds' => 'integer',
        'expiresAt' => 'datetime',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function contact(): BelongsTo
    {
        return $this->belongsTo(Contact::class, 'contactId');
    }

    public function workflow(): BelongsTo
    {
        return $this->belongsTo(Workflow::class, 'workflowId');
    }
}