<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class Project extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'projects';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'org_id',
        'deleted_at',
        'name',
        'retention_days',
        'has_traces',
        'metadata',
        'home_dashboard_id',
    ];

    protected $attributes = [
        'has_traces' => false,
    ];

    protected $casts = [
        'deleted_at' => 'datetime',
        'retention_days' => 'integer',
        'has_traces' => 'boolean',
        'metadata' => 'array',
    ];

    public function homeDashboard(): BelongsTo
    {
        return $this->belongsTo(Dashboard::class, 'home_dashboard_id');
    }

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'org_id');
    }

    public function tracingReadinessSettings(): HasOne
    {
        return $this->hasOne(TracingReadinessSettings::class, 'project_id');
    }

    public function ExternalMediaStorageIntegration(): HasOne
    {
        return $this->hasOne(ExternalMediaStorageIntegration::class, 'project_id');
    }

    public function TopicConfig(): HasOne
    {
        return $this->hasOne(TopicConfig::class, 'project_id');
    }

    public function SlackIntegration(): HasOne
    {
        return $this->hasOne(SlackIntegration::class, 'project_id');
    }

    public function projectMembers(): HasMany
    {
        return $this->hasMany(ProjectMembership::class, 'project_id');
    }

    public function apiKeys(): HasMany
    {
        return $this->hasMany(ApiKey::class, 'project_id');
    }

    public function roleAssignments(): HasMany
    {
        return $this->hasMany(RoleAssignment::class, 'project_id');
    }

    public function dataset(): HasMany
    {
        return $this->hasMany(Dataset::class, 'project_id');
    }

    public function invitations(): HasMany
    {
        return $this->hasMany(MembershipInvitation::class, 'project_id');
    }

    public function sessions(): HasMany
    {
        return $this->hasMany(TraceSession::class, 'project_id');
    }

    public function Prompt(): HasMany
    {
        return $this->hasMany(Prompt::class, 'project_id');
    }

    public function Skill(): HasMany
    {
        return $this->hasMany(Skill::class, 'project_id');
    }

    public function SkillFile(): HasMany
    {
        return $this->hasMany(SkillFile::class, 'project_id');
    }

    public function SkillBlob(): HasMany
    {
        return $this->hasMany(SkillBlob::class, 'project_id');
    }

    public function Model(): HasMany
    {
        return $this->hasMany(Model::class, 'project_id');
    }

    public function EvalTemplate(): HasMany
    {
        return $this->hasMany(EvalTemplate::class, 'project_id');
    }

    public function JobConfiguration(): HasMany
    {
        return $this->hasMany(JobConfiguration::class, 'project_id');
    }

    public function JobExecution(): HasMany
    {
        return $this->hasMany(JobExecution::class, 'project_id');
    }

    public function Evaluator(): HasMany
    {
        return $this->hasMany(Evaluator::class, 'project_id');
    }

    public function EvaluationRule(): HasMany
    {
        return $this->hasMany(EvaluationRule::class, 'project_id');
    }

    public function EvaluationRuleEvaluatorAssignment(): HasMany
    {
        return $this->hasMany(EvaluationRuleEvaluatorAssignment::class, 'project_id');
    }

    public function LlmApiKeys(): HasMany
    {
        return $this->hasMany(LlmApiKeys::class, 'project_id');
    }

    public function PosthogIntegration(): HasMany
    {
        return $this->hasMany(PosthogIntegration::class, 'project_id');
    }

    public function MixpanelIntegration(): HasMany
    {
        return $this->hasMany(MixpanelIntegration::class, 'project_id');
    }

    public function BlobStorageIntegration(): HasMany
    {
        return $this->hasMany(BlobStorageIntegration::class, 'project_id');
    }

    public function webCalloutEndpoints(): HasMany
    {
        return $this->hasMany(WebCalloutEndpoint::class, 'project_id');
    }

    public function scoreConfig(): HasMany
    {
        return $this->hasMany(ScoreConfig::class, 'project_id');
    }

    public function BatchExport(): HasMany
    {
        return $this->hasMany(BatchExport::class, 'project_id');
    }

    public function BatchAction(): HasMany
    {
        return $this->hasMany(BatchAction::class, 'project_id');
    }

    public function comment(): HasMany
    {
        return $this->hasMany(Comment::class, 'project_id');
    }

    public function commentReactions(): HasMany
    {
        return $this->hasMany(CommentReaction::class, 'project_id');
    }

    public function annotationQueue(): HasMany
    {
        return $this->hasMany(AnnotationQueue::class, 'project_id');
    }

    public function annotationQueueItem(): HasMany
    {
        return $this->hasMany(AnnotationQueueItem::class, 'project_id');
    }

    public function Media(): HasMany
    {
        return $this->hasMany(Media::class, 'project_id');
    }

    public function PromptDependency(): HasMany
    {
        return $this->hasMany(PromptDependency::class, 'project_id');
    }

    public function LlmSchema(): HasMany
    {
        return $this->hasMany(LlmSchema::class, 'project_id');
    }

    public function LlmTool(): HasMany
    {
        return $this->hasMany(LlmTool::class, 'project_id');
    }

    public function PromptProtectedLabels(): HasMany
    {
        return $this->hasMany(PromptProtectedLabels::class, 'project_id');
    }

    public function Dashboard(): HasMany
    {
        return $this->hasMany(Dashboard::class, 'project_id');
    }

    public function DashboardWidget(): HasMany
    {
        return $this->hasMany(DashboardWidget::class, 'project_id');
    }

    public function TableViewPreset(): HasMany
    {
        return $this->hasMany(TableViewPreset::class, 'project_id');
    }

    public function actions(): HasMany
    {
        return $this->hasMany(Action::class, 'project_id');
    }

    public function triggers(): HasMany
    {
        return $this->hasMany(Trigger::class, 'project_id');
    }

    public function automationExecutions(): HasMany
    {
        return $this->hasMany(AutomationExecution::class, 'project_id');
    }

    public function Automation(): HasMany
    {
        return $this->hasMany(Automation::class, 'project_id');
    }

    public function monitors(): HasMany
    {
        return $this->hasMany(Monitor::class, 'project_id');
    }

    public function DefaultLlmModel(): HasMany
    {
        return $this->hasMany(DefaultLlmModel::class, 'project_id');
    }

    public function Price(): HasMany
    {
        return $this->hasMany(Price::class, 'project_id');
    }

    public function PendingDeletion(): HasMany
    {
        return $this->hasMany(PendingDeletion::class, 'project_id');
    }

    public function AnnotationQueueAssignment(): HasMany
    {
        return $this->hasMany(AnnotationQueueAssignment::class, 'project_id');
    }

    public function NotificationPreference(): HasMany
    {
        return $this->hasMany(NotificationPreference::class, 'project_id');
    }

    public function DefaultView(): HasMany
    {
        return $this->hasMany(DefaultView::class, 'project_id');
    }

    public function InAppAgentConversation(): HasMany
    {
        return $this->hasMany(InAppAgentConversation::class, 'project_id');
    }

    public function InAppAgentEvent(): HasMany
    {
        return $this->hasMany(InAppAgentEvent::class, 'project_id');
    }

    public function InAppAgentRun(): HasMany
    {
        return $this->hasMany(InAppAgentRun::class, 'project_id');
    }

    public function InAppAgentPendingToolApproval(): HasMany
    {
        return $this->hasMany(InAppAgentPendingToolApproval::class, 'project_id');
    }

    public function topicClusteringRuns(): HasMany
    {
        return $this->hasMany(TopicClusteringRun::class, 'project_id');
    }

    public function gatewayConfigs(): HasMany
    {
        return $this->hasMany(GatewayConfig::class, 'default_ingestion_project_id');
    }
}