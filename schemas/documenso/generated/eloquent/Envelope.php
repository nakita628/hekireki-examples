<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class Envelope extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Envelope';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'secondaryId',
        'externalId',
        'type',
        'completedAt',
        'deletedAt',
        'title',
        'status',
        'source',
        'qrToken',
        'signatureLevel',
        'internalVersion',
        'useLegacyFieldInsertion',
        'authOptions',
        'formValues',
        'visibility',
        'templateType',
        'publicTitle',
        'publicDescription',
        'templateId',
        'userId',
        'teamId',
        'folderId',
        'documentMetaId',
    ];

    protected $attributes = [
        'status' => 'DRAFT',
        'useLegacyFieldInsertion' => false,
        'visibility' => 'EVERYONE',
        'templateType' => 'PRIVATE',
        'publicTitle' => '',
        'publicDescription' => '',
    ];

    protected $casts = [
        'type' => EnvelopeType::class,
        'completedAt' => 'datetime',
        'deletedAt' => 'datetime',
        'status' => DocumentStatus::class,
        'source' => DocumentSource::class,
        'internalVersion' => 'integer',
        'useLegacyFieldInsertion' => 'boolean',
        'authOptions' => 'array',
        'formValues' => 'array',
        'visibility' => DocumentVisibility::class,
        'templateType' => TemplateType::class,
        'templateId' => 'integer',
        'userId' => 'integer',
        'teamId' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function folder(): BelongsTo
    {
        return $this->belongsTo(Folder::class, 'folderId');
    }

    public function documentMeta(): BelongsTo
    {
        return $this->belongsTo(DocumentMeta::class, 'documentMetaId');
    }

    public function directLink(): HasOne
    {
        return $this->hasOne(TemplateDirectLink::class, 'envelopeId');
    }

    public function envelopeItems(): HasMany
    {
        return $this->hasMany(EnvelopeItem::class, 'envelopeId');
    }

    public function recipients(): HasMany
    {
        return $this->hasMany(Recipient::class, 'envelopeId');
    }

    public function fields(): HasMany
    {
        return $this->hasMany(Field::class, 'envelopeId');
    }

    public function shareLinks(): HasMany
    {
        return $this->hasMany(DocumentShareLink::class, 'envelopeId');
    }

    public function auditLogs(): HasMany
    {
        return $this->hasMany(DocumentAuditLog::class, 'envelopeId');
    }

    public function envelopeAttachments(): HasMany
    {
        return $this->hasMany(EnvelopeAttachment::class, 'envelopeId');
    }
}