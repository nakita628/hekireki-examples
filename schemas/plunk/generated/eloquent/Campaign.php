<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Campaign extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'campaigns';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'name',
        'description',
        'status',
        'subject',
        'body',
        'from',
        'fromName',
        'replyTo',
        'type',
        'audienceType',
        'audienceCondition',
        'segmentId',
        'scheduledFor',
        'totalRecipients',
        'sentCount',
        'deliveredCount',
        'openedCount',
        'clickedCount',
        'bouncedCount',
        'complainedCount',
        'unsubscribedCount',
        'archived',
        'projectId',
        'sentAt',
    ];

    protected $attributes = [
        'status' => 'DRAFT',
        'type' => 'MARKETING',
        'audienceType' => 'ALL',
        'totalRecipients' => 0,
        'sentCount' => 0,
        'deliveredCount' => 0,
        'openedCount' => 0,
        'clickedCount' => 0,
        'bouncedCount' => 0,
        'complainedCount' => 0,
        'unsubscribedCount' => 0,
        'archived' => false,
    ];

    protected $casts = [
        'status' => CampaignStatus::class,
        'type' => TemplateType::class,
        'audienceType' => CampaignAudienceType::class,
        'audienceCondition' => 'array',
        'scheduledFor' => 'datetime',
        'totalRecipients' => 'integer',
        'sentCount' => 'integer',
        'deliveredCount' => 'integer',
        'openedCount' => 'integer',
        'clickedCount' => 'integer',
        'bouncedCount' => 'integer',
        'complainedCount' => 'integer',
        'unsubscribedCount' => 'integer',
        'archived' => 'boolean',
        'sentAt' => 'datetime',
    ];

    public function segment(): BelongsTo
    {
        return $this->belongsTo(Segment::class, 'segmentId');
    }

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function emails(): HasMany
    {
        return $this->hasMany(Email::class, 'campaignId');
    }
}