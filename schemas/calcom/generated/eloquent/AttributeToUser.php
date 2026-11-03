<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class AttributeToUser extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'AttributeToUser';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'memberId',
        'attributeOptionId',
        'weight',
        'createdById',
        'createdByDSyncId',
        'updatedById',
        'updatedByDSyncId',
    ];

    protected $casts = [
        'memberId' => 'integer',
        'weight' => 'integer',
        'createdById' => 'integer',
        'updatedById' => 'integer',
    ];

    public function member(): BelongsTo
    {
        return $this->belongsTo(Membership::class, 'memberId');
    }

    public function attributeOption(): BelongsTo
    {
        return $this->belongsTo(AttributeOption::class, 'attributeOptionId');
    }

    public function createdBy(): BelongsTo
    {
        return $this->belongsTo(User::class, 'createdById');
    }

    public function createdByDSync(): BelongsTo
    {
        return $this->belongsTo(DSyncData::class, 'createdByDSyncId', 'directoryId');
    }

    public function updatedBy(): BelongsTo
    {
        return $this->belongsTo(User::class, 'updatedById');
    }

    public function updatedByDSync(): BelongsTo
    {
        return $this->belongsTo(DSyncData::class, 'updatedByDSyncId', 'directoryId');
    }
}