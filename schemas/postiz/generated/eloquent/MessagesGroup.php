<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class MessagesGroup extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'MessagesGroup';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'buyerOrganizationId',
        'buyerId',
        'sellerId',
    ];

    public function buyer(): BelongsTo
    {
        return $this->belongsTo(User::class, 'buyerId');
    }

    public function buyerOrganization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'buyerOrganizationId');
    }

    public function seller(): BelongsTo
    {
        return $this->belongsTo(User::class, 'sellerId');
    }

    public function messages(): HasMany
    {
        return $this->hasMany(Messages::class, 'groupId');
    }

    public function orders(): HasMany
    {
        return $this->hasMany(Orders::class, 'messageGroupId');
    }
}