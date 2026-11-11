<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class LiteLLMWorkflowMessage extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const UPDATED_AT = null;

    protected $table = 'LiteLLM_WorkflowMessage';

    protected $primaryKey = 'message_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'run_id',
        'role',
        'content',
        'sequence_number',
        'session_id',
    ];

    protected $casts = [
        'sequence_number' => 'integer',
    ];

    public function run(): BelongsTo
    {
        return $this->belongsTo(LiteLLMWorkflowRun::class, 'run_id');
    }
}