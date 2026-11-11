<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Support\Str;

class LiteLLMAgentIdentity extends Model
{
    use PrismaDates;

    protected $table = 'LiteLLM_AgentIdentity';

    protected $primaryKey = 'agent_id';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'agent_id',
        'active',
        'provider',
        'issuer',
        'tenant_id',
        'client_id',
        'service_principal_id',
        'required_roles',
        'required_scopes',
        'revision',
        'last_authenticated_at',
    ];

    protected $attributes = [
        'active' => true,
    ];

    protected $casts = [
        'active' => 'boolean',
        'last_authenticated_at' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('revision', $this->attributes)) {
                $this->setAttribute('revision', (string) Str::orderedUuid());
            }
        }

        return parent::save($options);
    }

    public function agent(): BelongsTo
    {
        return $this->belongsTo(LiteLLMAgentsTable::class, 'agent_id');
    }
}