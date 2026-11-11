<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class LiteLLMRetiredAgentIdentity extends Model
{
    use HasVersion4Uuids;

    protected $table = 'LiteLLM_RetiredAgentIdentity';

    protected $primaryKey = 'binding_id';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'agent_id',
        'provider',
        'issuer',
        'tenant_id',
        'client_id',
    ];

    public function agent(): BelongsTo
    {
        return $this->belongsTo(LiteLLMAgentsTable::class, 'agent_id');
    }
}