<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class TopicConfig extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'topic_configs';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'created_by',
        'updated_by',
        'project_id',
        'enabled',
        'blocked_at',
        'block_reason',
        'block_message',
        'summary_llm_api_key_id',
        'summary_model',
        'embedding_llm_api_key_id',
        'embedding_model',
        'embedding_dimensions',
        'clustering_llm_api_key_id',
        'clustering_model',
    ];

    protected $attributes = [
        'enabled' => false,
        'embedding_dimensions' => 1024,
    ];

    protected $casts = [
        'enabled' => 'boolean',
        'blocked_at' => 'datetime',
        'block_reason' => EvaluatorBlockReason::class,
        'embedding_dimensions' => 'integer',
    ];

    public function createdByUser(): BelongsTo
    {
        return $this->belongsTo(User::class, 'created_by');
    }

    public function updatedByUser(): BelongsTo
    {
        return $this->belongsTo(User::class, 'updated_by');
    }

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }

    public function summaryLlmApiKey(): BelongsTo
    {
        return $this->belongsTo(LlmApiKeys::class, 'summary_llm_api_key_id');
    }

    public function embeddingLlmApiKey(): BelongsTo
    {
        return $this->belongsTo(LlmApiKeys::class, 'embedding_llm_api_key_id');
    }

    public function clusteringLlmApiKey(): BelongsTo
    {
        return $this->belongsTo(LlmApiKeys::class, 'clustering_llm_api_key_id');
    }
}