<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class DocumentRedactionJob extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'DocumentRedactionJob';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'documentId',
        'documentVersionId',
        'teamId',
        'createdById',
        'status',
        'customTerms',
        'reasons',
        'resultVersionId',
        'error',
        'detectRunId',
        'applyRunId',
    ];

    protected $attributes = [
        'status' => 'PENDING',
    ];

    public function document(): BelongsTo
    {
        return $this->belongsTo(Document::class, 'documentId');
    }

    public function documentVersion(): BelongsTo
    {
        return $this->belongsTo(DocumentVersion::class, 'documentVersionId');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function createdBy(): BelongsTo
    {
        return $this->belongsTo(User::class, 'createdById');
    }

    public function redactions(): HasMany
    {
        return $this->hasMany(DocumentRedaction::class, 'jobId');
    }
}