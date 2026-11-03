<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ClaimableCustomPlan extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'ClaimableCustomPlan';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'claimedAt',
        'name',
        'description',
        'price',
        'currency',
        'workspaceId',
        'chatsLimit',
        'storageLimit',
        'seatsLimit',
        'isYearly',
        'companyName',
        'vatType',
        'vatValue',
    ];

    protected $attributes = [
        'isYearly' => false,
    ];

    protected $casts = [
        'claimedAt' => 'datetime',
        'price' => 'integer',
        'chatsLimit' => 'integer',
        'storageLimit' => 'integer',
        'seatsLimit' => 'integer',
        'isYearly' => 'boolean',
    ];

    public function workspace(): BelongsTo
    {
        return $this->belongsTo(Workspace::class, 'workspaceId');
    }
}