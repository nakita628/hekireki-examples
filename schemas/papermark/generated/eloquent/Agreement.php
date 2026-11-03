<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Agreement extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Agreement';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'content',
        'contentType',
        'signingProvider',
        'signingExternalId',
        'signingEnvelopeId',
        'signingTemplateId',
        'requireName',
        'teamId',
        'deletedAt',
        'deletedBy',
    ];

    protected $attributes = [
        'contentType' => 'LINK',
        'signingProvider' => 'LEGACY',
        'requireName' => true,
    ];

    protected $casts = [
        'requireName' => 'boolean',
        'deletedAt' => 'datetime',
    ];

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function links(): HasMany
    {
        return $this->hasMany(Link::class, 'agreementId');
    }

    public function responses(): HasMany
    {
        return $this->hasMany(AgreementResponse::class, 'agreementId');
    }
}