<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class LlmApiKeys extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'llm_api_keys';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'provider',
        'adapter',
        'display_secret_key',
        'secret_key',
        'base_url',
        'custom_models',
        'with_default_models',
        'extra_headers',
        'extra_header_keys',
        'config',
        'project_id',
        'organization_id',
    ];

    protected $attributes = [
        'with_default_models' => true,
    ];

    protected $casts = [
        'with_default_models' => 'boolean',
        'config' => 'array',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organization_id');
    }

    public function DefaultLlmModel(): HasMany
    {
        return $this->hasMany(DefaultLlmModel::class, 'llm_api_key_id');
    }

    public function topicSummaryConfigs(): HasMany
    {
        return $this->hasMany(TopicConfig::class, 'summary_llm_api_key_id');
    }

    public function topicEmbeddingConfigs(): HasMany
    {
        return $this->hasMany(TopicConfig::class, 'embedding_llm_api_key_id');
    }

    public function topicClusteringConfigs(): HasMany
    {
        return $this->hasMany(TopicConfig::class, 'clustering_llm_api_key_id');
    }
}