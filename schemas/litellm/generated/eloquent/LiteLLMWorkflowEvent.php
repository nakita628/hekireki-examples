<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class LiteLLMWorkflowEvent extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const UPDATED_AT = null;

    protected $table = 'LiteLLM_WorkflowEvent';

    protected $primaryKey = 'event_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'run_id',
        'event_type',
        'step_name',
        'sequence_number',
        'data',
    ];

    protected $casts = [
        'sequence_number' => 'integer',
        'data' => 'array',
    ];

    public function run(): BelongsTo
    {
        return $this->belongsTo(LiteLLMWorkflowRun::class, 'run_id');
    }
}