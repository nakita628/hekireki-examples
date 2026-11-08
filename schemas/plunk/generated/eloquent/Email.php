<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Email extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'emails';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'contactId',
        'toName',
        'subject',
        'body',
        'from',
        'fromName',
        'replyTo',
        'headers',
        'attachments',
        'messageId',
        'sourceType',
        'templateId',
        'campaignId',
        'workflowExecutionId',
        'workflowStepExecutionId',
        'status',
        'scheduledFor',
        'sentAt',
        'deliveredAt',
        'openedAt',
        'clickedAt',
        'bouncedAt',
        'complainedAt',
        'unsubscribedAt',
        'opens',
        'clicks',
        'simulated',
        'error',
        'projectId',
    ];

    protected $attributes = [
        'status' => 'PENDING',
        'opens' => 0,
        'clicks' => 0,
        'simulated' => false,
    ];

    protected $casts = [
        'headers' => 'array',
        'attachments' => 'array',
        'sourceType' => EmailSourceType::class,
        'status' => EmailStatus::class,
        'scheduledFor' => 'datetime',
        'sentAt' => 'datetime',
        'deliveredAt' => 'datetime',
        'openedAt' => 'datetime',
        'clickedAt' => 'datetime',
        'bouncedAt' => 'datetime',
        'complainedAt' => 'datetime',
        'unsubscribedAt' => 'datetime',
        'opens' => 'integer',
        'clicks' => 'integer',
        'simulated' => 'boolean',
    ];

    public function contact(): BelongsTo
    {
        return $this->belongsTo(Contact::class, 'contactId');
    }

    public function template(): BelongsTo
    {
        return $this->belongsTo(Template::class, 'templateId');
    }

    public function campaign(): BelongsTo
    {
        return $this->belongsTo(Campaign::class, 'campaignId');
    }

    public function workflowExecution(): BelongsTo
    {
        return $this->belongsTo(WorkflowExecution::class, 'workflowExecutionId');
    }

    public function workflowStepExecution(): BelongsTo
    {
        return $this->belongsTo(WorkflowStepExecution::class, 'workflowStepExecutionId');
    }

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function events(): HasMany
    {
        return $this->hasMany(Event::class, 'emailId');
    }
}