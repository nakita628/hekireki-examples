<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class UserOrganization extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'UserOrganization';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'userId',
        'organizationId',
        'disabled',
        'role',
    ];

    protected $attributes = [
        'disabled' => false,
        'role' => 'USER',
    ];

    protected $casts = [
        'disabled' => 'boolean',
        'role' => Role::class,
    ];

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organizationId');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }
}