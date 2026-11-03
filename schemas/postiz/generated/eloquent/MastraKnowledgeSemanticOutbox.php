<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class MastraKnowledgeSemanticOutbox extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;
    const ZONED_DATES = ['availableAtZ', 'claimedAtZ', 'createdAtZ', 'completedAtZ'];

    protected $table = 'mastra_knowledge_semantic_outbox';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'idempotencyKey',
        'documentId',
        'documentType',
        'operation',
        'scope',
        'scopeKey',
        'status',
        'attempts',
        'availableAt',
        'claimedAt',
        'claimedBy',
        'completedAt',
        'availableAtZ',
        'claimedAtZ',
        'createdAtZ',
        'completedAtZ',
    ];

    protected $casts = [
        'scope' => 'array',
        'attempts' => 'integer',
        'availableAt' => 'datetime',
        'claimedAt' => 'datetime',
        'completedAt' => 'datetime',
        'availableAtZ' => 'datetime',
        'claimedAtZ' => 'datetime',
        'createdAtZ' => 'datetime',
        'completedAtZ' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('availableAtZ', $this->attributes)) {
                $this->setAttribute('availableAtZ', $this->freshTimestamp());
            }
            if (! array_key_exists('claimedAtZ', $this->attributes)) {
                $this->setAttribute('claimedAtZ', $this->freshTimestamp());
            }
            if (! array_key_exists('createdAtZ', $this->attributes)) {
                $this->setAttribute('createdAtZ', $this->freshTimestamp());
            }
            if (! array_key_exists('completedAtZ', $this->attributes)) {
                $this->setAttribute('completedAtZ', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}