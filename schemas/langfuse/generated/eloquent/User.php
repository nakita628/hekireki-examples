<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\HasMany;

class User extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'users';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'email',
        'email_verified',
        'password',
        'sessions_expired_at',
        'image',
        'admin',
        'v4_beta_enabled',
        'support_access',
        'feature_flags',
    ];

    protected $attributes = [
        'admin' => false,
        'v4_beta_enabled' => false,
    ];

    protected $casts = [
        'email_verified' => 'datetime',
        'sessions_expired_at' => 'datetime',
        'admin' => 'boolean',
        'v4_beta_enabled' => 'boolean',
        'support_access' => 'array',
    ];

    public function accounts(): HasMany
    {
        return $this->hasMany(Account::class, 'user_id');
    }

    public function sessions(): HasMany
    {
        return $this->hasMany(Session::class, 'user_id');
    }

    public function organizationMemberships(): HasMany
    {
        return $this->hasMany(OrganizationMembership::class, 'user_id');
    }

    public function projectMemberships(): HasMany
    {
        return $this->hasMany(ProjectMembership::class, 'user_id');
    }

    public function invitations(): HasMany
    {
        return $this->hasMany(MembershipInvitation::class, 'invited_by_user_id');
    }

    public function annotatedLockedItem(): HasMany
    {
        return $this->hasMany(AnnotationQueueItem::class, 'locked_by_user_id');
    }

    public function annotatedCompletedItem(): HasMany
    {
        return $this->hasMany(AnnotationQueueItem::class, 'annotator_user_id');
    }

    public function dashboardWidgetsCreated(): HasMany
    {
        return $this->hasMany(DashboardWidget::class, 'created_by');
    }

    public function dashboardWidgetsUpdated(): HasMany
    {
        return $this->hasMany(DashboardWidget::class, 'updated_by');
    }

    public function dashboardCreated(): HasMany
    {
        return $this->hasMany(Dashboard::class, 'created_by');
    }

    public function dashboardUpdated(): HasMany
    {
        return $this->hasMany(Dashboard::class, 'updated_by');
    }

    public function tableViewPresetCreated(): HasMany
    {
        return $this->hasMany(TableViewPreset::class, 'created_by');
    }

    public function tableViewPresetUpdated(): HasMany
    {
        return $this->hasMany(TableViewPreset::class, 'updated_by');
    }

    public function monitorsCreated(): HasMany
    {
        return $this->hasMany(Monitor::class, 'created_by');
    }

    public function monitorsUpdated(): HasMany
    {
        return $this->hasMany(Monitor::class, 'updated_by');
    }

    public function topicConfigsCreated(): HasMany
    {
        return $this->hasMany(TopicConfig::class, 'created_by');
    }

    public function topicConfigsUpdated(): HasMany
    {
        return $this->hasMany(TopicConfig::class, 'updated_by');
    }

    public function tracingReadinessCreated(): HasMany
    {
        return $this->hasMany(TracingReadinessSettings::class, 'created_by');
    }

    public function tracingReadinessUpdated(): HasMany
    {
        return $this->hasMany(TracingReadinessSettings::class, 'updated_by');
    }

    public function annotationQueueAssignment(): HasMany
    {
        return $this->hasMany(AnnotationQueueAssignment::class, 'user_id');
    }

    public function surveys(): HasMany
    {
        return $this->hasMany(Survey::class, 'user_id');
    }

    public function commentReactions(): HasMany
    {
        return $this->hasMany(CommentReaction::class, 'user_id');
    }

    public function notificationPreferences(): HasMany
    {
        return $this->hasMany(NotificationPreference::class, 'user_id');
    }

    public function defaultViews(): HasMany
    {
        return $this->hasMany(DefaultView::class, 'user_id');
    }

    public function inAppAgentConversations(): HasMany
    {
        return $this->hasMany(InAppAgentConversation::class, 'created_by_user_id');
    }

    public function inAppAgentRuns(): HasMany
    {
        return $this->hasMany(InAppAgentRun::class, 'triggered_by_user_id');
    }

    public function apiKeysCreated(): HasMany
    {
        return $this->hasMany(ApiKey::class, 'created_by_user_id');
    }

    public function apiKeys(): HasMany
    {
        return $this->hasMany(ApiKey::class, 'user_id');
    }

    public function roleAssignments(): HasMany
    {
        return $this->hasMany(RoleAssignment::class, 'user_id');
    }

    public function evaluatorsCreated(): HasMany
    {
        return $this->hasMany(Evaluator::class, 'created_by_user_id');
    }

    public function evaluatorVersionsCreated(): HasMany
    {
        return $this->hasMany(EvaluatorVersion::class, 'created_by_user_id');
    }

    public function evaluationRulesCreated(): HasMany
    {
        return $this->hasMany(EvaluationRule::class, 'created_by_user_id');
    }
}