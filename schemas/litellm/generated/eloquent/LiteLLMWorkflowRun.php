<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Support\Str;

class LiteLLMWorkflowRun extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'LiteLLM_WorkflowRun';

    protected $primaryKey = 'run_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'session_id',
        'workflow_type',
        'status',
        'created_by',
        'input',
        'output',
        'metadata',
    ];

    protected $attributes = [
        'status' => 'pending',
    ];

    protected $casts = [
        'input' => 'array',
        'output' => 'array',
        'metadata' => 'array',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('session_id', $this->attributes)) {
                $this->setAttribute('session_id', (string) Str::orderedUuid());
            }
        }

        return parent::save($options);
    }

    public function events(): HasMany
    {
        return $this->hasMany(LiteLLMWorkflowEvent::class, 'run_id');
    }

    public function messages(): HasMany
    {
        return $this->hasMany(LiteLLMWorkflowMessage::class, 'run_id');
    }
}