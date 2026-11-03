<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Customer extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Customer';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'name',
        'orgId',
        'deletedAt',
    ];

    protected $casts = [
        'deletedAt' => 'datetime',
    ];

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'orgId');
    }

    public function integrations(): HasMany
    {
        return $this->hasMany(Integration::class, 'customerId');
    }
}