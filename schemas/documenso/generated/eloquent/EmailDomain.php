<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class EmailDomain extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'EmailDomain';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'status',
        'selector',
        'domain',
        'publicKey',
        'privateKey',
        'lastVerifiedAt',
        'organisationId',
    ];

    protected $attributes = [
        'status' => 'PENDING',
    ];

    protected $casts = [
        'status' => EmailDomainStatus::class,
        'lastVerifiedAt' => 'datetime',
    ];

    public function organisation(): BelongsTo
    {
        return $this->belongsTo(Organisation::class, 'organisationId');
    }

    public function emails(): HasMany
    {
        return $this->hasMany(OrganisationEmail::class, 'emailDomainId');
    }
}